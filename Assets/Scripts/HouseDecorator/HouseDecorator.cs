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
        if(Input.GetMouseButtonDown(0) && currentFurniture != null && canPlace)
        {
            if(player.playerCurrency >= currentFurniturePrice)
            {
            Instantiate(currentFurniture, transform.position, transform.rotation);
            player.playerCurrency -= currentFurniturePrice;
            } else Debug.Log("poor bastard no money");
        }
    }
}
