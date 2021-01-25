using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_Fire : Player_Fire
{
    Vector3 rt;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override void CreateBullet()
    {
        if(!pv.isMine)
        return;

        //gunbody.transform.localEulerAngles += new Vector3(0, 0, 90);

        
            object[] pam = new object[5];
            rt= VectorRotation(PointDirection(transform.position,
            Camera.main.ScreenToWorldPoint(Input.mousePosition))+ Random.Range(-tanning,tanning));
            pam[0] = rt;
            pam[1] = OBsize;
            pam[2] = Bulletspeed;
            pam[3] = BulletDestroy;
            pam[4] = pv.viewID;
            PhotonNetwork.Instantiate(bullname, flamePosition.position,Quaternion.Euler(rt), 0)
            .GetComponent<PhotonView>().RPC("setToVector", PhotonTargets.All, pam);
        
    }
}
