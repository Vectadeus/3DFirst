using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairScript : MonoBehaviour
{
    private RectTransform rect;

    [Range(10f, 250f)]
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.sizeDelta = new Vector2(size,size);
    }
}//CLASS
