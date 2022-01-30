using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player Instance;

    [SerializeField]
    private List<GameObject> weapons;
    [SerializeField]
    private float maxHealth;
    public float Health { get; private set; }

    private CharacterController characterController;

    public Vector3 TargetPoint => characterController.bounds.center + 0.35f*Vector3.up;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !weapons[0].activeInHierarchy)
        {
            foreach (var weapon in weapons) weapon.SetActive(false);
            weapons[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !weapons[1].activeInHierarchy)
        {
            foreach (var weapon in weapons) weapon.SetActive(false);
            weapons[1].SetActive(true);
        }
    }

    public void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
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
