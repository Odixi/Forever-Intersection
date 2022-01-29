using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FurnitureButton : MonoBehaviour
{
    [SerializeField] private HouseDecorator houseDecorator;
    [SerializeField] private TextMeshProUGUI nameText;
    public Furniture furniture;

    private void Start()
    {
        nameText.text = furniture.furnitureName;
    }

    public void OnClick()
    {
        var bluePrint = GameObject.FindGameObjectWithTag("Blueprint");
        if(bluePrint != null)
        {
            Destroy(bluePrint);
        }
        Instantiate(furniture.furnitureBlueprint, transform.position, Quaternion.identity);
        houseDecorator.currentFurniture = furniture.fullFurniture;
        houseDecorator.currentFurniturePrice = furniture.furniturePrice;
    }
}