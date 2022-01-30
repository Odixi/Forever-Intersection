using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedFurniture : MonoBehaviour
{
    public enum furnitureType {sofa, lamp, chair}
    public furnitureType type;
    public string furniType;
    public delegate void OnPlaceItem(string furniType);
    public event OnPlaceItem onPlaceItem;
    public int furniturePrice;

    private void OnEnable()
    {
        onPlaceItem += DemonController.singleton.DemonPreference;
    }
    private void OnDisable()
    {
         onPlaceItem -= DemonController.singleton.DemonPreference;
    } 
    private void Start()
    {
        furniType = type.ToString();
        FurnitureLikeCheck();
    }
    public void FurnitureLikeCheck()
    {
        onPlaceItem?.Invoke(furniType);
    }

}
