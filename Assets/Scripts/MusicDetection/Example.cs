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
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.Video;

public class Example : MonoBehaviour
{
    public List<GameObject> colorsObjects;
    private int lastIndex = -1;

    public AudioProcessor audioProcessor;

    public VideoPlayer videoPlayer;

    public bool colorSwitch = true;

    public float currentSongBPM = 80f;

    public AudioSource audioSource;

    public float beatRate = 1f;

    void Start()
    {
        //Select the instance of AudioProcessor and pass a reference
        //to this object
        audioProcessor = FindObjectOfType<AudioProcessor>();
        audioProcessor.onBeat.AddListener(onOnbeatDetected);
        audioProcessor.onSpectrum.AddListener(onSpectrum);

        videoPlayer = FindObjectOfType<VideoPlayer>();

        //将所有的孙子物体添加为colorsObjects
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child)
            {
                colorsObjects.Add(grandchild.gameObject);
            }
        }

        currentSongBPM = UniBpmAnalyzer.AnalyzeBpm(audioSource.clip);
        currentSongBPM *= beatRate;
        SetVideoSpeed(currentSongBPM);
    }

    public void SetVideoSpeed(float bpm)
    {
        videoPlayer.playbackSpeed = GetVideoSpeed(bpm);
    }

    private float GetVideoSpeed(float bpm)
    {
        return bpm / (float)80f;
    }

    //this event will be called every time a beat is detected.
    //Change the threshold parameter in the inspector
    //to adjust the sensitivity
    void onOnbeatDetected()
    {
        if (colorSwitch)
            SwitchColors();
    }

    //This event will be called every frame while music is playing
    void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }


    public void SwitchColors()
    {
        if (colorsObjects == null || colorsObjects.Count == 0)
            return;

        int newIndex;
        do
        {
            newIndex = UnityEngine.Random.Range(0, colorsObjects.Count);
        } while (newIndex == lastIndex);

        for (int i = 0; i < colorsObjects.Count; i++)
        {
            colorsObjects[i].SetActive(i == newIndex);
        }

        lastIndex = newIndex;
    }
}
