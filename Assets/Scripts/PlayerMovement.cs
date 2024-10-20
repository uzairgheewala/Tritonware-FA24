using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D PlayerRB;
    public SpriteRenderer PlayerSR;
    public float Speed;
    public float horizontal, vertical;
    private Vector2 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = PlayerRB.position;
        Logger.Log("PlayerMovement initialized.");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // Rigidbody2D physics updates occur here
    void FixedUpdate()
    {
        // After physics updates, check if position has changed
        if (PlayerRB.position != previousPosition)
        {
            //Logger.Log($"Player moved to position: {PlayerRB.position}");
            previousPosition = PlayerRB.position;
        }
    }

    private void MovePlayer() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        PlayerRB.velocity = new Vector2(horizontal * Speed, vertical * Speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Logger.Log($"Player collided with {collision.gameObject.name}");
    }
}