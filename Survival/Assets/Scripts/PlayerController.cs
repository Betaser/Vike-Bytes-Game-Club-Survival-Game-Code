using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public PlayerManager manager;
    public float speed;
    private UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Menu").GetComponent<UIManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        manager = gameObject.GetComponent<PlayerManager>();
        Destroy(GameObject.Find("DefaultCamera"));
    }

    // Update is called once per frame
    void Update()
    {
        UI.playerHealth = manager.health;
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.UpArrow),
            Input.GetKey(KeyCode.DownArrow),
            Input.GetKey(KeyCode.LeftArrow),
            Input.GetKey(KeyCode.RightArrow)
        };

        ClientSend.PlayerMovement(_inputs);
    }

    public void ChangeHealth(int healthDelta)
    {
        ClientSend.ChangeHealth(healthDelta);
    }
}
