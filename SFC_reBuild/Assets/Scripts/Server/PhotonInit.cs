using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class PhotonInit : Photon.PunBehaviour
{
    float nextTime;
    void Awake()
    {
        //PhotonNetwork.ConnectUsingSettings("1.0");
    }
    // public override void OnJoinedLobby()
    // {
    //     Debug.Log("Joinnerd Lobby");
    //     PhotonNetwork.JoinRoom(PlayerPrefs.GetString("roomName"));
    //     //PhotonNetwork.JoinRandomRoom();
    // }
    // string rmname;
    // public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    // {
    //     Debug.Log("NO Room");
    //     RoomOptions roomOptions = new RoomOptions();
    //     roomOptions.IsVisible = true;
    //     roomOptions.MaxPlayers = 2;
    //     roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "val", 0 } };
    //     PhotonNetwork.CreateRoom(PlayerPrefs.GetString("roomName"),roomOptions,null);
        
    // }

    // public override void OnCreatedRoom()
    // {
    //     Debug.Log("Finish make a room");
    // }
    // public override void OnJoinedRoom()
    // {
    //     Debug.Log("Joined room");
    //     CreatePlayer();

    // }
    public IEnumerator CreatePlayer()
    {

        yield return new WaitForSeconds(2f);
        
        GameManager.Instance.startCu();
        PhotonNetwork.Instantiate("p"+PlayerPrefs.GetInt("Player_ID"),new Vector3((PoolingManager.Instance.isOPner)?-3:3,0,0),Quaternion.identity,0);
        yield return null;
    }
    public static void leaveRoom()
    {
        PhotonNetwork.room.IsOpen=false;
        PhotonNetwork.LeaveRoom();
    }
    void OnGUI()
    {
    GUILayout.Label(PhotonNetwork.room.Name);
    }
// Start is called before the first frame update
    void Start()
    {
        dis_popup.SetActive(false);
        StartCoroutine(CreatePlayer());
        nextTime=Time.time+3;
        Debug.Log("<color=cyan>Room Name:"+PhotonNetwork.room.Name+"</color>");
    }
    public void goMain()
    {
        PhotonNetwork.room.IsOpen=false;
        PhotonNetwork.LeaveRoom();
        LoadingSceneManager.LoadScene("Main_menu");
    }
    public GameObject dis_popup;
// Update is called once per frame
    void Update()
    {
        if(Time.time>nextTime)
        {
            if(PhotonNetwork.room.PlayerCount<2&&!GameManager.Instance.isend)
            {
                if(!dis_popup.GetActive())
                {
                    dis_popup.SetActive(true);
                    //Time.timeScale=0;
                }
            }
        }
    }
}
