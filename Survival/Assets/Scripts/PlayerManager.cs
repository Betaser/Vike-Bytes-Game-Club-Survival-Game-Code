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

    public bool spectate = false;

    private TextMeshPro usernameBox;
    private Transform spriteTransform;

    private Vector2 defaultSwordPosition;


    void Start()
    {
        defaultSwordPosition = sword.transform.localPosition;
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
        if (attack)
        {
            sword.SetActive(true);
            sword.transform.localPosition = defaultSwordPosition;
        }
        else
        {
            sword.SetActive(false);
            sword.transform.localPosition = Vector2.zero;
        }
    }
}
