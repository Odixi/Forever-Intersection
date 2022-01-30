using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public static Blueprint singleton;
    public HouseDecorator houseDecorator;
    public LayerMask layerMask;
    public PlacedFurniture placedFurniture;
    public Material blueMat;
    public Material redMat;
    public Renderer blueprintRend;
    public bool canSell = false;
 
    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        placedFurniture = null;
        houseDecorator.canPlace = true;
    }
    public void MatChanger()
    {
        if(houseDecorator.canPlace == false)
        {
            for(int i = 0; i < blueprintRend.materials.Length; i++)
            {
                blueprintRend.materials[i].CopyPropertiesFromMaterial(redMat);
            }
        }
        if(houseDecorator.canPlace == true)
        {
            for(int i = 0; i < blueprintRend.materials.Length; i++)
            {
                blueprintRend.materials[i].CopyPropertiesFromMaterial(blueMat);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Furniture") || other.gameObject.CompareTag("DemonWaifu"))
        {
            houseDecorator.canPlace = false;
            MatChanger();
            placedFurniture = other.gameObject.GetComponent<PlacedFurniture>();
            if(placedFurniture != null)
            {
                canSell = true;
            }
        } 
    }
    private void OnTriggerExit(Collider other)
        {
            houseDecorator.canPlace = true;
            canSell = false;
            MatChanger();
        }
    }
