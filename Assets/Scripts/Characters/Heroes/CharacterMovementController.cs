using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] Collider heroWeaponCollider;
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform targetTransform;
    [SerializeField] LayerMask mouseAimMask;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float walkingSpeed;
    [SerializeField] float runningSpeed;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3;

    Camera mainCamera;
    Vector2 input;
    float speed = 0;
    Vector3 velocity;
    bool isGrounded;
    bool isFalling;

    PlayerInput playerInput;

    int FacingSide 
    {
        get 
        {
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0 ? 1 : 0;
        }
    }
    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    void Start()
    {
        mainCamera = Camera.main;
        playerInput.InputHandler.Attack.performed += context => Attack();playerInput.InputHandler.Attack.performed += context => Attack();
        playerInput.InputHandler.Block.started += context => Block(true);
        playerInput.InputHandler.Block.canceled += context => Block(false);
        playerInput.InputHandler.Jump.performed += context => Jump();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            input = playerInput.InputHandler.Movement.ReadValue<Vector2>();
            Move();
            isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

            #region MOUSE AIMING
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            {
                targetTransform.position = hit.point;
            }
            #endregion

            #region GRAVITY

            // Apply gravity
            velocity.y += gravity * 2 *  Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2;
            }
            #endregion

            #region FACING DIRECTION
            // Character Facing Direction of Mouse Logic
            Quaternion rotateTo = Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0));
            transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, 10 * Time.deltaTime);

            animator.SetFloat("multiplier", (FacingSide * input.x));

            #endregion

            #region ANIMATIONS
            if (isGrounded)
            {
                animator.SetBool("isGrounded", true);
                isFalling = false;
                //Walking & Runnning Animation Logic
                if (input.x != 0 && !Input.GetButton("Fire3"))
                {
                    speed = walkingSpeed;
                    ChangeTag("Player");
                }
                else if (input.x != 0 && Input.GetButton("Fire3"))
                {
                    speed = runningSpeed;
                    ChangeTag("Player");
                }
                else
                {
                    animator.SetFloat("multiplier", 1);
                    ChangeTag("Player");
                    speed = 0;
                }
            }
            // Jumping Animation Logic
            else
            {
                animator.SetBool("isGrounded", false);
                isFalling = true;
            }
            animator.SetFloat("speed", speed);
        }
        #endregion
    }
    void FixedUpdate()
    {
        // Character Movement Logic
        Vector3 move = new Vector3(input.x, 0, 0);
        characterController.Move(move * Time.fixedDeltaTime * speed);
    }

    public void Move()
    {
        // Hack to alwayys evaluate the multiplying speed of the animation to 1
        float animationSpeedMultiplier = 1;

        if (input.x < 0)
            animationSpeedMultiplier = -1;
        else
            animationSpeedMultiplier = 1;

        // Sets the animation parameter speed so it can make the player turn where the mouse is aiming
        animator.SetFloat("speed", animationSpeedMultiplier * FacingSide);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            animator.SetBool("isJumping", true);
            isFalling = true;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("BasicAttack");
        heroWeaponCollider.enabled = true;
        ChangeTag("Player");
    }

    public void Block(bool state)
    {
        // Blocking Animation Logic
        animator.SetBool("isBlocking", state);
        ChangeTag("ShieldedPlayer");
    }

    private void OnAnimatorIK()
    {
        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(targetTransform.position);
    }

    void ChangeTag(string tag)
    {
        gameObject.tag = tag;
    }

    private void OnEnable()
    {
        playerInput.InputHandler.Enable();
    }

    private void OnDisable()
    {
        playerInput.InputHandler.Disable();
    }

}