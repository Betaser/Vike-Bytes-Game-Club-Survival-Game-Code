using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, AnimalManager> animals = new Dictionary<int, AnimalManager>();
    public static Dictionary<int, Tree> trees = new Dictionary<int, Tree>();
    public static Dictionary<int, Rock> rocks = new Dictionary<int, Rock>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject wolfPrefab;
    public GameObject harePrefab;
    public GameObject treePrefab;
    public GameObject rockPrefab;

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

    public void SpawnPlayer(int _id, string _username, Vector2 _position, float _rotation)
    {
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
        Debug.Log(_username);
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
            _animal.GetComponent<AnimalManager>().health = 100;
            Debug.Log("spawned a wolf");
        }
        else if (_species == "hare")
        {
            _animal = Instantiate(harePrefab);
            _animal.transform.position = _position;
            _animal.GetComponent<AnimalManager>().health = 100;
        }
        else
        {
            _animal = null;
            Debug.LogError("species \"" + _species + "\" does not exist!");
        }

        if (animals.ContainsKey(_id))
        {
            Destroy(animals[_id].gameObject);
            animals[_id] = _animal.GetComponent<AnimalManager>();
        }
        else
        {
            animals.Add(_id, _animal.GetComponent<AnimalManager>());
        }

        _animal.GetComponent<AnimalManager>().id = _id;
        _animal.GetComponent<AnimalManager>().species = _species;
        _animal.GetComponent<AnimalManager>().rotation = _rotation;
    }

    public void spawnTree(int id, short x, short y)
    {
        GameObject tree = Instantiate(treePrefab, new Vector2(x, y), Quaternion.identity);
        tree.GetComponent<Tree>().id = id;
        tree.GetComponent<Tree>().hp = 100;
        trees.Add(id, tree.GetComponent<Tree>());
    }
    public void spawnRock(int id, short x, short y)
    {
        GameObject rock = Instantiate(rockPrefab, new Vector2(x, y), Quaternion.identity);
        rock.GetComponent<Rock>().id = id;
        rock.GetComponent<Rock>().hp = 100;
        rocks.Add(id, rock.GetComponent<Rock>());
    }
}
