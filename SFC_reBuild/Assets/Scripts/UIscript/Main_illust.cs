using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
public class Main_illust : MonoBehaviour
{
   // Image myImg;
    Vector3 mousePos;
    Vector3 updateMyPos;
    Vector3 zeroPoint;
    public float focus;
    public float setDrainage=10;
    Vector3 targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        zeroPoint=transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
       
       mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       //updateMyPos= gameObject.transform.localPosition;
       //updateMyPos=;
       if(focus>=0)
       targetPoint=Vector3.Lerp(zeroPoint,mousePos,focus)*setDrainage;
       else
         {    
       targetPoint=Vector3.Lerp(zeroPoint,mousePos,-focus)*-setDrainage;
         }
         transform.localPosition+=(targetPoint-transform.localPosition)/10;
    }   
}
