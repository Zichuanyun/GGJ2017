using UnityEngine;
using System.Collections;

public class WoController: MonoBehaviour {
    public AudioSource woSound;
    public bool isPlaying = false;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (!woSound.isPlaying)
                woSound.Play(0);
        }
        else
        {
            if(woSound.isPlaying)
                woSound.Stop();
        }
    }
    public void play()
    {
        if(!isPlaying)
        isPlaying = true;
    }
    public void stop()
    {
        isPlaying = false;
    }
}
