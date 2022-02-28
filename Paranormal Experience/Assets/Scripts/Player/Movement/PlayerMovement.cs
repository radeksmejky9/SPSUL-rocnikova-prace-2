
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Transform playerCam;
    public Transform orientation;
    public Transform equipPosition;
    private Rigidbody rb;
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;
    public float moveSpeed;
    public float maxSpeed;
    public float counterMovement;
    public float threshold = 1;
    public bool eastereggdone = false;
    public bool easteregg = false;
    public Rigidbody eastereggrb;
    public Rigidbody eastereggrb2;
    float x, y;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    private void FixedUpdate()
    {
        if (!easteregg)
            Movement();
        else
            Easteregg();
    }

    private void Update()
    {
        if (easteregg)
            eastereggdone = true;
        if (!easteregg)
        {
            MyInput();
            Look();
        }
    }

    private void Easteregg()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            easteregg = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            eastereggrb.AddForce(eastereggrb.transform.forward * 25);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 a = eastereggrb.transform.eulerAngles;
            eastereggrb.transform.eulerAngles = new Vector3(a.x, a.y - 1, a.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            eastereggrb.AddForce(-eastereggrb.transform.forward * 25);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 a = eastereggrb.transform.eulerAngles;
            eastereggrb.transform.eulerAngles = new Vector3(a.x, a.y + 1, a.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            eastereggrb2.AddForce(eastereggrb2.transform.forward * 25);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 a = eastereggrb2.transform.eulerAngles;
            eastereggrb2.transform.eulerAngles = new Vector3(a.x, a.y - 1, a.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            eastereggrb2.AddForce(-eastereggrb2.transform.forward * 25);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 a = eastereggrb2.transform.eulerAngles;
            eastereggrb2.transform.eulerAngles = new Vector3(a.x, a.y + 1, a.z);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            easteregg = false;
        }
    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

    }


    private void Movement()
    {
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        float maxSpeed = this.maxSpeed;



        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime);

        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
    }





    private float desiredX;

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;


        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
        this.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }



    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitude = rb.velocity.magnitude;
        float yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }



}
