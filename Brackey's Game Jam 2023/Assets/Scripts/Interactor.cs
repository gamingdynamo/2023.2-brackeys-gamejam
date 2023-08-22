using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactorPosition;
    public float interactorRadius = 1f;
    public LayerMask interactableLayerMask;
    public Interactable currentInteractable=null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            currentInteractable = Physics.OverlapSphere(interactorPosition.position, interactorRadius, interactableLayerMask)[0].GetComponent<Interactable>();
        }
        catch (System.Exception)
        {

            currentInteractable = null;
        }

       if (currentInteractable!=null)
       {
            if (Input.GetButtonDown("Interact"))
            {
                currentInteractable.Interact();
            }
       }
    }


    private void OnDrawGizmos()
    {
        if (currentInteractable != null)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.cyan;
        }
        Gizmos.DrawWireSphere(interactorPosition.position, interactorRadius);
    }
}
