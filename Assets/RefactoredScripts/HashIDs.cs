using UnityEngine;
using System.Collections;

public static class HashIDs
{
    // Here we store the hash tags for various strings used in our animators.
    public static int dyingState = Animator.StringToHash("Base Layer.Dying");
    public static int deadState = Animator.StringToHash("Base Layer.Dead");
    public static int locomotionState = Animator.StringToHash("Base Layer.Locomotion");
    public static int shoutState = Animator.StringToHash("Shouting.Shout");
    public static int deadBool = Animator.StringToHash("Dead");
    public static int speedFloat = Animator.StringToHash("Speed");
    public static int sneakingBool = Animator.StringToHash("Sneaking");
    public static int shoutingBool = Animator.StringToHash("Shouting");
    public static int playerInSightBool = Animator.StringToHash("PlayerInSight");
    public static int shotFloat = Animator.StringToHash("Shot");
    public static int aimWeightFloat = Animator.StringToHash("AimWeight");
    public static int angularSpeedFloat = Animator.StringToHash("AngularSpeed");
    public static int openBool = Animator.StringToHash("Open");
}