using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mainfade_logo : MonoBehaviour
{
    Image parImg;
    Image myImg;
    // Start is called before the first frame update
    void Start()
    {
        parImg=GetComponentInParent<Image>();
        myImg=GetComponent<Image>();
        myImg.color=new Color(1,1,1,parImg.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        myImg.color=new Color(1,1,1,parImg.color.a);
    }
}
