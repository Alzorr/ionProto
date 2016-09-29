using UnityEngine;
using System.Collections;

public class nucleusScript : MonoBehaviour
{
    //COUNTERS AND TIMERS
    public int protonCount= 0;
    public int electronCount =0;
    public int neutronCount =0;

    //EXTERNAL GAMEOBJECTS AND SCRIPTS
    public Transform nucleus;
    private Vector3 desiredPosition;

    //ORBIT SPEED/RADIUS/SPIN BY SHELL AND PARTICLE
    //ELECTRON
        //Shell 0
    public float shell0Radius = 4f;
    public float shell0Speed = 20f;
    public float shell0Spin = 1f;
        //Shell 1
    public float shell1Radius = 6f;
    public float shell1Speed = 30f;
    public float shell1Spin = 1f;
        //Shell 2
    public float shell2Radius = 9f;
    public float shell2Speed = 40f;
    public float shell2Spin = 1f;
        //Shell 3
    public float shell3Radius = 12f;
    public float shell3Speed = 50f;
    public float shell3Spin = 1f;
    //PROTON
    public float protonRadius = 0.2f;
    public float protonSpeed = 40f;
    public float protonSpin = 1f;
    //NEUTRON
    public float neutronRadius = 0.2f;
    public float neutronSpeed = 50f;
    public float neutronSpin = 1f;

    //GAMEOBJECT ARRAYS OF THE ELECTRONS BY SHELL
    public GameObject[] shell0 = new GameObject[2];
    public GameObject[] shell1 = new GameObject[8];
    public GameObject[] shell2 = new GameObject[8];
    public GameObject[] shell3 = new GameObject[2];

    //AND THE PROTONS AND NEUTRONS, ARRAY SIZE = MAX SIZE
    public GameObject[] protons = new GameObject[20];
    public GameObject[] neutrons = new GameObject[20];

    //CHARGE CALCULATOR
    public bool positiveCharge = false;
    public bool negativeCharge = false;
    public bool neutralCharge = true;

    public int rejectForce = 500;


    void Start()
    {
        nucleus = GameObject.FindWithTag("nucleus").transform;
    }

    void Update()
    {
        calculateCharge();

        //ORBIT ALL THE ELECTRONS AROUND THEIR SHELL RADII
        for (int electronID = 0; electronID < electronCount; electronID++)
        {
            if (electronID <= 1)
                orbitNucleus(shell0[electronID], shell0Radius, shell0Speed, shell0Spin);
            if (electronID > 1 && electronID <= 9)
            {
                orbitNucleus(shell1[electronID - 2], shell1Radius, shell1Speed, shell1Spin);
                Debug.Log(electronID);
            }
            if (electronID > 9 && electronID <= 17)
                orbitNucleus(shell2[electronID - 10], shell2Radius, shell2Speed, shell2Spin);
            if (electronID > 17 && electronID <= 19)
                orbitNucleus(shell3[electronID - 18], shell3Radius, shell3Speed, shell3Spin);
        }
        //ORBIT PROTONS AND NEUTRONS
        for (int protonID = 0; protonID < protonCount; protonID++)
            orbitNucleus(protons[protonID], protonRadius, protonSpeed, protonSpin);
        for (int neutronID = 0; neutronID < neutronCount; neutronID++)
            orbitNucleus(neutrons[neutronID], neutronRadius, neutronSpeed, neutronSpin);
    }


    //APPLY ORBIT PHYSICS
    public void orbitNucleus(GameObject particle, float shellRadius, float shellSpeed, float shellSpin)
    {
        particle.transform.RotateAround(nucleus.position, Vector3.forward, shellSpeed * Time.deltaTime);
        desiredPosition = (particle.transform.position - nucleus.position).normalized * shellRadius + nucleus.position;
        particle.transform.position = Vector2.MoveTowards(particle.transform.position, desiredPosition, Time.deltaTime * shellSpin);
    }

    //COLLIDED WITH PROTON
    public void protonCollision(GameObject proton)
    {
  //      if (neutronCount > protonCount && (negativeCharge||neutralCharge))
        if (neutronCount > protonCount)
        {
            protons[protonCount] = proton;
            protonCount++;
            proton.gameObject.GetComponent<protonScript>().ionised = true;
        }
        else
        {
            Debug.Log("reject");
            //EXPLODE AWAY
            Vector2 explosiveForce = -proton.GetComponent<Rigidbody2D>().velocity.normalized * rejectForce;
            proton.GetComponent<Rigidbody2D>().AddForce(explosiveForce);
        }
    }

    //COLLIDED WITH ELECTRON
    public void electronCollision(GameObject electron)
    {
       // if (positiveCharge || neutralCharge)
     //   {
            if (electronCount <= 1)
                shell0[electronCount] = electron;
            if (electronCount >= 2 && electronCount < 8)
            shell1[electronCount-2] = electron;
            if (electronCount >= 8 && electronCount < 18)
                shell2[electronCount-10] = electron;
            if (electronCount >= 18 && electronCount < 20)
                shell3[electronCount-18] = electron;

            electronCount++;
            electron.gameObject.GetComponent<electronScript>().ionised = true;
       // }
    }

    //COLLIDED WITH NEUTRON
    public void neutronCollision(GameObject neutron)
    {
        neutrons[neutronCount] = neutron;
        neutronCount++;
        neutron.gameObject.GetComponent<neutronScript>().ionised = true;
    }

    //CHARGE CALCULATOR
    private void calculateCharge()
    {
        if (protonCount > electronCount)
            positiveCharge = true;
        else positiveCharge = false;

        if (electronCount > protonCount)
            negativeCharge = false;
        else negativeCharge = true;

        if (protonCount == electronCount)
            neutralCharge = true;
        else neutralCharge = false;
    }

    //APPLY FORCE PHYSICS
}