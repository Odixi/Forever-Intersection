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
    private float fallOffDistance = 5f;

    public void GetHit()
    {
        if (gotHit) return;
        gotHit = true;
        StartCoroutine(Explode());
    }

    private void ExplosionDamage()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {

            var damage = Mathf.RoundToInt(Mathf.Max(0f, 110f - 110f * (Vector3.Distance(player.transform.position, transform.position) / fallOffDistance)));
            player.GetComponent<Player>().TakeDamage(damage);
        }
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            var damage = Mathf.RoundToInt(Mathf.Max(0f, 110f - 110f * (Vector3.Distance(enemy.transform.position, transform.position) / fallOffDistance)));
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        foreach (var barrel in GameObject.FindGameObjectsWithTag("Barrel"))
        {
            if (Vector3.Distance(barrel.transform.position, transform.position) < 3)
            {
                barrel.GetComponent<ExplosiveBarrel>().GetHit();
            }
        }
    }

    IEnumerator Explode()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        explosion.SetActive(true);
        light.intensity = 0;
        ExplosionDamage();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return null;
    }
}
