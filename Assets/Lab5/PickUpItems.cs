using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public GameObject heldItem = null;
    public bool dontPickUp = false;
    
    void Update() 
    {
        if(Input.GetButtonDown("Fire2"))
            DropItem();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Weapon" && heldItem == null && !dontPickUp)
        {
            heldItem = other.gameObject;
            heldItem.SetActive(false);
            dontPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Weapon" && heldItem == null && dontPickUp)
        {
            dontPickUp = false;
        }
    }

    private void DropItem()
    {
        if(heldItem!=null)
        {
            heldItem.transform.position = transform.position;
            heldItem.SetActive(true);
            heldItem = null;
        }
    }
}
