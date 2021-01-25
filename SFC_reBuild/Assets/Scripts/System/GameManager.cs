using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
public class GameManager : PunBehaviour
{
    public static GameManager Instance;
    public PhotonView pv;
    int gameTime=180;
    public Text leftTime;
    bool isend=false;
    void Awake()
    {
        if(Instance==null)
        Instance=this;
    }
    void Start()
    {
        pv=GetComponent<PhotonView>();
        pv.ObservedComponents[0] = this;
        if(PoolingManager.Instance.isOPner)
         StartCoroutine(timer());


    }
    public void startCu()
    {
    }
    public IEnumerator timer()
    {
        while(gameTime>=0)
        {    gameTime--;
           yield return new WaitForSeconds(1);
        }
        yield return null;
    }
    string sectime;
    // Update is called once per frame
    void Update()
    {
        if(gameTime<=0)
        {
            isend=true;
        }
        if(!isend)
        {
            sectime=(gameTime/60)+":";
            if(((gameTime%60)<10))
            {
                sectime+="0"+(gameTime%60);
            }
            else
            {
                sectime+=(gameTime%60).ToString();
            }
            leftTime.text=sectime;
        }
        else
        leftTime.text="Time Over!";

        if(Input.GetKey(KeyCode.P))
         {
             PhotonInit.leaveRoom();
             LoadingSceneManager.LoadScene("Main_menu");
         }
        
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameTime);
            //Debug.Log("send time!:"+gameTime);
        }
        else 
        {   
            if(!PoolingManager.Instance.isOPner)
            gameTime = (int)stream.ReceiveNext();
        }
    }
}
