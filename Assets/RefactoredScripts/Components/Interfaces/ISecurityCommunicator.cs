using UnityEngine;
using System.Collections;

// This component is used by the security system to communicate
// and notify any agents who are interested in intrusion
interface ISecurityCommunicator{

    void NotifyTargetSpotted(Vector3 targetPositonAt);

    void NotifyAlertLevel(AlertLevel alertLevelEnum);
}
