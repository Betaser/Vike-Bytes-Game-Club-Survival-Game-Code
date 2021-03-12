using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimalManager : MonoBehaviour
{
    public int id;
    public string species;
    public float rotation;

    public int health = 100;

    public Sprite sprite;

    private TextMeshPro textBox;
    private SpriteRenderer sr;

    void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        textBox = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        sr.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        textBox.text = "" + health;
        if (health <= 0)
        {
            Debug.Log("animal " + id + " just died");
        }
    }
}
