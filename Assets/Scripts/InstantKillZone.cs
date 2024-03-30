using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantKillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<CharacterController2D>().ApplyDamage(col.gameObject.GetComponent<CharacterController2D>().life, col.gameObject.transform.position);
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}
