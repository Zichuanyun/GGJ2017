using UnityEngine;
using System.Collections;

public class GoalManager : MonoBehaviour {

    public int playerNumber = 1;
    GameController GC;
    float scoreInterval = 0.5f;
    bool canScore = true;

    // Use this for initialization
    void Start () {
        GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        scoreInterval = GC.scoreInterval;
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

    IEnumerator CantScore()
    {
        yield return new WaitForSeconds(scoreInterval);
        canScore = true;
    }

}
