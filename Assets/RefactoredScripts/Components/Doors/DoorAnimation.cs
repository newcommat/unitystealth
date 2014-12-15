using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {

    public bool requireKey;
    public AudioClip doorSwishClip;
    public AudioClip accessDeniedClip;

    private Animator myAnimator;
    private GameObject player;
    private bool playerHasKey;
    private int count;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(Tag.player);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (requireKey)
            {
                if (player.GetComponent<PlayerInventory>().hasKey)
                {
                    count++;
                }
                else
                {
                    audio.clip = accessDeniedClip;
                    audio.Play();
                }
            }
            else
            {
                count++;
            }
        }

        else if (other.gameObject.tag == Tag.enemy)
        {
            if (other is CapsuleCollider)
            {
                count++;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player || (other.gameObject.tag == Tag.enemy && other is CapsuleCollider))
        {
            count = Mathf.Max(0, count - 1);
        }
    }

    void Update()
    {
        myAnimator.SetBool(HashIDs.openBool, count > 0);

        if (myAnimator.IsInTransition(0) && !audio.isPlaying)
        {
            audio.clip = doorSwishClip;
            audio.Play();
        }
    }
}
