using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public HouseDecorator houseDecorator;
    public LayerMask layerMask;
    public Material blueMat;
    public Material redMat;
    public Renderer blueprintRend;
    
    private void Start()
    {
        houseDecorator.canPlace = true;
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
            houseDecorator.canPlace = false;
            blueprintRend.material.CopyPropertiesFromMaterial(redMat);
        } 
    }
    private void OnTriggerExit(Collider other)
        {
            houseDecorator.canPlace = true;
            blueprintRend.material.CopyPropertiesFromMaterial(blueMat);
        }
    }
