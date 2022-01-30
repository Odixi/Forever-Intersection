using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDecorator : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] private Camera mainCam;
    [SerializeField] public DebugPlayer player;
    public GameObject currentFurniture;
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
            if(Blueprint.singleton != null)
            {
             Blueprint.singleton.transform.position = raycastHit.point;
             Blueprint.singleton.transform.rotation = Quaternion.Euler(0,rotation,0);
            }
        }
    }
    public void PlayerInput()
    {
        if(Input.GetButtonDown("Fire1") && currentFurniture != null && canPlace)
        {
            if(player.playerCurrency >= currentFurniturePrice)
            {
           GameObject placedFurnitureObj = Instantiate(currentFurniture, transform.position, transform.rotation);
           PlacedFurniture placedFurniture = placedFurnitureObj.GetComponentInChildren<PlacedFurniture>();
           if(placedFurniture != null)
            {
               placedFurniture.furniturePrice = currentFurniturePrice;
            }
            player.playerCurrency -= currentFurniturePrice;
            }
            if(player.playerCurrency < currentFurniturePrice)
            {
                canPlace = false;
                Blueprint.singleton.MatChanger();
            }
        }
        if(Input.GetButtonDown("Fire2") && currentFurniture != null &&Blueprint.singleton.canSell)
            {
                Debug.Log("Sell");
                player.playerCurrency += Blueprint.singleton.placedFurniture.furniturePrice;
                Destroy(Blueprint.singleton.placedFurniture.transform.parent.gameObject);
                Blueprint.singleton.placedFurniture = null;
                canPlace = true;
                Blueprint.singleton.MatChanger();
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
