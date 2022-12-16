using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class apple_spawn : MonoBehaviour
{
    float timeplus = 0.0f;
    public GameObject Apple;
    float random_pos_x;
    float random_pos_y;
    public List<GameObject> apples = new List<GameObject>();
    public static apple_spawn Instance { get; set; }
    public int score = 0;
    public int snake_length = 1;
    int pool_size = 200;
    int pool_index = 0;

    public GameObject cv1;
    public GameObject backgroundmusic;
    public GameObject snake;
    public GameObject gom;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cv1.SetActive(false);
        gom = GameObject.Find("gameovermusic");
        gom.SetActive(false);

        for (int i=0;i<pool_size;i++)
        {
            apples.Add(Instantiate(Apple, new Vector3(100,100,0), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeplus += Time.deltaTime;
        if (timeplus > 5.0f)
        {
            random_pos_x = Random.insideUnitCircle[0];
            random_pos_y = Random.insideUnitCircle[0];
            pool_index++;
            apples[pool_index].transform.position = new Vector3(random_pos_x * 50.0f, random_pos_y * 50.0f, 0.0f);
            //apples.Add(Instantiate(Apple, , Quaternion.identity));
            timeplus = 0;
        }

    }

    public void gameOver()
    {
        cv1.SetActive(true);
        gom.SetActive(true);
        backgroundmusic.SetActive(false);
        Destroy(snake);
    }





}
