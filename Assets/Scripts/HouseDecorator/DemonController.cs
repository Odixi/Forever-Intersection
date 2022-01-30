using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    public Camera rayCam;
    public static DemonController singleton;
    public enum demonType {sofa, lamp, chair}
    public enum demonDifficulty {easy, medium, hard}
    public demonDifficulty demonDiff;
    public demonType demon;
    public HouseDecorator houseDecorator;
    public GameObject hoverIndicator, demonLike, happyDemon, sadDemon, donut, ass, canvasThings, goToHell, helloText, helpText;
    public bool happyDemonPart = false;
    public string demonText;
    public Animator assAnimator, donutAnimator;
    public float speed;
    [Range(0,2)] float range = 1f;
    public int requiredPoints;
    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        demon = (demonType)Random.Range(0,3);
        demonDiff = (demonDifficulty)Random.Range(0,3);
        demonText = demon.ToString();
        helloText.SetActive(true);
        CheckDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        MoveDemonUpAndDown();
        CheckIfPlayerTouchesMe();
    }
    void CheckDifficulty()
    {
        switch (demonDiff)
            {
                case demonDifficulty.easy:
                    requiredPoints = 10;
                         break;
                case demonDifficulty.medium:
                    requiredPoints = 20;
                        break;
                case demonDifficulty.hard:
                    requiredPoints = 30;
                        break;
                      }
    }
    void MoveDemonUpAndDown()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * range + 2f;
        transform.position = new Vector3(0, y, 1.849f);   
    }
    public void DemonPreference(string furniType)
    {
        helloText.SetActive(false);
        helpText.SetActive(true);
        if(furniType == demonText)
        {
           houseDecorator.points += houseDecorator.currentFurnitureBasepoints * 2;
           demonLike.SetActive(true);
        } else demonLike.SetActive(false);

    }
    public void CheckIfPlayerTouchesMe()
    {
        Ray ray;
        RaycastHit hit;
        ray = rayCam.ScreenPointToRay(Input.mousePosition);
          if(Physics.Raycast(ray, out hit))
          {
              if(hit.collider.tag == "DemonWaifu")
              {
                  hoverIndicator.gameObject.SetActive(true);
                  if(Input.GetButtonDown("Fire1"))
                  {                   
                      if(houseDecorator.points >= requiredPoints)
                      {                       
                          happyDemon.gameObject.SetActive(true);
                          donut.gameObject.SetActive(false);
                          ass.gameObject.SetActive(false);
                          canvasThings.gameObject.SetActive(false);
                          goToHell.gameObject.SetActive(true);
                          demonLike.SetActive(false);
                          helpText.SetActive(false);
                          helloText.SetActive(false);
                          houseDecorator.currentFurniture = null;
                          Blueprint.singleton = null;
                      }
                      if(houseDecorator.points <= requiredPoints)
                      {
                          sadDemon.gameObject.SetActive(true);
                          donut.gameObject.SetActive(false);
                          ass.gameObject.SetActive(false);
                          canvasThings.gameObject.SetActive(false);
                          goToHell.gameObject.SetActive(true);
                          demonLike.SetActive(false);
                          helpText.SetActive(false);
                          helloText.SetActive(false);
                          houseDecorator.currentFurniture = null;
                          Blueprint.singleton = null;
                      }            
                  }
              } else hoverIndicator.gameObject.SetActive(false);
          }
    }
}
