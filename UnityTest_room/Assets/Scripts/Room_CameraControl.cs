using UnityEngine;
using System.Collections;

public class Room_CameraControl : MonoBehaviour {
    GameObject cameraParent;

    public GameObject menuWindow;
    public GameObject titleWindow;
    public GameObject exitWindow;
    public GameObject zoomOutButton;

    public GameObject direction1;
    public GameObject direction2;
    public GameObject direction3;
    public GameObject direction4;

    Vector3 defaultPosition;
    Quaternion defaultRotation;
    float defaultZoom;

    Vector3 prePosition;

    float movePosX, prePosX;

    bool zoomInState;
    bool slideState;
    int touchDelay;

    bool windowOnCheck;
    bool preWindowOnCheck;

    float halfWidth;
    float halfHeight;
    
    // Use this for initialization
    void Start () {
        //Get parent of camera
        cameraParent = GameObject.Find("CameraParent");

        //Store default state
        defaultPosition = Camera.main.transform.position;
        defaultRotation = cameraParent.transform.rotation;
        defaultZoom = Camera.main.fieldOfView;

        //initialize previous state
        prePosition = defaultPosition;

        zoomInState = false;
        slideState = false;
        touchDelay = 10;

        windowOnCheck = false;
        preWindowOnCheck = false;

        zoomOutButton.SetActive(false);

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
	}
	
	void Update () {
        RaycastHit hit = new RaycastHit();

        //Touch delay setting
        windowOnCheck = menuWindow.activeSelf || titleWindow.activeSelf || exitWindow.activeSelf;
        if (windowOnCheck != preWindowOnCheck)
        {
            touchDelay = 10;
        }
        touchDelay--;

        //touch occur
        if (Input.touchCount > 0 && !windowOnCheck && touchDelay < 0)
        {
            //get touch coordinate
            Touch touchPos = Input.GetTouch(0);
            float posX = touchPos.position.x - halfWidth - transform.localPosition.x;

            switch (touchPos.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("[Touch]Began.");
                    Debug.Log("x=" + touchPos.position.x + " y=" + touchPos.position.y);
                    prePosX = touchPos.position.x;
                    slideState = false;
                    break;
                
                //rotate camera
                case TouchPhase.Moved:
                    Debug.Log("[Touch]Moved.");
                    Debug.Log("x=" + touchPos.position.x + " y=" + touchPos.position.y);
                    movePosX = prePosX - touchPos.position.x;
                    cameraParent.transform.Rotate(0, movePosX * Time.deltaTime * 1.5f, 0);
                    prePosX = touchPos.position.x;
                    slideState = true;
                    break;

                case TouchPhase.Ended:
                    Debug.Log("[Touch]Ended.");

                    if (!slideState && !zoomInState)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touchPos.position);
                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log("[Hit]Point: " + hit.point);
                            Debug.Log("[Hit]Object: " + hit.collider.name);
                            if (hit.collider.name == "Closet")
                            {
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction3.transform.position);
                                Camera.main.transform.Translate(4.75f, -1.0f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.20f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                        }
                    }
                    break;
            }
        }

        preWindowOnCheck = windowOnCheck;
    }

    //zoom out
    public void ZoomOutButton()
    {
        Debug.Log("Zoom out camera.");
        Camera.main.transform.position = prePosition;
        Camera.main.fieldOfView = defaultZoom;

        zoomOutButton.SetActive(false);
        zoomInState = false;
        touchDelay = 10;
    }
}

