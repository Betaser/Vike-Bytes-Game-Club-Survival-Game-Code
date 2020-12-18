using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;
    public InputField ipField;
    public InputField portField;
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
        usernameField.interactable = false;
        ipField.interactable = false;
        portField.interactable = false;
        //playerManager.username = usernameField.text; dont mind this
        //playerManager.isConnected = true;
        //Destroy(GameObject.Find("DefaultCamera"));
        Client.instance.ip = ipField.text;
        Client.instance.port = Int32.Parse(portField.text);
        Client.instance.ConnectToServer();
    }
}
