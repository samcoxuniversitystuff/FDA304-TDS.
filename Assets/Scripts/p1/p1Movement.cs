using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class p1Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;

    [FormerlySerializedAs("a")] [FormerlySerializedAs("_a")] [SerializeField] Animator playerAnimator;
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float rushSpeed;

    [SerializeField] private float originalSpeed;
    [SerializeField] Vector2 movementDirection;

    public Camera gameCam;
    private Vector2 _mousePosition;

    public PolyDungeons MovementControls;

    private InputAction _move;

    private void Awake()
    {
        MovementControls = new PolyDungeons();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerPos = transform.position;
        PlayerPrefs.SetFloat("PlayerX", playerPos.x);
        PlayerPrefs.SetFloat("PlayerY", playerPos.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPos.z);
        
        rb2d = GetComponent<Rigidbody2D>();
        originalSpeed = movementSpeed;
        rushSpeed = movementSpeed * speedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementDirection();
        UpdateAnimation();
        ChoosePlayerSpeed();

    }

    private void OnEnable()
    {
        _move = MovementControls.Player.Move;
        _move.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void ChoosePlayerSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = rushSpeed;
        }
        else
        {
            movementSpeed = originalSpeed;
        }
        
        // Debug.Log("MovementSpeed = " + movementSpeed);
    }


    void UpdateAnimation()
    {
        playerAnimator.SetFloat("Horizontal", movementDirection.x);
        playerAnimator.SetFloat("Vertical", movementDirection.y);
        playerAnimator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }



    void UpdateMovementDirection()
    {
        //movementDirection.x = Input.GetAxisRaw("Horizontal");
        // movementDirection.y = Input.GetAxisRaw("Vertical");
        // movementDirection = movementDirection.normalized;
        movementDirection = _move.ReadValue<Vector2>().normalized;

    }

    void MovePlayer()
    {
        rb2d.MovePosition(rb2d.position + movementDirection * (movementSpeed * Time.fixedDeltaTime));
    }

    public Vector2 GetPlayerDirection()
    {
        return movementDirection;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
}

// References
// https://youtu.be/HmXU4dZbaMw
