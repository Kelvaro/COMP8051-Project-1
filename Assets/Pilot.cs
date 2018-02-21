using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pilot : MonoBehaviour {


    public static int PilotMass = 500;
    public double PilotMomentOfInertia;
    public static double PMoI;
    GameObject pilot;
	// Use this for initialization
	void Start () {


        PilotMomentOfInertia = ((PilotMass) * ((Math.Pow(GameObject.Find("Pilot").transform.localScale.x, 2)) + Math.Pow(GameObject.Find("Pilot").transform.localScale.z, 2))) / 12; ;
        PMoI = PilotMomentOfInertia;
        Debug.Log("The mass of the Pilot is: " + PilotMass + "g");

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
