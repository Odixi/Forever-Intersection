using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player Instance;

    [SerializeField]
    private float maxHealth;
    public float Health { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void TakeDamage(float damageAmount)
    {
        print($"Ouch! You take {damageAmount} damage, resulting in {Health} health");
        Health -= damageAmount;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO
        Debug.Log("You Dieded!");
    }

}
