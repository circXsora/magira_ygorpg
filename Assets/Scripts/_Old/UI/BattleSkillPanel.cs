using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillPanel : MonoBehaviour
{
    public GameObject BattleSkillPointUIPrefab;
    public List<GameObject> BattleSkillPoints = new List<GameObject>();

    public BattleSkillPanelViewModel ViewModel
    {
        set
        {
            if (value == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                BattleSkillPoints.ForEach(g => Destroy(g));
                BattleSkillPoints.Clear();

                for (int i = 0; i < value.MaxBattleSkillPoint; i++)
                {
                    var bsp = Instantiate(BattleSkillPointUIPrefab, transform);
                    bsp.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    BattleSkillPoints.Add(bsp);
                }
                for (int i = 0; i < value.CurrentBattleSkillPoint; i++)
                {
                    BattleSkillPoints[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
            }
        }
    }

    public class BattleSkillPanelViewModel
    {
        public int MaxBattleSkillPoint, CurrentBattleSkillPoint;
    }
}
