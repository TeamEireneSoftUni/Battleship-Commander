using System;
using UnityEngine;
using System.Collections;

public class EnemyMotion : MonoBehaviour {
    private Camera MainCam;
    public GameObject target;

	// Use this for initialization
	void Start () {
        MainCam = (Camera)GameObject.Find("Main Camera").GetComponent<Camera>();
        target = GameObject.Find("MainShip");
    }
	
	// Update is called once per frame
	void Update () {
        MainCam.transform.position = this.transform.position + new Vector3(0, 5, -10);
        MainCam.transform.LookAt(transform);

        Fly();
        TurnRight(target.transform.position.normalized);
        TurnDown((target.transform.position - transform.position).normalized);
	}

    void Fly()
    {
        transform.position += transform.forward * 0.2f;
    }

    private float rotAng = 0;

    void TurnLeft(float rotateAngle = 0.5f)
    {
        rotAng += rotateAngle;
        if(rotAng <= 90)
        transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle);

        transform.RotateAround(collider.bounds.center, new Vector3(0,1,0), rotateAngle *-1);
    }

    void TurnRight(Vector3 targetVector)
    {
        float rotateAngle = (float)Math.Round(Vector3.Angle(new Vector3(targetVector.x,0,targetVector.z) , new Vector3(transform.forward.x, 0, transform.forward.z)),2);
        if (rotateAngle > 0.5f)
        {
            rotateAngle = 0.5f;
        }

        //rotAng -= rotateAngle;
        //if (rotAng >= -90)
        //{
        //    transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle * -1);
        //}

        transform.RotateAround(collider.bounds.center, new Vector3(0, 1, 0), rotateAngle);
    }

    void TurnDown(Vector3 downVector)
    {
        float rotateAngle = (float)Math.Round(Vector3.Angle(downVector, new Vector3(downVector.x,transform.forward.y,downVector.z)),2);
        //print(new Vector3(0, downVector.y, 0));
        //print(new Vector3(0, transform.forward.y, 0));
        if (rotateAngle > 0.5f)
        {
            rotateAngle = 0.5f;
        }
        transform.RotateAround(collider.bounds.center, new Vector3(transform.right.x,0,transform.right.z), rotateAngle);

    }

    private bool AreVectorsInOnePlane(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        var delta = (v1.x * v2.y * v3.z)
            + (v1.y * v2.z * v3.x)
            + (v1.z * v2.x * v3.y)
            - (v1.z * v2.y * v3.x)
            - (v1.x * v2.z * v3.y)
            - (v1.y * v2.x * v3.z);
        return Math.Abs(0f - delta) < 1e-2;
    }
}
