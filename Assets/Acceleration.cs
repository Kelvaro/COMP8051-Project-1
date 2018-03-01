using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{

    public float position;
    public float acceleration;
    public float LinearAcceleration;
    private float velocity;
    public float velocityinitial;
    public float time;
    public float drag;
    public float timeelapsed;
    public float frame;
    public bool damped;
    public float curPos;



    public float LinearForce;
    public float SThrust;
    public float CThrust;
    public float positionZ;
    public float angle;

    public float distance;
    public int COMMass;
    public float CurPos;


    public float AngularForce, AngularSinForce, AngularCosForce;
    public float RLeftX, RLeftZ;
    public float RRightX, RRightZ;
    public float TorqueLeft, TorqueRight;
    public float AccelerationLeft, AccelerationRight;
    public float ITotal;

    public float displacementLeft, displacementRight;
    public float Tleft, TRight;
    public float TV, RV;

    public float leftDistance, rightDistance;


    // Use this for initialization
    void Start()
    {
        //position = (velocityinitial * timeelapsed) + ((1 / 2) * (acceleration * timeelapsed * timeelapsed));
        Debug.Log(position);
        /* frame = 0;

         if (damped)
         {
             drag = (-1*acceleration) / Mathf.Pow(velocityinitial, 2);

         }
         else {

         }*/


        SThrust = LinearForce * Mathf.Sin((angle * Mathf.PI) / 180);
        CThrust = LinearForce * Mathf.Cos((angle * Mathf.PI) / 180);
        LinearAcceleration = CThrust / Boat.TotalM;
        COMMass = Boat.TotalM;
        RLeftX = 2 - GameObject.Find("COM").transform.position.x;
        RRightX = -2 - GameObject.Find("COM").transform.position.x;

        RLeftZ = -4 - GameObject.Find("COM").transform.position.z;
        RRightZ = -4 - GameObject.Find("COM").transform.position.z;

        AngularSinForce = AngularForce * Mathf.Sin((angle * Mathf.PI) / 180);
        AngularCosForce = AngularForce * Mathf.Cos((angle * Mathf.PI) / 180);
        TorqueLeft = (RLeftZ * AngularSinForce) - (RLeftX * AngularCosForce);
        TorqueRight = (RRightZ * AngularSinForce) - (RRightX * AngularCosForce);
        AccelerationLeft = TorqueLeft / COM.COMItotal;
        AccelerationRight = TorqueRight / COM.COMItotal;



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AccelerationLeft = TorqueLeft / COM.COMItotal;
        AccelerationRight = TorqueRight / COM.COMItotal;

        ITotal = COM.COMItotal;

        timeelapsed = Time.fixedDeltaTime * frame;
        frame++;

        CurPos = (LinearAcceleration * timeelapsed * timeelapsed) / 2;
        positionZ = (Mathf.Sqrt((2 * distance) / LinearAcceleration));

        //positionZ=(speed * timeelapsed) + ((acceleration * timeelapsed * timeelapsed) / 2);

        TV = Mathf.Abs(AccelerationLeft) * timeelapsed;
        RV = Mathf.Abs(AccelerationRight) * timeelapsed;

        Tleft = (AccelerationLeft * (timeelapsed * timeelapsed)) / 2;
        TRight = (AccelerationRight * (timeelapsed * timeelapsed)) / 2;

        transform.position = new Vector3(Tleft, 0, CurPos);
        //transform.eulerAngles = new Vector3(Tleft, TRight, 0);




        if (CurPos >= distance)
        {
            UnityEditor.EditorApplication.isPaused = true;
        }






        /* if (damped) {


             timeelapsed = Time.fixedDeltaTime * frame;
             frame++;
             velocity = velocityinitial / (1 + drag * velocityinitial * time);

             curPos = (Mathf.Log((1 + drag * velocityinitial * timeelapsed)) / drag);
             transform.position = new Vector3(0, 0, curPos);



             if (curPos >= position) {
                 UnityEditor.EditorApplication.isPaused = true;
             }
         }  

         else { 

         timeelapsed = Time.fixedDeltaTime * frame;
         frame++;

         //uses Sf= Si + viT + 1/2 at ^2  a * t * t / 2
         curPos = (velocityinitial * timeelapsed) + ((acceleration * timeelapsed * timeelapsed)/2);

             //updates the velocity with vf = vi + at for constant acceleration
             velocity = velocityinitial + acceleration * timeelapsed;
         transform.position = new Vector3(0, 0, curPos);

         if ( curPos >= position) {
             UnityEditor.EditorApplication.isPaused = true;
         }

         }   */ //this is the lab project 1 you've done.

    }
}
