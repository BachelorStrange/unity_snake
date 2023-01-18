using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : MonoBehaviour
{
    float speed = 15f;
    int current_snake_length = 1;
    public GameObject snake_extension;
    public List<Vector3> directions = new List<Vector3>();
    public List<Vector3> saved_points = new List<Vector3>();
    public List<Vector3> saved_directions = new List<Vector3>();
    public List<GameObject> snake_extension_list = new List<GameObject>();
    int pool_size = 300;
    int pool_index = 0;
    int maxListSize = 15000;
    Vector3 new_position;
    public GameObject gameManager;
    GameManager gameManagerScript;
    float moveMentBlock = 0.25f;
    float time = 0.0f;
    float extrapolation_factor = 1.0f;
    float snake_dist = 3.0f;
    float dist_tol = 0.9f;
    GameObject sprite;


    Vector3 vecspeed = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        sprite = GameObject.Find("sprite");
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        vecspeed.Set(speed, 0, 0);
        directions.Add(vecspeed);
        snake_extension_list.Add(gameObject);
        for (int i = 0; i<pool_size;i++)
        {
            GameObject tmp = Instantiate(snake_extension, new Vector3(100, 100, 0), Quaternion.identity);
            tmp.SetActive(false);
            snake_extension_list.Add(tmp);  
        }
        
    }

   
    void Update()
    {
        time += Time.deltaTime;
        if (time > moveMentBlock)
        {
            
            if (gameManagerScript.snake_length != current_snake_length)
            {
                
                current_snake_length = gameManagerScript.snake_length;
                new_position = snake_extension_list[pool_index].transform.position + (-1) * directions[pool_index] / speed * snake_dist;
                pool_index++;
                snake_extension_list[pool_index].SetActive(true);
                snake_extension_list[pool_index].transform.position = new_position;
                directions.Add(directions[directions.Count - 1]);
            }

            input();
           
            
        }
        ModifyPositions();
        delete_old_positions();

    }

    private void input()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (directions[0].y != -speed)
            {
                updateHead(new Vector3(0, speed, 0), new Vector3(0, 0, 90));

            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (directions[0].x != speed)
            {
                updateHead(new Vector3(-speed, 0, 0), new Vector3(0, 0, 180));
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (directions[0].y != speed)
            {
                updateHead(new Vector3(0, -speed, 0), new Vector3(0, 0, -90));
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (directions[0].x != -speed)
            {
                updateHead(new Vector3(speed, 0, 0), new Vector3(0, 0, 0));
            }

        }
    }

    private void updateHead(Vector3 speed, Vector3 rotation)
    {
        time = 0.0f;
        vecspeed = speed;
        Vector3 pos = new Vector3(snake_extension_list[0].transform.position.x, snake_extension_list[0].transform.position.y, snake_extension_list[0].transform.position.z);
        saved_points.Add(pos);
        saved_directions.Add(vecspeed);
        directions[0] = vecspeed;
        sprite.transform.localEulerAngles = rotation;
    }

    private void ModifyPositions()
    {
        for (int i = directions.Count-1; i>0; i--)
        {
            Vector3 currentPosition = snake_extension_list[i].transform.position;
            Vector3 positionOfFrontman = snake_extension_list[i - 1].transform.position;
            Vector3 extrapolatedNewPosition = snake_extension_list[i].transform.position + directions[i] * Time.deltaTime;
            if (directions[i - 1] != directions[i]) //Frontman hat seine Richtung bereits geändert
            {
                if (directions[i].x > 0)
                {
                    if (extrapolation_factor * extrapolatedNewPosition.x > positionOfFrontman.x)
                    {
                        directions[i] = directions[i - 1];
                        Vector3 interpolated_pos =
                            new Vector3(snake_extension_list[i - 1].transform.position.x, snake_extension_list[i].transform.position.y , snake_extension_list[i].transform.position.z);
                        snake_extension_list[i].transform.position = interpolated_pos;
                    }
                }
                else if (directions[i].x < 0)
                {
                    if (extrapolation_factor * extrapolatedNewPosition.x < positionOfFrontman.x)
                    {
                        directions[i] = directions[i - 1];
                        Vector3 interpolated_pos =
                            new Vector3(snake_extension_list[i - 1].transform.position.x, snake_extension_list[i].transform.position.y , snake_extension_list[i].transform.position.z);
                        snake_extension_list[i].transform.position = interpolated_pos;
                    }
                }
                else if (directions[i].y > 0)
                {
                    if (extrapolation_factor * extrapolatedNewPosition.y > positionOfFrontman.y)
                    {
                        directions[i] = directions[i - 1];
                        Vector3 interpolated_pos =
                            new Vector3(snake_extension_list[i].transform.position.x , snake_extension_list[i-1].transform.position.y, snake_extension_list[i].transform.position.z);
                        snake_extension_list[i].transform.position = interpolated_pos;
                    }
                }
                else if (directions[i].y < 0)
                {
                    if (extrapolation_factor * extrapolatedNewPosition.y < positionOfFrontman.y)
                    {
                        directions[i] = directions[i - 1];
                        Vector3 interpolated_pos =
                            new Vector3(snake_extension_list[i].transform.position.x , snake_extension_list[i - 1].transform.position.y, snake_extension_list[i].transform.position.z);
                        snake_extension_list[i].transform.position = interpolated_pos;
                    }
                }

            }
        
            float distance = (snake_extension_list[0].transform.position - snake_extension_list[1].transform.position).magnitude;
            if(distance<snake_dist-dist_tol)
            {
                float diff = Mathf.Abs(snake_dist - dist_tol - Mathf.Abs(distance));
                Debug.Log(diff);
                snake_extension_list[0].transform.Translate(directions[1] / speed * diff);

            }
            
            snake_extension_list[i].transform.position += directions[i] * Time.deltaTime;          
        }
        snake_extension_list[0].transform.position += directions[0] * Time.deltaTime;
    }


    private void delete_old_positions()
    {
        if (saved_directions.Count > maxListSize)
        {
            
            for (int i = 0; i < Mathf.Floor(saved_points.Count / 2); i++)
            {
                saved_points.RemoveAt(i);
                saved_directions.RemoveAt(i);
            }
        }
        
    }


}
