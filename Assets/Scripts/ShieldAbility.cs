using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class ShieldAbility : Ability
{
    public GameObject playerObj;
    public AnimatorController phantasusController;
    public bool isActive;


    public override void Activate()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject shieldObj = playerObj.transform.Find("Shield").gameObject;
        if (playerObj != null)
        {
            isActive = true;
            Debug.Log("Shield radi");
            //shieldObj.GetComponent<SpriteRenderer>().enabled = false;

            shieldObj.SetActive(true);
        }

    }

    public override void Deactivate()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject shieldObj = playerObj.transform.Find("Shield").gameObject;
        if (playerObj != null)
        {
            isActive = true;
            Debug.Log("Shield radi");
            //shieldObj.GetComponent<SpriteRenderer>().enabled = false;

            shieldObj.SetActive(false);
        }

    }
}
