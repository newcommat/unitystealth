using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
    
    public float fieldOfViewAngle = 110f;
    public bool playerInSight = false;
    public Vector3 personalLastSighting = new Vector3(1000f, 1000f, 1000f);

    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private SphereCollider col;
    private Animator anim;
    private Animator playerAnim;
    private PlayerHealth playerHealth;
    private Vector3 previousLastSighting;

    void Awake()
    {
        // Setup references to my components
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();

        // Setup references to player
        player = GameObject.FindGameObjectWithTag(Tag.player);
        playerAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();

       // personalLastSighting = lastPlayerSighting.resetPosition;
       // previousLastSighting = lastPlayerSighting.resetPosition;
    }

    void Update()
    {
       /* if (lastPlayerSighting.position != previousLastSighting)
        {
            personalLastSighting = lastPlayerSighting.position;
        }

        previousLastSighting = lastPlayerSighting.position;

        if (playerHealth.health > 0f)
        {
            anim.SetBool(hash.playerInSightBool, playerInSight);
        }
        else
        {
            anim.SetBool(hash.playerInSightBool, false);
        }*/
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;

            Vector3 vectorToPlayer = player.transform.position - transform.position;
            Vector3 facingVector = player.transform.forward;

            float angle = Vector3.Angle(vectorToPlayer, facingVector);

            if (angle < 0.5f*fieldOfViewAngle)
            {
                RaycastHit rayCastOutput;

                // Construct ray cast between curr loc adn player
                if(Physics.Raycast(transform.position + transform.up, vectorToPlayer.normalized,out rayCastOutput, col.radius))
                {
                    if (rayCastOutput.collider.gameObject == player)
                    {
                        playerInSight = true;
                       // lastPlayerSighting.position = player.transform.position;
                    }
                }
            }

            int playerLayerZeroStatHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
            int playerLayerOneStatHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;

            if (playerLayerZeroStatHash == HashIDs.locomotionState || playerLayerOneStatHash == HashIDs.shoutState)
            {
                if (CalculatePathLength(player.transform.position) <= col.radius)
                {
                    previousLastSighting = player.transform.position;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (navMeshAgent.enabled)
        {
            navMeshAgent.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length+2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length -1; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWayPoints.Length-1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
