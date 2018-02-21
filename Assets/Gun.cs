using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Gun : MonoBehaviour {


    public static int GunMass = 0;
    public double GunMomentOfInertia;
    public static double GMoI;
	// Use this for initialization
	void Start () {

        GunMomentOfInertia = ((GunMass) * ((Math.Pow(GameObject.Find("GunCentre").transform.localScale.x, 2)) + Math.Pow(GameObject.Find("GunCentre").transform.localScale.z, 2))) / 12;
        GMoI = GunMomentOfInertia;
        Debug.Log("The mass of the Gun is: " + GunMass + "g");
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
