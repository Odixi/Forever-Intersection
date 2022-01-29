using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    private bool hasSeenPlayer = false;
    [SerializeField]
    private float MoveSpeed = 0.008f;
    [SerializeField]
    private float FireCooldown = 2;

    [SerializeField]
    private GameObject projectilePrefab;

    private float lastShot;

    private void Start()
    {
        lastShot = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasSeenPlayer && IsPlayerInSight())
        {
            hasSeenPlayer = true;
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
        var go = Instantiate(projectilePrefab, eyes.position, Quaternion.identity);
        var projectile = go.GetComponent<Projectile>();
        projectile.Direction = (Player.Instance.TargetPoint - eyes.position).normalized;
    }


    protected override void OnAboutToDie()
    {
        // Animate
    }

    protected override void OnTakeDamage(float damageAmount)
    {
        // Animate

    }
}
