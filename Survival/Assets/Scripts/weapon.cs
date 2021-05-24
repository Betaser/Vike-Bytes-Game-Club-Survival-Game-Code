using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            //HitTree(collision.gameObject);
            Debug.Log("hit a tree");
        }
        else if (collision.gameObject.tag == "Rock")
        {
            //HitRock(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Animal")
        {
            //HitAnimal(collision.gameObject);
        }

    }
}
