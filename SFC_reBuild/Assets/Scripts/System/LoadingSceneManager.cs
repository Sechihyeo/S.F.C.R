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
    float amount=0;
    float smooth=0;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    void Update()
    {
        smooth+=(amount-smooth)/10;
        progressBar.fillAmount += ((amount/10)-progressBar.fillAmount)/10;
        if(smooth>10)smooth=10;
        percent.text=Truncate((smooth*100))/10+"%";
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



        while(amount<=10)
        {
            float temp=2;
            yield return new WaitForSeconds(0.1f);
            amount+=temp;
        }
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return new WaitForSeconds(0.2f);

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