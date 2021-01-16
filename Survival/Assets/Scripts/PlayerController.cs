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
    private float rotation;
    private Vector2 mousePosition;
    private Vector2 mouseWorldPosition;

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
        mousePosition = Input.mousePosition;
        mouseWorldPosition = Camera.current.ScreenToWorldPoint(mousePosition)- transform.position;
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
            Input.GetKey(KeyCode.UpArrow),
            Input.GetKey(KeyCode.DownArrow),
            Input.GetKey(KeyCode.LeftArrow),
            Input.GetKey(KeyCode.RightArrow),
            Input.GetKey(KeyCode.Space)
        };
        float _rotation = rotation;

        ClientSend.PlayerMovement(_inputs, _rotation);
    }

    public void ChangeHealth(int healthDelta)
    {
        ClientSend.ChangeHealth(healthDelta);
    }
}
