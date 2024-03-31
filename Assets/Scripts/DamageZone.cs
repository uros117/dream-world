using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour { 

    public int damage = 2;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<CharacterController2D>().ApplyDamage(damage, col.gameObject.transform.position);
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}
