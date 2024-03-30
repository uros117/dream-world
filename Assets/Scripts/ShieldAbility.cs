using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class ShieldAbility : Ability
{
    public GameObject playerObj, shieldObj;
    public CharacterController2D playerController;
    public bool isActive;


    public override void Activate()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        shieldObj = playerObj.transform.Find("Shield").gameObject;
        playerController = playerObj.GetComponent<CharacterController2D>();
        if (playerObj != null)
        {
            isActive = true;
            Debug.Log("Shield radi");
            playerController.invincible = true;

            shieldObj.SetActive(true);
        }

    }

    public override void Deactivate()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        shieldObj = playerObj.transform.Find("Shield").gameObject;
        playerController = playerObj.GetComponent<CharacterController2D>();
        if (playerObj != null)
        {
            isActive = true;
            Debug.Log("Shield ugasen");
            playerController.invincible = false;

            //shieldObj.GetComponent<SpriteRenderer>().enabled = false;

            shieldObj.SetActive(false);
        }

    }
}
