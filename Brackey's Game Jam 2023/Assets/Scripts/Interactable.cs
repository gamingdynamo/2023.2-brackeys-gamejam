using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool toggleable;
    public bool interacted;
    
   public virtual void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
        if (toggleable)
        {
            interacted = true;
        }

    }

    public virtual void UnInteract()
    {
        Debug.Log($"Uninteracting with {gameObject.name}");
        if (toggleable)
        {
            interacted = false;
        }
    }
}
