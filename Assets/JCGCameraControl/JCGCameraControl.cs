using UnityEngine;
using System.Collections;

public class JCGCameraControl : MonoBehaviour {

    //A 3rd person camera control script by Joe Censored Games

    //This script is used to control the camera, having it follow the player or whatever we're watching
    //Hold the right mouse button and move the mouse to rotate the camera, use the scroll function to zoom

    //To use this script, simply attach it to your camera object, and set Target to whatever the camera is to follow


    
    public bool EnableControls = true;  //Set to false whenever you want to disable right click rotation and scroll function
    //for example you may want to disable when interacting with UI elements

    public GameObject Target;  //You're required to set this

    public float MaxDistance = 400f;
    public float MinDistance = 100f;

    public float StartDistance = 200f;

    private float currentDistance;

    public float RotationSpeed = 90f;
    public float ScrollSpeed = 50f;


    public bool UseRotationMultipler1 = false;
    public bool UseRotationMultipler2 = false;

    public static float RotationMultiplier1 = 1f;  //Used as a rotation multiplier from an options menu
    public static float RotationMultiplier2 = 1f;

    [Range(-89.0f, 89.0f)]
    public float MaxAngle = 70f;
    [Range(-89.0f, 89.0f)]
    public float MinAngle = 10f;

    private float xMovement = 0.0f;
    private float yMovement = 0.0f;

    public int MouseButton = 1;  //Use 1 for the right mouse button, otherwise change it to whichever button you'd prefer
    //Typically left mouse button is 0, center button or pressing down on a scroll wheel is 2

    public bool ReverseScrollDirection = false;  //set to true if you want the scroll wheel zoom function to operate in reverse

    public bool AlwaysRotateWithMouseMovement = false;  //true enables mouse rotation without having to hold down a mouse button

    private Quaternion rotation;

    private float timeSinceLastWarningAboutMissingTarget = 10.0f;

    private bool initialized = false;

    public bool UseLateUpdate = true;  //Set to true if you want the camera position to update when LateUpdate() is called, set to false if you would rather use Update()

    public delegate void CallDelegateAfterPositionUpdate();
    public CallDelegateAfterPositionUpdate CallAfterPositionUpdate;  //Assign another method on another object to this if you want it called after the camera updates position
    //This is so you can reliably do some UI positioning after the camera updates, such as UI overlays above world space objects

    public bool DisableLogging = false;  //You may have a situation where you don't want to set your Target, if so set DisableLogging to true to not receive messages about it

    // Use this for initialization
    void Start () {
        currentDistance = StartDistance;
        yMovement = MinAngle;  //Camera starts set to the minimum angle, if you'd like it to start at a different angle you can change it here

        initialized = true;
    }

    private void OnEnable()
    {
        if (initialized)
        {
            updatePosition();
        }
    }


    private void Update()
    {
        if (UseLateUpdate == false)
        {
            masterUpdate();
        }
    }

    private void LateUpdate ()
    {
        if (UseLateUpdate == true)
        {
            masterUpdate();
        }
    }

    private void masterUpdate()
    {
        if (Target == null)
        {
            if (DisableLogging == false)
            {
                timeSinceLastWarningAboutMissingTarget = timeSinceLastWarningAboutMissingTarget + Time.deltaTime;
                if (timeSinceLastWarningAboutMissingTarget >= 10.0f)
                {
                    Debug.Log("JCGCameraControl.cs needs a Target");
                    timeSinceLastWarningAboutMissingTarget = 0f;
                }
            }
            return;
        }

        if (EnableControls)
        {
            zoom();

            //Here's where we check if either the chosen mouse button is held down, or if AlwaysRotateWithMouseMovement is enabled
            //to check if we should rotate the camera or not
            if (Input.GetMouseButton(MouseButton) || AlwaysRotateWithMouseMovement)
            {
                doRotation();
            }
        }

        updatePosition();
        if (CallAfterPositionUpdate != null)
        {
            CallAfterPositionUpdate();
        }
    }

    private void updatePosition()
    {
        rotation = Quaternion.Euler(yMovement, xMovement, 0);

        Vector3 distanceVector = new Vector3(0.0f, 0.0f, -currentDistance);
        Vector3 position = rotation * distanceVector + Target.transform.position;

        transform.position = position;
        transform.LookAt(Target.transform, Vector3.up);
    }


    private void doRotation()
    {
        float rotationMultiplier = 1f;
        if (UseRotationMultipler1)
        {
            rotationMultiplier = RotationMultiplier1;
        }
        else if (UseRotationMultipler2)
        {
            rotationMultiplier = RotationMultiplier2;
        }


        xMovement = xMovement + Input.GetAxis("Mouse X") * RotationSpeed * rotationMultiplier * 0.02f;
        yMovement = yMovement - Input.GetAxis("Mouse Y") * RotationSpeed * rotationMultiplier * 0.02f;

        if (yMovement > MaxAngle)
        {
            yMovement = MaxAngle;
        }
        if (yMovement < MinAngle)
        {
            yMovement = MinAngle;
        }

    }


    private void zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (ReverseScrollDirection)
        {
            currentDistance = currentDistance + (scroll * ScrollSpeed);
        }
        else
        {
            currentDistance = currentDistance + (-scroll * ScrollSpeed);
        }

        if (currentDistance > MaxDistance)
        {
            currentDistance = MaxDistance;
        }
        if (currentDistance < MinDistance)
        {
            currentDistance = MinDistance;
        }

    }
}
