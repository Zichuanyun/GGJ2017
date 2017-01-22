using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Boom : MonoBehaviour {
    float time=0;
    public float duration;
    public float maxPulse;
    public Vector2 position;
    public float distance;
    public float timeOmega;
    public float distOmega;
    public float lowPassDuration;
    public float lowCutOff;
    public AudioClip boomSound;
    public static bool isBooming;
    // Use this for initialization
    void Start() {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space"))
            boom(position);
    }
    void FixedUpdate() {
        if (time > 0)
            booming();
        else
            isBooming = false;
    }


    public void boom(Vector2 _position) {
        time = duration;
        position = _position;
        AudioSource.PlayClipAtPoint(boomSound, position);
        this.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowCutOff;
        DOTween.To(() => this.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency, x => this.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency=x, 22000, lowPassDuration).SetEase(Ease.InExpo);
    }
    void booming()
    {
        int listSize = this.GetComponent<CubeList>().getListSize();
        for (int i = 0; i < listSize; i++)
        {
            for (int j = 0; j < listSize; j++)
            {
                GameObject go = this.GetComponent<CubeList>().getCube(i, j);
                if(!isBooming)
                    go.GetComponent<CubeBehavior>().reset();
                isBooming = true;
                Vector2 goPos = new Vector2(go.GetComponent<Transform>().position.x, go.GetComponent<Transform>().position.z);
                float dist = Vector2.Distance(goPos, position);
                if (2*Mathf.PI * (duration - time) > dist )
                {
                    float maxAmpli = maxPulse * time / duration; 
                    float ampli = maxAmpli * Mathf.Max((distance - dist), 0) / distance;
                    float height = ampli * Mathf.Sin(timeOmega*(time - duration) + dist*distOmega);
                    go.GetComponent<CubeBehavior>().pulse(height);
                }
            }
        }
        time -= 1f/30f;
    }
}
