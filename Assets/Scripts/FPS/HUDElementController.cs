using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDElementController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public TextMeshProUGUI ammoCurrentMag, ammoAll, health, gibleds;
    public float debugHealth;
    public Weapon weapon;

    public static HUDElementController Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerResources();
        ChangePlayerFace();
    }

    void UpdatePlayerResources()
    {
        weapon = Player.Instance.weapon;
        health.text = Player.Instance.Health.ToString();
        ammoCurrentMag.text = weapon.ammoInMagazine.ToString();
        ammoAll.text = weapon.ammo.ToString();
    }

    public bool IsExcited() => animator.GetBool("Excited");
    public void SetExcited(bool value) => animator.SetBool("Excited", value);

    public void HurtTrigger() => animator.SetTrigger("Damage");

    void ChangePlayerFace()
    {
        if(Player.Instance.Health >= 80f)
        {
            animator.SetBool("Full", true);
            animator.SetBool("Medium", false);
            animator.SetBool("Low", false);
        }
        if(Player.Instance.Health >= 30 && Player.Instance.Health <= 79f)
        {
            animator.SetBool("Full", false);
            animator.SetBool("Medium", true);
            animator.SetBool("Low", false);
        }
        if(Player.Instance.Health >= 0 && Player.Instance.Health <= 29f)
        {
            animator.SetBool("Full", false);
            animator.SetBool("Medium", false);
            animator.SetBool("Low", true);
        }
    }
}
