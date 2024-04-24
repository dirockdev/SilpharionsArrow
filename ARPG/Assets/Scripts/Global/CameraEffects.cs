using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }
    public void CameraShake(float fuerza)
    {
        StopAllCoroutines();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = fuerza;
        StartCoroutine(defaultValues());

    }
    IEnumerator defaultValues()
    {

        yield return Yielders.Get(.1f);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

    }
}
