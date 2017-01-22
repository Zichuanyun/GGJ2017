using UnityEngine;
using System.Collections;

public class SpectrumReader : MonoBehaviour {
    public int windowSize;
    public float ampliGain;
    public float maxGain;
    public float highThreshold;
    public float lowThreshold;
    public float time;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKey("space"))
        //{
       //     this.gameObject.GetComponent<AudioSource>().time = time;
       //
       // }
        float[] spect = AudioListener.GetSpectrumData(windowSize, 0,FFTWindow.Hamming);
        if (!Boom.isBooming)
        {
            float maxSpect = 0;
            for (int i = 0; i < spect.GetLength(0); i++)
                if (spect[i] > maxSpect)
                    maxSpect = spect[i];
            int listSize = this.GetComponent<CubeList>().getListSize();
            float linearMaxGained = maxSpect * ampliGain;
            float maxDist = Vector2.Distance(new Vector2(0, 0), new Vector2(((float)listSize - 1) / 2, ((float)listSize - 1) / 2));
            for (int i = 0; i < listSize; i++)
            {
                for (int j = 0; j < listSize; j++)
                {
                    GameObject go = this.GetComponent<CubeList>().getCube(i, j);
                    float dist = Vector2.Distance(new Vector2(i, j), new Vector2(((float)listSize - 1) / 2, ((float)listSize - 1) / 2));
                    float distInFreq = dist / maxDist * windowSize;
                    int floor = Mathf.Max((int)Mathf.Floor(distInFreq), 1);
                    int ceil = Mathf.Max(Mathf.Min((int)Mathf.Ceil(distInFreq), windowSize), 1);
                    float ampli = (spect[ceil - 1] - spect[floor - 1]) * (distInFreq - floor) + spect[floor - 1];
                    float linearGained = ampli * ampliGain;
                    if (linearGained > highThreshold)
                        linearGained = highThreshold;
                    if (linearGained < lowThreshold && linearGained > 0.1)
                        linearGained = lowThreshold;
                    //linearGained = linearGained / linearMaxGained * maxGain;
                    go.GetComponent<CubeBehavior>().pulse(linearGained);
                }
            }
        }
       Camera.main.GetComponent<CircleSpectrum>().showSpectrum(spect);
	}
}
