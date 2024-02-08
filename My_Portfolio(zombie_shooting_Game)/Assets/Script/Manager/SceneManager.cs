using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject screen;
    [SerializeField] Slider progressBar;

    public int sceneIndex = 0;

    public void NextScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int index)
    {
        screen.SetActive(true);

        AsyncOperation operation = UnitySceneManager.LoadSceneAsync(index);

        progressBar.value = 0f;

        // allowSceneActivation : ¾À ÀÌµ¿ Á¦¾î

        operation.allowSceneActivation = false;
        
        float progress = 0;

        while (!operation.isDone) 
        {
            progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);

            progressBar.value = progress;

            // 0.9f¿¡¼­ ¾À ·ÎµùÀ» ³¡³À´Ï´Ù.
            if(progress >= 0.9f)
            {
                progressBar.value = 1f;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
