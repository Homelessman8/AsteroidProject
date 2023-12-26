using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONExampleComplex : MonoBehaviour
{
    
    void Start()
    {
        //Serializing a data object
        SampleDataComplex sample = new SampleDataComplex();

        sample.name = "Indi";

        sample.address = new Address();
        sample.address.unit = 1;
        sample.address.road = "2nd avenue";
        sample.address.city = "New York";

        sample.books = new book[2]; //creating the array
        sample.books[0] = new book(); //creating an object to add to the array
        sample.books[0].name = "Intro to Game Dev";
        sample.books[0].isDigital = true;
        sample.books[0].author = "John Doe";

        sample.books[1] = new book();
        sample.books[1].name = "Hatty Porrer";
        sample.books[1].isDigital = false;
        sample.books[1].author = "Just Kidding Rolling";


        string data = JsonUtility.ToJson(sample);
        Debug.Log(data);

        //Deserializing the same, use an example as before

    }

    
}
