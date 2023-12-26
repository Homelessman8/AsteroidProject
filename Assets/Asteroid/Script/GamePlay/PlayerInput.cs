using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerInput handles player input and communicates with the Player class
public class PlayerInput : MonoBehaviour
{
    private Player player;
    private float horizontal, vertical;
    private Vector2 lookTarget;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Player component attached to the same GameObject
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is currently playing
        if (!GameManager.GetInstance().IsPlaying())
            return;

        // Get horizontal and vertical input axis values
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Get the mouse position for player aiming
        lookTarget = Input.mousePosition;

        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            // Call the Shoot function in the Player class
            player.Shoot();
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            // Call the StopShoot function in the Player class
            player.StopShoot();
        }

        // Check if the right mouse button is clicked
        if (Input.GetMouseButtonDown(1))
        {
            // Call the Attack function in the Player class with a specified interval (0)
            player.Attack(0);
        }
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate()
    {
        // Call the Move function in the Player class with the input values
        player.Move(new Vector2(horizontal, vertical), lookTarget);
    }
}
