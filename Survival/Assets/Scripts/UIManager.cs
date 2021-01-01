using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public GameObject playerHUD;
    public InputField usernameField;
    public InputField ipField;
    public InputField portField;
    public Text healthText;

    public int playerHealth;

    //public PlayerManager playerManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        playerHUD.SetActive(true);
        usernameField.interactable = false;
        ipField.interactable = false;
        portField.interactable = false;
        Client.instance.ip = ipField.text;
        Client.instance.port = Int32.Parse(portField.text);
        Client.instance.ConnectToServer();
    }

    public void Update()
    {
        healthText.text = "Health: " + playerHealth;
    }

    public void AddHealth(int health)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ChangeHealth(health);
    }

    public void CreateAnimal(string species)
    {
        ClientSend.CreateAnimal(species);
    }
}
