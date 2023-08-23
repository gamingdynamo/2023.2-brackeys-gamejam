using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactorPosition;
    public float interactorRadius = 1f;
    public LayerMask interactableLayerMask;
    public Interactable currentInteractable=null;
    public bool interacting = false;
    public Transform holdingposition;
    public static Interactor instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
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
            if (Input.GetButtonDown("Interact")&&currentInteractable.interacted==false)
            {
                currentInteractable.Interact();
            }
            else if(Input.GetButtonDown("Interact") && currentInteractable.interacted == true)
            {
                currentInteractable.UnInteract();
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
