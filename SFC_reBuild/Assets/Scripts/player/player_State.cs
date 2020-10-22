using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon;

public class player_State : PunBehaviour
{
    public float player_HP = 100;
    public float Max_HP=100;
    public PhotonView pv;
    public bool isDead;
    BoxCollider2D box_collider;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        box_collider = transform.GetComponent<BoxCollider2D>();//컴포넌트를 얻어옴
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        hitScan();

        if (player_HP <= 0)
        {
            pv.RPC("playDie", PhotonTargets.All, null);
            player_HP = Max_HP;
        }
    }
    [PunRPC]
    void playDie()
    {
        Camera.main.GetComponent<ShakeManager>().Shake(0,0,15,1,10);
        StartCoroutine(coDie());
    }
    IEnumerator coDie()
    {
        isDead = true;
        Vector3 playerorisize = transform.localScale;
        GetComponent<player_Move>().gunfocus.enabled = false;
        GetComponent<player_Move>().enabled = false;
        GetComponent<Player_Fire>().enabled = false;
        box_collider.enabled = false;
        transform.localScale = new Vector3(0, 0);
        for (int i = 5; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.5f);
        }
        transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-7, 7));
        GetComponent<player_Move>().gunfocus.enabled = true;
        GetComponent<player_Move>().enabled = true;
        GetComponent<Player_Fire>().enabled = true;
        transform.localScale = playerorisize;
        Camera.main.GetComponent<ShakeManager>().Shake(0, 0, 0, 0.72f, 8);
        box_collider.enabled = true;
        isDead = false;
        yield return null;
    }
    void hitScan()
    {
        box_collider.enabled = false;
        //자신 충돌 체크 꺼줌
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position + new Vector3(box_collider.offset.x, box_collider.offset.y),
         new Vector2(box_collider.size.x * transform.localScale.x, box_collider.size.y * transform.localScale.y), 0, 1 << 8);//충돌  정보를 얻어옴
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].CompareTag("Bullet") && hit[i].GetComponent<Bullet>().fireID != pv.viewID)
                {

                    if (pv.isMine)
                    {
                        //hit[i].GetComponent<Bullet>().PV.RPC("LocalDestroy", PhotonTargets.All, true);
                        hit[i].GetComponent<Bullet>().LocalDestroy(true);
                        player_HP -= hit[i].GetComponent<Bullet>().bullet_damage;
                    }
                    else
                    {
                        hit[i].GetComponent<Bullet>().LocalDestroy(true);
                    }
                }
            }
        }
        box_collider.enabled = true;
    }
}
