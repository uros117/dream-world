using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantKillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col);
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<CharacterController2D>().instantDeath(col.gameObject.transform.position);
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}
