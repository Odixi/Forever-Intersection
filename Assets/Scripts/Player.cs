using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player Instance;

    public List<GameObject> weapons;
    public Weapon weapon;
    [SerializeField]
    private float maxHealth;
    public float Health { get; private set; }

    private CharacterController characterController;

    public Vector3 TargetPoint => characterController.bounds.center + 0.35f*Vector3.up;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
    }

    private void SelectWeapon(int index)
    {
        var selectedWeapon = weapons[index];
        if (selectedWeapon.activeInHierarchy) return;
        foreach (var weapon in weapons) weapon.SetActive(false);
        weapon = selectedWeapon.GetComponent<Weapon>();
        selectedWeapon.SetActive(true);
    }

    public void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
        Health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        print($"Ouch! You take {damageAmount} damage, resulting in {Health} health");
        HUDElementController.Instance.HurtTrigger();
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
