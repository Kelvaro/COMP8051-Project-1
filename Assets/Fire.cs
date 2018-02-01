using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public float angle; //theta
    public float gunangle; // angle of the gun
    public float GunVelocityInitial;
    public float GunVelocity;
    public float speed;
    private float gravity = -9.81f;
    public GameObject gunball;
    public float timeelapsed;
    public float positionX;
    public float positionY;
    public float frame;
    public float cAngle;
    public float sAngle;
    public float tick;
    public float range;
   
	// Use this for initialization
	void Start () {
        angle = (Mathf.Asin(gravity * (GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z) / Mathf.Pow(speed,2))/2 ) ;
        gunangle = angle * 180 / Mathf.PI;
        cAngle = Mathf.Cos(gunangle * Mathf.PI/ 180);
        sAngle = -Mathf.Sin(gunangle * Mathf.PI / 180);
        GameObject.Find("GunCentre").transform.eulerAngles = new Vector3(gunangle, 0, 0);
       
        Instantiate(gunball, GameObject.Find("GunCentre").transform.position, Quaternion.identity);
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        
        timeelapsed = Time.fixedDeltaTime * frame;
        frame++;
        tick += Time.deltaTime * frame;
        positionX = GameObject.Find("GunCentre").transform.position.z + speed * cAngle * timeelapsed;
        positionY = ((speed * sAngle) * timeelapsed) + ((gravity / 2) * Mathf.Pow(timeelapsed,2));

        GameObject.Find("Gunball(Clone)").transform.position = new Vector3(0,positionY,positionX);
     
        if (positionY <= 0.05 && tick > 0.5) {
            UnityEditor.EditorApplication.isPaused = true;
        }

	}
}
