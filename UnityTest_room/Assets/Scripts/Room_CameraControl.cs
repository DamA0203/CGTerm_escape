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
    public GameObject dp4;
    public GameObject wallhint;

    Vector3 defaultPosition;
    Quaternion defaultRotation;
    float defaultZoom;

    Vector3 prePosition;

    float movePosX, prePosX;

    bool zoomInState;
    bool slideState;
    int touchDelay;

    bool pianoZoom;
    bool wallhintZoom;

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

        pianoZoom = false;
        wallhintZoom = false;

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
                    if (!zoomInState)
                    {
                        Debug.Log("x=" + touchPos.position.x + " y=" + touchPos.position.y);
                        movePosX = prePosX - touchPos.position.x;
                        cameraParent.transform.Rotate(0, movePosX * Time.deltaTime * 1.25f, 0);
                        prePosX = touchPos.position.x;
                        slideState = true;
                    }
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
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction3.transform.position);
                                Camera.main.transform.Translate(4.75f, -1.0f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.20f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Desk")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction2.transform.position);
                                Camera.main.transform.Translate(4.0f, 0, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.50f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Piano")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction1.transform.position);
                                Camera.main.transform.Translate(3.3f, 1.0f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.10f;
                                Camera.main.transform.LookAt(dp4.transform.position);
                                zoomInState = true;
                                pianoZoom = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Door")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction1.transform.position);
                                Camera.main.transform.Translate(-2.0f, 0, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.80f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Picture_frame" || hit.collider.name == "Window")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction2.transform.position);
                                Camera.main.transform.Translate(-1.5f, 0.5f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.80f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Wall_fake2")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction2.transform.position);
                                Camera.main.transform.Translate(-1.5f, 0.5f, 5.25f);
                                Camera.main.transform.LookAt(wallhint.transform.position);
                                zoomInState = true;
                                wallhintZoom = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Kitchen_closet")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction4.transform.position);
                                Camera.main.transform.Translate(1.25f, -0.5f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Clock")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction1.transform.position);
                                Camera.main.transform.Translate(0, 1.0f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "box_big_closed")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction1.transform.position);
                                Camera.main.transform.Translate(1.0f, -0.5f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Flowerpot")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction2.transform.position);
                                Camera.main.transform.Translate(0.75f, 0, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Sofa")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction3.transform.position);
                                Camera.main.transform.Translate(0.75f, 0, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Lamp")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction3.transform.position);
                                Camera.main.transform.Translate(-1.5f, 0, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Kitchen_closet2")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction4.transform.position);
                                Camera.main.transform.Translate(4.5f, 0.5f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Shelf_small")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction4.transform.position);
                                Camera.main.transform.Translate(1.0f, 0.5f, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                            else if (hit.collider.name == "Blue_shelf")
                            {
                                Debug.Log("Zoom in camera.");
                                prePosition = Camera.main.transform.position;
                                Camera.main.transform.LookAt(direction4.transform.position);
                                Camera.main.transform.Translate(-1.0f, 0, 0);
                                Camera.main.fieldOfView = Camera.main.fieldOfView * 0.35f;
                                zoomInState = true;
                                zoomOutButton.SetActive(true);
                            }
                        }
                    }
                    else if (!slideState)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touchPos.position);
                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log("[Hit]Point: " + hit.point);
                            Debug.Log("[Hit]Object: " + hit.collider.name);

                            if (hit.collider.name == "Wall_fake2")
                            {
                                Camera.main.transform.Translate(0, 0, 5.25f);
                                Camera.main.transform.LookAt(wallhint.transform.position);
                                wallhintZoom = true;
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

        if (pianoZoom)
        {
            Camera.main.transform.LookAt(direction1.transform.position);
            pianoZoom = false;
        }
        else if (wallhintZoom)
        {
            Camera.main.transform.LookAt(direction2.transform.position);
            wallhintZoom = false;
        }

        zoomOutButton.SetActive(false);
        zoomInState = false;
        touchDelay = 10;
    }
}

