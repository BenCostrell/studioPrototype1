using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySceneManager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public GameObject playerPrefab;
    public Vector3 player1Spawn;
    public Vector3 player2Spawn;

    // Use this for initialization
    void Start()
    {
        InitializePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<PlayerController>().playerToys.Count == 2 && player2.GetComponent<PlayerController>().playerToys.Count == 2)
        {
            StartVoiceOvers();
        }
    }

    void InitializePlayers()
    {
        player1 = Instantiate(playerPrefab, player1Spawn, Quaternion.identity) as GameObject;
        player1.GetComponent<SpriteRenderer>().sprite = player1Sprite;
        player1.GetComponent<PlayerController>().playerNum = 1;

        player2 = Instantiate(playerPrefab, player2Spawn, Quaternion.identity) as GameObject;
        player2.GetComponent<SpriteRenderer>().sprite = player2Sprite;
        player2.GetComponent<PlayerController>().playerNum = 2;


    }

    void StartVoiceOvers()
    {
        Debug.Log("Player1 has the " + player1.GetComponent<PlayerController>().playerToys[0] + " and the " + player1.GetComponent<PlayerController>().playerToys[1]);

        Debug.Log("Player2 has the " + player2.GetComponent<PlayerController>().playerToys[0] + " and the " + player2.GetComponent<PlayerController>().playerToys[1]);

        //player1 archetypes
        FindToyCombos(player1, "Cat", "Calculator", "NERD");
        FindToyCombos(player1, "Cat", "Kazoo", "JD SALINGER");
        FindToyCombos(player1, "Dog", "Cat", "DAVID");
        FindToyCombos(player1, "Dog", "Kazoo", "ROCKSTAR");
        FindToyCombos(player1, "Dog", "Calculator", "POLITICIAN");
        FindToyCombos(player1, "Calculator", "Kazoo", "ARCHITECT");

        //player2 archetypes
        FindToyCombos(player2, "Cat", "Calculator", "NERD");
        FindToyCombos(player2, "Cat", "Kazoo", "JD SALINGER");
        FindToyCombos(player2, "Dog", "Cat", "DAVID");
        FindToyCombos(player2, "Dog", "Kazoo", "ROCKSTAR");
        FindToyCombos(player2, "Dog", "Calculator", "POLITICIAN");
        FindToyCombos(player2, "Calculator", "Kazoo", "ARCHITECT");


    }

    void FindToyCombos(GameObject player, string toy1, string toy2, string archetpye)
    {
        if ((player.GetComponent<PlayerController>().playerToys.Contains(toy1) && (player.GetComponent<PlayerController>().playerToys.Contains(toy2))))
        {
            Debug.Log(player.name + " is a " + archetpye + "!!!");
        }
    }
}
