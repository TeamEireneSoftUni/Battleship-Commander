using System;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class AirplainMovement : MonoBehaviour
{
    private Camera MainCam;
    private bool inAttack;
    private bool isTurning;
    private bool isChangingAltitude;
    public GameObject target;

    // Use this for initialization
    void Start()
    {
        MainCam = (Camera)GameObject.Find("Main Camera").GetComponent<Camera>();
        target = GameObject.Find("MainShip");
        handler = TurnRight;
    }

    // Update is called once per frame
    void Update()
    {
        MainCam.transform.position = this.transform.position + new Vector3(0, 5, -10);
        MainCam.transform.LookAt(transform);

        Fly();
        Attack();
        //TurnRight();
        //TurnLeft();
        //NormalizeHorizontal();
        //NormalizeVetrical();
    }

    private void NormalizeHorizontal()
    {
        if (!isTurning)
        {
            var equal = isHorizontalAlligned();
            float rotateAngle = 0.5f;
            if (!equal && transform.up.x > 0)
            {
                transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle);
            }
            if (!equal && transform.up.x < 0)
            {
                transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle * -1);
            }
        }
    }
    private void NormalizeVetrical(float rotateAngle = 0.5f)
    {
        if (!isChangingAltitude)
        {
            var equal = AreVectorsInOnePlane(transform.forward, (target.transform.position - transform.position).normalized, transform.right);
            
            if (!equal && transform.up.z < 0)
            {
                transform.RotateAround(collider.bounds.center, transform.right, rotateAngle);
            }
            if (!equal && transform.up.z > 0)
            {
                transform.RotateAround(collider.bounds.center, transform.right, rotateAngle*-1);
            }
        }
    }
    void Fly()
    {
        transform.position += transform.forward * 0.2f;
    }
    void TurnLeft(float rotateAngle = 0.5f)
    {
        Vector3 upVector3 = new Vector3(1, 0, 0);

        var equal = AreVectorsInOnePlane(transform.forward, upVector3, transform.up);
        if (!equal && transform.up.y > 0)
        {
            transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle);
        }
        if (!equal && transform.up.y < 0)
        {
            transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle * -1);
        }
        if (equal)
        {
            transform.RotateAround(collider.bounds.center, transform.right, rotateAngle * -1);
        }
        else
        {
            transform.RotateAround(collider.bounds.center, new Vector3(0, 1, 0), rotateAngle * -1);
        }
        
    }

    void TurnRight(float rotateAngle = 0.5f)
    {
        Vector3 upVector3 = new Vector3(1, 0, 0);

        var equal = AreVectorsInOnePlane(transform.forward, upVector3, transform.up);
        if (!equal && transform.up.y < 0)
        {
            transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle);
        }
        if (!equal && transform.up.y > 0)
        {
            transform.RotateAround(collider.bounds.center, transform.forward, rotateAngle * -1);
        }
        if (equal)
        {
            transform.RotateAround(collider.bounds.center, transform.right, rotateAngle * -1);
        }
        else
        {
            transform.RotateAround(collider.bounds.center, new Vector3(0, 1, 0), rotateAngle * 1);
        }
    }

    private bool isHorizontalAlligned()
    {
        Vector3 upVector3 = new Vector3(0, 1, 0);
        return AreVectorsInOnePlane(transform.forward, upVector3, transform.up);
    }
    private bool isVerticalAlligned()
    {
        Vector3 upVector3 = new Vector3(0, 0, 1);
        return AreVectorsInOnePlane(transform.forward, upVector3, transform.right);
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

    private bool CompareFloats(float v1, float v2)
    {
        return Math.Abs(v1 - v2) < 1e-2;
    }

    void Normalize()
    {
        if (!isHorizontalAlligned())
        {
            NormalizeHorizontal();
        }
        //else if (!isVerticalAlligned())
        //{
        //    NormalizeVetrical();
        //}

    }

    void Attack()
    {
        AttackHorizontalAllignment();
    }

    private void AttackVerticalAllignment()
    {
    }

    private delegate void turnDir(float b = 0.5f);

    private turnDir handler;




    void AttackHorizontalAllignment()
    {
        Vector3 dirTotarget = new Vector3((target.transform.position - transform.position).x, 0, (target.transform.position - transform.position).z).normalized;
        Vector3 curDirection = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;


        

        bool goRight = (dirTotarget.x - curDirection.x) > 0;
        bool goLeft = (dirTotarget.x - curDirection.x) < 0;

        if (target.transform)
        {
            var forward = transform.TransformDirection(Vector3.forward);
            var toOther = target.transform.position - transform.position;
            if (Vector3.Dot(forward, toOther) < 0)
            {
                handler();
                return;
            }
        }

        Random random = new Random();
        int thisTime = random.Next() * (2 - 1) + 1;

        if (thisTime == 0)
        {
            handler = TurnLeft;
        }
        else
        {
            handler = TurnRight;
        }

        if (CompareFloats(dirTotarget.x - curDirection.x, 0))
        {
            goRight = false;
            goLeft = false;
            isTurning = false;
        }

        if (goRight)
        {
            isTurning = true;
            if (Math.Abs(Vector3.Angle(dirTotarget, curDirection)) > 1)
            {
                TurnRight();
            }
            else
            {
                TurnRight(0.1f);
            }

        }
        else
        if (goLeft)
        {
            isTurning = true;
            if (Math.Abs(Vector3.Angle(dirTotarget, curDirection)) > 1)
            {
                TurnLeft();
            }
            else
            {
                TurnLeft(0.1f);
            }
        }
        else
        {
            Normalize();
        }
    }
    
}
