using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance;

    LinkedList<GameObject> PoolList = new LinkedList<GameObject>();
    LinkedList<GameObject> ActiveList = new LinkedList<GameObject>();
    [SerializeField] public Color Graysmoke;
    public Player_Fire Player;
    public Hashtable otherPlayers=new Hashtable();
    //public List<Player_Fire> otherPlayers=new List<Player_Fire>();
    public player_Move TruePlayer;
    public GameObject BulletOrbit;
    public int Startamount = 100;
    public Text Player_magazine;
    public Hp_bar hpbar;
    [SerializeField] public Image Curser;
    public bool isOPner=false;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    ///<summary>오브젝트 해제</summary> 
    public void ObjectRelease(GameObject target)
    {
        target.transform.localScale = new Vector3(target.GetComponent<GunOrbit>().orix, target.GetComponent<GunOrbit>().orix);
        target.GetComponent<GunOrbit>().myColor = Graysmoke;
        target.GetComponent<GunOrbit>().targetFigure = 0;
        ActiveList.Remove(target);
        target.SetActive(false);
        PoolList.AddLast(target);
    }
    ///<summary>오브젝트 추가 생성</summary> 
    void MakeObject(int amount)
    {
        GameObject inst = null;
        for (int i = 0; i < amount; i++)
        {
            inst = Instantiate(BulletOrbit);
            inst.transform.parent = this.transform;
            inst.SetActive(false);
            PoolList.AddLast(inst);
        }
    }
    ///<summary>활성화 리스트에서 가장 오래된 오브젝트를 가져옴</summary> 
    void stealObject(int amount)
    {
        GameObject inst = null;
        for (int i = 0; i < amount; i++)
        {
            inst = ActiveList.First.Value;
            PoolList.AddLast(inst);
            ActiveList.Remove(inst);
        }
    }
    ///<summary>오브젝트 반환</summary> 
    public GameObject ObjectResume()
    {
        if (PoolList.Count == 0)
        {
            MakeObject(100);
        }
        GameObject inst = PoolList.First.Value;
        ActiveList.AddLast(inst);
        PoolList.RemoveFirst();
        inst.SetActive(true);
        return inst;
    }
    void Start()
    {
        MakeObject(Startamount);
    }
    public void PlayerInstantiateAfterInit()
    {
        hpbar.player = TruePlayer.GetComponent<player_State>();
        hpbar.gameObject.SetActive(true);
        hpbar.Init();
    }
    void Update()
    {
        if (Player != null)
        {
            if (!Player.nowReload)
            {
                Player_magazine.text = Player.Cur_magazine + "/" + Player.Max_magazine;
                Player_magazine.color = new Color(1, 1, 1, 1);
                Curser.fillAmount += (1 - Curser.fillAmount) / 10;
            }
            else
            {
                Player_magazine.text = "Reload..." + Player.tempreloadTime;
                Player_magazine.color = new Color(1, 1, 1, 0.5f);
                Curser.fillAmount += (((Player.reloadTime - Player.tempreloadTime) / Player.reloadTime) - Curser.fillAmount) / 10;
            }
        }//Debug.Log("Chiled Count : "+transform.childCount);
    }
}
