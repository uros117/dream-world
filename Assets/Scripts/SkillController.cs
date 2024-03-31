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

    void Start()
    {
        acquiredSkills = new ArrayList();
    }

    void Update()
    {
        
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
