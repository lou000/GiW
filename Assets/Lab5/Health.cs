using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int MaxHP = 200;
    private int CurrentHP;

    private void Start() {
        CurrentHP = MaxHP;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
    }

    private void Update() {
        if(CurrentHP<=0)
            Destroy(gameObject);
    }

}
