using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject newmesh;
    public GameObject brokenmesh;
    public float breakingforce = 1f;
    public BoxCollider bc;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " has entered the trigger");
        Rigidbody rb=other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log(rb.velocity.magnitude + " is the entering magnitude");
            if (rb.velocity.magnitude >= breakingforce)
            {
                bc.enabled = false;
                newmesh.SetActive(false);
                brokenmesh.SetActive(true);
                foreach(Rigidbody rb2 in brokenmesh.GetComponentsInChildren<Rigidbody>())
                {
                    rb2.AddExplosionForce(rb.velocity.magnitude*rb.mass, bc.ClosestPointOnBounds(other.transform.position), 25f);
                }
               // brokenmesh.GetComponent<Rigidbody>().AddExplosionForce(rb.velocity.magnitude, other.transform.position, 5f);
            }
        }
    }
}
