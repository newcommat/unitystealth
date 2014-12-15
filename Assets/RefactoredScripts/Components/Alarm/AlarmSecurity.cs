using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AlarmSecurity : MonoBehaviour, ISecurityCommunicator {

    private Vector3 targetPosition;
    private bool targetHasBeenSpotted;
    AlertLevel alertLevel = AlertLevel.Searching;

    void ISecurityCommunicator.NotifyTargetSpotted(Vector3 targetPos)
    { }

    void ISecurityCommunicator.NotifyAlertLevel(AlertLevel alertLevelEnum)
    {   
        if (alertLevel != alertLevelEnum)
        {
            alertLevel = alertLevelEnum;

            switch (alertLevel)
            {
                case AlertLevel.Searching:
                    GetComponent<AudioSource>().Stop();
                    break;
                case AlertLevel.TargetSpotted:
					GetComponent<AudioSource>().Play();
                    break;
                default:
                    break;
            }
        }
    }
}
