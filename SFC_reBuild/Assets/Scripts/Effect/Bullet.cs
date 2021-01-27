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
     public float size_y=0;
    float nextfireQ;
    public float firerateQ = 0.0185f;
    public GameObject BulletOrbitObject;
    public float outline=0.6f,outline_targetFigure=0.2f;
    public float DestroyTime=1.5f;
    public float OrbitSize=0.65f;
    public float bullet_damage=10;
    public int fireID;
    Vector3 mPosition;
    Vector3 oPosition;
    Vector3 target;
    public float rotateDegree;
    public float toDegree;
    Vector3 startpos;
    void Start()
    {
        size=transform.localScale.x;
        size_y=transform.localScale.y;
        PV=GetComponent<PhotonView>();
        startpos=transform.position;
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
    void Update()
    {
        if(nextfireQ<Time.time)
        BulletOrbit();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(toVector*Time.deltaTime*speed,Space.World);
        //speed+=(0-speed)/160;
        //size-=size/190;
        transform.localScale=new Vector3(size,size_y);
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
        if(PoolingManager.Instance.TruePlayer.pv.viewID==fireID)
        Camera.main.GetComponent<ShakeManager>().Shake(0,0,0,1.1f,5);
        GameObject tempOb = PoolingManager.Instance.ObjectResume();
        tempOb.GetComponent<GunOrbit>().Init(false,transform.position-new Vector3(0,0,1),new Vector3(OrbitSize*4,OrbitSize*4));
        tempOb.GetComponent<GunOrbit>().sizeReduction=0.06f/((float)Application.targetFrameRate/144);
        tempOb.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        tempOb.GetComponent<GunOrbit>().myColor = new Color(255/255f,24/255f,45/255f);
        tempOb.GetComponent<GunOrbit>().colored=true;
        tempOb.GetComponent<SpriteRenderer>().renderingLayerMask=1;
        GameObject tempObshadow = PoolingManager.Instance.ObjectResume();
        tempObshadow.GetComponent<GunOrbit>().Init(false,tempOb.transform.position+ new Vector3(0, 0, 0.1f),tempOb.transform.localScale+new Vector3(outline,outline, 0));
        tempObshadow.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        tempObshadow.GetComponent<GunOrbit>().targetFigure=outline_targetFigure;
        tempObshadow.GetComponent<GunOrbit>().sizeReduction=tempOb.GetComponent<GunOrbit>().sizeReduction;
    }
    [PunRPC]
    public void setToVector(Vector3 pVector,float obSize,float pspeed,float pdTime,int pfireID)
    {
        this.toVector=pVector;
        OrbitSize=obSize;
        speed=pspeed;
        DestroyTime=pdTime;
        fireID=pfireID;
        //transform.localRotation = Quaternion.Euler(0,0,PointDirection(new Vector2(transform.localPosition.x,transform.localPosition.y),new Vector2(toVector.x,toVector.y)));
        //개짓거리
        // mPosition = toVector;
        // oPosition = transform.position;
        // target = mPosition - oPosition;
        // rotateDegree = -1 * Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0f, 0f, PointDirection(startpos,toVector));
        
        return;
    }
    public float PointDirection(Vector2 pos1, Vector2 pos2)
    {
        Vector2 pos = pos2 - pos1;
        float angle = (float)Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }
}
