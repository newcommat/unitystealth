using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class DimmingLightSecurity : MonoBehaviour, ISecurityCommunicator
{
    public float fadeSpeed = 8f;
    public float highIntensity = 0.25f;
    public float lowIntensity = 0f;

    private bool alarmOn = false;

    void Awake()
    {
        GetComponent<Light>().intensity = highIntensity;
    }

    public void Update()
    {
        if (alarmOn)
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, lowIntensity, fadeSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, highIntensity, fadeSpeed * Time.deltaTime);
        }
    }

    void ISecurityCommunicator.NotifyTargetSpotted(Vector3 targetPos)
    { 
        // No need to react directly to the target being spotted
    }

    void ISecurityCommunicator.NotifyAlertLevel(AlertLevel alertLevelEnum)
    {
        switch (alertLevelEnum)
        {
            case AlertLevel.Searching:
                alarmOn = false;
                break;
            case AlertLevel.TargetSpotted:
                alarmOn = true;
                break;
            default:
                break;
        }
    }
}
