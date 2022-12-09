using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillsPlayer : MonoBehaviour
{
    CharacterScript ChScr;

    // Start is called before the first frame update
    void Start()
    {
        ChScr = transform.parent.GetComponent<CharacterScript>();
    }

    private void OnTriggerEnter2D(Collider2D playercollider)
    {
        if(playercollider.gameObject.CompareTag("playerkillbox"))
        {
            PlayerCollidesWithEnemy();
        }
    }

    public void PlayerCollidesWithEnemy()
    {
        ChScr.killPlayer();
    }
}
