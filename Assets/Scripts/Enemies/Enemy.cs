using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    public float Health { get; private set; }

    [SerializeField]
    protected Transform eyes;

    protected Camera playerCamera;

    private void Awake()
    {
        Health = maxHealth;
        playerCamera = Camera.main;
    }

    protected void MoveTovardsPlayer(float amount)
    {
        // Try to avoid obstacles
        RaycastHit hit;
        var direction = Player.Instance.TargetPoint - eyes.position;
        Vector3 moveDirection = direction;
        if (Physics.Raycast(eyes.position, direction, out hit, 1.5f))
        {
            var c = Vector3.Cross(hit.normal, direction);
            moveDirection = Vector3.Cross(c, hit.normal);
            if (Physics.Raycast(eyes.position, moveDirection, out hit, 1f)){
                return;
            }
        }


        transform.position += amount * moveDirection.normalized;
    }

    protected bool IsPlayerInSight()
    {
        var d = playerCamera.transform.position - eyes.position;
        return !Physics.Raycast(eyes.position, d, d.magnitude-0.5f);
    }

    public bool TakeDamage(float damageAmount)
    {
        if (Health <= 0)
        {
            // Already ded
            return false;
        }
        Health -= damageAmount;

        OnTakeDamage(damageAmount);

        if (Health <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    abstract protected void OnTakeDamage(float damageAmount);
    abstract protected void OnAboutToDie();

    // TODO drop loot

    private void Die()
    {
        OnAboutToDie();
        Player.Instance.OnEnemyKill(this);
        Destroy(gameObject);
    }

}
