﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private TextMeshPro usernameBox;
    public PlayerManager manager;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteRight;
    public Sprite spriteLeft;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        usernameBox = gameObject.GetComponentInChildren<TextMeshPro>();
        manager = gameObject.GetComponent<PlayerManager>();
        sr.sprite = spriteDown;
        Destroy(GameObject.Find("DefaultCamera"));
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!manager.isConnected)
        {
            return;
        }*/

        //rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed * 1000;

        //determines proper sprite based on velocity. Will be updated once more sprites are available
        if (rb.velocity.y > 0)
        {
            if (rb.velocity.x > 0)
            {
                if (rb.velocity.y > rb.velocity.x)
                {
                    sr.sprite = spriteUp;
                }
                else
                {
                    sr.sprite = spriteRight;
                }
            }
            else
            {
                if (rb.velocity.y > rb.velocity.x * -1)
                {
                    sr.sprite = spriteUp;
                }
                else
                {
                    sr.sprite = spriteLeft;
                }
            }
        }
        else if (rb.velocity.y < 0)
        {
            if (rb.velocity.x > 0)
            {
                if (rb.velocity.y * -1 > rb.velocity.x)
                {
                    sr.sprite = spriteDown;
                }
                else
                {
                    sr.sprite = spriteRight;
                }
            }
            else
            {
                if (rb.velocity.y * -1 > rb.velocity.x * -1)
                {
                    sr.sprite = spriteDown;
                }
                else
                {
                    sr.sprite = spriteLeft;
                }
            }
        }
        else if (rb.velocity.y == 0)
        {
            if (rb.velocity.x > 0)
            {
                sr.sprite = spriteRight;
            }
            else if (rb.velocity.x < 0)
            {
                sr.sprite = spriteLeft;
            }
        }

        usernameBox.text = manager.username;
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.UpArrow),
            Input.GetKey(KeyCode.DownArrow),
            Input.GetKey(KeyCode.LeftArrow),
            Input.GetKey(KeyCode.RightArrow)
        };

        ClientSend.PlayerMovement(_inputs);
    }
}
