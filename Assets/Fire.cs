using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public float angle;
    public float gunangle;
    public float GunVelocityInitial;
    public float GunVelocity;
    public float speed;
    private float gravity = 9.81f;

	// Use this for initialization
	void Start () {

        angle = (Mathf.Asin(gravity * GameObject.Find("Target").transform.position.z / Mathf.Pow(speed,2))/2 ) ;
        gunangle = angle * 180 / Mathf.PI;
        GameObject.Find("GunCentre").transform.eulerAngles = new Vector3(-gunangle, 0, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
