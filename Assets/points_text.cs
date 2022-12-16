using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class points_text : MonoBehaviour
{
    public TMP_Text textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = this.gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Points: "+apple_spawn.Instance.score.ToString();
    }
}
