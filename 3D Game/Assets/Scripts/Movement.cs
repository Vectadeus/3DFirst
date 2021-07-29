using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed;
    public CharacterController Controller;
    public LookAround lookAround;
    Vector3 MoveVector;


    Vector3 Velocity;
    public float Gravity;

    //Jumping
    public float JumpStrength;
    bool IsGrounded;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask GroundMask;



    //MOVING
    bool isMoving;
    Vector3 PlayerSavedScale;
    Vector3 CControllerSavedSizes; //Character controller's Saved sizes



    //SLIDING
    public bool IsSliding;
    float PrevControllerColliderSize;
    Vector3 prevControllerCetner;

    //ANIMATIONS
    public Animator anim;




    private void OnEnable()
    {
        lookAround = GetComponent<LookAround>();
        anim = GetComponentInParent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckMovement());

    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        PlayerMove();
        Gravitation();
        Jump();
        Slide();
        Animations();


    }











    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        MoveVector = lookAround.PlayerTransform.right * x + lookAround.PlayerTransform.forward * y;
        Controller.Move(MoveVector * Speed * Time.deltaTime);
    }


    void Gravitation()
    {
        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButton("Jump") && IsGrounded)
        {
            Velocity.y = JumpStrength;
        }
    }

    void CheckGround()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundMask);
    }




    void Slide()
    {
        if (isMoving && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.C) && !IsSliding )
        {
            //Collider Heigh
            PrevControllerColliderSize = Controller.height;
            Controller.height = 0.6f;
            
            //COLLIDER CENTER OFFSET
            prevControllerCetner = new Vector3(Controller.center.x, Controller.center.y, Controller.center.z);
            Vector3 controllerCenter = new Vector3(Controller.center.x, 0.26f, Controller.center.z);
            Controller.center = controllerCenter;


            IsSliding = true;


        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            //Morchi gasrualebaas
            IsSliding = false;
            Controller.height = PrevControllerColliderSize;
            Controller.center = prevControllerCetner;
        }
    }//CLASS


    IEnumerator CheckMovement()
    {
        //WALKING
        Vector3 pos1 = lookAround.PlayerTransform.position;
        yield return new WaitForSeconds(0.05f);

        Vector3 pos2 = lookAround.PlayerTransform.position;
        if(pos1.x == pos2.x && pos1.z == pos2.z)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        StartCoroutine(CheckMovement());


    }



    //ANIMATIONS

    void Animations()
    {
        //WALKING
        if(anim != null)
        {
            if (isMoving)
            {
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }


            //Jumping
            if (!IsGrounded)
            {
                anim.SetBool("IsJumping", true);
            }
            else
            {
                anim.SetBool("IsJumping", false);
            }


            //Sliding
            if (IsSliding)
            {
                //Gaasrualdi
                anim.SetBool("IsSliding", true);
            }
            else
            {
                //Morchi gasrualebaas
                anim.SetBool("IsSliding", false);
            }
        }
        

    }






}//CLASS
