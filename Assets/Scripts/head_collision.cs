using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head_collision : MonoBehaviour
{

    public GameObject gameManager;
    GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "snake")
        {
            gameManagerScript.gameOver();
        }

    }
}
