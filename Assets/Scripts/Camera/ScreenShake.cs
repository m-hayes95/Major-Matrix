using System.Collections;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance {  get; private set; }
    private CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin camPerlinNoise;

    private void Awake()
    {
        // Singleton pattern used to call screen shake from other scripts (Shoot, Special Attack)
        if (Instance == null)   
            Instance = this;
        else
            Destroy(gameObject);

        virtualCam = GetComponent<CinemachineVirtualCamera>();
        camPerlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        camPerlinNoise.m_AmplitudeGain = intensity;
        //Debug.Log("Camera shake applied");
        StartCoroutine(ShakeTimer(time));
    }
    private IEnumerator ShakeTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        camPerlinNoise.m_AmplitudeGain = 0f;
    }
}
