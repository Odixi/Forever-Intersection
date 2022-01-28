using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDecorator : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Furniture furniture;
    public GameObject currentFurniture;
    public GameObject furnitureBlueprint;
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
            Instantiate(currentFurniture, transform.position, Quaternion.identity);
        }
    }
}
