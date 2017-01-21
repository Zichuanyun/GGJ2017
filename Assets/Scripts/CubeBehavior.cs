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
        float setHeight = (height + floorHeight) * CubeList.ratio;
        if (setHeight> originHeight)
            this.gameObject.GetComponent<Transform>().localScale= new Vector3(this.transform.GetComponent<Transform>().localScale.x, setHeight, this.transform.GetComponent<Transform>().localScale.z);
    }
    public void fall()
    {
        float height = this.transform.GetComponent<Transform>().localScale.y;
        if (height > floorHeight*CubeList.ratio)
        {
            float fallHeight = Mathf.Max(floorHeight * CubeList.ratio, height - fallVelocity);
            this.gameObject.GetComponent<Transform>().localScale = new Vector3(this.transform.GetComponent<Transform>().localScale.x, fallHeight, this.transform.GetComponent<Transform>().localScale.z);
        }
    }
    public void reset() {
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(this.transform.GetComponent<Transform>().localScale.x, floorHeight * CubeList.ratio, this.transform.GetComponent<Transform>().localScale.z);
    }
}
