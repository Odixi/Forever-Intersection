using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    private bool hasSeenPlayer = false;
    private int framesSeen = 0;
    private int framesNotSeen = 0;
    [SerializeField]
    private float MoveSpeed = 0.008f;
    [SerializeField]
    private float FireCooldown = 2;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite dyeSprite;

    [SerializeField]
    Sprite idleSprite;
    [SerializeField]
    Sprite shootSprite;

    [SerializeField]
    Transform shootFrom;

    [SerializeField]
    AudioClip shootClip;

    private AudioSource audioSource;

    private float lastShot;

    private void Start()
    {
        lastShot = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.isDead)
        {
            spriteRenderer.sprite = dyeSprite;
            return;
        }
        bool seesPlayer = IsPlayerInSight();
        if (seesPlayer)
        {
            framesNotSeen = 0;
            framesSeen++;
        }
        else
        {
            framesSeen = 0;
            framesNotSeen++;
        }

        if (!hasSeenPlayer && framesSeen >= 8)
        {
           hasSeenPlayer = true;
        }
        if (hasSeenPlayer && framesNotSeen >= 150)
        {
            hasSeenPlayer = false;
        }

        if (hasSeenPlayer)
        {
            MoveTovardsPlayer(MoveSpeed);
        }

        if ((Time.time - lastShot) > FireCooldown && IsPlayerInSight())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        lastShot = Time.time;
        var go = Instantiate(projectilePrefab, shootFrom.position, Quaternion.identity);
        var projectile = go.GetComponent<Projectile>();
        projectile.Direction = (Player.Instance.TargetPoint - eyes.position).normalized;
        audioSource.PlayOneShot(shootClip);
        StartCoroutine(shootAnimation());
    }

    IEnumerator shootAnimation()
    {
        spriteRenderer.sprite = shootSprite;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = idleSprite;
    }


    protected override void OnAboutToDie()
    {
        // Animate
    }

    protected override void OnTakeDamage(float damageAmount)
    {
        hasSeenPlayer = true;

    }
}
