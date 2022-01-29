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
    }
    void UpdatePlayerResources()
    {
        health.text = player.Health.ToString();

        ammoCurrentMag.text = weapon.ammoInMagazine.ToString();
        ammoAll.text = weapon.ammo.ToString();
        
    }
}
