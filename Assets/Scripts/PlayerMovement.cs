using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;

    public float speed;
    public float walkSpeed = 12.5f;
    public float sprintSpeed = 20f;
    public float gravity = -9.81f;

    public float stamina;
    public float maxStamina = 100;
    public Slider staminaBar;
    public float changeStamina;

    public Transform playerTransform;

    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    bool onGround;

    public float jumpHeight = 3f;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        staminaBar.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        Score.timeAlive += Time.deltaTime;

        Vector3 playerPos = playerTransform.position;

        staminaBar.value = stamina;

        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0)
        {
            velocity.y = 0;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Move Speed
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            speed = sprintSpeed; // sprinting speed
            DecreaseEnergy();
            if (stamina <= 0)
                speed = walkSpeed;
        }
        else
        {
            speed = walkSpeed; // normal speed
            if (stamina != maxStamina)
                IncreaseEnergy();
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    public void DecreaseEnergy()
    {
        if (stamina >= 0)
            stamina -= changeStamina * Time.deltaTime;
    }

    public void IncreaseEnergy()
    {
        if (stamina <= 100)
            stamina += (changeStamina/5) * Time.deltaTime;
    }
}
