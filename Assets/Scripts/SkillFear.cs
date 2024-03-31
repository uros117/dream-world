using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillTree;

public class SkillFear : Skill
{
    public AbilityHolder abilityHolder;

    public override void UpgradeUI()
    {
        TitleText.text = $"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescription[id]}\nCost: {skillTree.FearPoint}/1 SP";

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.white
            : skillTree.FearPoint >= 1 ? Color.green : Color.white;

        foreach (var connectedSkill in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            //skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);
        }
    }

    public override void Buy(int id)
    {
        Debug.Log("desi bate");
        if (skillTree.FearPoint < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
        skillTree.FearPoint -= 1;
        skillTree.SkillLevels[id]++;
        skillTree.UpdateAllSkillUI();

        if (skillTree.SkillLevels[id] >= skillTree.SkillCaps[id])
        {
            AbilityHolder abilityHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityHolder>();

            // Check if we successfully got the AbilityHolder script
            if (abilityHolder != null)
            {
                // Enable the AbilityHolder script
                abilityHolder.enabled = true;
            }
            else
            {
                Debug.LogError("Unable to find AbilityHolder script on player GameObject.");
            }
        }
    }
}
