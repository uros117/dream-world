using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public enum DreamState:int
{
    Fantasy = 0,
    Phobia = 1
};


public class SwapController : MonoBehaviour
{

    public GameObject playerObj;

    public AnimatorController phantasusController;
    public AnimatorOverrideController phobetorController;

    public UnityEvent swapToFantasy, swapToPhobia;

    [NonSerialized]
    public DreamState currDreamState;

    public GameObject phobiaBlocks;

    void Start()
    {
        currDreamState = DreamState.Fantasy;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerObj.GetComponent<Animator>().runtimeAnimatorController = 
                currDreamState == DreamState.Fantasy ? phobetorController : phantasusController;

            if(currDreamState == DreamState.Fantasy)
            {
                swapToPhobia.Invoke();
                phobiaBlocks.SetActive(true);

            }
            else
            {
                swapToFantasy.Invoke();
                phobiaBlocks.SetActive(false);
            }
            currDreamState = (currDreamState == DreamState.Fantasy) ? DreamState.Phobia : DreamState.Fantasy;
        }
    }
}
