using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Interactable
{
    Rigidbody rb;
    public float throwforce = 10f;

    public void Start()
    {
        rb= GetComponent<Rigidbody>();
    }
    public override void Interact()
    {
        base.Interact();
        transform.parent = Interactor.instance.holdingposition;
        transform.localPosition = Vector3.zero;
        transform.rotation=Quaternion.identity;
    }

    public override void UnInteract()
    {
        base.UnInteract();
        transform.parent = null;
        rb.AddForce(Interactor.instance.holdingposition.forward * throwforce,ForceMode.Impulse);
    }
}
