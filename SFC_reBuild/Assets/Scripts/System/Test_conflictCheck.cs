using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_conflictCheck : MonoBehaviour
{
    public float Hp=100;
    void collisionCheck()
    {
        BoxCollider2D box_collider = transform.GetComponent<BoxCollider2D>();//컴포넌트를 얻어옴
        box_collider.enabled = false;//자신 충돌 체크 꺼줌
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position + new Vector3(box_collider.offset.x, box_collider.offset.y),
         new Vector2(box_collider.size.x * transform.localScale.x, box_collider.size.y * transform.localScale.y), 0, 1 << 8);//충돌  정보를 얻어옴
        if (hit != null)
        {
            foreach (Collider2D i in hit)
            {
                if (i.tag == "Bullet"&&i.GetComponent<Bullet>()!=null)
                {
                   if(Hp>0)
                   Hp-=i.GetComponent<Bullet>().bullet_damage; 
                   else
                   Hp=0;
                   i.GetComponent<Bullet>().LocalDestroy(true);
                }
            }
        }
        box_collider.enabled = true;//다시 자신의 충돌 체크를 켜줌		
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Die()
    {
        Camera.main.GetComponent<ShakeManager>().Shake(0,0,15,1,10);
        for(int i=0;i<5;i++)
        {
        GameObject tempOb = PoolingManager.Instance.ObjectResume();
        tempOb.GetComponent<GunOrbit>().Init(false,transform.position-new Vector3(0,0,1)+new Vector3(Random.Range(-0.7f,0.7f),Random.Range(-0.7f,0.7f)),new Vector3(0.65f*4,0.65f*4));
        tempOb.GetComponent<GunOrbit>().sizeReduction=0.08f;
        tempOb.GetComponent<SpriteRenderer>().color = new Color(255/255f, 51/255f, 21/255f);
  
        tempOb.GetComponent<SpriteRenderer>().renderingLayerMask=1;
        GameObject tempObshadow = PoolingManager.Instance.ObjectResume();
        tempObshadow.GetComponent<GunOrbit>().Init(false,tempOb.transform.position+ new Vector3(0, 0, 0.1f),tempOb.transform.localScale+new Vector3(0.3f,0.3f, 0));
        tempObshadow.gameObject.GetComponent<SpriteRenderer>().color = new Color(0/255f,0/255f,00/255f);
        tempObshadow.GetComponent<GunOrbit>().sizeReduction=0.08f;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(Hp<=0)
        Die();
        collisionCheck();
        Vector3 tempPo=transform.position;
        tempPo.z=transform.position.y;
        transform.position=tempPo;
    }
}
