using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    float random_pos_x;
    float random_pos_y;
    public GameObject snake;
    public GameObject gameOverMusic;
    public GameObject restartScreen;
    public GameObject titleScreen;
    public GameObject backgroundmusic;
    public List<GameObject> apples = new List<GameObject>();
    public float spawnRate = 0.05f;

    public int score = 0;
    public int snake_length = 1;

    int pool_size = 16;
    int pool_index = 0;

    public GameObject Apple;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMusic.SetActive(false);
        backgroundmusic.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameOver()
    {
        restartScreen.SetActive(true);
        gameOverMusic.SetActive(true);
        backgroundmusic.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        restartScreen.SetActive(false);
        titleScreen.SetActive(false);
        backgroundmusic.SetActive(true);

        for (int i = 0; i < pool_size; i++)
        {
            apples.Add(Instantiate(Apple, new Vector3(100, 100, 0), Quaternion.identity));
            apples[i].SetActive(false);
        }
        Instantiate(snake, snake.transform.position, snake.transform.rotation);
        InvokeRepeating("spawnApple", 1.0f, spawnRate);
    }

    public void spawnApple()
    {
        if (pool_index < pool_size - 1)
        {
            RandomApple();
        }
        else
        {
            pool_index = 0;
            RandomApple();
        }
        
    }

    public void RandomApple()
    {
        random_pos_x = Random.insideUnitCircle[0];
        random_pos_y = Random.insideUnitCircle[0];
        apples[pool_index].SetActive(true);
        apples[pool_index].transform.position = new Vector3(random_pos_x * 50.0f, random_pos_y * 50.0f, 0.0f);
        pool_index++;
    }

}
