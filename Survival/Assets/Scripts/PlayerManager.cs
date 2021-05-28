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
    private Animator animator;

    private Vector2 defaultSwordPosition;


    void Start()
    {
        usernameBox = gameObject.GetComponentInChildren<TextMeshPro>();
        defaultSwordPosition = sword.transform.localPosition;
        spriteTransform = gameObject.GetComponentInChildren<SpriteRenderer>().transform;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        usernameBox.text = username;

        spriteTransform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    private void FixedUpdate()
    {
        if (attack)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            sword.transform.localPosition = Vector2.zero;
        }
    }
}
