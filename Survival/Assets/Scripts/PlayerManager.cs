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

    public Sprite[] sprites;

    public int attackDir = -1;
    public GameObject[] attacks;

    private TextMeshPro usernameBox;
    private SpriteRenderer sr;


    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        usernameBox = gameObject.GetComponentInChildren<TextMeshPro>();
        sr.sprite = sprites[3];
    }

    void Update()
    {
        sr.sprite = sprites[sprite];

        usernameBox.text = username;
    }

    private void FixedUpdate()
    {
        foreach (GameObject attack in attacks)
        {
            attack.SetActive(false);
        }
        if (attackDir != -1)
        {
            attacks[attackDir].SetActive(true);
        }
    }
}
