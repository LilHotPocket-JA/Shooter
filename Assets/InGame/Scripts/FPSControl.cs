using UnityEngine;

public class FPSControl : MonoBehaviour
{
    private CharacterController chara;


    [Range(0, 30)] public float walkSpd = 5;
    [Range(0, 10)] public float sensitivity = 2;
    [Range(0, 10)]  public float jumpHeight;

    float vertR;
    public float lookLimit = 80f;
    private Camera cam;
    private float defWalk;
    private float runSpd;
    private Vector3 currMovement;
    private float g = 9.81f;

    private Vector3 hitPoint;
    public ParticleSystem impact;
    [Range(10,30)] public int particleC = 20;
    private Transform location;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        location = GetComponent<Transform>();
        runSpd = walkSpd * 2;
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
        Jumping();

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            walkSpd = runSpd; 
        }

        else 
        {
            walkSpd = defWalk;
        }

        if (ObjectInFocus() != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ObjectInFocus().gameObject.tag == "Enemy")
                {
                    impact.transform.position = hitPoint;
                    impact.Emit(particleC);
                    DestroyObject(ObjectInFocus().gameObject);
                }

                else if(ObjectInFocus().gameObject.tag == "Box")
                {
                    ObjectInFocus().gameObject.transform.position = location.localPosition;
                }


            }
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
        currMovement.x = horizontalMov.x;
        currMovement.z = horizontalMov.z;

        chara.Move(currMovement * Time.deltaTime);
    }

     void MouseLook()
    {
        float mouseXR = Input.GetAxis("Mouse X") * sensitivity;

        transform.Rotate(0,mouseXR,0);
        vertR -= Input.GetAxis("Mouse Y") * sensitivity;
        vertR = Mathf.Clamp(vertR, -lookLimit , lookLimit);
        cam.transform.localRotation = Quaternion.Euler(vertR, 0, 0);
    }

    void Jumping()
    {
        if (chara.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currMovement.y = jumpHeight;
                Debug.Log("jump");
            }
            
        }

        else
        {
            currMovement.y -= g * Time.deltaTime;
            Debug.Log("falling");
        }
    }

    public GameObject ObjectInFocus()
    {
        GameObject result = null;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            result = hit.transform.gameObject;
            hitPoint = hit.point;
        }
        return result;
    }
}
