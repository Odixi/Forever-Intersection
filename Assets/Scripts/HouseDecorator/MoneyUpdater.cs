using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoneyUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public DebugPlayer debugPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = PlayerResources.Gibs.ToString();
    }

    // Update is called once per frame
    void Update()
    {
      moneyText.text = PlayerResources.Gibs.ToString();  
    }
}
