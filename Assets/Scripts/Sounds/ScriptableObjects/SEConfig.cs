using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SEName
{
    HealthDown,
    HealthUp,
    Die,
    LevelUp,
    AttackUp,
    AttackDown,
    Burn,
    Freeze,
    GetDamage,
    DuelStart
}

[System.Serializable]
public class SEPair
{
    public SEName Name;
    public AudioClip Clip;
}

[CreateAssetMenu(fileName = "New SE Config", menuName = "Sounds/New SE Config")]
public class SEConfig : ScriptableObject
{

    public List<SEPair> SEList;
    private void OnEnable()
    {
        if (SEList != null)
        {
            var additems = from name in System.Enum.GetValues(typeof(SEName)).Cast<SEName>()
                          where !SEList.Any(se => se.Name == name)
                          select new SEPair { Name = name };

            SEList.AddRange(additems);
        }
        else
        {
            SEList = (from name in System.Enum.GetValues(typeof(SEName)).Cast<SEName>()
                       select new SEPair { Name = name })
                      .ToList();
        }
    }
}
