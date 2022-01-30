using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    public enum demonType {sofa, lamp, chair}
    public Furniture[] furnitures;
    public Furniture preferredFurniture;
    public demonType demon;
    public Animator assAnimator, donutAnimator;
    public float speed;
    [Range(0,2)] float range = 1f;
    // Start is called before the first frame update
    void Start()
    {
        demon = (demonType)Random.Range(0,3);
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
    public void DemonPreference()
    {
        switch (demon)
        {
            case demonType.chair:
                
            break;
            case demonType.lamp:

            break;
            case demonType.sofa:

            break;
        }
        
    }
}
