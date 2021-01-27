using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class char_Img : MonoBehaviour
{
    [SerializeField]
    int myId;
    Image myImg;
    float poscale=15;
    float colored=10;
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
        poscale*=PlayerPrefs.GetFloat("FPS")/144f;
        colored*=PlayerPrefs.GetFloat("FPS")/144f;
    }

    // Update is called once per frame
    void Update()
    {
        if(MainMenu_Manager.Instant.setPlayerID<myId)
        {
            transform.localPosition+=(new Vector3(1500,0)-transform.localPosition)/poscale;
            myImg.color +=(new Color(1, 1, 1, 0)-myImg.color)/colored;
            transform.localScale+=(new Vector3(0.75f,0.75f)-transform.localScale)/poscale;
        }
        else if(MainMenu_Manager.Instant.setPlayerID>myId)
        {
             transform.localPosition+=(new Vector3(-1500,0)-transform.localPosition)/poscale;
            myImg.color +=(new Color(1, 1, 1, 0)-myImg.color)/colored;
            transform.localScale+=(new Vector3(0.75f,0.75f)-transform.localScale)/poscale;
        }
        else
        {
            transform.localPosition +=(new Vector3(0,0)-transform.localPosition)/poscale;
            myImg.color +=(new Color(1, 1, 1, 1)-myImg.color)/(20*(PlayerPrefs.GetFloat("FPS")/144f));
            transform.localScale+=(new Vector3(0.8f,0.8f)-transform.localScale)/poscale;
        }
    }
}
