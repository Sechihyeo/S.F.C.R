using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon;
public class Player_Fire : PunBehaviour
{

    float nextfireQ;
    public float ShotDelay = 0.8f;
    public float Bulletspeed = 3, BulletDestroy = 1.5f;
    public GameObject bulletInst;
    [HideInInspector]
    AudioSource Fireaudio, ReloadAudio;
    public AudioClip FireSfx, ReloadSfx;
    public float sound_volume = 1;
    public Transform flamePosition;
    public GameObject gunflame;
    public int Max_magazine = 2, Cur_magazine;
    public float reloadTime = 2;
    [SerializeField] public Color naturalColor;
    float oriscale;
    public float OBsize = 0.65f;
    public bool isAuto = false;
    public bool nowReload = false;
    public float tempreloadTime;
    [HideInInspector]
    public PhotonView pv;
    public GameObject gunbody;
    protected void Start()
    {
        Fireaudio = gameObject.AddComponent<AudioSource>();
        Fireaudio.clip = FireSfx;
        Fireaudio.volume = sound_volume;
        ReloadAudio = gameObject.AddComponent<AudioSource>();
        ReloadAudio.clip = ReloadSfx;
        ReloadAudio.volume = sound_volume * 2;
        oriscale = gunbody.transform.localScale.x;
        Cur_magazine = Max_magazine;
        Debug.Log("Fire ID:"+pv.viewID);
        if (pv.isMine)
        {
            PoolingManager.Instance.Player = this;
        }
        else
        {
            PoolingManager.Instance.otherPlayers.Add(pv.viewID, this);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
         
        gunbody.transform.localEulerAngles += (new Vector3(0, 0, 0) - gunbody.transform.localEulerAngles) / 10;
        gunbody.transform.localScale += (new Vector3(oriscale, oriscale, 1) - gunbody.transform.localScale) / 10;
        if (!pv.isMine) return;

        if (isAuto)
        {
            if (Input.GetMouseButton(0) && Time.time > nextfireQ)
            {
                pv.RPC("FireBullet",PhotonTargets.All,null);
                //FireBullet();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.time > nextfireQ)
            {
                pv.RPC("FireBullet",PhotonTargets.All,null);
                //FireBullet();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && Cur_magazine < Max_magazine && !nowReload)
        {
           pv.RPC("co_Reload",PhotonTargets.All,null);
        }

    }
    protected virtual void CreateBullet()
    {
        if(!pv.isMine)
        return;

        gunbody.transform.localEulerAngles += new Vector3(0, 0, 90);

        for (int i = -3; i < 4; i++)
        {
            object[] pam = new object[5];
            pam[0] = VectorRotation(PointDirection(transform.position,
            Camera.main.ScreenToWorldPoint(Input.mousePosition)) + (5 * i) + Random.Range(-10.0f, 10.0f));
            pam[1] = OBsize;
            pam[2] = Bulletspeed;
            pam[3] = BulletDestroy;
            pam[4] = pv.viewID;
            PhotonNetwork.Instantiate("sechi_bullet", flamePosition.position, Quaternion.identity, 0)
            .GetComponent<PhotonView>().RPC("setToVector", PhotonTargets.All, pam);

        }
    }

    [PunRPC]
    protected virtual void FireBullet()
    {

        if (Cur_magazine > 0 && !nowReload)
        {
            Cur_magazine--;
            gunbody.transform.localScale += new Vector3(0, 1f);
            gunbody.transform.localEulerAngles += new Vector3(0, 0, 5);
            Instantiate(gunflame).transform.position = flamePosition.position + new Vector3(0, 0, -3);
            Camera.main.GetComponent<ShakeManager>().Shake(0, 0, 0, 0.8f, 8);
            CreateBullet();

            Fireaudio.Play();
            if (Cur_magazine == 0 && !nowReload)
                pv.RPC("co_Reload",PhotonTargets.All,null);
        }
        nextfireQ = Time.time + ShotDelay;
    }
    [PunRPC]
    void co_Reload()
    {
        StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        tempreloadTime = reloadTime;
        nowReload = true;
        ReloadAudio.Play();
        PoolingManager.Instance.Curser.fillAmount = 0;
        while (tempreloadTime - 0.1f > 0.0f)
        {
            tempreloadTime -= 0.1f;
            GameObject tempOb = PoolingManager.Instance.ObjectResume();
            tempOb.GetComponent<GunOrbit>().Init(false, flamePosition.transform.position - new Vector3(0, 0, 1) + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), new Vector3(0.65f, 0.65f));
            tempOb.GetComponent<GunOrbit>().sizeReduction = 0.0075f;
            float RandomColor = Random.Range(-20.0f, 80.0f);
            tempOb.GetComponent<SpriteRenderer>().color = new Color((100f + RandomColor) / 255f, (101f + RandomColor) / 255f, (117f + RandomColor) / 255f);
            //tempOb.GetComponent<GunOrbit>().colored = false;
            tempOb.GetComponent<GunOrbit>().targetFigure = 0.3f;
            tempOb.GetComponent<SpriteRenderer>().renderingLayerMask = 1;

            GameObject tempObshadow = PoolingManager.Instance.ObjectResume();
            tempObshadow.GetComponent<GunOrbit>().Init(false, tempOb.transform.position + new Vector3(0, 0, 0.1f), tempOb.transform.localScale + new Vector3(0.3f, 0.3f, 0));
            tempObshadow.gameObject.GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 0 / 255f, 00 / 255f);
            tempObshadow.GetComponent<GunOrbit>().sizeReduction = 0.0075f;
            tempObshadow.GetComponent<GunOrbit>().targetFigure = 0.3f;

            yield return new WaitForSeconds(0.1f);
        }
        nowReload = false;
        Cur_magazine = Max_magazine;
    }
    public float PointDirection(Vector2 pos1, Vector2 pos2)
    {
        Vector2 pos = pos2 - pos1;
        float angle = (float)Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }

    public Vector3 VectorRotation(float _angle)
    {

        _angle -= 90;
        _angle *= -1;
        if (_angle < 0f)
            _angle += 360f;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), Mathf.Cos(_angle * Mathf.Deg2Rad), 0);
    }
}
