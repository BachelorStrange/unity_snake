using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : MonoBehaviour
{
    float speed = 0.1f;
    int current_snake_length = 1;
    public GameObject snake_extension;
    public List<Vector3> directions = new List<Vector3>();
    public List<Vector3> saved_points = new List<Vector3>();
    public List<Vector3> saved_directions = new List<Vector3>();
    public List<GameObject> snake_extension_list = new List<GameObject>();
    float tol = 0.04f;
    int interval = 3;
    int pool_size = 300;
    int pool_index = 0;
    Vector3 new_position;
    

    Vector3 vecspeed = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        vecspeed.Set(speed, 0, 0);
        directions.Add(vecspeed);
        snake_extension_list.Add(gameObject);
        GameObject tmp;
        for (int i = 0; i<pool_size;i++)
        {
            tmp = Instantiate(snake_extension, new Vector3(100, 100, 0), Quaternion.identity);
            tmp.SetActive(false);
            snake_extension_list.Add(tmp);
        }
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if (apple_spawn.Instance.snake_length != current_snake_length)
        {
            current_snake_length = apple_spawn.Instance.snake_length;
            new_position = snake_extension_list[pool_index].transform.position + (-1) * directions[pool_index] / speed * 2.1f;
            pool_index++;
            snake_extension_list[pool_index].SetActive(true);
            snake_extension_list[pool_index].transform.position = new_position;
            directions.Add(directions[directions.Count - 1]);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (directions[0].y != -speed)
            {
                vecspeed.Set(0, speed, 0);
                Vector3 pos = new Vector3(snake_extension_list[0].transform.position.x, snake_extension_list[0].transform.position.y, snake_extension_list[0].transform.position.z);
                saved_points.Add(pos);
                //set_directions(vecspeed);
                saved_directions.Add(vecspeed);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (directions[0].x != speed)
            {
                vecspeed.Set(-speed, 0, 0);
                Vector3 pos = new Vector3(snake_extension_list[0].transform.position.x, snake_extension_list[0].transform.position.y, snake_extension_list[0].transform.position.z);
                saved_points.Add(pos);
                saved_directions.Add(vecspeed);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (directions[0].y != speed)
            {
                vecspeed.Set(0, -speed, 0);
                Vector3 pos = new Vector3(snake_extension_list[0].transform.position.x, snake_extension_list[0].transform.position.y, snake_extension_list[0].transform.position.z);
                saved_points.Add(pos);
                saved_directions.Add(vecspeed);
                //set_directions(vecspeed);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (directions[0].x != -speed)
            {
                vecspeed.Set(speed, 0, 0);
                Vector3 pos = new Vector3(snake_extension_list[0].transform.position.x, snake_extension_list[0].transform.position.y, snake_extension_list[0].transform.position.z);
                saved_points.Add(pos);
                saved_directions.Add(vecspeed);
                //set_directions(vecspeed);
            }

        }

        modify_positions();
    }

    private void modify_positions()
    {
        for (int i = 0; i < snake_extension_list.Count; i++)
        {
            for (int j = 0; j < saved_points.Count; j++)
            {
                if (check_coordinates(snake_extension_list[i].transform.position, saved_points[j]))
                {
                    directions[i] = saved_directions[j];
                }
            }

        }
        for (int i = 0; i< snake_extension_list.Count; i++)
        {
            Debug.Log(i);
            snake_extension_list[i].transform.position += directions[i];
        }
    }

    private bool check_coordinates(Vector3 snake_position, Vector3 saved_position)
    {
        if (snake_position.x > saved_position.x-tol && snake_position.x < saved_position.x+tol)
            {
            if (snake_position.y > saved_position.y - tol && snake_position.y < saved_position.y + tol)
                return true; 
            }
       
        return false;
        
    }

    private void set_directions(Vector3 vecspeed)
    {
       

        for (int i = directions.Count-1; i > 0 ; i--)
        {
            Vector3 speedpuffer = directions[i - 1];
            directions[i] = speedpuffer;
        }

        directions[0] = vecspeed;
    }

    public Vector3 get_vecspeed()
    {
        return this.vecspeed;
    }
}
