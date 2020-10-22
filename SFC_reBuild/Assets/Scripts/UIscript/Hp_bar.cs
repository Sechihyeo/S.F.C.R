using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hp_bar : MonoBehaviour
{
    Image myImage;
    public player_State player;
    void Start()
    {
        PoolingManager.Instance.hpbar = this;
        gameObject.SetActive(false);
        //if(PoolingManager.Instance.TruePlayer!=null)
        //player=PoolingManager.Instance.TruePlayer.GetComponent<player_State>();
        //else
        //    Debug.Log("PoolingManager.Instance.TruePlayer 없음!");
        //myImage = GetComponent<Image>();
        //if (player == null)
        //    Debug.Log("플레이어 없음!");
        //else
        //    Debug.Log("플레이어 있음!");
        //if (myImage == null)
        //    Debug.Log("이미지 없음!");
        //else
        //    Debug.Log("이미지 있음!");
        //myImage.fillAmount = 0.5f;
    }
    public void Init()
    {
        myImage = GetComponent<Image>();
    }
    void Update()
    {
        if (!player.isDead)
            myImage.fillAmount += ((player.player_HP / player.Max_HP) - myImage.fillAmount) / 10;
        else
            myImage.fillAmount=0;
    }
}
