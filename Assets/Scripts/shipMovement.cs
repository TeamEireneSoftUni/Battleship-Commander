using UnityEngine;
using System.Collections;

public class shipMovement : MonoBehaviour
{

    private float speed = 0f;
    private float turnSpeed = 0f;
    //private Camera MainCam;
    private const float SHAKE_INTERVAL = 5;
    private float shakeCounter = 0;


	// Use this for initialization
	void Start ()
	{
        //MainCam = (Camera) GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{

        //MainCam.transform.position = this.transform.position + new Vector3(0,10,-20);


        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (speed < 10)
            {
                speed += 1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (speed > -10)
            {
                speed -= 1f;
            }
        }

	    if (Input.GetKey(KeyCode.Z))
	    {
            this.transform.Rotate(Vector3.up, 0.2f);
	        
	    }else if (Input.GetKey(KeyCode.X))
        {
            this.transform.Rotate(Vector3.up, -0.2f);
        }
        else
        {
            turnSpeed = 0;
        }
        MoveForward();
	}

    private void MoveForward()
    {
        this.collider.rigidbody.AddRelativeForce(Vector3.forward * speed * this.collider.rigidbody.mass);
        //shakeCounter += 0.1f;
        //float hitpoint = 10;
        //if (shakeCounter >= SHAKE_INTERVAL)
        //{
        //    shakeCounter = 0;
        //    this.collider.rigidbody.AddForceAtPosition((new Vector3(Vector3.up.x,Vector3.up.y,Vector3.up.z)) * collider.rigidbody.mass * 20, collider.bounds.max);
        //    Debug.Log(shakeCounter);
        //}
    }


}
