using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class PHLobby : Photon.PunBehaviour
{
    [SerializeField]
    GameObject matchingLogo;
    bool nowFindgame = false;
    bool isJoined=false;
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
        PlayerPrefs.SetInt("isOner",0);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joinnerd Lobby");
    }
    public void matching_button()
    {
        if (nowFindgame == false)
        {
            nowFindgame = true;
            PhotonNetwork.JoinRandomRoom();
        }
    }
    string rmname;
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("NO Room");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "val", 0 } };
        PhotonNetwork.CreateRoom(Random.Range(0, 199).ToString(), roomOptions, null);
        PlayerPrefs.SetInt("isOner",1);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Finish make a room");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        Debug.Log(PhotonNetwork.room.Name);
        PlayerPrefs.SetString("roomName", rmname);
        isJoined=true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void matCancel()
    {
       nowFindgame=false;
       isJoined=false;
       PhotonNetwork.LeaveRoom();
    }
    // Update is called once per frame
    void Update()
    {
        matchingLogo.SetActive(nowFindgame);
        if (isJoined == true)
        {
            if (PhotonNetwork.room.PlayerCount == 2)
            {

                StartCoroutine(MainMenu_Manager.Instant.FadeOut("Ingame"));
            }
        }
    }


}
