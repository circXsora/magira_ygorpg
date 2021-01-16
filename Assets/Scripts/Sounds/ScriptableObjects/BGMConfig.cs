using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BGMName
{
    Free,
    Battle,
}

[System.Serializable]
public class BGMPair
{
    public BGMName Name;
    public AudioClip Clip;
}
   
[CreateAssetMenu(fileName = "New BGM Config", menuName = "Sounds/New BGM Config")]
public class BGMConfig : ScriptableObject
{
    public List<BGMPair> BGMList;
    private void OnEnable()
    {
        if (BGMList != null)
        {
            var additems = from name in System.Enum.GetValues(typeof(BGMName)).Cast<BGMName>()
                           where !BGMList.Any(se => se.Name == name)
                           select new BGMPair { Name = name };

            BGMList.AddRange(additems);
        }
        else
        {
            BGMList = (from name in System.Enum.GetValues(typeof(BGMName)).Cast<BGMName>()
                      select new BGMPair { Name = name })
                      .ToList();
        }
    }
}
