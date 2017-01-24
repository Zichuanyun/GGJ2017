using UnityEngine;
using System.Collections;
public class SpectrumLine : MonoBehaviour {
    int windowSize;
    float[] spect;
    public float ampli;
    public float lowAmp;
    public LineRenderer lineRenderer;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void showSpectrum(float[] _spect)
    {
        spect = _spect;
        windowSize = spect.GetLength(0);
        lineRenderer.SetVertexCount(windowSize);
        for (int i = 0; i < windowSize; i++)
        {
            float theta = (float)i / (float)windowSize * 2f * Mathf.PI - Mathf.PI / 2;
            float mag = spect[i] * ampli + lowAmp;
            Vector3 vertex = new Vector3(mag * Mathf.Cos(theta), mag * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i,vertex);
        }
    }
}
