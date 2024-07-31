using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoadingManager : MonoBehaviour
{

    public int sceneNumber = 2;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionNextScene(sceneNumber));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TransitionNextScene(int _num)
    {
        AsyncOperation _ao = SceneManager.LoadSceneAsync(_num);
        _ao.allowSceneActivation = false;

        while (!_ao.isDone)
        {
            loadingBar.value = _ao.progress;
            loadingText.text = (_ao.progress * 100f).ToString() + " %";

            if (_ao.progress >= 0.9f)
            {
                _ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
