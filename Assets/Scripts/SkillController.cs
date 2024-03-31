using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public int inspiration = 0;
    public int fear = 0;

    public ArrayList allSkills;
    public ArrayList acquiredSkills;

    public pointsScript insPoints, fearPoints;

    public GameObject SkillMenu;
    public bool SkillMenuActive;



    void Start()
    {
        acquiredSkills = new ArrayList();
        SkillMenuActive = false;
        SkillMenu.SetActive(SkillMenuActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SkillMenuActive = !SkillMenuActive;
            SkillMenu.SetActive(SkillMenuActive);
            Cursor.lockState = CursorLockMode.None; // Make the cursor freely movable
            Cursor.visible = true;
            //Time.timeScale = SkillMenuActive ? 0 : 1;
        }
    }

    public void acquiredInspiration()
    {
        inspiration++;
        insPoints.updateVal(inspiration);
    }
    public void acquiredFear()
    {
        fear++;
        fearPoints.updateVal(fear);
    }
}
