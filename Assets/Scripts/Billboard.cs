using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    Transform cam;
    public void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(cam, Vector3.up);
    }
}
