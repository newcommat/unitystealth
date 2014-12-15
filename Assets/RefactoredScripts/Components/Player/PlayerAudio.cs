using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

    public AudioClip pickup;
    public AudioClip shout;

    private Animator animator;

	void Awake () {
        EventDispatcher.RegisterCallback<PlayerPickupEvent>(OnPlayerPickup);
        EventDispatcher.RegisterCallback<PlayerShoutedEvent>(OnPlayerShouted);

        animator = GetComponent<Animator>();
	}
	
    private void OnPlayerPickup(PlayerPickupEvent playerPickupEvent)
    {
        AudioSource.PlayClipAtPoint(pickup, playerPickupEvent.pickupObject.GetComponent<Transform>().position);
    }

    private void OnPlayerShouted(PlayerShoutedEvent playerShoutedEvent)
    {
        AudioSource.PlayClipAtPoint(shout, transform.position);
    }

	void Update () {
        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.locomotionState)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
	}
}
