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
    public GameObject hoverIndicator;
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
        Debug.Log(furniType);
        if(furniType == demonText)
        {
           houseDecorator.points += houseDecorator.currentFurnitureBasepoints * 2;
        }

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
                          Debug.Log("GOOD JOB");
                      }
                      if(houseDecorator.points <= requiredPoints)
                      {
                          Debug.Log("BAD JOB!!!!");
                      }            
                  }
              } else hoverIndicator.gameObject.SetActive(false);
          }
    }
}
