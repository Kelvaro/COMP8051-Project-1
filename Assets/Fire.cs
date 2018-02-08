using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public float angle; //theta
    public float gunangle; // angle of the gun
    public float GunVelocityInitial;
    public float GunVelocity;
    public float speed;
    private float gravity = 9.81f;
    public GameObject gunball;
    public float timeelapsed;
    public float positionZ;
    public float positionY;
    public float frame;
    public float cAngle;
    public float sAngle;
    public float tick;
    public float range;

    public float Rad;
    public float degree;
    public float Alpha;
    public float positionX;
    public float CAlpha;
    public float SAlpha;

    public float Gamma;
    public float CGamma;
    public float SGamma;

    public float SpeedYI;
    public float SpeedY;
   
	// Use this for initialization
	void Start () {
        angle = (Mathf.Asin(gravity * (GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z) / Mathf.Pow(speed,2))/2 ) ;
        gunangle = angle * 180 / Mathf.PI;
        cAngle = Mathf.Cos(gunangle * Mathf.PI/ 180);
        sAngle = Mathf.Sin(gunangle * Mathf.PI / 180);
        
       
        Instantiate(gunball, GameObject.Find("GunCentre").transform.position, Quaternion.identity);

       
        Rad = Mathf.Asin(gravity * (Mathf.Sqrt(Mathf.Pow(GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z, 2) + Mathf.Pow(GameObject.Find("Target").transform.position.x, 2)) / Mathf.Pow(speed, 2)));
        degree = Rad * (180 / Mathf.PI);
        Alpha = (180 - degree) / 2;
        GameObject.Find("GunCentre").transform.eulerAngles = new Vector3(-gunangle, degree/2, 0);

        CAlpha = Mathf.Cos(Alpha * Mathf.PI / 180);
        SAlpha = Mathf.Sin(Alpha * Mathf.PI / 180);

        Gamma = Mathf.Asin(GameObject.Find("Target").transform.position.x / Mathf.Sqrt(Mathf.Pow(GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z, 2) + Mathf.Pow(GameObject.Find("Target").transform.position.x , 2) ));
        CGamma = Mathf.Cos(Gamma);
        SGamma = Mathf.Sin(Gamma);

        SpeedYI = speed * CAlpha;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        
        timeelapsed = Time.fixedDeltaTime * frame;
        frame++;
        tick += Time.deltaTime * frame;
        positionZ = GameObject.Find("GunCentre").transform.position.z + (speed * SAlpha * CGamma) * timeelapsed;
        positionY = SpeedY * timeelapsed - ((-gravity * timeelapsed * timeelapsed)/ 2);
        SpeedY = SpeedYI - gravity * timeelapsed;

        positionX = GameObject.Find("GunCentre").transform.position.x + (speed * SAlpha * SGamma) * timeelapsed;

        GameObject.Find("Gunball(Clone)").transform.position = new Vector3(positionX,positionY,positionZ);
     
        if (positionY <= 0.05 && tick > 0.5) {
            UnityEditor.EditorApplication.isPaused = true;
        }

	}
}
