using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AsyncOperation nextScene;
    // Start is called before the first frame update
    void Start()
    {
        nextScene = SceneManager.LoadSceneAsync("SampleScene");
        nextScene.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) nextScene.allowSceneActivation = true;
    }
}
