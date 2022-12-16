using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playgroundcol : MonoBehaviour
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
        apple_spawn.Instance.gameOver();
    }

   
}
