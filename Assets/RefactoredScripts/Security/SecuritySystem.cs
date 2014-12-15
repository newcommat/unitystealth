using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AlertLevel { TargetSpotted, Searching };

public class SecuritySystem : MonoBehaviour {
    
    private float timeToTrackTarget = 5f;
    private Vector3 targetKnownPosition = Vector3.zero;

    private float alertTimer = 0f;
    private AlertLevel alertLevel = AlertLevel.Searching;

    public void Update()
    {
        if (alertLevel == AlertLevel.TargetSpotted)
        {
            alertTimer += Time.deltaTime;

            if (alertTimer >= timeToTrackTarget)
            {
                alertTimer = 0f;
                alertLevel = AlertLevel.Searching;
                StepDownAlertLevel();
            }
        }
    }

    public void TargetSpotted(GameObject targetObject, Vector3 locatedPosition)
    {
        // The Alarm System has become aware that the target object has been spotted by some entity
        targetKnownPosition = locatedPosition;
        alertLevel = AlertLevel.TargetSpotted;
		alertTimer = 0f;

        InformAgents(locatedPosition, alertLevel);
    }

    public void InformAgents(Vector3 locatedPosition, AlertLevel alertLevel)
    {
        // We want all GameObjects who have implemented a particular component to be informed
        List<ISecurityCommunicator> securityCommunicators = GameObjectExtensions.GetAllComponentsWhichImplementInterface<ISecurityCommunicator>();

        foreach (ISecurityCommunicator communicator in securityCommunicators)
        {
            communicator.NotifyTargetSpotted(locatedPosition);
            communicator.NotifyAlertLevel(alertLevel);
        }
    }

    public void StepDownAlertLevel()
    {
        List<ISecurityCommunicator> securityCommunicators = GameObjectExtensions.GetAllComponentsWhichImplementInterface<ISecurityCommunicator>();

        foreach (ISecurityCommunicator communicator in securityCommunicators)
        {
            communicator.NotifyAlertLevel(alertLevel);
        }
    }
}
