using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectTest : MonoBehaviour
{

    public ScriptableObjectSample sample;
    
    void Start()
    {
        if (!sample)
            return;

        Debug.Log($"Loaded the object, Name :{sample.objectName}, Score:{sample.score}, Position:{sample.startPosition}");
    }

    
}
