using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 dropLocationLeftPos;
    private Vector3 dropLocationMiddlePos;
    private Vector3 dropLocationRightPos;

    // Start is called before the first frame update
    void Start()
    {
        dropLocationLeftPos = GameObject.Find("DropLocationLeft").transform.position;
        dropLocationMiddlePos = GameObject.Find("DropLocationMiddle").transform.position;
        dropLocationRightPos = GameObject.Find("DropLocationRight").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2(dropLocationMiddlePos.x, transform.position.y);
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position = new Vector2(dropLocationLeftPos.x, transform.position.y);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2(dropLocationRightPos.x, transform.position.y);
        } else
        {
            transform.position = new Vector2(dropLocationMiddlePos.x, transform.position.y);
        }
    }
}