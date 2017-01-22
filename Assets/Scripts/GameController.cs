using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //[HideInInspector]
    public GameObject[] players;
    public Transform[] playerSpawns;
    public Transform ballSpawn;
    public Color[] playerColors;
    public GameObject playerPrefab;

    public Text[] scoreTexts;
    public int[] scores;

    public Text titleText;
    string titleContent = "Pepper and Salt\nWave Pineapple";

    public GoalManager[] goals;

    public float scoreInterval = 0.5f;
    public int scoreStep = 5;

    int playerNum;
    GameObject ball;

    /// <summary>
    /// 0 start screen
    /// 1 on play
    /// 2 after play
    /// </summary>
    int status = 0;
    

    

    // Use this for initialization
    void Start() {
        ball = GameObject.FindWithTag("Ball");
        scores = new int[2];
        playerNum = playerColors.GetLength(0);
        SpawnNewPlayers();
        ResetBall();
        initToStartScreen();
    }

    void initToStartScreen() {
        TranslatePlayersToSpawn();
        StartText();
        GiveControl();
        HideGoals();
    }

    void initToPlay() {
        titleText.text = "";
        TranslatePlayersToSpawn();
        ResetBall();
        ShowGoals();
        for (int i = 0; i < playerNum; i++) {
            scores[i] = 0;
        }
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (status == 0)
            {
                status = 1;
                initToPlay();
            }
            else if (status == 1) {
                status = 0;
                initToStartScreen();
            }
        }

        if (status == 1) {
            UpdateScore();
        }
    }

    public void ResetBall() {
        ball.GetComponent<Rigidbody>().velocity = new Vector3();
        ball.transform.position = ballSpawn.position;
    }

    void SpawnNewPlayers() {
        if (players.GetLength(0) != 0) {
            for (int i = 0; i < playerNum; i++)
            {
                Destroy(players[i]);
            }
            players = null;
        }

        players = new GameObject[playerNum];
        for (int i = 0; i < playerNum; i++) {
            players[i] = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation) as GameObject;
            PlayerMovement pm = players[i].GetComponent<PlayerMovement>();
            pm.Setup(playerColors[i], i + 1);
        }
    }

    void TranslatePlayersToSpawn() {
        for (int i = 0; i < playerNum; i++)
        {
            players[i].transform.position = playerSpawns[i].position;
        }
    }

    void StartText() {
        titleText.text = titleContent;
        for (int i = 0; i < playerNum; i++)
        {
            scoreTexts[i].text = "";
        }
    }

    void GiveControl() {
        for (int i = 0; i < playerNum; i++)
        {
            PlayerMovement pm = players[i].GetComponent<PlayerMovement>();
            pm.canControl = true;
            pm.isDead = false;
        }
    }

    void TakeControl()
    {
        for (int i = 0; i < playerNum; i++)
        {
            PlayerMovement pm = players[i].GetComponent<PlayerMovement>();
            pm.canControl = false;
        }
    }

    public void getScore(int playerNum) {
        if (status == 1) {
            scores[playerNum - 1] += scoreStep;
        }


    }

    void HideGoals() {
        for (int i = 0; i < playerNum; i++) {
            goals[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void ShowGoals()
    {
        for (int i = 0; i < playerNum; i++)
        {
            goals[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }

    IEnumerator StartGameLoop() {


        yield return null;
    }

    void UpdateScore() {
        for (int i = 0; i < playerNum; i++) {
            scoreTexts[i].text = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColors[i]) + ">PLAYER " + (i + 1) + ": " + "</color>"
                + scores[i];
        }
    }

}
