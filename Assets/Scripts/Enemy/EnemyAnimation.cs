using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {
    public float deadZone = 5f;

    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTime = 0.6f;

    private Transform playerTransform;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Animator anim;
   // private AnimatorSetup animSetup;


    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tag.player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        nav.updateRotation = false;
        //animSetup = new AnimatorSetup(anim);

        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);

        deadZone *= Mathf.Deg2Rad;
    }

    void Update()
    {
        NavAnimSetup();
    }

    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }

    void NavAnimSetup()
    {
        float speed;
        float angle;

        if (enemySight.playerInSight)
        {
            speed = 0f;

            angle = FindAngle(transform.forward, playerTransform.position - transform.position, transform.up);
            
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            if (Mathf.Abs(angle) <= deadZone)
            {
                transform.LookAt(transform.position = nav.desiredVelocity);
                angle = 0f;
            }
        }

        float angularSpeed = angle / angleResponseTime;

        anim.SetFloat(HashIDs.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(HashIDs.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }

    float FindAngle(Vector3 from, Vector3 to, Vector3 up)
    {
        if (to == Vector3.zero)
            return 0f;

        float angle = Vector3.Angle(from, to);

        Vector3 normal = Vector3.Cross(from, to);
        angle *= Mathf.Sign(Vector3.Dot(normal, up));
        angle *= Mathf.Deg2Rad;

        return angle;
    }
}
