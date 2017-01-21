using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    //[HideInInspector]
    public GameObject[] players;
    public Transform[] playerSpawns;
    public Color[] playerColors;
    public GameObject playerPrefab;
    public GameObject ballPrefab;

    public 


	// Use this for initialization
	void Start () {
        SpawnPlayers();

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void SpawnPlayers() {
        players = new GameObject[playerSpawns.GetLength(0)];
        for (int i = 0; i < players.GetLength(0); i++) {
            players[i] = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation) as GameObject;
            PlayerMovement pm = players[i].GetComponent<PlayerMovement>();
            pm.Setup(playerColors[i], i + 1);
        }
    }

    void GiveControl() {
        for (int i = 0; i < players.GetLength(0); i++)
        {
            PlayerMovement pm = players[i].GetComponent<PlayerMovement>();
            pm.canControl = true;
        }
    }
}
