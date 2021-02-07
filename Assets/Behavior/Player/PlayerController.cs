using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //TODO: Fix Sprint wtf dumbass

    public CharacterController controller;
    public Transform cam;

    public float speed;

    public float runSpeed = 6f;
    public float sprintSpeed = 12f;

    public float gravity = -9.81f;

    public float jumpHeight = 2;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    //stamina things
    public Slider staminaSlider;
    public float maxStamina = 4;
    public float stamina = 10;


    bool sprint = false;

    bool isGrounded = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Start
    void Start() 
    {
        //lock cursor invisible in scene
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //set stamina slider
        staminaSlider.maxValue = stamina;
    }


    // Update is called once per frame
    void Update()
    {
        //check ground state for jumping/air strafing
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            //hold player to ground when grounded
            velocity.y = -1f;
        }

        //get move input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //check sprint
        sprint = (Input.GetKey(KeyCode.LeftShift));

        if (sprint && stamina > 0)
        {
            speed = sprintSpeed;
            stamina -= Time.deltaTime;
        }
        else
        {
            if (stamina < maxStamina)
                stamina += Time.deltaTime;
            speed = runSpeed;
        }

        staminaSlider.value = stamina;

        //add gravity to movement
        velocity.y += gravity * Time.deltaTime;

        //check jump (get button down "jump" just checks for spacebar input)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir;

            if (vertical >= 0)
            {
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            else
            {
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.back;
            }

            //character move
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        
        //move in accordance to velocity
        controller.Move(velocity * Time.deltaTime);

        //remove cursor lock on escape key
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.lockState == CursorLockMode.Locked) 
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
