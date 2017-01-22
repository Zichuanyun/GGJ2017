using UnityEngine;
using System.Collections;

public class PlayerAntiStuck : MonoBehaviour {

    public float offsetDistance = 10f;
    public float manHeight = 2.09f;

    CubeList cl;
    Rigidbody rg;

	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody>();
        cl = GameObject.FindWithTag("GroundController").GetComponent<CubeList>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        //Debug.Log("Triggered");
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        float scale = 1.5f;
        if (cl) {
            scale = cl.getScale(pos);

        }
        float Y = manHeight + scale / 2 + 1;
        Vector3 newPos = new Vector3(transform.position.x, Y, transform.position.z);
        transform.position = newPos;
    }
}
