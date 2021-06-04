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
    public GameObject inventoryGUI;

    public GameObject readyButton;
    public Text healthText;

    public Text woodText;
    public Text stoneText;
    public Text meatText;

    private GameObject localPlayer;
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
        if (localPlayer == null)
        {
            try
            {
                localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
            }
            catch (Exception)
            {

            }
        }
        else
        {
            PlayerController pController = localPlayer.GetComponent<PlayerController>();
            healthText.text = "Health: " + localPlayer.GetComponent<PlayerManager>().health;
            woodText.text = pController.GetItemCount("wood").ToString();
            stoneText.text = pController.GetItemCount("stone").ToString();
            meatText.text = pController.GetItemCount("meat").ToString();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryGUI.SetActive(!inventoryGUI.activeSelf);
        }
    }

    public void AddHealth(int health)
    {
        localPlayer.GetComponent<PlayerController>().ChangeHealth(health);
    }

    public void CreateAnimal(string species)
    {
        ClientSend.CreateAnimal(species);
    }

    public void Ready()
    {
        // readyButton.SetActive(false); // need to get a reference of this
        ClientSend.Ready();
    }
}
