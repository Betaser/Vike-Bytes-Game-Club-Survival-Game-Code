using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimalManager : MonoBehaviour
{
    public int id;
    public string species;
    public float rotation;

    public int health;

    public Sprite sprite;

    private TextMeshPro nameBox;
    private SpriteRenderer sr;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        nameBox = gameObject.GetComponentInChildren<TextMeshPro>();
        sr.sprite = sprite;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f,0f,rotation);
        nameBox.text = species;
    }
}
