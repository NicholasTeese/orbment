using UnityEngine;
using UnityEditor;

public class MakeScripableObject
{
    [MenuItem("Assets/Create/ScriptableObject")]
    public static void CreateScriptableObject()
    {
        BulletData bulletData = ScriptableObject.CreateInstance<BulletData>();
        AssetDatabase.CreateAsset(bulletData, "Assets/Resources/ScriptableObjects/BulletData.asset");
        AssetDatabase.SaveAssets();
    }
}
