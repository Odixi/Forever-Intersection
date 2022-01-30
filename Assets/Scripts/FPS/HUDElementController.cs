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
    [SerializeField]
    private GameObject pressEtoEnter;

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
        ammoCurrentMag.text =  $"{weapon.ammoInMagazine}/{weapon.ammo}";
        ammoAll.text = weapon.ammo.ToString();
        pressEtoEnter.SetActive(Player.Instance.LooksAtHouse);
    }

    private bool IsExcited() => animator.GetBool("Excited");
    private void SetExcited(bool value) => animator.SetBool("Excited", value);

    public void GetExcited()
    {
        if (!IsExcited()) StartCoroutine(ExcitedCoroutine());
    }

    IEnumerator ExcitedCoroutine()
    {
        SetExcited(true);
        yield return new WaitForSeconds(2);
        SetExcited(false);
        yield return null;
    }

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
