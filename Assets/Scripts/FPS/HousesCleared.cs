using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HousesCleared : MonoBehaviour
{
    public TextMeshProUGUI housesTextGood, housesTextBad;
    // Start is called before the first frame update
    void Start()
    {
        housesTextGood.text = PlayerResources.HousesDecoratedGood.ToString();
        housesTextBad.text = PlayerResources.HousesDecoratedBad.ToString();
    }

}
