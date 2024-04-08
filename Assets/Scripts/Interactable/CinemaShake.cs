using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemaShake : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    private CinemachineBasicMultiChannelPerlin perlin;

    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        perlin.m_AmplitudeGain = intensity; // Intensity of the shake
        StartCoroutine(StopShake(time));
    }

    private IEnumerator StopShake(float time)
    {
        yield return new WaitForSeconds(time);
        perlin.m_AmplitudeGain = 0;
    }
}
