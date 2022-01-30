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
    [SerializeField]
    AudioClip dieSound;
    protected bool isDead = false;

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

    protected bool IsPlayerInSight(float maxDistance = 100)
    {
        var d = playerCamera.transform.position - eyes.position;
        var mag = d.magnitude;
        if (mag > maxDistance)
        {
            return false;
        }
        var res = Physics.Raycast(eyes.position, d, out var hit, mag);
        return !res || hit.collider.gameObject.tag == "Player";
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
        gameObject.GetComponent<Collider>().enabled = false;
        isDead = true;
        Player.Instance.OnEnemyKill(this);

        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = dieSound;
        audioSource.Play();
        StartCoroutine(DeleteSpriteRendersDelayed());
        StartCoroutine(DestroyAfterSound());
    }

    private void DeleteSpriteRenders(GameObject obj)
    {
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        for (var i = 0; i < obj.transform.childCount; i++)
        {
            DeleteSpriteRenders(obj.transform.GetChild(i).gameObject);
        }
    }

    IEnumerator DeleteSpriteRendersDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        DeleteSpriteRenders(gameObject);
        yield return null;
    }

    IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(dieSound.length);
        Destroy(gameObject);
    }

}
