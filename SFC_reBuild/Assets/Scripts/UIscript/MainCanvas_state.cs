using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas_state : MonoBehaviour
{
    public int myCanvasNum=0;
    
    // Start is called before the first frame update
    void Start()
    {
        if(myCanvasNum<MainMenu_Manager.Instant.menuState)
        transform.localPosition=new Vector3(0,-1500);
        if(myCanvasNum>MainMenu_Manager.Instant.menuState)
        transform.localPosition=new Vector3(0,1500);
    }

    // Update is called once per frame
    void Update()
    {
        if(myCanvasNum<MainMenu_Manager.Instant.menuState)
        transform.localPosition+=(new Vector3(0,-1500)-transform.localPosition)/20;
        
        if(myCanvasNum>MainMenu_Manager.Instant.menuState)
        transform.localPosition+=(new Vector3(0,1500)-transform.localPosition)/20;
        if(myCanvasNum==MainMenu_Manager.Instant.menuState)
        transform.localPosition+=(new Vector3(0,0)-transform.localPosition)/20;
    }
}
