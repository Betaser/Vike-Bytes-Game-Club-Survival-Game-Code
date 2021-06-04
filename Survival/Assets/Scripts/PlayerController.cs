using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public PlayerManager manager;
    public float speed;
    private UIManager UI;
    private float rotation;
    private Vector2 mousePosition;
    private Vector2 mouseWorldPosition;

    public Dictionary<string, int> inventory = new Dictionary<string, int>(); // {
                                                                              // {"wood", 0}
                                                                              // };

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Menu").GetComponent<UIManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        manager = gameObject.GetComponent<PlayerManager>();
        Destroy(GameObject.Find("DefaultCamera"));

        inventory.Add("wood", 0);
        inventory.Add("rock", 0);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        try
        {
            mouseWorldPosition = Camera.current.ScreenToWorldPoint(mousePosition) - transform.position;
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
            //not really sure why this one happens
        }
        rotation = Mathf.Atan(mouseWorldPosition.y / mouseWorldPosition.x) * 180f / Mathf.PI + 90f;
        if (mouseWorldPosition.x > 0)
        {
            rotation += 180f;
        }
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetMouseButton(0)
        };
        float _rotation = rotation;

        ClientSend.PlayerMovement(_inputs, _rotation);
    }

    public void ChangeHealth(int healthDelta)
    {
        ClientSend.ChangeHealth(healthDelta);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            HitTree(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Rock")
        {
            HitRock(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Animal")
        {
            HitAnimal(collision.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Animal") return;
        int damage = collision.gameObject.GetComponent<AnimalManager>().damage;
        if (damage == 0) return;
        PlayerHurt(damage, collision.gameObject.GetComponent<AnimalManager>().id);
    }

    void PlayerHurt(int damage, int id)
    {
        ClientSend.PlayerDamage(damage, id);
    }

    void HitTree(GameObject tree)
    {
        int damage = 25;
        Debug.Log("you hit a tree");
        ClientSend.Hit("tree", tree.GetComponent<Tree>().id, damage);
        if (tree.GetComponent<Tree>().hp <= damage)
        {
            ClientSend.AddItem("wood", 2);
        }
    }
    void HitRock(GameObject rock)
    {
        int damage = 25;
        ClientSend.Hit("rock", rock.GetComponent<Rock>().id, damage);
        if (rock.GetComponent<Rock>().hp <= damage)
        {
            ClientSend.AddItem("rock", 2);
        }
    }
    void HitAnimal(GameObject animal)
    {
        int damage = 25;
        ClientSend.Hit("animal", animal.GetComponent<AnimalManager>().id, damage);
        if (animal.GetComponent<AnimalManager>().health <= damage)
        {
            ClientSend.AddItem("meat", 2);
        }
    }

    public int GetItemCount(String itemString)
    {
        Debug.LogWarning("This is a fake method!!!");
        return 1; //just for testing purposes
    }
}
