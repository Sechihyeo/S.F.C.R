using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu_Manager : MonoBehaviour
{
    AudioSource buttonSoundSource;
    [SerializeField]
    AudioClip pressSfx;
    [SerializeField]
    float sfxvolume=1;
    [SerializeField]
    Image fadeImg;
    bool isFadeIn=true;
    float fadeImgAlpha=0;
    // Start is called before the first frame update
    void Start()
    {
        buttonSoundSource = gameObject.AddComponent<AudioSource>();
        buttonSoundSource.clip=pressSfx;
        buttonSoundSource.volume=sfxvolume;
        fadeImgAlpha=fadeImg.color.a;
        //fadeImg.color=new Color(1,1,1,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isFadeIn)
        fadeImgAlpha+=(0-fadeImgAlpha)/10;
        else
        fadeImgAlpha+=(1-fadeImgAlpha)/10;

        fadeImg.color=new Color(1,1,1,fadeImgAlpha);
    }
    
    void press_buttonSfx()
    {
        buttonSoundSource.Play();
    }
    public void TestButton()
    {
        press_buttonSfx();
    }
}
