using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class char_Img : MonoBehaviour
{
    [SerializeField]
    int myId;
    Image myImg;
    // Start is called before the first frame update
    void Start()
    {
        myImg = gameObject.GetComponent<Image>();
        if(MainMenu_Manager.Instant.setPlayerID<myId)
        {
            transform.localPosition=new Vector3(1500,0);
            myImg.color =new Color(1, 1, 1, 0);
        }
        else if(MainMenu_Manager.Instant.setPlayerID>myId)
        {
            transform.localPosition=new Vector3(-1500,0);
            myImg.color =new Color(1, 1, 1, 0);
        }
        else
        {
            transform.localPosition =new Vector3(0,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MainMenu_Manager.Instant.setPlayerID<myId)
        {
            transform.localPosition+=(new Vector3(1500,0)-transform.localPosition)/15;
            myImg.color +=(new Color(1, 1, 1, 0)-myImg.color)/10;
            transform.localScale+=(new Vector3(0.75f,0.75f)-transform.localScale)/15;
        }
        else if(MainMenu_Manager.Instant.setPlayerID>myId)
        {
             transform.localPosition+=(new Vector3(-1500,0)-transform.localPosition)/15;
            myImg.color +=(new Color(1, 1, 1, 0)-myImg.color)/10;
            transform.localScale+=(new Vector3(0.75f,0.75f)-transform.localScale)/15;
        }
        else
        {
            transform.localPosition +=(new Vector3(0,0)-transform.localPosition)/15;
            myImg.color +=(new Color(1, 1, 1, 1)-myImg.color)/20;
            transform.localScale+=(new Vector3(0.8f,0.8f)-transform.localScale)/15;
        }
    }
}
