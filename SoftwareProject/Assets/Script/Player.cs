using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float jumpForce = 400f;

    private float walkMovementSpeed = 10f;
    private float attackMovementSpeed = 1f;

    // Wont walk of screen
    private float xMin = -50f, xMax = 50f, zMin = -8f, zMax = 8f;
    private float movementSpeed;

    //the characters body
    private Rigidbody rigidBody;

    //bool condition
    public bool facingRight;

    // accesses animator
    private Animator anim;

    //Animator State Info
    AnimatorStateInfo currentStateInfo;

    public GameObject attack1Box;
    public Sprite attack1SpriteHitFrame;

    SpriteRenderer currentSprite;

    //States
    static int currentState;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int runState = Animator.StringToHash("Base Layer.Run");

    static int attack1State = Animator.StringToHash("Base Layer.Attack1");
    static int heavySlashState = Animator.StringToHash("Base Layer.HeavySlash");

    //jump
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    //block
    static int blockState = Animator.StringToHash("Base Layer.Block");
    //hurt
    static int hurtState = Animator.StringToHash("Base Layer.Hurt");
    //fall
    static int fallState = Animator.StringToHash("Base Layer.Fall");








    private void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody>();
        movementSpeed = walkMovementSpeed;
        facingRight = true;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log(idleState);
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        currentState = currentStateInfo.fullPathHash;

        if (currentState == idleState)
        {
            Debug.Log("Idle State");
        }
        if (currentState == runState)
        {
            Debug.Log("Run State");
        }
        if (currentState == hurtState)
        {
            Debug.Log("Hurt State");
        }
        if (currentState == fallState)
        {
            Debug.Log("Fall State");
        }
        if (currentState == blockState)
        {
            Debug.Log("Block State");
        }

        //-Control Speed Based on Commands --------------------------------------------------
        if (currentState == idleState || currentState == runState)
        {
            movementSpeed = walkMovementSpeed;
        }
        else
        {
            movementSpeed = attackMovementSpeed;
        }

    }

    private void FixedUpdate()
    {

        // ----Movement -------------------------------------------------------------------------------------------


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * movementSpeed, rigidBody.velocity.y, moveVertical * movementSpeed);

        rigidBody.velocity = movement;

        rigidBody.position = new Vector3(Mathf.Clamp(rigidBody.position.x, xMin, xMax), transform.position.y, Mathf.Clamp(rigidBody.position.z, zMin, zMax));



        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }

        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        anim.SetFloat("Speed", rigidBody.velocity.sqrMagnitude);


        // - Combo Attacks ----------------------------------------------

        //Attack1
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Attack", true);
        }

        else
        {
            anim.SetBool("Attack", false);
        }


        if (attack1SpriteHitFrame == currentSprite.sprite)
        {
            attack1Box.gameObject.SetActive(true);
        }
        else
        {
            attack1Box.gameObject.SetActive(false);
        }


        // - Block ------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            anim.SetBool("Jump", true);
            rigidBody.AddForce(Vector3.up * jumpForce);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 thisScale = transform.localScale;

        thisScale.x *= -1;
        transform.localScale = thisScale;
    }





}
