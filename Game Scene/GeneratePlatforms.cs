using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatforms : MonoBehaviour
{
    public GameObject gameOverScreen;

    //https://forum.unity.com/threads/randomly-generated-2d-platforms-help.348174/

    public GameObject platform;
    public GameObject Enemy;
    //spawntime of platform
    //public float spawnTime;
    //yUp max y on y axe for random spawn, yDown for min on y axis
    public float yMin, yMax;
    //public float yIncrease; deprecated
    public int numberOfPlatforms;
    public float screenWidth;
    Vector3 spawnPosition = new Vector3();
    Vector3 enemySpawnPos = new Vector3();
    
    void Start()
    {
        //https://www.youtube.com/watch?v=fHN-26GEVhA 36:00
        
        spawnPosition.y = -4; // to make sure the player has somewhere to jump at the start

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(yMin, yMax);
            spawnPosition.x = Random.Range(-screenWidth, screenWidth);
            Instantiate(platform, spawnPosition, Quaternion.identity); //Quaternion.identity ???????? I guess Instantiate does require a quaternion rotation? so this just makes the rotation default?
        }
    }

    //make sure game is endless (see CharacterScript)
    public void SpawnMOREPlatforms()
    {
        spawnPosition.y += Random.Range(yMin, yMax);
        spawnPosition.x = Random.Range(-screenWidth, screenWidth);
        Instantiate(platform, spawnPosition, Quaternion.identity);
    }

    public void SpawnEnemies()
    {
        enemySpawnPos = spawnPosition;
        enemySpawnPos.y = spawnPosition.y - 350; //make sure enemies spawn at the correct height, instead of off-screen below the player
        enemySpawnPos.x = Random.Range(-screenWidth, screenWidth);
        enemySpawnPos.y += Random.Range(yMin, (yMax + 10));
        Instantiate(Enemy, enemySpawnPos, Quaternion.identity);
    }

    /**
     * Deprecated code
    void SpawnPlatform()
    {//note: get camera position, check if camera has moved, ensure camera is not still moving, then spawn 2 platforms
        if (!gameOverScreen.activeSelf) //if player is not dead
        {
            float x = Random.Range(-(Screen.width / 20), (Screen.width / 20));
            float y = Random.Range(yMin, yMax);
            //make vector3 for spawn position
            Vector3 pos = new Vector3(x, y, 0);
            //set platform in the world (transform.rotation as from main gameObject ("platformController")
            Instantiate(platform, pos, transform.rotation);
            yMin += 2.42f; yMax += 2.42f; //technically 2 lines of code. Since they're similar, might as well merge them
            Debug.Log("aaaaaa");
        }
    }

    void SpawnInitialPlatforms()
    {
        float y = Random.Range(yMin, yMax);
        float x = Random.Range(-(Screen.width / 20), (Screen.width / 20));
        Debug.Log(x + " vs " + Screen.width);
        //make vector3 for spawn position
        Vector3 pos = new Vector3(x, y, 0);
        //set platform in the world (transform.rotation as from main gameObject ("platformController")
        Instantiate(platform, pos, transform.rotation);
        yMin += yIncrease; yMax += yIncrease; //technically 2 lines of code. Since they're similar, might as well merge them
        Debug.Log(pos);
        if(yMin > Screen.height)
        {
            InvokeRepeating(nameof(SpawnPlatform), 0, 3);
            CancelInvoke(nameof(SpawnInitialPlatforms));
        }
    }
    */
}
