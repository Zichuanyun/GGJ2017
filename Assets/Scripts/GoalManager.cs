using UnityEngine;
using System.Collections;

public class GoalManager : MonoBehaviour {

    public int playerNumber = 1;
    GameController GC;
    float scoreInterval = 0.5f;
    bool canScore = true;
    int scoreStep = 5;

    // Use this for initialization
    void Start () {
        GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        scoreInterval = GC.scoreInterval;
        scoreStep = GC.scoreStep;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Ball" && canScore) {
            canScore = false;
            StartCoroutine("CantScore");
            GC.getScore(playerNumber);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            GC.leaveScore();
            //Debug.Log("leave");
        }
    }


    IEnumerator CantScore()
    {
        yield return new WaitForSeconds(scoreInterval);
        canScore = true;
    }

}
