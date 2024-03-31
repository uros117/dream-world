using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointOnDeath : MonoBehaviour
{
    public GameObject inspirationPoint, fearPoint;
    public GameObject fantasyObject, phobiaObject;
    public GameObject player;

    public SwapController swapController;
    public void dropPoint()
    {
        GameObject newObj;
        if(swapController.currDreamState == DreamState.Fantasy)
        {
            newObj = Instantiate(inspirationPoint, transform.position, Quaternion.identity, fantasyObject.transform);
            newObj.GetComponent<PointAcquiredScript>().pickedUp.AddListener(player.GetComponent<SkillController>().acquiredInspiration);
        }
        else
        {
            newObj = Instantiate(fearPoint, transform.position, Quaternion.identity, fantasyObject.transform);
            newObj.GetComponent<PointAcquiredScript>().pickedUp.AddListener(player.GetComponent<SkillController>().acquiredFear);
        }

    }
}
