/*
 * Copyright (c) 2015 Allan Pichardo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]
public class AudioProcessor : MonoBehaviour
{
    public AudioSource audioSource;

    private long lastT, nowT, diff, entries, sum;

    public int bufferSize = 1024;
    // FFT 大小

    public float currentSongBPM = 120f;

    private int samplingRate = 44100;
    // FFT 采样频率

    /* 对数频率平均控制 */
    private int nBand = 12;
    // 频带数量

    public float gThresh = 0.1f;
    // 灵敏度

    int blipDelayLen = 16;
    int[] blipDelay;

    private int sinceLast = 0;
    // 抑制双重节拍的计数器

    private float framePeriod;

    /* 存储空间 */
    private int colmax = 120;
    float[] spectrum;
    float[] averages;
    float[] acVals;
    float[] onsets;
    float[] scorefun;
    float[] dobeat;
    int now = 0;
    // 环形缓冲区中的时间索引

    float[] spec;
    // 上一步的频谱

    /* 自相关结构 */
    int maxlag = 100;
    // （以帧为单位）跟踪的最大滞后
    float decay = 0.997f;
    // 运行平均值的平滑常数
    Autoco auco;

    private float alph;
    // 节奏偏差惩罚和起始强度之间的权衡常数

    [Header ("事件")]
    public OnBeatEventHandler onBeat;
    public OnSpectrumEventHandler onSpectrum;

    //////////////////////////////////
    private long getCurrentTimeMillis ()
    {
        long milliseconds = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        return milliseconds;
    }

    private void initArrays ()
    {
        blipDelay = new int[blipDelayLen];
        onsets = new float[colmax];
        scorefun = new float[colmax];
        dobeat = new float[colmax];
        spectrum = new float[bufferSize];
        averages = new float[12];
        acVals = new float[maxlag];
        alph = 100 * gThresh;
    }


    // 初始化时使用
    void Start ()
    {
        initArrays ();

        audioSource = GetComponent<AudioSource> ();
        samplingRate = audioSource.clip.frequency;

        framePeriod = (float)bufferSize / (float)samplingRate;

        // 初始化之前的频谱记录
        spec = new float[nBand];
        for (int i = 0; i < nBand; ++i)
            spec [i] = 100.0f;

        auco = new Autoco (maxlag, decay, framePeriod, getBandWidth ());

        lastT = getCurrentTimeMillis ();

        currentSongBPM = auco.avgBpm ();

    }

    public void tapTempo ()
    {
        nowT = getCurrentTimeMillis ();
        diff = nowT - lastT;
        lastT = nowT;
        sum = sum + diff;
        entries++;

        int average = (int)(sum / entries);

        Debug.Log ("average = " + average);
    }

    double[] toDoubleArray (float[] arr)
    {
        if (arr == null)
            return null;
        int n = arr.Length;
        double[] ret = new double[n];
        for (int i = 0; i < n; i++) {
            ret [i] = (float)arr [i];
        }
        return ret;
    }

    // 每帧更新一次
    void Update ()
    {
        if (audioSource.isPlaying) {
            audioSource.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);
            computeAverages (spectrum);
            onSpectrum.Invoke (averages);

            /* 计算此帧中的起始函数值 */
            float onset = 0;
            for (int i = 0; i < nBand; i++) {
                float specVal = (float)System.Math.Max (-100.0f, 20.0f * (float)System.Math.Log10 (averages [i]) + 160); // 此频带的 dB 值
                specVal *= 0.025f;
                float dbInc = specVal - spec [i]; // 自上帧以来的 dB 增量
                spec [i] = specVal; // 记录此帧以供下次使用
                onset += dbInc; // 起始函数是 dB 增量的总和
            }

            onsets [now] = onset;

            /* 更新自相关器并找到峰值滞后 = 当前节奏 */
            auco.newVal (onset);
            // 记录（加权）自相关中的最大值，因为它将是节奏
            float aMax = 0.0f;
            int tempopd = 0;
            //float[] acVals = new float[maxlag];
            for (int i = 0; i < maxlag; ++i) {
                float acVal = (float)System.Math.Sqrt (auco.autoco (i));
                if (acVal > aMax) {
                    aMax = acVal;
                    tempopd = i;
                }
                // 反向存储在数组中，以便从右到左显示，与轨迹一致
                acVals [maxlag - 1 - i] = acVal;
            }

            /* 计算 DP-ish 函数以更新最佳得分函数 */
            float smax = -999999;
            int smaxix = 0;
            // 权重可以通过鼠标动态变化
            alph = 100 * gThresh;
            // 考虑所有可能的前一个节拍时间，从 0.5 到 2.0 倍当前节奏周期
            for (int i = tempopd / 2; i < System.Math.Min (colmax, 2 * tempopd); ++i) {
                // 目标函数 - 此节拍的成本 + 上一个节拍的得分 + 过渡惩罚
                float score = onset + scorefun [(now - i + colmax) % colmax] - alph * (float)System.Math.Pow (System.Math.Log ((float)i / (float)tempopd), 2);
                // 跟踪得分最高的前一个节拍
                if (score > smax) {
                    smax = score;
                    smaxix = i;
                }
            }

            scorefun [now] = smax;
            // 将得分函数窗口中的最小值保持为零，通过减去最小值
            float smin = scorefun [0];
            for (int i = 0; i < colmax; ++i)
                if (scorefun [i] < smin)
                    smin = scorefun [i];
            for (int i = 0; i < colmax; ++i)
                scorefun [i] -= smin;

            /* 找到得分函数窗口中的最大值，以决定是否发出提示 */
            smax = scorefun [0];
            smaxix = 0;
            for (int i = 0; i < colmax; ++i) {
                if (scorefun [i] > smax) {
                    smax = scorefun [i];
                    smaxix = i;
                }
            }

            // dobeat 数组记录我们实际放置节拍的位置
            dobeat [now] = 0;  // 默认情况下此帧没有节拍
            ++sinceLast;
            // 如果当前值是数组中最大的，可能意味着我们在一个节拍上
            if (smaxix == now) {
                //tapTempo();
                // 确保最近的节拍不是太近
                if (sinceLast > tempopd / 4) {
                    onBeat.Invoke ();			
                    blipDelay [0] = 1;
                    // 记录我们确实在此帧标记了一个节拍
                    dobeat [now] = 1;
                    // 重置自上次节拍以来的帧计数器
                    sinceLast = 0;
                }
            }

            /* 更新列索引（用于环形缓冲区） */
            if (++now == colmax)
                now = 0;

            // Debug.Log(System.Math.Round(60 / (tempopd * framePeriod)) + " bpm");
            // Debug.Log(System.Math.Round(auco.avgBpm()) + " bpm");
        }
    }

    public void changeCameraColor ()
    {
        //Debug.Log("camera");
        float r = Random.Range (0f, 1f);
        float g = Random.Range (0f, 1f);
        float b = Random.Range (0f, 1f);

        //Debug.Log(r + "," + g + "," + b);
        Color color = new Color (r, g, b);

        GetComponent<Camera> ().clearFlags = CameraClearFlags.Color;
        Camera.main.backgroundColor = color;

        //camera.backgroundColor = color;
    }

    public float getBandWidth ()
    {
        return (2f / (float)bufferSize) * (samplingRate / 2f);
    }

    public int freqToIndex (int freq)
    {
        // 特殊情况：频率低于 spectrum[0] 的带宽
        if (freq < getBandWidth () / 2)
            return 0;
        // 特殊情况：频率在 spectrum[512] 的带宽内
        if (freq > samplingRate / 2 - getBandWidth () / 2)
            return (bufferSize / 2);
        // 其他所有情况
        float fraction = (float)freq / (float)samplingRate;
        int i = (int)System.Math.Round (bufferSize * fraction);
        //Debug.Log("frequency: " + freq + ", index: " + i);
        return i;
    }

    public void computeAverages (float[] data)
    {
        for (int i = 0; i < 12; i++) {
            float avg = 0;
            int lowFreq;
            if (i == 0)
                lowFreq = 0;
            else
                lowFreq = (int)((samplingRate / 2) / (float)System.Math.Pow (2, 12 - i));
            int hiFreq = (int)((samplingRate / 2) / (float)System.Math.Pow (2, 11 - i));
            int lowBound = freqToIndex (lowFreq);
            int hiBound = freqToIndex (hiFreq);
            for (int j = lowBound; j <= hiBound; j++) {
                //Debug.Log("lowbound: " + lowBound + ", highbound: " + hiBound);
                avg += data [j];
            }
            // 这一行已根据评论中的讨论进行了更改
            // avg /= (hiBound - lowBound);
            avg /= (hiBound - lowBound + 1);
            averages [i] = avg;
        }
    }

    float map (float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public float constrain (float value, float inclusiveMinimum, float inlusiveMaximum)
    {
        if (value >= inclusiveMinimum) {
            if (value <= inlusiveMaximum) {
                return value;
            }

            return inlusiveMaximum;
        }

        return inclusiveMinimum;
    }

    [System.Serializable]
    public class OnBeatEventHandler : UnityEngine.Events.UnityEvent
    {

    }

    [System.Serializable]
    public class OnSpectrumEventHandler : UnityEngine.Events.UnityEvent<float []>
    {

    }

    // 计算一组在线自相关器的类
    private class Autoco
    {
        private int del_length;
        private float decay;
        private float[] delays;
        private float[] outputs;
        private int indx;

        private float[] bpms;
        private float[] rweight;
        private float wmidbpm = 120f;
        private float woctavewidth;

        public Autoco (int len, float alpha, float framePeriod, float bandwidth)
        {
            woctavewidth = bandwidth;
            decay = alpha;
            del_length = len;
            delays = new float[del_length];
            outputs = new float[del_length];
            indx = 0;

            // 计算对数滞后高斯加权函数，以偏好约 120 bpm 的节奏
            bpms = new float[del_length];
            rweight = new float[del_length];
            for (int i = 0; i < del_length; ++i) {
                bpms [i] = 60.0f / (framePeriod * (float)i);
                //Debug.Log(bpms[i]);
                // 加权在对数 BPM 轴上是高斯分布的，中心在 wmidbpm，标准差 = woctavewidth 八度
                rweight [i] = (float)System.Math.Exp (-0.5f * System.Math.Pow (System.Math.Log (bpms [i] / wmidbpm) / System.Math.Log (2.0f) / woctavewidth, 2.0f));
            }
        }

        public void newVal (float val)
        {

            delays [indx] = val;

            // 更新运行自相关器值
            for (int i = 0; i < del_length; ++i) {
                int delix = (indx - i + del_length) % del_length;
                outputs [i] += (1 - decay) * (delays [indx] * delays [delix] - outputs [i]);
            }

            if (++indx == del_length)
                indx = 0;
        }

        // 读取特定滞后下的当前自相关器值
        public float autoco (int del)
        {
            float blah = rweight [del] * outputs [del];
            return blah;
        }

        public float avgBpm ()
        {
            float sum = 0;
            for (int i = 1; i < bpms.Length; ++i) {
                sum += bpms [i];
            }
            return sum / (del_length - 1);
        }
    }
}