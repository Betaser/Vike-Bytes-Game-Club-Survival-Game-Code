using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tree : MonoBehaviour
{
    public int id;
    public int hp;
    public TextMeshPro textBox;

    private void Update()
    {
        textBox.text = "" + hp;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
