using UnityEngine;
using System.Collections;

public class LaserGridDetector : MonoBehaviour {

    private SecuritySystem securitySystem;
    private bool enabled = true;

    void Awake()
    {
        securitySystem = GameObject.FindGameObjectWithTag(Tag.gameController).GetComponent<SecuritySystem>();
    }

    // Collider
    void OnTriggerStay(Collider other)
    { 
        // Only need to test if we're rendering the beams
        if (enabled)
        {
            // If our hit object has a playerControllerComponent we know it's an enemy
            PlayerController playerControllerComponent = other.gameObject.GetComponent<PlayerController>();

            if (playerControllerComponent != null)
            {
                GameObject player = other.gameObject;
                securitySystem.TargetSpotted(player, player.transform.position);

                // Hack to kill the player
              //   player.GetComponent<PlayerHealth>().TakeDamage(100f);
            }
        }
    }

    public void SetEnabled(bool detectorEnabled)
    {
        enabled = detectorEnabled;
    }
}
