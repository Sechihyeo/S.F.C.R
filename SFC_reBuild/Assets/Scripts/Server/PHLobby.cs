using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class PHLobby : Photon.PunBehaviour
{
    [SerializeField]
    GameObject matchingLogo;
    bool nowFindgame = false;
    bool isJoined = false;
    float NextTime;
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("1.1");
        PlayerPrefs.SetInt("isOner", 0);
        NextTime = Time.time + 3;
    }
    public override void OnJoinedLobby()
    {

        Debug.Log("Joinnerd Lobby");
    }
    public void matching_button()
    {
        if (PhotonNetwork.insideLobby == true)
        {
            if (nowFindgame == false)
            {
                nowFindgame = true;
                PhotonNetwork.JoinRandomRoom();
            }
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
        PlayerPrefs.SetInt("isOner", 1);
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
        isJoined = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        failed_popup.SetActive(false);
    }
    public void matCancel()
    {
        nowFindgame = false;
        isJoined = false;
        iscalled = false;
        PhotonNetwork.LeaveRoom();
    }
    bool iscalled = false;
    bool isFailed = false;
    // Update is called once per frame
    public void goIntro()
    {
        StartCoroutine(MainMenu_Manager.Instant.FadeOut("Intro"));
    }
    public GameObject failed_popup;
    void Update()
    {
        matchingLogo.SetActive(nowFindgame);
        if (isJoined == true)
        {
            if (PhotonNetwork.room.PlayerCount == 2 && iscalled == false)
            {
                StartCoroutine(MainMenu_Manager.Instant.FadeOut("Ingame"));
                PhotonNetwork.room.IsOpen = false;
                iscalled = true;
            }
        }
        if (Time.time > NextTime)
        {
            if (!PhotonNetwork.connected&&!isFailed)
            {
                //TODO: failed popup
                failed_popup.SetActive(true);
                isFailed = true;
            }
        }
    }


}
