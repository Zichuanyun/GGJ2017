using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
    public float fallVelocity;
    public float floorHeight;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        fall();
	}

    public void pulse(float height)
    {
        float originHeight = this.transform.GetComponent<Transform>().localScale.y;
        if (height+floorHeight > originHeight)
            this.gameObject.GetComponent<Transform>().localScale= new Vector3(this.transform.GetComponent<Transform>().localScale.x, height + floorHeight, this.transform.GetComponent<Transform>().localScale.z);
    }
    public void fall()
    {
        float height = this.transform.GetComponent<Transform>().localScale.y;
        if (height > floorHeight)
        {
            float fallHeight = Mathf.Max(floorHeight, height - fallVelocity);
            this.gameObject.GetComponent<Transform>().localScale = new Vector3(this.transform.GetComponent<Transform>().localScale.x, fallHeight, this.transform.GetComponent<Transform>().localScale.z);
        }
    }
    public void reset() {
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(this.transform.GetComponent<Transform>().localScale.x, floorHeight, this.transform.GetComponent<Transform>().localScale.z);
    }
}
