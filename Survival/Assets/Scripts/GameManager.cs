using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, AnimalManager> animals = new Dictionary<int, AnimalManager>();

	public GameObject localPlayerPrefab;
	public GameObject playerPrefab;
    public GameObject wolfPrefab;

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

    public void SpawnPlayer(int _id, string _username, Vector2 _position, float _rotation) {
    	GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab);
            _player.transform.position = _position;
        }
        else
        {
            _player = Instantiate(playerPrefab, transform);
            _player.transform.position = _position;
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        _player.GetComponent<PlayerManager>().rotation = _rotation;
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void SpawnAnimal(int _id, string _species, Vector2 _position, float _rotation)
    {
        GameObject _animal;
        if (_species == "wolf")
        {
            _animal = Instantiate(wolfPrefab);
            _animal.transform.position = _position;
        }
        else
        {
            _animal = null;
            Debug.LogError("species \"" + _species + "\" does not exist!");
        }

        _animal.GetComponent<AnimalManager>().id = _id;
        _animal.GetComponent<AnimalManager>().species = _species;
        _animal.GetComponent<AnimalManager>().rotation = _rotation;
        animals.Add(_id, _animal.GetComponent<AnimalManager>());
    }
}
