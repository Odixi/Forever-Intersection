using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Furniture", menuName = "ScriptableObjects/FurnitureScriptableObject", order = 1)]
public class Furniture : ScriptableObject
{
  public string furnitureName;
  public int furniturePrice;
  public int basePoints;
  public GameObject fullFurniture;
  public GameObject furnitureBlueprint;
}
