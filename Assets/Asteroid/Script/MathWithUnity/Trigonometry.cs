using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigonometry : MonoBehaviour
{
    public Transform bot;

    [Header("Elipse")]
    public Vector2 startPos;
    public Vector2 amplitude;
    public Vector2 frequency;

    [Header("Degrees and Radians")]
    public float angle;
    
    void Start()
    {
        
    }

    void Update()
    {
        //Sine();
        //Cos();
        //Tan();
        Elipse();
        //RotateInDeg();
        //RotateInRad();
    }

    void Sine()
    {
        float xpos = startPos.x + Time.timeSinceLevelLoad;
        float ypos = startPos.y + amplitude.y * Mathf.Sin(frequency.y * Time.timeSinceLevelLoad);

        bot.position = new Vector2(xpos, ypos);
    }

    void Cos()
    {
        float xpos = startPos.x + Time.timeSinceLevelLoad;
        float ypos = startPos.y + amplitude.y * Mathf.Cos(frequency.y * Time.timeSinceLevelLoad);

        bot.position = new Vector2(xpos, ypos);
    }

    void Tan()
    {
        float xpos = startPos.x + Time.timeSinceLevelLoad;
        float ypos = startPos.y + amplitude.y * Mathf.Tan(frequency.y * Time.timeSinceLevelLoad);

        bot.position = new Vector2(xpos, ypos);
    }


    void Elipse()
    {
        float xpos = startPos.x + amplitude.x * Mathf.Sin(frequency.x * Time.timeSinceLevelLoad);
        float ypos = startPos.y + amplitude.y * Mathf.Cos(frequency.y * Time.timeSinceLevelLoad);

        bot.position = new Vector2(xpos, ypos);
    }

    void RotateInDeg()
    {
         bot.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void RotateInRad()
    {
        bot.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }
}
