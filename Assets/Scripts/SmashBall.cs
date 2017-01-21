using UnityEngine;
using System.Collections;

public class SmashBall : MonoBehaviour {
    public float distance;
    public float maxForce;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void smashBall() {
        Vector2 heroPos = new Vector2(this.gameObject.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.z);
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        Vector2 ballPos = new Vector2(ball.GetComponent<Transform>().position.x, ball.GetComponent<Transform>().position.z);
        Vector2 direction = (ballPos - heroPos);
        direction.Normalize();
        float dist = Vector2.Distance(ballPos, heroPos);
        if (dist < distance)
        {
            float forcePower = (distance - dist) / distance * maxForce;
            Vector2 force2D = forcePower * direction;
            Vector3 force = new Vector3(force2D.x,0, force2D.y);
            ball.GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
