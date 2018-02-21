using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour {

    public float position;
    public float acceleration;
    private float velocity;
    public float velocityinitial;
    public float time;
    public float drag;
    public float timeelapsed;
    public float frame;
    public bool damped;
    public float curPos;



    public float Force;
    public float SThrust;
    public float CThrust;
    public float positionZ;

    
	// Use this for initialization
	void Start () {
        //position = (velocityinitial * timeelapsed) + ((1 / 2) * (acceleration * timeelapsed * timeelapsed));
        Debug.Log(position);
       /* frame = 0;

        if (damped)
        {
            drag = (-1*acceleration) / Mathf.Pow(velocityinitial, 2);

        }
        else {
            
        }*/


        SThrust = Force * Mathf.Sin((GameObject.Find("boat").transform.eulerAngles.y * Mathf.PI) / 180);
        CThrust = Force * Mathf.Cos((GameObject.Find("boat").transform.eulerAngles.y * Mathf.PI) / 180);

    }
	
	// Update is called once per frame
	void FixedUpdate () {




        timeelapsed = Time.fixedDeltaTime * frame;


        GameObject.Find("Gunball(Clone)").transform.position = new Vector3(0, 0, positionZ);
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
