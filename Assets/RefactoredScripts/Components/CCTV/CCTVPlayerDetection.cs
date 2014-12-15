using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour {
    private SecuritySystem securitySystem;

    void Awake()
    {
        securitySystem = GameObject.FindGameObjectWithTag(Tag.gameController).GetComponent<SecuritySystem>();
    }

    void OnTriggerStay(Collider other)
    {
        // If our hit object has a playerControllerComponent we know it's an enemy
        PlayerController playerControllerComponent = other.gameObject.GetComponent<PlayerController>();

        if (playerControllerComponent != null)
        {
            GameObject player = other.gameObject;

            Vector3 vectorToPlayer = player.transform.position - transform.position;
            RaycastHit hit;

            // If we can draw a direct raycast to the player then we can see them
            // and we're not inside a wall or something stupid
            if (Physics.Raycast(transform.position, vectorToPlayer, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    securitySystem.TargetSpotted(player, player.transform.position);
                }
            }
        }
    }
}
