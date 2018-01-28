using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

   
    public static int HullMass = 500;
    public int TotalMass;
    public static int TotalM;
    public double HullMomentOfInertia;
    public static double HMoI;
    

	// Use this for initialization
	void Start () {
        TotalMass = HullMass + Gun.GunMass + Pilot.PilotMass;
        HullMomentOfInertia = ((HullMass) * ((Math.Pow(GameObject.Find("Hull").transform.localScale.x, 2)) + Math.Pow(GameObject.Find("Hull").transform.localScale.z,2)))/12;
        HMoI = HullMomentOfInertia;
        //Math.Pow((GameObject.Find("COM").transform.position.x)-(GameObject.Find("Hull").transform.position.x), 2) + Math.Pow((GameObject.Find("COM").transform.position.z)- (GameObject.Find("Hull").transform.position.z),2);
        //(Math.Pow(((GameObject.Find("COM").transform.position.x)-(GameObject.Find("Hull").transform.position.x)),2)) + Math.Pow(((GameObject.Find("COM").transform.position.z) - (GameObject.Find("Hull").transform.position.z)), 2);

        TotalM = TotalMass;
       
        Debug.Log("The mass of the hull is: " + HullMass + "g");
        Debug.Log("The Total mass is " + TotalMass + "g");
        

    }

    // Update is called once per frame
    void Update () {
		
	}
}
