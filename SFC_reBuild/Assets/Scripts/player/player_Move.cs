using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon;
//using UnityStandardAssets.Utility;
public class player_Move : PunBehaviour
{
    public float speed = 0.1f;
    float hspeed, vspeed;
    public Vector3 plusVec;
    Vector2 speedNomal;
    int x, y;
    [HideInInspector]
    public PhotonView pv;
    Vector3 currPos;
    Vector2 Getmove;
    public Player_Fire firescript;
    [HideInInspector]
    public focus_Gun gunfocus;
    void Awake()
    {
        pv = GetComponent<PhotonView>();
        firescript.pv=pv;
        if (pv.isMine)
        {
            Camera.main.GetComponent<ShakeManager>().target = this.transform;
            PoolingManager.Instance.TruePlayer=this;
            PoolingManager.Instance.PlayerInstantiateAfterInit();
        }
        else
        {
            Debug.Log(pv.viewID+"joinned!");
        }
    }
    void Start()
    {
        pv.ObservedComponents[0] = this;
    }
    void Update()
    {
    }
    void LateUpdate()
    {
    }
    
    void FixedUpdate()
    {
        if (pv.isMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            speedNomal = (new Vector2(h, v));
            if (speedNomal.magnitude > 1)
                speedNomal = speedNomal.normalized;
            transform.Translate(speedNomal * (speed));
        }
        else
        {

            if (Getmove.magnitude > 1)
                Getmove = Getmove.normalized;
            transform.Translate(Getmove * (speed));
            if(Vector3.SqrMagnitude(transform.position-currPos)>=0.05f)
            {
                transform.position += (currPos-transform.position)/5;
            }

        }
        Vector3 tempPo=transform.position;
        tempPo.z=transform.position.y;
        transform.position=tempPo;
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext((Vector2)speedNomal);
            stream.SendNext(gunfocus.rotateDegree);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            Getmove = (Vector2)stream.ReceiveNext();
            gunfocus.toDegree = (float)stream.ReceiveNext();
        }
    }
}
