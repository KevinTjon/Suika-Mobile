using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruitcon : MonoBehaviour
{
    private string inthespawn = "y";
    private bool gameStarted = true;
    private bool gameover = false;
    public float gameoverYThreshold = 4f;
    private bool touchedafriut = false;

    void Start()
    {
        gameover = false;
        gameStarted = true;
        inthespawn = "y";

        if (transform.position.y < 2)
        {
            inthespawn = "n";
            GetComponent<Rigidbody2D>().gravityScale = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !gameover)
        {
            //Debug.Log(touchedafriut+ "in update");
            //Debug.Log(transform.position.y+ "gameover?");
            // Check if the fruit has reached the game over threshold after passing it
            if (touchedafriut && transform.position.y > gameoverYThreshold)
            {
                GameOver();
            }
        }
        if(inthespawn == "y")
        {
            GetComponent<Transform>().position = spawncon.spawnXPos;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            GetComponent<Rigidbody2D>().gravityScale = 2;
            inthespawn = "n";
            spawncon.spawnedYet = "n";
        }   
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(touchedafriut+ "out collision if");
        if (collision.gameObject.tag == gameObject.tag)
        {
            //Debug.Log(touchedafriut+ "in collision if");
            touchedafriut = true;
            spawncon.fruitPos = transform.position;
            spawncon.newfruit = "y";
            spawncon.whichFruit = int.Parse(gameObject.tag);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag != "wall")
        {
            touchedafriut = true;
        }        
    }

    void GameOver()
    {
        gameover = true;
        SceneManager.LoadSceneAsync(2);
    }

    
}

