using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class COM : MonoBehaviour {

    public int TMass;
    private float COMPoSz;
    private float COMPoSx;
    public double HullH2;
    public double HullMH2;
    public double HullTotalMomentOfInertia;
    public double GunH2;
    public double GunMH2;
    public double GunTotalMomentOfInertia;
    public double PilotH2;
    public double PilotMH2;
    public double PilotTotalMomentOfInertia;
    public double COMTotalMomentofInertia;


    // Use this for initialization
    void Start () {
     
        calculateCOM();
        
        Debug.Log("The mass of COM is: " + COMPoSx + "m, " + COMPoSz+"m" );

        HullH2 = Math.Pow((GameObject.Find("COM").transform.position.x) - (GameObject.Find("Hull").transform.position.x), 2) + Math.Pow((GameObject.Find("COM").transform.position.z) - (GameObject.Find("Hull").transform.position.z), 2); 
        Debug.Log("The Hull H2 is: " + HullH2);

        HullMH2 = HullH2 * Boat.HullMass;

        HullTotalMomentOfInertia = Boat.HMoI + HullMH2;
        Debug.Log(Boat.HMoI + " check value");

        GunH2 = Math.Pow((GameObject.Find("COM").transform.position.x) - (GameObject.Find("GunCentre").transform.position.x), 2) + Math.Pow((GameObject.Find("COM").transform.position.z) - (GameObject.Find("GunCentre").transform.position.z), 2);
        Debug.Log("The Gun H2 is: " + HullH2);

        GunMH2 = GunH2 * Gun.GunMass;

        GunTotalMomentOfInertia = Gun.GMoI + GunMH2;
      

        PilotH2 = Math.Pow((GameObject.Find("COM").transform.position.x) - (GameObject.Find("Pilot").transform.position.x), 2) + Math.Pow((GameObject.Find("COM").transform.position.z) - (GameObject.Find("Pilot").transform.position.z), 2);
        Debug.Log("The Pilot H2 is: " + HullH2);

        PilotMH2 = PilotH2 * Pilot.PilotMass;

        PilotTotalMomentOfInertia = PilotMH2 + Pilot.PMoI;
    }

    void calculateCOM() {
        TMass = Boat.TotalM;
        COMPoSz = ((Gun.GunMass * GameObject.Find("GunCentre").transform.position.z) + (Boat.HullMass * GameObject.Find("Hull").transform.position.z) + (Pilot.PilotMass * GameObject.Find("Pilot").transform.position.z)) / TMass ;
        COMPoSx = ((Gun.GunMass * GameObject.Find("GunCentre").transform.position.x) + (Boat.HullMass * GameObject.Find("Hull").transform.position.x) + (Pilot.PilotMass * GameObject.Find("Pilot").transform.position.x)) / TMass;

        transform.position = new Vector3(COMPoSx,0,COMPoSz);


    }
	
	// Update is called once per frame
	void Update () {

        calculateCOM();

        GunH2 = Math.Pow((GameObject.Find("COM").transform.position.x) - (GameObject.Find("GunCentre").transform.position.x), 2) + Math.Pow((GameObject.Find("COM").transform.position.z) - (GameObject.Find("GunCentre").transform.position.z), 2);
    

        GunMH2 = GunH2 * Gun.GunMass;

        GunTotalMomentOfInertia = Gun.GMoI + GunMH2;

        PilotH2 = Math.Pow((GameObject.Find("COM").transform.position.x) - (GameObject.Find("Pilot").transform.position.x), 2) + Math.Pow((GameObject.Find("COM").transform.position.z) - (GameObject.Find("Pilot").transform.position.z), 2);
        

        PilotMH2 = PilotH2 * Pilot.PilotMass;

        PilotTotalMomentOfInertia = PilotMH2 + Pilot.PMoI;

        COMTotalMomentofInertia = HullTotalMomentOfInertia + GunTotalMomentOfInertia + PilotTotalMomentOfInertia;
    }
}
