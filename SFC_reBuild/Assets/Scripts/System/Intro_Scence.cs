using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Intro_Scence : MonoBehaviour
{
    [SerializeField]
    Image FadeOutImg;
    // Start is called before the first frame update
    void Start()
    {
        FadeOutImg.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeOut()
    {
        float fade = 0;
        while (fade <= 1)
        {
            fade+=0.01f;
            FadeOutImg.color = new Color(1, 1, 1, fade);
           // Debug.Log("페이드 아웃중"+fade);
            yield return null;

        }
        yield return new WaitForSeconds(0.5f);
        LoadingSceneManager.LoadScene("Main_menu");
    }
}
