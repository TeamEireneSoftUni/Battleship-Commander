using UnityEngine;
using System.Collections;

public class shipTrail : MonoBehaviour {
    GameObject a;
	// Use this for initialization
	void Start ()
	{
	    a = (GameObject) Resources.Load("Trail");
	}
	
	// Update is called once per frame
	void Update () {
        Instantiate(a);
        a.transform.position = transform.position - transform.forward*10;
	}
}
