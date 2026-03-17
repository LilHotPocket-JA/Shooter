using UnityEngine;

public class FPSControl : MonoBehaviour
{
    private CharacterController chara;
    public float walkSpd = 5;
    float vertR;
    public float lookLimit = 80f;
    public float sensitivity = 2;

    private Camera cam;
    private float defWalk;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defWalk = walkSpd;
        chara = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        MouseLook();

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            walkSpd = 10; 
        }

        else 
        {
            walkSpd = defWalk;
        }


    }


    void Movement()
    {
        //Initialize/ Declare variaables to get UNITY's axis
        float verInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        //Makes a variable that allows us to walk;
        float vSpeed = verInput * walkSpd;
        float horSpeed = horInput * walkSpd;

        Vector3 horizontalMov = new Vector3(horSpeed, 0, vSpeed);

        chara.Move(horizontalMov * Time.deltaTime);
    }

     void MouseLook()
    {
        float mouseXR = Input.GetAxis("Mouse X") * sensitivity;

        transform.Rotate(0,mouseXR,0);
        vertR -= Input.GetAxis("Mouse Y") * sensitivity;
        vertR = Mathf.Clamp(vertR, -lookLimit , lookLimit);
        cam.transform.localRotation = Quaternion.Euler(vertR, 0, 0);
    }
}
