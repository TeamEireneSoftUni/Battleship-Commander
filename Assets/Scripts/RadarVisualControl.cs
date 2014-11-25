using UnityEngine;
using System.Collections;

public class RadarVisualControl : MonoBehaviour
{

    public bool isTurnedOn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (isTurnedOn)
	    {
	        RadarVisuals();
	    }
	}

    void RadarVisuals()
    {
        transform.RotateAround(collider.bounds.center, transform.up, 5f);
    }
}
