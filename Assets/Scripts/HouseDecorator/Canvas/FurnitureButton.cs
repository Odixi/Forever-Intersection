using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FurnitureButton : MonoBehaviour
{
    [SerializeField] private HouseDecorator houseDecorator;
    [SerializeField] private TextMeshProUGUI nameText, price;
    public Furniture furniture;

    private void Start()
    {
        nameText.text = furniture.furnitureName;
        price.text = furniture.furniturePrice.ToString();
    }

    public void OnClick()
    {
        var bluePrint = GameObject.FindGameObjectWithTag("Blueprint");
        if(bluePrint != null)
        {
            Destroy(bluePrint);
        }
        GameObject furnitureObj = Instantiate(furniture.furnitureBlueprint, transform.position, Quaternion.identity);
        Blueprint blueprintScript = furnitureObj.GetComponent<Blueprint>();
        if(blueprintScript)
        {
            blueprintScript.houseDecorator = houseDecorator;
        }   
        houseDecorator.currentFurniture = furniture.fullFurniture;
        houseDecorator.currentFurniturePrice = furniture.furniturePrice;
        houseDecorator.currentFurnitureBasepoints = furniture.basePoints;
    }
}