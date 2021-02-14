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
    public bool isend=false;
    public int myScore,otScore;
    public GameObject result;
    AudioSource musicSS;
    AudioSource sfx;
    [SerializeField]
    AudioClip pressSfxmusic;
      [SerializeField]
    AudioClip pressSfx;
    [SerializeField]
    Image eliminate;
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
        musicSS = gameObject.AddComponent<AudioSource>();
        sfx = gameObject.AddComponent<AudioSource>();
        sfx.clip = pressSfx;
        sfx.volume = PlayerPrefs.GetFloat("sfXVol") * PlayerPrefs.GetFloat("masterVol");

        musicSS.clip = pressSfxmusic;
        musicSS.volume = PlayerPrefs.GetFloat("musicVol") * PlayerPrefs.GetFloat("masterVol");
        musicSS.loop = true;
        musicSS.Play();
        eliminate.color = new Color32(1,1,1,0);
        
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
        {
            leftTime.text="Time Over!";
            if(result.GetActive()==false)
            {
                sfx.Play();
                result.SetActive(true);

            }
        }
        if(Input.GetKey(KeyCode.P))
         {
             PhotonInit.leaveRoom();
             LoadingSceneManager.LoadScene("Main_menu");
         }
        eliminate.color += (new Color(1,1,1,0)-eliminate.color)/20;
        eliminate.transform.localScale += (new Vector3(1,1)-eliminate.transform.localScale)/20;
    }
    public void onEliminate()
    {
        eliminate.color = new Color(1,1,1,1);
        eliminate.transform.localScale = new Vector3(2,2);
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
