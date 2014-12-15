using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float smooth = 1.5f;

    private Transform playerPosition;
    private Vector3 relativeCameraPosition;
    private float relativeCameraPositionMagnitude;
    private Vector3 newPos;

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<Transform>();
        relativeCameraPosition = transform.position - playerPosition.position;
        relativeCameraPositionMagnitude = relativeCameraPosition.magnitude - 0.5f;

        // We want to stop following the player once the lift takes off
        EventDispatcher.RegisterCallback<DynamicExitLiftStartEvent>(OnLiftStart);
    }

    // We are following a physics object so should update in sync
    void FixedUpdate()
    {
        Vector3 standardPosition = playerPosition.position + relativeCameraPosition;
        Vector3 abovePos = playerPosition.position + Vector3.up * relativeCameraPositionMagnitude;

        Vector3[] checkPoints = new Vector3[5];
        checkPoints[0] = standardPosition;
        checkPoints[1] = Vector3.Lerp(standardPosition, abovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(standardPosition, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPosition, abovePos, 0.75f);
        checkPoints[4] = abovePos;

        foreach(Vector3 checkPoint in checkPoints)
        {
            if (IsLineOfSightObscured(checkPoint))
            {
                newPos = checkPoint;
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
        SmoothLookAt();
    }

    bool IsLineOfSightObscured(Vector3 checkPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(checkPos, playerPosition.position - checkPos, out hit, relativeCameraPositionMagnitude))
        {
            if (hit.transform != playerPosition)
            {
                return false;
            }
        }

        return true;
    }

    void SmoothLookAt()
    {
        // Get directional vector to player
        Vector3 relPlayerPosition = playerPosition.position - transform.position;

        // Construct look at quaternion (Rotates Quat around Up vector by Rel vector
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

        // Lerp
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }

    void OnLiftStart(DynamicExitLiftStartEvent liftStartEvent)
    {
        enabled = false;
    }
}
