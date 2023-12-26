using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONExample : MonoBehaviour
{
    
    void Start()
    {
        //Serielizing data to JSON

        SampleData sample = new SampleData();
        sample.name = "Mike";
        sample.score = 10.0f;

        string data = JsonUtility.ToJson(sample);
        Debug.Log(data);

        // Deserializing JSON to data
        string JSON = "{\n\t\"name\": \"Alice\",\n\t\"score\": 90.34\n}";
        SampleData sample2 = JsonUtility.FromJson<SampleData>(JSON);

        Debug.Log($"Deserialized {sample2.name} - Score : {sample2.score}");
    }

    
}
