using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private Light light;
    private bool gotHit = false;

    public void GetHit()
    {
        if (gotHit) return;
        gotHit = true;
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        explosion.SetActive(true);
        light.intensity = 0;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return null;
    }
}
