using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyToStart : MonoBehaviour
{
    public SceneControl changeScene;

    public GameObject background;
    public GameObject gameArt;
    public GameObject fadeScreen;

    private void Start()
    {
        background.SetActive(true);
        gameArt.SetActive(true);
        fadeScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            changeScene.loadScene(1);
        }
    }
}
