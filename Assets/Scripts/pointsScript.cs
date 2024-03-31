using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pointsScript : MonoBehaviour
{

    public TMP_Text txtVal;
    public int currentVal = 0;


    // Start is called before the first frame update
    void Start(){
        txtVal.text = currentVal.ToString();
    }

    public void updateVal(int newVal)
    {
        currentVal = newVal;
        txtVal.text = currentVal.ToString();
    }
}