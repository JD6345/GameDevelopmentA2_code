using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    //You may notice that the casing of method names are not consistent (eg ResetHighscore vs pauseUnpause). At some point in time I unknowingly reverted to how I was previously taught. Not a big deal but I thought I'd point it out for documentation's sake.

    public ParticleSystem dust;

    Rigidbody2D myChar;

    [SerializeField]
    Transform gameCamera; //cam movement

    [SerializeField]
    Camera cam; //unity docs Camera.WorldToViewportPoint

    [SerializeField]
    GameObject gameOverMenu;

    [SerializeField]
    GameObject activeUI;

    [SerializeField]
    Text score;

    float scoreNum = 0; //used for comparisons relating to score

    [SerializeField]
    Text highscore;

    float highscoreNum; //used for comparisons relating to highscore

    [SerializeField]
    float jumpHeightMult; //set to 7

    [SerializeField]
    float movementDistance; //AKA movement speed. recommended value: 10

    [SerializeField]
    float camHeightOffset; //recommended value: 0-2

    [SerializeField]
    float camKillzoneHeight; //how far offscreen in the -y direction the player must go to be considered "dead"

    [SerializeField]
    GeneratePlatforms platformSpawn;

    public SoundEffects sound; //for boing and game over sound effect method calls

    [SerializeField]
    GameObject music; //to stop music on death

    bool spawnOnePlatform = true;

    bool alive = true;

    bool backflip = false;
    int currentRotation = 0;

    bool facingRight = true;


    bool spawnEnemy = true;

    // Start is called before the first frame update
    void Start()
    {
        myChar = GetComponent<Rigidbody2D>();
        score.text = "Score: 0m"; //show starting score
        highscoreNum = PlayerPrefs.GetFloat("highscore", 0); //get highscore, or default to 0 if none
        highscore.text = "Highscore: " + highscoreNum + "m"; //show highscore
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) //self explanatory, if player is not dead
        {

            //movement input
            transform.Translate(Vector3.right * movementDistance * Input.GetAxis("Horizontal") * Time.deltaTime);

            //flip character based on direction
            //https://www.youtube.com/watch?v=ccxXxvlS4mI "wrongWayToFlip()"
            if ((Input.GetAxis("Horizontal") < 0 && facingRight) || (Input.GetAxis("Horizontal") > 0 && !facingRight))
            {
                facingRight = !facingRight;
                Vector3 mirrorVector = transform.localScale;
                mirrorVector.x *= -1;
                transform.localScale = mirrorVector;
            }

            //a little easter egg. when I was trying to make the player change direction depending on movement,
            //I accidentally referenced the wrong axis, resulting in what looked like a backflip. It put a smile on my face.
            if(backflip && (currentRotation % 360) == 350)
            {
                transform.Rotate(Vector3.right * -10);
                currentRotation = 0;
                backflip = false;
            }
            else if (backflip)
            {
                transform.Rotate(Vector3.right * -10);
                currentRotation += 10;
            }

            //moving camera with the player
            if ((transform.position.y - camHeightOffset) > gameCamera.position.y)
            {
                Vector3 camPos = gameCamera.position;
                camPos.y = (transform.position.y - camHeightOffset); //offset the camera movement to give player more breathing room
                gameCamera.position = camPos;

                if (spawnOnePlatform) //if at apex of trajectory and camera has moved up
                {
                    platformSpawn.SpawnMOREPlatforms();
                    spawnOnePlatform = false; //make sure only one platform spawns, otherwise approximately 50-60 spawn in 1 jump (tested)
                }
            }
            else if(myChar.velocity.y < 0)
            {
                spawnOnePlatform = true;
            }

            //wrapping player from one side of the screen to the other. Unity docs; Camera.WorldToViewportPoint
            Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);
            if(playerPos.x <= 0)
            {
                //https://forum.unity.com/threads/how-to-detect-screen-edge-in-unity.109583/
                Vector3 rightSide = Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0.0f, 0.0f));
                transform.position = new Vector3(rightSide.x, transform.position.y, transform.position.z);
            }
            else if(playerPos.x >= 1)
            {
                Vector3 leftSide = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
                transform.position = new Vector3(leftSide.x, transform.position.y, transform.position.z);
            }

            //updating score according to camera height
            if (myChar.velocity.y > 0 && myChar.position.y > scoreNum) //if player is bouncing vertically AND has progressed past their current score
            {
                scoreNum = Mathf.Round(myChar.position.y * 100) / 100; //update score internally, rounding score to 2dp.
                score.text = "Score: " + scoreNum + "m"; //update score visually
            }

            //update highscore
            if (scoreNum > highscoreNum) //if new personal record
            {
                highscoreNum = scoreNum; //update highscore (constant updating, see next line for reasoning)
                highscore.text = "Highscore: " + highscoreNum + "m"; //constant visual updates to highscore, for visual appeal
            }

            //trigger death
            if (transform.position.y < (gameCamera.position.y - camKillzoneHeight))
            {
                killPlayer();
            }

            //spawn some enemies every "X" score
            if (scoreNum > 1 && spawnEnemy)
            {
                if (scoreNum % 50 < 1f) //the -1 is to prevent constant spawning at 0m score, spawnOneEnemy is a boolean to stop the screen from flooding with enemies
                {
                    platformSpawn.SpawnEnemies();
                    spawnEnemy = false;
                }
                if (scoreNum % 250 < 1f)
                {
                    //spawn an additional enemy
                    platformSpawn.SpawnEnemies();
                    spawnEnemy = false;
                }
                if (scoreNum % 1250 < 1f)
                {
                    //spawn an additional enemy (3 enemies total)
                    platformSpawn.SpawnEnemies();
                    spawnEnemy = false;
                }
                if (scoreNum % 2500 < 1f)
                {
                    //spawn an additional enemy (4 enemies total)
                    platformSpawn.SpawnEnemies();
                    spawnEnemy = false;
                }
            }
            else if ((scoreNum + 10) % 50 < 1f)
            {
                spawnEnemy = true;
            }

            /**
             * Alternative movement code:
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * movementDistance * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * movementDistance * Time.deltaTime);
            }*/

        }
    }
    
    public void ResetHighscore() //polymorphism. Used by Reset Highscore button on pause menu
    {
        highscoreNum = 0;
        PlayerPrefs.SetFloat("highscore", highscoreNum); //set highscore to 0
        highscore.text = "Highscore: " + highscoreNum + "m";
    }

    private void OnCollisionEnter2D(Collision2D collision) //bounce player
    {
        if (myChar.velocity.y <= 0 && alive) //if player is falling AND player is alive
        {
            myChar.velocity = Vector2.up * jumpHeightMult; //bounce
            CreateDust(); //add particles on bounce

            
            if (Input.GetKey(KeyCode.Space) && ! backflip)
            {
                backflip = true;
                
            }
        }
        if (collision.gameObject.CompareTag("enemykillbox")) //check if what the player bounced on was an enemy
        {
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>()); //Disable the enemy's bounce hitbox after bouncing off of it *********UNTESTED WITH MULTIPLE ENEMIES*********
            //collision.gameObject.GetComponent<EnemyScript>().killEnemy();
        }
    }

    /**Referenced from https://www.youtube.com/watch?v=1CXVbCbqKyg */
    void CreateDust() {
        dust.Play();
        sound.boingSound();
    }

    public void killPlayer()
    {
        sound.gameOverSound(); //play game over sound
        music.SetActive(false);
        alive = false;
        PlayerPrefs.SetFloat("highscore", highscoreNum); //set highscore to highscoreNum

        activeUI.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
