using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu_Manager : MonoBehaviour
{
    public static MainMenu_Manager Instant;
    AudioSource buttonSoundSource;
    AudioSource musicSS;
    [SerializeField]
    AudioClip pressSfx;
    [SerializeField]
    AudioClip pressSfx2;
    [SerializeField]
    AudioClip pressSfxmusic;
    [SerializeField]
    float sfxvolume = 1;
    [SerializeField]
    Image fadeImg, fadelogo;
    bool isFadeIn = true;
    float fadeImgAlpha = 0;
    public int menuState = 0;
     [SerializeField]
     GameObject setObj;
      [SerializeField]
     GameObject cerObj;
     bool isSet;
     bool isCer;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instant == null)
            Instant = this;
    }
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVol"))
        PlayerPrefs.SetFloat("musicVol",1);
        if(!PlayerPrefs.HasKey("sfXVol"))
        PlayerPrefs.SetFloat("sfXVol",1);
        if(!PlayerPrefs.HasKey("masterVol"))
        PlayerPrefs.SetFloat("masterVol",1);

        buttonSoundSource = gameObject.AddComponent<AudioSource>();
        buttonSoundSource.clip = pressSfx;
        buttonSoundSource.volume = PlayerPrefs.GetFloat("sfXVol")*PlayerPrefs.GetFloat("masterVol");
        musicSS = gameObject.AddComponent<AudioSource>();
        musicSS.clip = pressSfxmusic;
        musicSS.volume = PlayerPrefs.GetFloat("musicVol")*PlayerPrefs.GetFloat("masterVol");
        musicSS.Play();
        fadeImg.color = new Color(1, 1, 1, 1);
        fadeImgAlpha = fadeImg.color.a;
        fadelogo.color = new Color(1, 1, 1, 0);
        StartCoroutine(FadeIn());
        setObj.SetActive(false);
        cerObj.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        buttonSoundSource.volume = PlayerPrefs.GetFloat("sfXVol")*PlayerPrefs.GetFloat("masterVol");
        musicSS.volume = PlayerPrefs.GetFloat("musicVol")*PlayerPrefs.GetFloat("masterVol");
        // if(isFadeIn)
        // fadeImgAlpha+=(0-fadeImgAlpha)/10;
        // else
        // //fadeImgAlpha+=(1-fadeImgAlpha)/10;

        // fadeImg.color=new Color(1,1,1,fadeImgAlpha);

    }

    public void press_buttonSfx()
    {
        buttonSoundSource.clip = pressSfx;
        buttonSoundSource.Play();
    }
    public void press_buttonSfx2()
    {
        buttonSoundSource.clip = pressSfx2;
        buttonSoundSource.Play();
    }
    public void DefultButton_sound()
    {
        press_buttonSfx();
        //StartCoroutine(FadeOut("Ingame"));
        //LoadingSceneManager.LoadScene("Ingame");
    }
    public void CharButton()
    {
        menuState = 1;
    }
    public void SetButton()
    {
        press_buttonSfx2();

    }
    public void backButton(bool justBack = false)
    {
        if (!justBack)
        {
            if (menuState - 1 >= 0)
                menuState--;
        }
        else
            menuState = 0;
    }
    public void setObj_act()
    {
        setObj.SetActive(!setObj.GetActive());
    }
    public void cerObj_act()
    {
        cerObj.SetActive(!cerObj.GetActive());
    }
    IEnumerator FadeIn()
    {
        float fade = 1;
        while (fade >= 0)
        {
            fade -= 0.01f;
            fadeImg.color = new Color(1, 1, 1, fade);
            // Debug.Log("페이드 아웃중"+fade);
            yield return null;
        }
    }
    public IEnumerator FadeOut(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        float fade = 0;
        while (fade <= 1)
        {
            fade += 0.01f;
            fadeImg.color = new Color(0, 0, 0, fade);
            fadelogo.color = new Color(1, 1, 1, fade * 0.3f);
            // Debug.Log("페이드 아웃중"+fade);
            yield return null;

        }
        yield return new WaitForSeconds(1f);
        LoadingSceneManager.LoadScene(sceneName);
    }
}
