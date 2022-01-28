using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDecorator : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private DebugPlayer player;
    public GameObject currentFurniture;
    public GameObject currentBlueprint;
    public int currentFurniturePrice;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerMousePos();
        PlayerInput();
    }
    public void GetPlayerMousePos()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit)) 
        {
            transform.position = raycastHit.point;
        }
    }
    public void PlayerInput()
    {
        if(Input.GetMouseButtonDown(0) && currentFurniture != null)
        {
            if(player.playerCurrency >= currentFurniturePrice)
            {
            Instantiate(currentFurniture, transform.position, Quaternion.identity);
            player.playerCurrency -= currentFurniturePrice;
            } else Debug.Log("poor bastard no money");
        }
    }
}
