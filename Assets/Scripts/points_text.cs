using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class points_text : MonoBehaviour
{
    public TMP_Text textMesh;
    public GameObject gameManager;
    GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        textMesh = this.gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Points: "+gameManagerScript.score.ToString();
    }
}
