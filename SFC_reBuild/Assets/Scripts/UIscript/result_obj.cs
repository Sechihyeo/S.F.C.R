using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class result_obj : MonoBehaviour
{
    public Sprite[] resultTextImg;
    int myScore,otScore;
    public Text result;
    public Image resultImg;
    // Start is called before the first frame update
    void Start()
    {
        myScore = GameManager.Instance.myScore;
        otScore = GameManager.Instance.otScore;
        if(myScore>otScore)//win
        {
            resultImg.sprite = resultTextImg[0];
        }
        else if(myScore<otScore)//defeat
        {
            resultImg.sprite = resultTextImg[1];
        }
        else if(myScore==otScore)//draw
        {
            resultImg.sprite = resultTextImg[2];
        }
        result.text = "결과\n"+myScore+":"+otScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
