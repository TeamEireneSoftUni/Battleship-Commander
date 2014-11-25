using UnityEngine;
using System.Collections;

public class proximity : MonoBehaviour
{

    private float evadeTime = 5;
    private GameObject evadeTarget;
    public GameObject orgTarget;


	// Use this for initialization
	void Start ()
	{
        evadeTarget = new GameObject();
        evadeTarget.transform.position = new Vector3(1000,1000,1000);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    void OnTriggerEnter(Collider c)
    {
        if (!orgTarget)
        {
            orgTarget = this.gameObject.GetComponent<AirplainMovement>().target;
        }

        this.gameObject.GetComponent<AirplainMovement>().target = evadeTarget;
    }

    void OnTriggerExit(Collider c)
    {
        this.gameObject.GetComponent<AirplainMovement>().target = orgTarget;
    }
}
