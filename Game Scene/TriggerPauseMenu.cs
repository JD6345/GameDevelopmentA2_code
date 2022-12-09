using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPauseMenu : MonoBehaviour
{
    //an error was made when structuring the code behind the pausing of the game, which was only realised after the problem was "resolved".
    //some residual formatting errors are unlikely yet possible as a direct result (such as unused scripts and strange naming conventions).
    //Only applies to the code relating to the pause menu UI

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject activeUI;

    [SerializeField]
    GameObject gameOverMenu;

    [SerializeField]
    GameObject settingsMenu; //this will only be used to make sure settings doesn't show upon opening the pause menu in case it was unintentionally left active

    [SerializeField]
    GameObject fadeScreen;

    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        fadeScreen.SetActive(true);
        activeUI.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //toggle pause with Escape
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverMenu.activeSelf) //if Esc is pushed AND player is NOT dead
        {
            pauseUnpause();
        }

        //show/hide pause menu
        if(pauseMenu.activeSelf != paused) //if the value of paused bool has been changed. explanation; checks if the pause menu is showing/hidden, then checks if 'paused' is the opposite of that (e.g. false != true)
        {
            pauseMenu.SetActive(paused); //Activates/deactivates pause menu depending on bool value 
            activeUI.SetActive(!paused); //Activates/deactivates pause menu depending on OPPOSITE of bool value
        }

        //show/hide cursor (Deprecated)
        /**Cursor.visible = paused; //if game is paused, show cursor. Else, hide cursor.
        Cursor.visible = gameOverMenu.activeSelf; //if player is dead, show cursor. Else, hide cursor.*/
    }

    public void pauseUnpause()
    {
        paused = !paused; //flips the value of the boolean
        Time.timeScale = paused ? 0 : 1; //if game was paused, revert game speed to normal. Else, set game speed to be zero AKA paused
    }


}
