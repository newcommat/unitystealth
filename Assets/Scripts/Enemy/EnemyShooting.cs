using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {
    public float maximumDamage = 120f;
    public float minimumDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float flashSpeed = 10f;
    public float fadeSpeed = 10f;

    private Animator anim;
    private LineRenderer laserShotline;
    private Light laserShotlight;
    private SphereCollider col;
    private Transform player;
    private PlayerHealth playerHealth;
    private bool shooting;
    private float scaledDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        laserShotline = GetComponentInChildren<LineRenderer>();
        laserShotlight = laserShotline.gameObject.light;
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(Tag.player).transform;
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();

        laserShotlight.intensity = 0f;
        laserShotline.enabled = false;

        shooting = false;
        scaledDamage = 0f;
    }

    void Update()
    {
        float shot = anim.GetFloat(HashIDs.shotFloat);

        if (shot > 0.5f && !shooting)
        {
            Shoot();
        }
        
        if (shot < 0.5f)
        {
            shooting = false;
            laserShotline.enabled = false;
        }

        laserShotlight.intensity = Mathf.Lerp(laserShotlight.intensity, 0f, fadeSpeed * Time.deltaTime);
    }

    void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(HashIDs.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, player.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }

    void Shoot()
    {
        shooting = true;
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
        float damage = scaledDamage * fractionalDistance + minimumDamage;
        playerHealth.TakeDamage(damage);
    }

    void ShotEffects()
    {
        laserShotline.SetPosition(0, laserShotline.transform.position);
        laserShotline.SetPosition(1, player.position + Vector3.up * 1.5f);
        laserShotline.enabled = true;

        laserShotlight.intensity = flashIntensity;
        AudioSource.PlayClipAtPoint(shotClip, laserShotlight.transform.position);
        
    }
}
