using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

    public float speedDampTime = 0.1f;
	
    private enum AnimState { Dying, Dead, Alive };

    private Animator anim;
    private PlayerController playerController;

    private float timer;

    private AnimState state;

    void Awake()
    {
        state = AnimState.Alive;
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

		anim.SetLayerWeight(1, 1f);
		
        EventDispatcher.RegisterCallback<PlayerHealthUpdatedEvent>(OnPlayerHealthUpdatedEvent);
    }

    void Update()
    {
        switch (state)
        {
            case AnimState.Alive:
				UpdateAnimationSpeed();
                anim.SetBool(HashIDs.sneakingBool, playerController.sneaking);
                anim.SetBool(HashIDs.shoutingBool, playerController.shouting);
                break;
            case AnimState.Dying:
                if (anim.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.dyingState && anim.GetBool(HashIDs.deadBool) == true)
				{
                    anim.SetBool(HashIDs.deadBool, false);
				}
                break;
            case AnimState.Dead:
                break;
        }
    }
	
    public void DeathAnimationComplete()
    {
        state = AnimState.Dead;
        EventDispatcher.DispatchEvent<PlayerDiedEvent>(new PlayerDiedEvent());
    }

    public void ShoutAnimationTrigger()
    {
        EventDispatcher.DispatchEvent<PlayerShoutedEvent>(new PlayerShoutedEvent());
    }

	private void UpdateAnimationSpeed()
	{
		if (playerController.heading.magnitude > 0f) {
            anim.SetFloat(HashIDs.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		}
		else
		{
            anim.SetFloat(HashIDs.speedFloat, 0f);
		}
	}

    private void OnPlayerHealthUpdatedEvent(PlayerHealthUpdatedEvent playerHealthUpdatedEvent)
    {
        if (playerHealthUpdatedEvent.health <= 0f)
        {
            state = AnimState.Dying;
            anim.SetBool(HashIDs.deadBool, true);
        }
    }
}
