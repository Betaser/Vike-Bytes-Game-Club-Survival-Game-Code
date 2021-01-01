using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public int sprite;

    public int health;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteRight;
    public Sprite spriteLeft;

    private TextMeshPro usernameBox;
    private SpriteRenderer sr;


    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        usernameBox = gameObject.GetComponentInChildren<TextMeshPro>();
        sr.sprite = spriteDown;
    }

    void Update()
    {
        if (sprite == 1)
        {
            sr.sprite = spriteRight;
        }
        else if (sprite == 2)
        {
            sr.sprite = spriteUp;
        }
        else if (sprite == 3)
        {
            sr.sprite = spriteLeft;
        }
        else if (sprite == 4)
        {
            sr.sprite = spriteDown;
        }

        usernameBox.text = username;
    }
}
