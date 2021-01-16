using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float rotation;

    public int health;

    public bool attack = false;
    public GameObject sword;

    private TextMeshPro usernameBox;
    private Transform spriteTransform;


    void Start()
    {
        spriteTransform = gameObject.GetComponentInChildren<SpriteRenderer>().transform;
        usernameBox = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        spriteTransform.rotation = Quaternion.Euler(0f, 0f, rotation);

        usernameBox.text = username;
    }

    private void FixedUpdate()
    {
        sword.SetActive(false);
        if (attack)
        {
            sword.SetActive(true);
        }
    }
}
