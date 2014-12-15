using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class FlashingLightSecurity : MonoBehaviour, ISecurityCommunicator
{
    public float flashTime = 2f;
    public float fadeSpeed = 2f;

    public float idleIntensity = 0f;
    public float lowIntensity = 0.5f;
    public float highIntensity = 2f;

    private float targetIntensity;
    private float previousIntensity;

    private float flashTimer = 0f;
    private bool alarmOn = false;

    void Awake()
    {
        GetComponent<Light>().intensity = 0f;
        targetIntensity = highIntensity;
        previousIntensity = lowIntensity;
    }

    public void Update()
    {
        if (alarmOn)
        {
            flashTimer += Time.deltaTime;
            GetComponent<Light>().intensity = Mathf.Lerp(previousIntensity, targetIntensity, Mathf.Min((flashTimer / flashTime), 1f));

            if (flashTimer >= flashTime)
            {
                flashTimer = 0f;
                previousIntensity = targetIntensity;
                targetIntensity = previousIntensity == lowIntensity ? highIntensity : lowIntensity;
            }
        }
        else
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, idleIntensity, fadeSpeed * Time.deltaTime);
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
                    flashTimer = 0f;
                    targetIntensity = highIntensity;
                    previousIntensity = lowIntensity;
                break;
            case AlertLevel.TargetSpotted:
                    alarmOn = true;
                break;
            default:
                break;
        }
    }
}
