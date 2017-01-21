using UnityEngine;
using System.Collections;

public class BouncyWall : MonoBehaviour {
    public Vector3 force;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
     void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
            collision.gameObject.GetComponent<Rigidbody>().AddForce(force);
    }
}
