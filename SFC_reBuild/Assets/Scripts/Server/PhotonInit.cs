using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class PhotonInit : Photon.PunBehaviour
{
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joinnerd Lobby");
        
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("NO Room");
        PhotonNetwork.CreateRoom(null);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Finish make a room");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        CreatePlayer();

    }
    public void CreatePlayer()
    {
        PhotonNetwork.Instantiate("sechi",new Vector3(0,0,0),Quaternion.identity,0);
    }
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
