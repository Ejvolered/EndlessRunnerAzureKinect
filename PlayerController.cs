using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float Speed;
    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject Head;
    public GameObject RightShoulder;
    public GameObject LeftShoulder;
    public GameObject SpineChest;
    private bool jumpPermission; //Permission to jump
    private bool rightPermission; //Permission to go right
    private bool leftPermission; //Permission to go left
    private bool startPermission; //Permission to start
    public static bool startGame; //Starting game
    public static float forwardSpeed;

    private int desiredLane = 1; //0: left 1:middle 2:right
    public float laneDistance = 4;//distance between two lanes

    public float jumpForce;
    public float Gravity = -20;

    void Start()
    {
        jumpPermission = true;
        rightPermission = true;
        leftPermission = true;
        startPermission = false;
        startGame = false;
        forwardSpeed = Speed;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        direction.z = forwardSpeed;

        if(Head.transform.position.y < RightHand.transform.position.y)
        {
            UnityEngine.Debug.Log("Prawa rêka ponad g³ow¹!");
        }

        if (controller.isGrounded) 
        {

            if ((Head.transform.position.y < RightHand.transform.position.y) && (Head.transform.position.y < LeftHand.transform.position.y) && (jumpPermission == true))
            {
                Jump();
                UnityEngine.Debug.Log("Skok");
                jumpPermission = false;
            }
            if ((Head.transform.position.y > RightHand.transform.position.y) && (Head.transform.position.y > LeftHand.transform.position.y) && (jumpPermission == false))
            {
                jumpPermission = true;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                Jump();
                }

        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }



        if ((RightShoulder.transform.position.y < RightHand.transform.position.y) && (SpineChest.transform.position.y > LeftHand.transform.position.y) && (rightPermission == true))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
            rightPermission = false;
        }
        if ((RightShoulder.transform.position.y > RightHand.transform.position.y)  && (rightPermission == false))
        {
            rightPermission = true;
        }


        if ((LeftShoulder.transform.position.y < LeftHand.transform.position.y) && (SpineChest.transform.position.y > RightHand.transform.position.y) && (leftPermission == true))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
            leftPermission = false;
        }
        if ((LeftShoulder.transform.position.y > LeftHand.transform.position.y) && (leftPermission == false))
        {
            leftPermission = true;
        }



        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }


        //Calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane==0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80);
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        startingGame();
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    public void startingGame()
    {
            if ((Head.transform.position.y < RightHand.transform.position.y) && (Head.transform.position.y < LeftHand.transform.position.y && startPermission == false))
            {
                startPermission = true;
                UnityEngine.Debug.Log("startPermission = true");
            }

            if ((Head.transform.position.y > RightHand.transform.position.y) && (Head.transform.position.y > LeftHand.transform.position.y && startPermission == true))
            {
                startPermission = false;
                startGame = true;
            }

            if ((Input.GetKeyDown(KeyCode.Space)))
             {
            startGame = true;
             }
        
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag=="Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
