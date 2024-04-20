using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraShake : NetworkBehaviour
{
    public CinemachineVirtualCamera cineMachine;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startIntensity;
    public bool isShaking;

    public NoiseSettings noiseShakeProfile;
    public NoiseSettings noiseStandardProfile;

    private void Awake()
    {
        if (cineMachine == null)
        {
            cineMachine = FindAnyObjectByType<CinemachineVirtualCamera>();
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = cineMachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_NoiseProfile = noiseShakeProfile;
        perlin.m_AmplitudeGain = intensity;

        startIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
        isShaking = true;
        //  Debug.Log("Shake active");
    }

    public void SetDefaultNoise()
    {
        CinemachineBasicMultiChannelPerlin perlin = cineMachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_NoiseProfile = noiseStandardProfile;
        perlin.m_AmplitudeGain = 0.5f;
    }


    void Update()
    {
        if (isShaking)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                isShaking = false;
                shakeTimer = 0f;
                SetDefaultNoise();
            }
            else
            {
                CinemachineBasicMultiChannelPerlin perlin = cineMachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                float t = 1f - (shakeTimer / shakeTimerTotal);
                perlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, t * t);
            }
        }
    }
}
