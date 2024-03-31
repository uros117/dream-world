using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointAcquiredScript : MonoBehaviour
{
    public UnityEvent pickedUp;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pickedUp.Invoke();
            Destroy(gameObject);
        }
    }
}
