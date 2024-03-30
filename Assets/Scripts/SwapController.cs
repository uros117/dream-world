using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

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

    public GameObject phobiaBlocks, fantasyBlocks;

    public GameObject phobiaSky, fantasySky;

    public Material transparent, opaque;

    void Start()
    {
        currDreamState = DreamState.Fantasy;
        if (phobiaSky != null && fantasySky != null)
        {
            phobiaSky.SetActive(false);
            fantasySky.SetActive(true);
        }
        setAllRBodyCollider(phobiaBlocks, false);
        setAllRBodyCollider(fantasyBlocks, true);

        changeAllChildrenMaterial(phobiaBlocks, transparent);
        changeAllChildrenMaterial(fantasyBlocks, opaque);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerObj.GetComponent<Animator>().runtimeAnimatorController = 
                currDreamState == DreamState.Fantasy ? phobetorController : phantasusController;

            if (currDreamState == DreamState.Fantasy)
            {
                swapToPhobia.Invoke();
                if (phobiaSky != null && fantasySky != null)
                {
                    phobiaSky.SetActive(true);
                    fantasySky.SetActive(false);
                }
                setAllRBodyCollider(phobiaBlocks, true);
                setAllRBodyCollider(fantasyBlocks, false);

                changeAllChildrenMaterial(phobiaBlocks, opaque);
                changeAllChildrenMaterial(fantasyBlocks, transparent);
            }
            else
            {
                swapToFantasy.Invoke();
                if (phobiaSky != null && fantasySky != null)
                {
                    phobiaSky.SetActive(false);
                    fantasySky.SetActive(true);
                }
                setAllRBodyCollider(phobiaBlocks, false);
                setAllRBodyCollider(fantasyBlocks, true);

                changeAllChildrenMaterial(phobiaBlocks, transparent);
                changeAllChildrenMaterial(fantasyBlocks, opaque);
            }
            currDreamState = (currDreamState == DreamState.Fantasy) ? DreamState.Phobia : DreamState.Fantasy;
        }
    }


    public void setAllRBodyCollider(GameObject gameObject, bool setValue)
    {
        if(gameObject != null)
        {
            foreach (Collider2D childCol in gameObject.GetComponentsInChildren<Collider2D>())
            {
                childCol.enabled = setValue;
            }
            foreach (SpriteRenderer childSprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                childSprite.enabled = setValue;
            }
        }
    }

    public void changeAllChildrenMaterial(GameObject gameObject, Material mat)
    {
        if(gameObject != null)
        {
            foreach (TilemapRenderer childTileMap in gameObject.GetComponentsInChildren<TilemapRenderer>())
            {
                childTileMap.material = mat;
            }
        }
    }
}
