using UnityEngine;
using System.Collections;

public class GuardSecurity : MonoBehaviour, ISecurityCommunicator
{
    private Vector3 targetPosition;
    private bool targetHasBeenSpotted;

    void ISecurityCommunicator.NotifyTargetSpotted(Vector3 targetPositonAt)
    {
        targetHasBeenSpotted = true;
        this.targetPosition = targetPositonAt;
    }

    void ISecurityCommunicator.NotifyAlertLevel(AlertLevel alertLevelEnum)
    {
        switch (alertLevelEnum)
        {
            case AlertLevel.Searching:
                GetComponent<EnemyAI>().Patrol();
                break;
            case AlertLevel.TargetSpotted:
                GetComponent<EnemyAI>().ChaseTarget(targetPosition);
                break;
            default:
                break;
        }
    }
}
