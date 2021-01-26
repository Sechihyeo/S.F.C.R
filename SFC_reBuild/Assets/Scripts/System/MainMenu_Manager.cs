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
    public int setPlayerID = 0;
    string[] charName = new string[6] { "세치", "끼룩", "M-얌미&G", "HICES", "아귀", "개까치" };
    string[] charExName =
    new string[6]{
        "강력한 순간화력을 가진 샷건으로\n근거리를 압도합니다.",
        "돌격소총은 안정적인 중거리 화력을 보장합니다.",
        "높은 정확도와 절륜한 공격력을 자랑하는 리볼버로\n게임의 판도를 지배합니다.",
        "전장의 꽃, 저격소총입니다.",
        "중장거리의 스페셜 리스트인 지정사수 소총은\n신속하고 정확한 반자동 사격을 자랑합니다.",
        "제작자 입니다. 딱히 플레이는 못합니다."
        };
    string[] charFlText = new string[6]{
    "\"???:소리만 지르던게 그렇게 셀 줄은 몰랐지\"",
    "\"하얀 악마라고 불립니다\"",
    "\"치명적인 귀여움(리볼버, 물리)\"",
    "\"자꾸 죽은 사람 취급당한다는군요\"",
    "\"여긴 물 밖인데.\"",
    "\"2월 15일 입대합니다\n게임이 좋았ㄷ다면 애니에 특별 출현좀 헤헤\""};
    [SerializeField]
    Text charNameText;
    [SerializeField]
    Text charExNameText;
    [SerializeField]
    Text charFlText_Text;
    [SerializeField]
    Text choiced_Text;
    [SerializeField]
    Text fps_text;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instant == null)
            Instant = this;
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVol"))
            PlayerPrefs.SetFloat("musicVol", 1);
        if (!PlayerPrefs.HasKey("sfXVol"))
            PlayerPrefs.SetFloat("sfXVol", 1);
        if (!PlayerPrefs.HasKey("masterVol"))
            PlayerPrefs.SetFloat("masterVol", 1);
        if (!PlayerPrefs.HasKey("shakeVol"))
            PlayerPrefs.SetInt("shakeVol", 1);
        if (!PlayerPrefs.HasKey("Player_ID"))
            PlayerPrefs.SetInt("Player_ID", 0);
        if (!PlayerPrefs.HasKey("FPS"))
            PlayerPrefs.SetFloat("FPS", 60);
        setPlayerID = PlayerPrefs.GetInt("Player_ID");
        buttonSoundSource = gameObject.AddComponent<AudioSource>();
        buttonSoundSource.clip = pressSfx;
        buttonSoundSource.volume = PlayerPrefs.GetFloat("sfXVol") * PlayerPrefs.GetFloat("masterVol");
        musicSS = gameObject.AddComponent<AudioSource>();
        musicSS.clip = pressSfxmusic;
        musicSS.volume = PlayerPrefs.GetFloat("musicVol") * PlayerPrefs.GetFloat("masterVol");
        musicSS.loop = true;
        musicSS.Play();
        fadeImg.color = new Color(1, 1, 1, 1);
        fadeImgAlpha = fadeImg.color.a;
        fadelogo.color = new Color(1, 1, 1, 0);
        StartCoroutine(FadeIn());
        setObj.SetActive(false);
        cerObj.SetActive(false);
        charNameText.text = charName[setPlayerID];
        charExNameText.text = charExName[setPlayerID];
        charFlText_Text.text = charFlText[setPlayerID];
        if (PlayerPrefs.GetInt("Player_ID") == setPlayerID)
            choiced_Text.text = "선택됨!";
        else
            choiced_Text.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        buttonSoundSource.volume = PlayerPrefs.GetFloat("sfXVol") * PlayerPrefs.GetFloat("masterVol");
        musicSS.volume = PlayerPrefs.GetFloat("musicVol") * PlayerPrefs.GetFloat("masterVol");
        // if(isFadeIn)
        // fadeImgAlpha+=(0-fadeImgAlpha)/10;
        // else
        // //fadeImgAlpha+=(1-fadeImgAlpha)/10;

        // fadeImg.color=new Color(1,1,1,fadeImgAlpha);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            char_turn(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            char_turn(true);
        }
        
        fps_text.text=((int)PlayerPrefs.GetFloat("FPS")).ToString();
        //Debug.Log(PlayerPrefs.GetInt("FPS"));
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
    public void char_turn(bool isUpper = true)
    {
        if (isUpper)
        {
            if (setPlayerID <= 4)
            {
                setPlayerID++;
            }
        }
        else
        if (setPlayerID >= 1)
        {
            setPlayerID--;
        }
        charNameText.text = charName[setPlayerID];
        charExNameText.text = charExName[setPlayerID];
        charFlText_Text.text = charFlText[setPlayerID];
        if (PlayerPrefs.GetInt("Player_ID") == setPlayerID)
            choiced_Text.text = "선택됨!";
        else
            choiced_Text.text = "";
    }
    public void choice_button()
    {
        if (setPlayerID != 5)
        {
            PlayerPrefs.SetInt("Player_ID", setPlayerID);
            choiced_Text.text = "선택됨!";
        }
    }
}
