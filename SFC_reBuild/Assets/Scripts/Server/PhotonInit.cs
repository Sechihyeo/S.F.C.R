using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class PhotonInit : Photon.PunBehaviour
{
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
        PhotonNetwork.Instantiate("sechi",new Vector3((PoolingManager.Instance.isOPner)?-3:3,0,0),Quaternion.identity,0);
        yield return null;
    }
    void OnGUI()
    {
    GUILayout.Label(PhotonNetwork.room.Name);
    }
// Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatePlayer());

        Debug.Log("<color=cyan>Room Name:"+PhotonNetwork.room.Name+"</color>");
    }
// Update is called once per frame
    void Update()
    {

    }
}
