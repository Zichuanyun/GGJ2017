using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Boom : MonoBehaviour {

    public float duration;
    public float maxPulse;
    public float distance;
    public float timeOmega;
    public float distOmega;
    public float lowPassDuration;
    public float lowCutOff;
    public AudioSource boomSound;
    public static bool isBooming;
    int running;
    Queue boomQ = new Queue();
    // Use this for initialization
    void Start() {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        boomSound = this.transform.GetChild(0).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space"))
            boom(Vector2.zero);
    }
    void FixedUpdate() {
        booming();
    }


    public void boom(Vector2 _position) {
        boomSound.Play(0);
        this.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowCutOff;
        DOTween.To(() => this.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency, x => this.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency=x, 22000, lowPassDuration).SetEase(Ease.InExpo);
        BoomObject boomObject = new global::BoomObject(_position,duration);
        boomObject.time = duration;
        boomObject.position = _position;
        boomQ.Enqueue(boomObject);
        isBooming = true;
        }

    void booming()
    {
        if (boomQ.Count > 0)
        {
            isBooming = true;
            BoomObject bo = (BoomObject)boomQ.Dequeue();
            StartCoroutine(elementBoom(bo));
        }
        if (boomQ.Count == 0 && running == 0)
            isBooming = false;
    }
    IEnumerator elementBoom(BoomObject bo)
    {
        running++;
        int listSize = this.GetComponent<CubeList>().getListSize();
        for (int i = 0; i < listSize; i++)
        {
            for (int j = 0; j < listSize; j++)
            {
                GameObject go = this.GetComponent<CubeList>().getCube(i, j);
                Vector2 goPos = new Vector2(go.GetComponent<Transform>().position.x, go.GetComponent<Transform>().position.z);
                float dist = Vector2.Distance(goPos, bo.position);
                if (2 * Mathf.PI * (duration - bo.time) > dist)
                {
                    float maxAmpli = maxPulse * bo.time / duration;
                    float ampli = maxAmpli * Mathf.Max((distance - dist), 0) / distance;
                    float height = ampli * Mathf.Sin(timeOmega * (bo.time - duration) + dist * distOmega);
                    go.GetComponent<CubeBehavior>().pulse(height);
                }
            }
        }
        bo.time = bo.time - (1f / 30f);
        if (bo.time > 0f)
        {
            boomQ.Enqueue(bo);
        }
        running--;
        yield return null;
    }
}
