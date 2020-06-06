using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        projectile = Resources.Load("Projectile") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject item = gameObject.GetComponent<PickUpItems>().heldItem;
        if(item != null && item.tag == "Weapon")
        {
            if(projectile)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    GameObject proj = Instantiate(projectile) as GameObject;
                    proj.transform.up = transform.forward;
                    proj.transform.position = transform.position + Vector3.up * 1.5f + transform.forward * 1.5f;
                    Rigidbody rb = proj.GetComponent<Rigidbody>();
                    rb.velocity = Camera.main.transform.forward * 20;
                }
            }
        }
    }
}
