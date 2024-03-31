using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;

    public string[] SkillNames;
    public string[] SkillDescription;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    //public List<GameObject> ConnectorList;
    //public GameObject ConnectorHolder;

    public int InspirationPoint;
    public int FearPoint;

    private void Start()
    {
        InspirationPoint = 20;
        FearPoint = 20;

        SkillLevels = new int[6];
        SkillCaps = new[] { 1, 1, 3, 5, 3, 5 };

        SkillNames = new[] { "The Brave", "Legacy", "Heroism", "Imortality", "The Hydra", "Lions Rawr" };
        SkillDescription = new[]
        {
            "Cast a protective barrier",
            "End their Legacy",
            "No Fear, No hurt",
            "Defeat the destiny",
            "Cast a tripple attack",
            "Roar of Zeuses anvil",
            
        };

        foreach (Skill skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
        //foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);


        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 2, 3 };
        SkillList[1].ConnectedSkills = new[] { 4, 5 };

        UpdateAllSkillUI();

    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpgradeUI();
    }

}
