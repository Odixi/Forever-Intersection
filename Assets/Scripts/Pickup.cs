using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Ammo,
    Health,
    Gibs
}
public class Pickup : MonoBehaviour
{
    public PickupType PickupType;
    public float Amount;
    AudioSource audioSource;

    private bool isAlreadyTaken = false;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isAlreadyTaken && collider.gameObject.tag == "Player")
        {
            Player.Instance.OnPickup(PickupType, Amount);
            audioSource.Play();
            HideRenderers(gameObject);
            StartCoroutine(DestroyOnAudioClipEnd());
            isAlreadyTaken = true;
        }
    }

    private void HideRenderers(GameObject obj)
    {
        var renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null) renderer.enabled = false;
        for (var i = 0; i < obj.transform.childCount; i++)
        {
            HideRenderers(obj.transform.GetChild(i).gameObject);
        }
    }

    IEnumerator DestroyOnAudioClipEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
        yield return null;
    }
}
