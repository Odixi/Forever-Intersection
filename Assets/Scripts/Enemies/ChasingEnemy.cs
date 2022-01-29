using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    private bool hasSeenPlayer = false;
    private int framesSeen = 0;
    private bool isAttacking = false;
    [SerializeField]
    private float MoveSpeed = 0.035f;
    [SerializeField]
    private Animator attackAnim;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip attackAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasSeenPlayer && IsPlayerInSight())
        {
            framesSeen++;
            if (framesSeen >= 8)
            {
                hasSeenPlayer = true;
            }
        }
        else
        {
            framesSeen = 0;
        }

        if (hasSeenPlayer)
        {
            
            if ((playerCamera.transform.position - eyes.position).magnitude < 2.8f && !isAttacking)
            {
                StartCoroutine(Attack());
            }
            MoveTovardsPlayer(MoveSpeed);

        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        attackAnim.SetTrigger("Attack");
        audioSource.PlayOneShot(attackAudio);
        yield return new WaitForSeconds(0.12f);
        if ((playerCamera.transform.position - eyes.position).magnitude < 2.2f)
        {
            Player.Instance.TakeDamage(5);
        }
        yield return new WaitForSeconds(0.85f);
        isAttacking = false;
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
