using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> startVoiceLines;
    [SerializeField]
    private List<AudioClip> generalKillVoiceLines;
    [SerializeField]
    private List<AudioClip> donutKillVoiceLines;
    [SerializeField]
    private List<AudioClip> damageSounds;
    // Singleton
    public static Player Instance;

    public List<GameObject> weapons;
    public Weapon weapon;
    [SerializeField]
    private float maxHealth;
    private float lastPlayedVoiceLine = 1f;
    private float voiceLineMinInterval = 10f;
    public float Health { get; private set; }

    private AudioSource audioSource;
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

    public void OnPickup(PickupType type, float amount)
    {
        switch (type)
        {
            case PickupType.Ammo:
                foreach(var wgo in weapons)
                {
                    var w = wgo.GetComponent<Weapon>();
                    w.ammo += (int)(amount * w.magazineSize);
                }
                break;
            case PickupType.Health:
                Health += amount;
                break;
            case PickupType.Gibs:
                // TODO
                break;
        }
    }

    public void OnEnemyKill(Enemy enemy)
    {
        if (Time.time < lastPlayedVoiceLine + voiceLineMinInterval) return;
        if (Random.Range(0f, 1f) > 0.3f) return;
        lastPlayedVoiceLine = Time.time;
        if (enemy.gameObject.GetComponent<ShootingEnemy>() && Random.Range(0, donutKillVoiceLines.Count + generalKillVoiceLines.Count) == 0)
        {
            audioSource.clip = donutKillVoiceLines[Random.Range(0, donutKillVoiceLines.Count)];
        } else
        {
            audioSource.clip = generalKillVoiceLines[Random.Range(0, generalKillVoiceLines.Count)];
        }
        audioSource.Play();
    }

    public void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
        Health = maxHealth;
        audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(PlayStartSound());
    }

    IEnumerator PlayStartSound()
    {
        yield return new WaitForSeconds(1f);
        audioSource.clip = startVoiceLines[Random.Range(0, startVoiceLines.Count)];
        audioSource.Play();
        yield return null;
    }

    public void TakeDamage(float damageAmount)
    {
        print($"Ouch! You take {damageAmount} damage, resulting in {Health} health");
        HUDElementController.Instance.HurtTrigger();
        Health -= damageAmount;
        audioSource.clip = damageSounds[Random.Range(0, damageSounds.Count)];
        audioSource.Play();
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
