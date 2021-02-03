using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inGameUI_manager : MonoBehaviour
{
    public Image HitImg;
    public Image WaringImg;
    public Text WaringTxt;
    static public inGameUI_manager Instanse;
    float per;
    public Text myScore;
    public Text otScore;
    void Awake()
    {
        if(Instanse==null)
        Instanse = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        per = (20*(PlayerPrefs.GetFloat("FPS"))/144f);
        HitImg.gameObject.SetActive(true);
        WaringImg.gameObject.SetActive(true);
        HitImg.color = new Color(1, 1, 1, 0);
        WaringImg.color = new Color(1, 1, 1, 0);
        WaringTxt.color = new Color(1,1,1,0);
        Debug.Log("now Per: "+per);
    }

    // Update is called once per frame
    void Update()
    {
        HitImg.color +=(new Color(1, 1, 1, 0)-HitImg.color)/per;
        WaringImg.color +=(new Color(1, 1, 1, 0)-WaringImg.color)/(per*2);
        myScore.text=GameManager.Instance.myScore.ToString();
        otScore.text=GameManager.Instance.otScore.ToString();
    }
    public void setHit()
    {
        HitImg.color = new Color(1, 1, 1, 1);
    }
    IEnumerator Text_out()
    {
        WaringTxt.color = new Color(1,1,1,1);
        yield return new WaitForSeconds(1);
        WaringTxt.color = new Color(1,1,1,0);
        yield return null;
    }
    public void setWar()
    {
        WaringImg.color = new Color(1, 1, 1, 0.7f);
        StartCoroutine(Text_out());
    }
}
