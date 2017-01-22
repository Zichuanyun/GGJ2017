using UnityEngine;
using System.Collections;

public class GoalManager : MonoBehaviour {

    public int playerNumber = 1;
    GameController GC;

    // Use this for initialization
	void Start () {
        GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ball") {
            GC.getScore(playerNumber);
        }
    }

}
