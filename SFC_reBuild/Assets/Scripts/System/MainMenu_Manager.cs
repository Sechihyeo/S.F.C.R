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
    AudioClip pressSfx2;
    [SerializeField]
    float sfxvolume=1;
    [SerializeField]
    Image fadeImg,fadelogo;
    bool isFadeIn=true;
    float fadeImgAlpha=0;
    // Start is called before the first frame update
    void Start()
    {
        buttonSoundSource = gameObject.AddComponent<AudioSource>();
        buttonSoundSource.clip=pressSfx;
        buttonSoundSource.volume=sfxvolume;
        fadeImg.color=new Color(1,1,1,1);
        fadeImgAlpha=fadeImg.color.a;
        fadelogo.color=new Color(1,1,1,0);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(isFadeIn)
        // fadeImgAlpha+=(0-fadeImgAlpha)/10;
        // else
        // //fadeImgAlpha+=(1-fadeImgAlpha)/10;

        // fadeImg.color=new Color(1,1,1,fadeImgAlpha);
        
    }
    
    void press_buttonSfx()
    {
        buttonSoundSource.clip=pressSfx;
        buttonSoundSource.Play();
    }
    void press_buttonSfx2()
    {
        buttonSoundSource.clip=pressSfx2;
        buttonSoundSource.Play();
    }
    public void TestButton()
    {
        press_buttonSfx();
        StartCoroutine(FadeOut("Ingame"));
        //LoadingSceneManager.LoadScene("Ingame");
    }
    public void SetButton()
    {
        press_buttonSfx2();
 
    }
    IEnumerator FadeIn()
    {      
        float fade = 1;
        while (fade >= 0)
        {
            fade-=0.01f;
            fadeImg.color = new Color(1, 1, 1, fade);
           // Debug.Log("페이드 아웃중"+fade);
            yield return null;
        }
        Debug.Log(fadeImg.color.a);
    }
    IEnumerator FadeOut(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        float fade = 0;
        while (fade <= 1)
        {
            fade+=0.01f;
            fadeImg.color = new Color(0, 0, 0, fade);
            fadelogo.color=new Color(1,1,1,fade*0.3f);
           // Debug.Log("페이드 아웃중"+fade);
            yield return null;

        }
        yield return new WaitForSeconds(1f);
        LoadingSceneManager.LoadScene(sceneName);
    }
}
