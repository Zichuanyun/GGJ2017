using UnityEngine;
using System.Collections;

public class BallSaver : MonoBehaviour {

    GameController GC;

	// Use this for initialization
	void Start () {
        GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other) {
        if (other.tag == "Ball") {
            if (!GC) {
                GC = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            }

            GC.ResetBall();
        }
    }
}
