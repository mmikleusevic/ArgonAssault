using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public class Effects : ScriptableObject
{
    static Effects _effects = null;

    public static Effects GetInstance()
    {
        if (_effects == null)
        {
            _effects = CreateInstance<Effects>();
        }
        return _effects;
    }

    public void ProcessFX(GameObject visualEffect, Vector3 position, GameObject parentGameObject)
    {
        GameObject fx = Instantiate(visualEffect, position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
    }
}
