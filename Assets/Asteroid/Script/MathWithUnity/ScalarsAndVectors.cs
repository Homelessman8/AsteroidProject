using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarsAndVectors : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public Vector3 direction;
    public float rotation;

    public float scalar;

    public Transform bot;


    void Start()
    {
        
    }

    void Update()
    {
        //INSTRUCTORS - Show each line individually to demostrate things. (not all at once)

        // position----------------------
        //bot.position = position;

        //scaled position----------------
        //bot.position = scalar * position;

        //velocity-----------------------
        /*if(Input.GetKeyDown(KeyCode.V))
        {
            bot.GetComponent<Rigidbody>().velocity = velocity;
        }*/

        //scaled velocity----------------
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            bot.GetComponent<Rigidbody>().velocity = scalar * velocity;
        }*/

        //Direction
        //bot.rotation = Quaternion.Euler(direction);

        bot.rotation = Quaternion.Euler(0,0,rotation);

        //velocity towards direction----------------
        /*if (Input.GetKey(KeyCode.V))
        {
            bot.Translate(velocity);
        }*/

        // Showing Translate with scalar and direction change
        /*if (Input.GetKey(KeyCode.V))
        {
            //bot.Translate(Vector2.right * velocity);
            //bot.Translate(Vector2.right * scalar);
            bot.Translate(Vector2.right * scalar * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            rotation += scalar * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            rotation -= scalar *Time.deltaTime;
        }*/

        // Explain magnitude, unit vector (direction only if magnitude is zero etc.)
        /*if (Input.GetKey(KeyCode.M))
        {
            Debug.Log($"Distance from 0 - {position.magnitude}");
            Debug.Log($"Amount of the velocity - {velocity.magnitude}");
        }*/
        

        // INSTRUCTORS -> Show different examples of using basic vector values
        

    }
}
