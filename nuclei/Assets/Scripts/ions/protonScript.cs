using UnityEngine;
using System.Collections;

public class protonScript : MonoBehaviour
{
    private nucleusScript nucleus;

    public float driftSpeed = 5f;
    public float attractRange = 10f;
    public float repelRange = 3f;
    public float attractForce = 5f;
    public float repelForce = 5f;

    public bool ionised = false;
    public bool attractEnabled = true;


    void Start()
    {
        nucleus = GameObject.FindGameObjectWithTag("nucleus").GetComponent<nucleusScript>();
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D particle)
    {
        if (!ionised)
        {
            /*   if (particle.gameObject.tag == "neutron")
               if (particle.gameObject.tag == "proton")
               if (particle.gameObject.tag == "electron")
            */
            if (particle.gameObject.tag == "nucleus")
                nucleus.protonCollision(gameObject);
        }
    }
}

