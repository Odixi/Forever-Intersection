using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    public Animator assAnimator, donutAnimator;
    public float speed;
    [Range(0,2)] float range = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDemonUpAndDown();
    }
    void MoveDemonUpAndDown()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * range + 2f;
        transform.position = new Vector3(0, y, 1.849f);
    }
}
