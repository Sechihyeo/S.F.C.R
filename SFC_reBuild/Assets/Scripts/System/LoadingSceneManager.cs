using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Math;
public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField]
    Image progressBar;


    public Text percent;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    string nextSceneName;
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;



        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                    op.allowSceneActivation = true;
            }
            else
            {
                progressBar.fillAmount = op.progress/0.9f;
            }
            percent.text= (progressBar.fillAmount*100).ToString("F1")+" %";
        }
    }
}