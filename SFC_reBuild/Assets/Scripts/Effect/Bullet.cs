using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon;
public class Bullet : PunBehaviour
{
    public PhotonView PV;
    public Vector3 toVector;
    public float speed =3;
    public float size=0;
    float nextfireQ;
    public float firerateQ = 0.0185f;
    public GameObject BulletOrbitObject;
    public float outline=0.6f,outline_targetFigure=0.2f;
    public float DestroyTime=1.5f;
    public float OrbitSize=0.65f;
    public float bullet_damage=10;
    public int fireID;
    void Start()
    {
        size=transform.localScale.x;
        PV=GetComponent<PhotonView>();
    }
    void BulletOrbit()
    {
        nextfireQ=Time.time+firerateQ;
        
        GameObject tempOb = PoolingManager.Instance.ObjectResume();
        tempOb.GetComponent<GunOrbit>().oriColor=PoolingManager.Instance.Player.naturalColor;
        tempOb.GetComponent<GunOrbit>().Init(true,transform.position+new Vector3(Random.Range(-0.15f,0.15f),Random.Range(-0.15f,0.15f),0.1f),new Vector3(OrbitSize,OrbitSize));
        GameObject tempObshadow = PoolingManager.Instance.ObjectResume();
        tempObshadow.GetComponent<GunOrbit>().Init(false,tempOb.transform.position+new Vector3(0, 0,0.1f),tempObshadow.transform.localScale += new Vector3(outline,outline, 0));
        tempObshadow.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        tempObshadow.GetComponent<GunOrbit>().targetFigure=outline_targetFigure;

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(toVector*Time.deltaTime*speed);
        //speed+=(0-speed)/160;
        //size-=size/190;
        transform.localScale=new Vector3(size,size);
        if(nextfireQ<Time.time)
        BulletOrbit();
        LocalDestroy(false);
    }
    [PunRPC]
    public void LocalDestroy(bool isPaticle)
    {
        if(isPaticle)
        {
            Destroy_particle();
            Destroy(gameObject);    
        }
        Destroy(gameObject,DestroyTime);
    }
    public void Destroy_particle()
    {   
        Camera.main.GetComponent<ShakeManager>().Shake(0,0,0,1.1f,5);
        GameObject tempOb = PoolingManager.Instance.ObjectResume();
        tempOb.GetComponent<GunOrbit>().Init(false,transform.position-new Vector3(0,0,1),new Vector3(OrbitSize*4,OrbitSize*4));
        tempOb.GetComponent<GunOrbit>().sizeReduction=0.06f;
        tempOb.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        tempOb.GetComponent<GunOrbit>().myColor = new Color(255/255f,24/255f,45/255f);
        tempOb.GetComponent<GunOrbit>().colored=true;
        tempOb.GetComponent<SpriteRenderer>().renderingLayerMask=1;
        GameObject tempObshadow = PoolingManager.Instance.ObjectResume();
        tempObshadow.GetComponent<GunOrbit>().Init(false,tempOb.transform.position+ new Vector3(0, 0, 0.1f),tempOb.transform.localScale+new Vector3(outline,outline, 0));
        tempObshadow.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        tempObshadow.GetComponent<GunOrbit>().targetFigure=outline_targetFigure;
        tempObshadow.GetComponent<GunOrbit>().sizeReduction=0.06f;
    }
    [PunRPC]
    public void setToVector(Vector3 pVector,float obSize,float pspeed,float pdTime,int pfireID)
    {
        this.toVector=pVector;
        OrbitSize=obSize;
        speed=pspeed;
        DestroyTime=pdTime;
        fireID=pfireID;
        return;
    }
}
