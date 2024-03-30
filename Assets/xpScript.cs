using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class xpScript : MonoBehaviour
{

    public static xpScript instance;
    
    public TMP_Text xpTxt;
    public int currentXP = 0;


    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        xpTxt.text = "Lore: " + currentXP.ToString();
    }

    // Update is called once per frame
    public void xpAdd(int amount){
        currentXP += amount;
        xpTxt.text = "Lore: " + currentXP.ToString();
    }
    
    public void xpSubtract(int amount){
        currentXP -= amount;
        xpTxt.text = "Lore: " + currentXP.ToString();
    }
}