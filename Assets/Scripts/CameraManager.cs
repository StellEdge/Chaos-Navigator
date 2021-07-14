using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera vCamera;

    public FloatReference cameraShakeAmplitude;
    public FloatReference cameraShakeTime;

    private float cameraShakeTimer;
    private float cameraShakeTimerTotal;
    private float cameraShakeStartingAmplitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraShakeTimer > 0) {
            cameraShakeTimer -= Time.deltaTime;

            vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 
                Mathf.Lerp(cameraShakeAmplitude, 0f ,(1 - (cameraShakeTimer / cameraShakeTimerTotal)));

        }        



    }

    public void ShipCollideCameraShake()
    {
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 
            cameraShakeAmplitude;

        cameraShakeTimer = cameraShakeTime;
        cameraShakeTimerTotal = cameraShakeTime;
        cameraShakeStartingAmplitude = cameraShakeAmplitude.Value;

    }

}
