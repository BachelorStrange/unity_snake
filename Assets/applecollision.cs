using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applecollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "snake")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            apple_spawn.Instance.score++;
            apple_spawn.Instance.snake_length++;
            //Debug.Log(apple_spawn.Instance.score);
        }
        
    }
}
