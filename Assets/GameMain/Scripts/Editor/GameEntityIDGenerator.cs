using System.IO;
using UnityEditor;
using UnityEngine;

namespace BBYGO
{

    public static class GameEntityIDGenerator
    {
        public static string EntityConfigPath = "GameMain/DataTables/Entity.txt";

        [MenuItem("Tools/生成游戏实体ID索引")]
        public static void GenerateGameEntityIDs()
        {
            var path = Path.Combine(Application.dataPath, EntityConfigPath);
            Debug.Log("生成游戏实体ID索引, 路径"+ path);
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {

            }
        }
    }

}