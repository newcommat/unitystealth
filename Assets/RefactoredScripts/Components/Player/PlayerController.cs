using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private enum State { Playing, Frozen }
    private State state;

    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;

	public bool shouting;
	public bool sneaking;
	public Vector2 heading;

	private Animator anim;

    void Awake()
    {
		anim = GetComponent<Animator>();
        EventDispatcher.RegisterCallback<DynamicExitLiftStartEvent>(OnLiftStart);
        state = State.Playing;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Frozen:
                break;
            case State.Playing:
                {
                    sneaking = Input.GetButton("Sneak");
                    heading = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                    if (heading.magnitude > 0f)
                    {
                        RotateTowardsTargetDirection(heading);
                    }

                    CheckInteractions(Input.GetButton("Switch"));

                    shouting = Input.GetButtonDown("Attract");
                }
                break;
        }

    }

    void RotateTowardsTargetDirection(Vector2 heading)
    {
		Vector3 targetDirection = new Vector3(heading.x, 0, heading.y);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		rigidbody.MoveRotation(newRotation);
    }

    void CheckInteractions(bool interacting)
    {
        if (interacting)
        {
            // Get list of objects we are colliding with
            Collider[] colliders = Physics.OverlapSphere(GetComponent<CapsuleCollider>().transform.position, GetComponent<CapsuleCollider>().radius);

            // For all colliders trigger their interactions
            foreach(Collider collider in colliders)
            {
                IInteraction interactionComponent = GameObjectExtensions.GetInterface<IInteraction>(collider.gameObject);
                
                if (interactionComponent != null)
                {
                    interactionComponent.Interact();
                }
            }
        }
    }

    private void OnLiftStart(DynamicExitLiftStartEvent liftStartEvent)
    {
        heading = Vector2.zero;
        shouting = false;
        sneaking = false;
        state = State.Frozen;
    }
}
