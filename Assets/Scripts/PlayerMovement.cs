using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D PlayerRB;
    public SpriteRenderer PlayerSR;
    public Animator animator;
    public float Speed;
    public float horizontal, vertical;
    private Vector2 previousPosition;
    private float lastinputx;
    private float lastinputy;

    public static PlayerMovement Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Logger.LogWarning("Duplicate PlayerMovement instance destroyed.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Logger.Log("PlayerMovement instance created.");
        }
    }

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
            Logger.Log($"Player moved to position: {PlayerRB.position}");
            previousPosition = PlayerRB.position;
        }
    }

    private void MovePlayer() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        PlayerRB.velocity = new Vector2(horizontal * Speed, vertical * Speed);

        animator.SetBool("isWalking", true);

        if (Mathf.Abs(horizontal * Speed) > 0 || Mathf.Abs(vertical * Speed) > 0) {
            animator.SetFloat("InputX", horizontal * Speed);
            animator.SetFloat("InputY", vertical * Speed);
            lastinputx = horizontal * Speed;
            lastinputy = vertical * Speed;
        } else {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", lastinputx);
            animator.SetFloat("LastInputY", lastinputy);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Logger.Log($"Player collided with {collision.gameObject.name}");
    }
}