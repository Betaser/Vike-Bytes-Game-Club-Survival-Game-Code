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
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        nameBox = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        sr.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        nameBox.text = species;
    }
}
