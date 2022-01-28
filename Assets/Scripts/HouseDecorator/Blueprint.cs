using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public bool canPlace;

    void Update()
    {
        GetPlayerMousePos();
    }

    public void GetPlayerMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
    }

    private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag == "Furniture")
            {
                canPlace = false;
            } else canPlace = true;
        }
}
