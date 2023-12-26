using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public LineRenderer bot1Line;
    public LineRenderer bot2Line;
    public LineRenderer crossProductLine;

    public Transform bot1;
    public Transform bot2;

    public bool isNormalize;

    public TMP_Text crossValue;

    Vector3 crossProduct;

    void Start()
    {
        
    }

    void Update()
    {
        DrawLine();

        if(!isNormalize)
            crossProduct = Vector3.Cross(bot1.position, bot2.position);
        else
            crossProduct = Vector3.Cross(bot1.position.normalized, bot2.position.normalized);

        crossValue.SetText($"CROSS: {crossProduct}");

        DrawCrossProductLine(crossProduct);

    }

    void DrawLine()
    {
        bot1Line.SetPosition(0, transform.position);//zero
        bot1Line.SetPosition(1, bot1.position);

        bot2Line.SetPosition(0, transform.position);//zero
        bot2Line.SetPosition(1, bot2.position);
    }

    void DrawCrossProductLine(Vector3 value)
    {
        crossProductLine.SetPosition(0, transform.position);//zero
        crossProductLine.SetPosition(1,value);
    }
}
