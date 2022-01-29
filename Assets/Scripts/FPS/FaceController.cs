using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FaceController : MonoBehaviour
{
    public Animator animator;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("faceNormalFull");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
