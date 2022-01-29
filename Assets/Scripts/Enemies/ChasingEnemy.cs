using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    private bool hasSeenPlayer = false;
    [SerializeField]
    private float MoveSpeed = 0.035f;


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
            if ((playerCamera.transform.position - eyes.position).magnitude < 1)
            {
                Player.Instance.TakeDamage(1);
            }
        }
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
