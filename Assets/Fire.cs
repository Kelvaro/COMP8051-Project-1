using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float angle; //theta
    public float gunangle; // angle of the gun
    public float GunVelocityInitial;
    public float GunVelocity;
    public float speed;
    private float gravity = 9.81f;
    public GameObject gunball;
    public GameObject target;
    public float timeelapsed, timeelapsed2; // time elapsed and time elapsed upon IMPACT (Project 9)
    public float positionZ, positionZf;
    public float positionY;
    public float frame, frame2;
    public float cAngle;
    public float sAngle;
    public float tick;
    public float range;

    public float Rad;
    public float degree;
    public float Alpha;
    public float TwoAlpha;
    public float positionX;
    public float CAlpha;
    public float SAlpha;

    public float Gamma;
    public float CGamma;
    public float SGamma;

    public float SpeedYI;
    public float SpeedY;

    public float OmegaI;
    public float OmegaF;
    public float AngularAlpha; // represents Angular acceleration
    public float Theta;
    public float AngularDegree;
    public float FTheta;

    public float Kilogram;
    public float Cd;
    public float Tau;
    public float Vw;
    public float Cw;

    public float vx, vy, vz;
    public float DragVz, DragVx; // drag velocity? Wz? Wx?

    public float M1, M2; // Mass of Gunball and Target
    public float uiz, viz, uix, vix; // Speed of GUnball and Target
    public float e;
    public bool collided;
    public float Jz, Jx, vr, ufz, ufx, vfz, vfx;
    public float vf;
    public float M1iz, M1fz, M1ix, M1fx;
    public float M2iz, M2fz, M2ix, M2fx; //M2 initial velocity and position

    public float uin, uit, ufnz, ufnx, uftz, uftx;
    public float vin, vit, vfnz, vfnx, vftz, vftx;
    public float Jn;
    Vector3 ipg, ipt; //initial position of gunball and target
    public float ipg2z, ipg2x, ipt2z, ipt2x; //initial position of gunball and target when it COLLIDES
    public float TotalI, TotalF;
    public float r1z, r2z; // dunno it just means something to do with project 10. Collision Point apparently.
    public float r1x, r2x;
    public float normalZ, normalX; //calculated from r1 and r2;
    public float TangentialZ, TangentialX;

    public float ConversionMomentumZ, ConversionMomentumX;

    public float moveRight, moveZ, moveZ2, moveRight2;

    public float EnergyInitialGunballZ, EnergyInitialGunballX;
    public float EnergyInitialTargetZ, EnergyInitialTargetX;
    public float EnergyTotalZ, EnergyTotalX;
    public float EnergyFinalGunballZ, EnergyFinalGunballX;
    public float EnergyFinalTargetZ, EnergyFinalTargetX;
    public float EnergyFinalTotalZ, EnergyFinalTotalX;

    public float zPosGunball;
    public float r1, r2;
    public float Zcol, Xcol;
    //public var W1z, W2z; // ask what the W in the pdf means. Assuming it's rotation atm
    public float InertiaGunball, InertiaTarget;
    public float JMass1, JMass2;
    public float J;

    public Vector3 ColPoint;
    public Vector3 VecObj1;
    public Vector3 Obj1ToCol;//r1 in pdf file formula.
    public Vector3 VecObj2; //r2 in pdf file formula.
    public Vector3 Obt2ToCol;
    public Vector3 normal;
    public float AMi;
    public Vector3 LInitial, Lfinal1,Lfinal2, LfinalTotal;
    public float pi, pf;
 
    public Vector3 P1i, P2i , P2f, P1f;
    public Vector3 W1z, W2z;
    public float KineticEnergy, RotationalKineticEnergy, KineticEnergyFinal;
    // Use this for initialization
    void Start()
    {
        ipg = gunball.transform.position;
        ipt = target.transform.position;




        angle = (Mathf.Asin(gravity * (GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z) / Mathf.Pow(speed, 2)) / 2);
        gunangle = angle * 180 / Mathf.PI;
        cAngle = Mathf.Cos(gunangle * Mathf.PI / 180);
        sAngle = Mathf.Sin(gunangle * Mathf.PI / 180);


        //Instantiate(gunball, GameObject.Find("GunCentre").transform.position, Quaternion.identity);
        Debug.Log(GameObject.Find("GunCentre").transform.position);

        Rad = Mathf.Asin(gravity * (Mathf.Sqrt(Mathf.Pow(GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z, 2) + Mathf.Pow(GameObject.Find("Target").transform.position.x, 2)) / Mathf.Pow(speed, 2)));
        degree = Rad * (180 / Mathf.PI);
        Alpha = (180 - degree) / 2;


        CAlpha = Mathf.Cos(Alpha * Mathf.PI / 180);
        SAlpha = Mathf.Sin(Alpha * Mathf.PI / 180);

        Gamma = Mathf.Asin(GameObject.Find("Target").transform.position.x / Mathf.Sqrt(Mathf.Pow(GameObject.Find("Target").transform.position.z - GameObject.Find("GunCentre").transform.position.z, 2) + Mathf.Pow(GameObject.Find("Target").transform.position.x, 2)));
        CGamma = Mathf.Cos(Gamma);
        SGamma = Mathf.Sin(Gamma);
        //GameObject.Find("GunCentre").transform.eulerAngles = new Vector3(-gunangle, Gamma * 180 / Mathf.PI, 0); //Commented out for Project 9
        SpeedYI = speed * CAlpha; // what was this again?

        Tau = Kilogram / Cd;

        vx = speed * SGamma * SAlpha;//velocity of x in project 7?
        vy = speed * CAlpha;//velocity of y in project 7?
        vz = speed * CGamma * SAlpha; // velocity of z in project 7?
        //Note that Shaun got Vz and vx mixed up on the Excel Formula sheet.

        DragVz = ((Cw * Vw * CGamma) / Cd);
        DragVx = ((Cw * Vw * SGamma) / Cd);

        collided = false;
        //gunballCollider = GameObject.Find("Gunball").GetComponent<SphereCollider>();
        // targetCollider = GameObject.Find("Target").GetComponent<SphereCollider>();

        vr = uiz - viz;
        Jz = -vr * (e + 1) * M1 * M2 / (M1 + M2);
        vf = -Jz / M2 + viz;
        ufz = Jz / M1 + uiz;





    }

    // Update is called once per frame
    void FixedUpdate()
    {


        timeelapsed = Time.fixedDeltaTime * frame;
        frame++;
        tick += Time.deltaTime * frame;

        /* timeelapsed = Time.fixedDeltaTime * frame;
         frame++;
         tick += Time.deltaTime * frame; */
        // positionZ = GameObject.Find("GunCentre").transform.position.z + (speed * SAlpha * CGamma) * timeelapsed;

        // positionZ = vz * Tau * (1 - Mathf.Exp(-timeelapsed / Tau)) + DragVz * Tau * (1 - Mathf.Exp(-timeelapsed / Tau))  - (DragVz * timeelapsed); // project 7 Q 3a
        //vz = (Mathf.Exp(-timeelapsed / Tau) * vz) + ((Mathf.Exp(-timeelapsed/Tau) - 1) * DragVz); // project 7 Q 3a



        /*   SpeedY = SpeedYI - gravity * timeelapsed;
           positionY = (SpeedY * timeelapsed) - ((-gravity * Mathf.Pow(timeelapsed, 2) / 2));
           positionX = GameObject.Find("GunCentre").transform.position.x + (speed * SAlpha * SGamma) * timeelapsed;   */

        // positionY =  (vy * Tau * (1 - Mathf.Exp(-timeelapsed / Tau))) + (gravity * Tau * Tau * (1 - Mathf.Exp(-timeelapsed / Tau))) - (gravity*Tau*timeelapsed);  // project 7 Q 3b
        //vy = (Mathf.Exp(-timeelapsed / Tau) * vy) + ((Mathf.Exp(-timeelapsed / Tau) - 1) * (gravity * Tau) ); // project 7 Q 3b

        //  positionX = vx * Tau * (1 - Mathf.Exp(-timeelapsed / Tau)) +  DragVx * Tau * (1 - Mathf.Exp(-timeelapsed / Tau))   - (DragVx * timeelapsed)  ; // project 7 Q 3c
        //vx = (Mathf.Exp(-timeelapsed / Tau) * vx) + ((Mathf.Exp(-timeelapsed / Tau) - 1) * DragVx); // project 7 Q 3c




        Theta = (OmegaF * Time.deltaTime) + ((AngularAlpha * Time.deltaTime * Time.deltaTime) / 2f);
        OmegaF = OmegaI + AngularAlpha * timeelapsed;
        FTheta += Theta;
        AngularDegree = Theta * Mathf.Rad2Deg;

        if (collided == false)
        {
            M2iz = M2 * viz;
            M2ix = M2 * vix;
            M1iz = M1 * uiz;
            M1ix = M1 * uix;
            TotalI = M2iz + M1iz;
            gunball.transform.position = ipg + Vector3.forward * timeelapsed * uiz;
            target.transform.position = ipt + Vector3.forward * timeelapsed * viz;


            EnergyInitialGunballZ = M1 * uiz * uiz * 0.5f;
            EnergyInitialTargetZ = M2 * viz * viz * 0.5f;

            EnergyTotalZ = EnergyInitialGunballZ + EnergyInitialTargetZ;
            EnergyTotalX = EnergyInitialGunballX + EnergyInitialTargetX;


        }
        if (collided == true)
        {


            EnergyFinalGunballZ = 0.5f * M1 * ufz * ufz;
            EnergyFinalGunballX = 0.5f * M1 * ufx * ufx;

            EnergyFinalTargetZ = 0.5f * M2 * vfz * vfz;
            EnergyFinalTargetX = 0.5f * M2 * vfx * vfx;

            EnergyFinalTotalZ = EnergyFinalGunballZ + EnergyFinalTargetZ;
            EnergyFinalTotalX = EnergyFinalGunballX + EnergyFinalTargetX;


            moveRight = Time.fixedDeltaTime * M1fx;
            moveZ = Time.fixedDeltaTime * M1fz;

            moveRight2 = Time.fixedDeltaTime * M2fx;
            moveZ2 = Time.fixedDeltaTime * M2fz;

            TotalF = M1fz + M2fz;

            gunball.transform.position += new Vector3(moveRight, 0, moveZ);
            //(ipg2 + Vector3.forward * timeelapsed * M1fz) +(Vector3.right * M1fx);

            target.transform.position += new Vector3(moveRight2, 0, moveZ2);
            //(ipt2 + Vector3.forward * timeelapsed * M2fz) + (Vector3.right * M2fx);
             

            gunball.transform.Rotate(W1z * Time.fixedDeltaTime);
            target.transform.Rotate(W2z * Time.fixedDeltaTime);



         

        }

        //Debug.Log(positionZ + ", " + positionY + ", " + positionX);
        //Debug.Log(positionX + ", "+ positionY + ", " + positionZ);



        // GameObject.Find("Gunball(Clone)").transform.eulerAngles = new Vector3(-AngularDegree, 0);
        // GameObject.Find("Gunball(Clone)").transform.Rotate(Vector3.right * -AngularDegree);

        //Debug.Log("gunball position z: " + gunball.transform.position.z + ", " + target.transform.position.z + " at " + timeelapsed);

        //Debug.Log(timeelapsed);

    }



    void OnTriggerEnter(Collider collision)
    {


        Debug.Log("collided");
        collided = true;
        timeelapsed = 0;
        frame = 0;
        ipg2z = gunball.transform.position.z;
        ipt2z = target.transform.position.z;



        ipg2x = gunball.transform.position.x;
        ipt2x = target.transform.position.x;

        UnityEditor.EditorApplication.isPaused = true;

        //ipg = GameObject.Find("Gunball").transform.position.z;
        //ipt = GameObject.Find("Target").transform.position.z;

        r1z = gunball.transform.position.z;
        r1x = gunball.transform.position.x;

        r2z = target.transform.position.z;
        r2x = target.transform.position.x;


        
        float lord = target.transform.position.z - 1f;
        Zcol = target.transform.position.z - 0.5f;
        Xcol = gunball.transform.position.x + ((target.transform.position.x - gunball.transform.position.x) / 2);
        Vector3 goat = gunball.transform.position;
        goat.z = lord;
        gunball.transform.position = goat;





        ColPoint = new Vector3(Xcol, 0, Zcol);

        VecObj1 = gunball.transform.position;

        Obj1ToCol = ColPoint - VecObj1; //r1 in pdf file formula.

        VecObj2 = target.transform.position;

        Obt2ToCol = ColPoint - VecObj2; //r2 in pdf file formula.
        


        normalZ = r2z - r1z;
        normalX = r2x - r1x;

        normal = new Vector3(0, 0, 1);

        TangentialZ = -1 * normalX;
        TangentialX = 1 * normalZ;

        Jn = (Jz * normalZ) + (Jx * normalX); //Never used this in project 11 according to Jason. DOuble check in case
        
        uin = uiz * normalZ + uix * normalX;
        uit = uiz * TangentialZ + uix * TangentialX;
        vin = viz * normalZ + vix * normalX;
        vit = viz * TangentialZ + vix * TangentialX;

        ufnz = (Jn / M1 + uin) * normalZ;
        ufnx = (Jn / M1 + uin) * normalX;

        uftz = uit * TangentialZ;
        uftx = uit * TangentialX;

        vfnz = ((-Jn / M2) + vin) * normalZ; // fix values
        vfnx = ((-Jn / M2) + vin) * normalX; // incorrect values

        vftz = vit * TangentialZ;
        vftx = vit * TangentialX;

      /*  ufz = ufnz + uftz;
        ufx = ufnx + uftx;

        vfz = vfnz + vftz;
        vfx = vfnx + vftx; */





        M1fz = M1 * ufz;
        M1fx = M1 * ufx;
        //Zg = ipg + M1f * timeelapsed;

        M2fz = M2 * vfz;
        M2fx = M2 * vfx;
        //Zt = ipt + M2f * timeelapsed;

        InertiaGunball = M1 * (Mathf.Pow(gunball.transform.localScale.x,2) + Mathf.Pow(gunball.transform.localScale.y,2 )) / 12;
        InertiaTarget = M2 * (Mathf.Pow(target.transform.localScale.x, 2) + Mathf.Pow(target.transform.localScale.y, 2)) / 12;
            

        //N DOT [ (object1tCol CROSS normal / InertialGunball) CROSS object1toCol ]
        var JmassGP1 = Vector3.Cross(Obj1ToCol, normal/InertiaGunball);
        var JmassGP2 = Vector3.Cross(JmassGP1, Obj1ToCol);
        JMass1 = Vector3.Dot(normal,JmassGP2);


        var JmassTP1 = Vector3.Cross(Obt2ToCol, normal / InertiaTarget);
        var JmassTP2 = Vector3.Cross(JmassTP1, Obj1ToCol);
        JMass2 = Vector3.Dot(normal, JmassTP2);

        J = -vr * (e + 1) * (1 / (((1 / M1) + (1 / M2)) + JMass1 + -JMass2));

        ufz = (J / M1) + uiz;
        vfz = (-J / M2) + viz;

        P1i = new Vector3(0,0, M1 * uiz);  //P1 initial on pdf
        P2i = new Vector3(0,0,M2 * viz);  //P2 initial on pdf
        pi = P1i.z  + P2i.z; //pi final

        P1f = new Vector3(0,0,M1 * ufz);  //P1 final on pdf
        P2f = new Vector3(0,0,M2 * vfz); //P2 final on pdf
        pf = P1f.z + P2f.z; //pf final

        W1z = Vector3.Cross(Obj1ToCol, -J * normal) / InertiaGunball; //omega 1 on pdf
        W2z = Vector3.Cross(Obt2ToCol, J * normal) / InertiaTarget; //omega 2 on pdf


        LInitial = Vector3.Cross(Obj1ToCol, P1i) + Vector3.Cross(Obt2ToCol, P2i); //Angular Momentum
        Lfinal1 = Vector3.Cross(Obj1ToCol, P1f) + (InertiaGunball * W1z);
        Lfinal2 = Vector3.Cross(Obt2ToCol, P2f) + (InertiaTarget * W2z);
        LfinalTotal = Lfinal1 + Lfinal2;


        KineticEnergy = ((M1 * ufz * ufz) / 2) + ((M2 * vfz * vfz) / 2);
        RotationalKineticEnergy = ((InertiaGunball * W1z.y * W1z.y) / 2) + ((InertiaTarget * W2z.y * W2z.y) / 2);

        KineticEnergyFinal = KineticEnergy + RotationalKineticEnergy;




    }






    /*
    1) how is normalizedZ = 0 when you look at the r1z and r2z point?

    2) is ufn from project 10 same as ufz in project 11?

    3) 1kg m^2 

    4) r1 in 3 c iii

    5) omfg... wth is a vector again?

    6) What is Lcase R1 and R2 in his pdf?

    7) What is Jmass 1 and 2 in his pdf?

    8) what is k if normalized = k?


    */


}
