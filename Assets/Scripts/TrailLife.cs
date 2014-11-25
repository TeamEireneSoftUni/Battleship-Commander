using UnityEngine;
using System.Collections;

public class TrailLife : MonoBehaviour
{
    private float timeOut = 3;
    GameObject ship;
	// Use this for initialization
	void Start ()
	{
	    ship = GameObject.Find("MainShip");
        //transform.localRotation = Quaternion.Euler(ship.transform.forward);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timeOut -= Time.deltaTime;
        transform.localScale += new Vector3(0.1f,0,0);
	    if (timeOut <= 0f)
	    {
	        Destroy(this.gameObject);
	    }
	}
}
