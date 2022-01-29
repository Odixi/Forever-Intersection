using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public bool canPlace = true;
    public LayerMask layerMask;
    public Material blueMat;
    public Material redMat;
    public Renderer blueprintRend;
    
    private void Start()
    {
        Debug.Log(canPlace);
    }
    void Update()
    {
        GetPlayerMousePos();
    }
    public void GetPlayerMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 10000, layerMask))
        {
            transform.position = raycastHit.point;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Furniture"))
        {
            blueprintRend.material.CopyPropertiesFromMaterial(redMat);
        } 
    }
    private void OnTriggerExit(Collider other)
        {
            blueprintRend.material.CopyPropertiesFromMaterial(blueMat);
        }
    }
