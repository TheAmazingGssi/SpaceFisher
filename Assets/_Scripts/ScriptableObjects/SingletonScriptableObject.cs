using UnityEngine;

[CreateAssetMenu(fileName = "SingletonScriptableObject", menuName = "Scriptable Objects/SingletonScriptableObject")]
public class SingletonScriptableObject<T> : ScriptableObject where T :SingletonScriptableObject<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");
                if(assets == null || assets.Length < 1)
                {
                    throw new System.Exception("Could not find any Singleton Scriptable Object in Resources");
                }
                else if(assets.Length > 1)
                {
                    throw new System.Exception("Found multiple of Singleton Scriptable Object of class in Resources");
                }
                instance = assets[0];
            }
            return instance;
        }
    }
}
