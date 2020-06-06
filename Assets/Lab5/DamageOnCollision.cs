using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damage = 10;
    private void OnTriggerEnter(Collider other) {
        if(!other.isTrigger)
        {
            if(other.tag == "Enemy")
            {
                other.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
