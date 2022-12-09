using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMenuCanvasInitialisation : MonoBehaviour //MMenu shorthand for Main Menu ( < NOTICE THIS COMMENT!!!)
{
    public GameObject FadeScreen;
    public GameObject Menu;
    public GameObject Settings;
    public GameObject Credits;

    // Start is called before the first frame update
    void Start()
    {
        //make sure everything is working
        Cursor.visible = true;
        FadeScreen.SetActive(true);
        //Fade Screen will be overtop of Menu. Fade screen transitions from RGBA(0,0,0,255) to RGBA(0,0,0,0) (AKA transparent) in 0.45sec
        //Fade Screen is never actually disabled, it's inactive prior to running to make editing of other elements easier
        //WARNING! Changing FadeScreen's position in the canvas hierarchy WILL affect what is shown!
        Menu.SetActive(true);
        Settings.SetActive(false);
        Credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
