using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float enemyMovement;
    private bool enemyDirection; //false will be left, true will be right

    private Color spriteAlpha; //for making the enemy fade on death

    private GameObject enemyCollisionBoxChild;
    private bool enemyAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = Mathf.Round(Random.Range(-10f, 10f));
        if(enemyMovement < 0)
        {
            enemyDirection = false;
        }
        else
        {
            enemyDirection = true;
        }

        enemyCollisionBoxChild = gameObject.transform.GetChild(0).gameObject;

        spriteAlpha = transform.GetComponent<SpriteRenderer>().color;
        spriteAlpha.a = 1f;
        transform.GetComponent<SpriteRenderer>().color = spriteAlpha;
        enemyAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //emulated from CharacterScript
        if (enemyAlive)
        {
            transform.Translate(Vector3.right * enemyMovement * Time.deltaTime);
        }
        if (!enemyDirection)
        {
            Vector3 mirrorVector = transform.localScale;
            mirrorVector.x *= -1;
            transform.localScale = mirrorVector;
            enemyDirection = true; //enemy is now facing the appropriate direction, so the if statement should become disabled else the sprite will constantly flip due to negative values
        }

        //copied from CharacterScript
        //wrapping enemy from one side of the screen to the other. Unity docs; Camera.WorldToViewportPoint
        Vector3 enemyPos = Camera.main.WorldToViewportPoint(transform.position);
        if (enemyPos.x <= 0)
        {
            //https://forum.unity.com/threads/how-to-detect-screen-edge-in-unity.109583/
            Vector3 rightSide = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f));
            transform.position = new Vector3(rightSide.x, transform.position.y, transform.position.z);
        }
        else if (enemyPos.x >= 1)
        {
            Vector3 leftSide = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
            transform.position = new Vector3(leftSide.x, transform.position.y, transform.position.z);
        }

        if(transform.gameObject.GetComponent<BoxCollider2D>() == null) //if enemy's bounce hitbox has been destroyed
        {
            PlayerBouncesOnEnemy();
        }

        if (!enemyAlive && spriteAlpha.a > 0f)
        {
            spriteAlpha.a -= 0.01f; //fade the alpha
            transform.GetComponent<SpriteRenderer>().color = spriteAlpha;
        }
        else if(spriteAlpha.a <= 0f)
        {
            Destroy(transform.gameObject); //destroy this enemy
        }
    }


    //these next methods are for handling the death of an enemy

    private void PlayerBouncesOnEnemy()
    {
        killEnemy();
    }

    private void killEnemy()
    {
        Destroy(enemyCollisionBoxChild); //remove the chance for the player to get hit by the enemy post-mortem
        enemyAlive = false;
    }
}
