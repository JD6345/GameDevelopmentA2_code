using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    public Animator fadeAnim;
    public Image fadeScreenImage;

    public GameObject FadeScreen;

    bool currentlyFading = false;

    void Start()
    {

    }

    void Update()
    {
        if(fadeScreenImage.color.a == 0 && ! currentlyFading) //if fully transparent AND NOT in a fade transition
        {
            FadeScreen.SetActive(false); //disable the fade screen to stop issue of user being unable to click
        }
    }

    public void loadScene(int sceneNum)
    {
        //0 is Launch screen
        //1 is main menu
        //2 is game
        SceneManager.LoadScene(sceneNum);
    }

    public void FadeToScene(int sceneNum)
    {
        currentlyFading = true;
        FadeScreen.SetActive(true);
        StartCoroutine(Fade());
        loadScene(sceneNum);
    }

    //to be honest, I have little idea as to what is going on here. The assessment asked for fade transitions.
    //https://www.youtube.com/watch?v=iV-igTT5yE4
    public IEnumerator Fade()
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeScreenImage.color.a == 1);
    }
}
