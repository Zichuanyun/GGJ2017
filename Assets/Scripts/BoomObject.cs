using UnityEngine;
using System.Collections;

public class BoomObject  {
    public Vector2 position;
    public float time;
    // Use this for initialization
    public BoomObject(Vector2 _position, float duration)
    {
        position = _position;
        time = duration;
    }
}
