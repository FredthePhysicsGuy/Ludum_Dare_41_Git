using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Camera : SingletonManager<Head_Camera>
{
    [HideInInspector]
    public Transform cam_Pos_1;

    public float rotateSpeed;
    public bool invertYAxis;
    public bool pauseMovementandCursorLock;



    private float cameraSpeed;
    private float xRotate;
    private float yRotate;
    private float shiftSpeed;
    private GameObject cam_Obj;
    private CharacterController characterCont;

    private bool runPressed;
    private bool forwardPressed;
    private bool backPressed;
    private bool leftPressed;
    private bool rightPressed;
    private bool jumpPressed;
    private bool alreadyJumping;

    [HideInInspector]
    public Vector3 movingVec;

    override public void Awake()
    {
        base.Awake();
        pauseMovementandCursorLock = false;
    }


    // Use this for initialization
    void Start()
    {
        cameraSpeed = 0.05f;
        rotateSpeed = 3.0f;
        shiftSpeed = 1.0f;
        invertYAxis = true;


        forwardPressed = false;
        backPressed = false;
        leftPressed = false;
        rightPressed = false;
        jumpPressed = false;

        alreadyJumping = false;

        movingVec = Vector3.zero;

        yRotate = transform.localEulerAngles.x;
        xRotate = transform.localEulerAngles.y;
        cam_Obj = Camera.main.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterCont = gameObject.GetComponent<CharacterController>();
    }


    void Update()
    {

        forwardPressed = false;
        backPressed = false;
        leftPressed = false;
        rightPressed = false;
        jumpPressed = false;
        if(!pauseMovementandCursorLock)
        {
            if(Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                shiftSpeed = 7f;
            }
            else
            {
                shiftSpeed = 4f;
            }

            if (Input.GetKey(KeyCode.W))
            {

                //transform.Translate(Vector3.forward * cameraSpeed * shiftSpeed);
                forwardPressed = true;

            }
            if (Input.GetKey(KeyCode.A))
            {
                leftPressed = true;
                //transform.Translate(Vector3.left * cameraSpeed * shiftSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                backPressed = true;
                //transform.Translate(Vector3.back * cameraSpeed * shiftSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rightPressed = true;
                //transform.Translate(Vector3.right * cameraSpeed * shiftSpeed);
            }
            if(Input.GetKey(KeyCode.Space) && !Final_Launch.Instance.currentlyLaunching)
            {
                jumpPressed = true;
            }

            LookRotation();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private void FixedUpdate()
    {
        if(!pauseMovementandCursorLock)
        {
            Vector3 desiredMove = Vector3.zero;
            if (forwardPressed)
            {
                desiredMove = desiredMove + transform.forward;
            }
            if (backPressed)
            {
                desiredMove = desiredMove - transform.forward;
            }
            if (leftPressed)
            {
                desiredMove = desiredMove - transform.right;
            }
            if (rightPressed)
            {
                desiredMove = desiredMove + transform.right;
            }


            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, 1.0f, Vector3.down, out hitInfo,
                               1.0f / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            movingVec.x = desiredMove.x * shiftSpeed;
            movingVec.z = desiredMove.z * shiftSpeed;
            if(characterCont.isGrounded)
            {
                alreadyJumping = false;
            }
            if (jumpPressed && !alreadyJumping)
            {
                IEnumerator jumpCo = Jumping();
                StartCoroutine(jumpCo);
                alreadyJumping = true;
            }
            else if(alreadyJumping)
            {
                //Waiting for coroutine
            }
            else
            {
                movingVec.y = -2.0f;
            }
            characterCont.Move(movingVec * Time.fixedDeltaTime);
        }

    }

    private IEnumerator Jumping()
    {
        movingVec.y = 3.0f;
        while(movingVec.y > -1.5f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            movingVec.y = movingVec.y - 0.5f;
        }
        
    }


    private void LookRotation()
    {
        xRotate += Input.GetAxis("Mouse X") * rotateSpeed * shiftSpeed;
        if (invertYAxis)
        {
            yRotate -= Input.GetAxis("Mouse Y") * rotateSpeed * shiftSpeed;
        }
        else
        {
            yRotate += Input.GetAxis("Mouse Y") * rotateSpeed * shiftSpeed;
        }
        transform.rotation = Quaternion.Euler(yRotate, xRotate, 0);
    }

}
