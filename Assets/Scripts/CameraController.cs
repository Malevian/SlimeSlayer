using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private float border_x = 23f;
    private float border_y = 29f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boundaryRestrains();
    }

    public void boundaryRestrains()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        if (player.transform.position.x > border_x) //right border
        {
            transform.position = new Vector3(border_x, player.transform.position.y, transform.position.z);
        }
        if (player.transform.position.y > border_y) //top border
        {
            transform.position = new Vector3(player.transform.position.x, border_y, transform.position.z);
        }
        if (player.transform.position.x < -border_x) //left border
        {
            transform.position = new Vector3(-border_x, player.transform.position.y, transform.position.z);
        }
        if (player.transform.position.y < -border_y) //bottom border
        {
            transform.position = new Vector3(player.transform.position.x, -border_y, transform.position.z);
        }
        //top right border
        if (player.transform.position.x > border_x && player.transform.position.y > border_y)
        {
            transform.position = new Vector3(border_x, border_y, transform.position.z);
        }
        //bottom left border
        if (player.transform.position.x < -border_x && player.transform.position.y < -border_y)
        {
            transform.position = new Vector3(-border_x, -border_y, transform.position.z);
        }
        //top left border
        if (player.transform.position.x < -border_x && player.transform.position.y > border_y)
        {
            transform.position = new Vector3(-border_x, border_y, transform.position.z);
        }
        //bottom right border
        if (player.transform.position.x > border_x && player.transform.position.y < -border_y)
        {
            transform.position = new Vector3(border_x, -border_y, transform.position.z);
        }
    }
}
