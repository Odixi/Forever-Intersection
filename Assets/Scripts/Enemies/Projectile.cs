using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 Direction;
    public float Speed;
    public float Damage;
    public float MaxTimeAlive = 5;
    
    private float SpawnTime;


    private void Start()
    {
        SpawnTime = Time.time;
        var r = GetComponent<Rigidbody>();
        r.velocity = Direction * Speed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Player.Instance.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
