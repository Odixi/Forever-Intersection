using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDecorator : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] private Camera mainCam;
    [SerializeField] private DebugPlayer player;
    public GameObject currentFurniture;
    public GameObject currentBlueprint;
    public int currentFurniturePrice;
    public bool canPlace;
    public float rotation;
    public float rotAmount = 90f;
    

    // Update is called once per frame
    void Update()
    {
        GetPlayerMousePos();
        PlayerInput();
    }
    public void GetPlayerMousePos()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 10000, layerMask)) 
        {
            transform.position = raycastHit.point;
        }
    }
    public void PlayerInput()
    {
        if(Input.GetButtonDown("Fire1") && currentFurniture != null && canPlace)
        {
            if(player.playerCurrency >= currentFurniturePrice)
            {
            Instantiate(currentFurniture, transform.position, transform.rotation);
            player.playerCurrency -= currentFurniturePrice;
            } else Debug.Log("poor bastard no money");
        }
        if(Input.GetButtonDown("Reload"))
        {
            rotation += rotAmount;
            if(rotation >= 360)
            {
                rotation = 0;
            }
            transform.rotation = Quaternion.Euler(0,rotation,0);
        }
    }
}
