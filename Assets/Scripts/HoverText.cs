using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    private Vector3 startPosition;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        startPosition = rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localPosition = startPosition + new Vector3(0, 0, Mathf.Sin(Time.time * 3));
    }
}
