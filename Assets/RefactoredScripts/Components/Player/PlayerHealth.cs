using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float health = 100f;
    public float resetAfterDeath = 5f;
    public AudioClip deathClip;

    private PlayerController playerMovement;

    private float timer;

    private bool playerDead;
    private bool playerDying = false;

    void Awake()
    {
        playerMovement = GetComponent<PlayerController>();
    }

    void Update()
    {
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        EventDispatcher.DispatchEvent<PlayerHealthUpdatedEvent>(new PlayerHealthUpdatedEvent(health));

        if (health <= 0f )
        {
            playerMovement.enabled = false;
            audio.Stop();
        }
    }
}
