using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour {

    public float timeToDoorsClose = 2f;
    public float timeToLiftStart = 3f;
    public float timeToEnd = 6f;
    public float liftSpeed = 3f;

    private GameObject player;
    private Animator playerAnim;
    private CameraMovement camMovement;
    private SceneFadeInOut sceneFadeInOut;
    private LiftDoorsTracking liftDoorsTracking;
    private bool playerInLift;
    private float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tag.player);
        playerAnim = player.GetComponent<Animator>();
        camMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        sceneFadeInOut = GameObject.FindGameObjectWithTag(Tag.fader).GetComponent<SceneFadeInOut>();
        liftDoorsTracking = GetComponent<LiftDoorsTracking>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInLift = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInLift = false;
            timer = 0f;
        }
    }

    void Update()
    {
        if (playerInLift)
            LiftActivation();

        if (timer <timeToDoorsClose)
        {
            liftDoorsTracking.DoorFollowing();
        }
        else
        {
            liftDoorsTracking.CloseDoors();
        }
    }

    void LiftActivation()
    {
        timer += Time.deltaTime;

        if (timer >= timeToLiftStart)
        {
            player.transform.parent = transform;

            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);

            if (!audio.isPlaying)
            {
                EventDispatcher.DispatchEvent<DynamicExitLiftStartEvent>(new DynamicExitLiftStartEvent());
                audio.Play();
            }

            if (timer >= timeToEnd)
            {
                EventDispatcher.DispatchEvent<DynamicExitLiftFinishedEvent>(new DynamicExitLiftFinishedEvent());
            }
        }
    }
}
