using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    static private GameObject pref = null;
    static private GameObject Prefab {
        get
        {
            if (pref != null)
            {
                return pref;
            }
            pref = Resources.Load<GameObject>("BulletTrail");
            return pref;
        }
    }
    public static void Create(Vector3 start, Vector3 end)
    {
        var go = Instantiate(Prefab);
        var trail = go.GetComponent<BulletTrail>();
        trail.setTrail(start, end);
    }

    [SerializeField]
    float startThickness = 0.1f;
    [SerializeField]
    float fadeAmount = 0.003f;


    private LineRenderer lr;

    private void setTrail(Vector3 start, Vector3 end)
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.widthMultiplier = startThickness;
    }

    private void FixedUpdate()
    {
        if (lr != null)
        {
            lr.widthMultiplier -= fadeAmount;
            if (lr.widthMultiplier <= 0.0000001f)
            {
                Destroy(gameObject);
            }
        }
    }


}
