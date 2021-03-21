using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum YuGiAvatarName
{
    MyTurnDora,
    Attack,
    Win,
    Lose,
    Gekiha, // 击破！
    BeGekihaed // 被击破
}

[System.Serializable]
public class AvatarDictionary : SerializableDictionary<YuGiAvatarName, Sprite> { }

[CreateAssetMenu(fileName = "New Avatar Config", menuName = "Avatars/New Avatar Config")]
public class AvatarConfig : ScriptableObject
{
    [SerializeField]
    public AvatarDictionary Avatars;

    private void OnEnable()
    {
        Avatars.GenerateEnumKeysData();
        //Avatars.CheckDatas();
        //CheckData(Avatars);
    }


}
