using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : Magia.Singleton<MaterialManager>
{

    public enum MaterialMode
    {
        Shared,
        Instance
    }

    [System.Serializable]
    public class MaterialPack
    {
        public string name;
        public Material material;
    }

    public Material Normal;

    public List<MaterialPack> MaterialPacks;

    private void Start()
    {
        MaterialPacks.Add(new MaterialPack() { name = "Normal", material = Normal });
    }

    public void SetNormalMaterial(GameObject gameObj)
    {
        SetMaterial(gameObj, "Normal", mode:MaterialMode.Shared);
    }

    public void SetMaterial(GameObject gameObj, string matName, Color? color = null, MaterialMode mode = MaterialMode.Instance)
    {
        if (mode == MaterialMode.Instance)
        {
            var mat = Instantiate(MaterialPacks.Find(p => p.name == matName).material);
            if (color != null)
            {
                if (matName == "Outline")
                {
                    mat.SetColor("_OutlineColor", color.Value);
                }
                else
                {
                    mat.color = color.Value;
                }
            }
            gameObj.GetComponent<Renderer>().material = mat;
        }
        else
        {
            gameObj.GetComponent<Renderer>().material = MaterialPacks.Find(p => p.name == matName).material;
        }
    }
}

public static class MaterialExtensions
{
    public static void SetMaterial(this GameObject gameObject, string matName, MaterialManager.MaterialMode mode = MaterialManager.MaterialMode.Instance)
    {
        MaterialManager.Instance.SetMaterial(gameObject, matName, null, mode);
    }

    public static void SetMaterial(this GameObject gameObject, string matName, Color? color, MaterialManager.MaterialMode mode = MaterialManager.MaterialMode.Instance)
    {
        MaterialManager.Instance.SetMaterial(gameObject, matName, color, mode);
    }

    public static void SetNormalMaterial(this GameObject gameObject)
    {
        MaterialManager.Instance.SetNormalMaterial(gameObject);
    }
}