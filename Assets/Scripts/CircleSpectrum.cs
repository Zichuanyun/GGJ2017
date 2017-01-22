using UnityEngine;
using System.Collections;

public class CircleSpectrum : MonoBehaviour {
    public int windowSize;
    public float ampli;
    public float lowAmp;
    float[] spect;
    public Material mat;
    public float depth;

    public float y_offset = 10f;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //spect = AudioListener.GetSpectrumData(windowSize, 0, FFTWindow.Hamming);
        //showSpectrum(spect);
    }
  
    public void showSpectrum(float[] _spect)
    {
        spect = _spect;
        windowSize = spect.GetLength(0);
        GL.Clear(false,false,Color.black);
        mat.SetPass(0);
        for (int i = 0; i < windowSize-1; i++)
        {
            GL.PushMatrix();
            GL.Begin(GL.LINES);
            GL.Color(new Color(0.7f, 0f, (float)i / (float)windowSize)*2);
            float theta = (float)i / (float)windowSize * 2f*Mathf.PI-Mathf.PI/2;
            float mag = spect[i] * ampli+lowAmp;
            GL.Vertex(new Vector3(mag * Mathf.Cos(theta), mag * Mathf.Sin(theta) + y_offset, depth));
            theta = (float)(i+1) / (float)windowSize * 2f * Mathf.PI-Mathf.PI / 2;
            mag = spect[i+1] * ampli + lowAmp;
            GL.Vertex(new Vector3(mag * Mathf.Cos(theta), mag * Mathf.Sin(theta) + y_offset, depth));
            GL.End();
            GL.PopMatrix();
        }
    }
    void OnPostRender()
    {
        showSpectrum(spect);
    }
}
