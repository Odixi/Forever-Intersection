using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDElementController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public TextMeshProUGUI ammoCurrentMag, ammoAll, health, gibleds;
    public Player player;
    public float debugHealth;
    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerResources();
        ChangePlayerFace();
    }
    void UpdatePlayerResources()
    {
        health.text = player.Health.ToString();
        ammoCurrentMag.text = weapon.ammoInMagazine.ToString();
        ammoAll.text = weapon.ammo.ToString();      
    }
    void ChangePlayerFace()
    {
        if(debugHealth >= 80f)
        {
            animator.SetBool("Full", true);
            animator.SetBool("Medium", false);
            animator.SetBool("Low", false);
        }
        if(debugHealth >= 30 && debugHealth <= 79f)
        {
            animator.SetBool("Full", false);
            animator.SetBool("Medium", true);
            animator.SetBool("Low", false);
        }
        if(debugHealth >= 0 && debugHealth <= 29f)
        {
            animator.SetBool("Full", false);
            animator.SetBool("Medium", false);
            animator.SetBool("Low", true);
        }
    }
}
