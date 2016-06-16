using UnityEngine;
using System.Collections;

public class Room_CameraControl : MonoBehaviour {
    GameObject cameraParent;

    public GameObject menuWindow;
    public GameObject titleWindow;
    public GameObject exitWindow;

    public GameObject zoomOutButton;

    Vector3 defaultPosition;
    Quaternion defaultRotation;
    float defaultZoom;

    float movePosX, prePosX;
    Vector3 prePos_zoom;

    bool zoomInState;
    bool oneTouchState;
    int touchDelay;

    bool windowOnCheck;
    bool preWindowOnCheck;

    float halfWidth;
    float halfHeight;

    // Use this for initialization
    void Start () {
        //Get parent of camera
        cameraParent = GameObject.Find("CameraParent");

        //Store default camera position
        defaultPosition = Camera.main.transform.position;
        defaultRotation = cameraParent.transform.rotation;
        defaultZoom = Camera.main.fieldOfView;

        zoomInState = false;
        oneTouchState = false;
        touchDelay = 10;

        windowOnCheck = false;
        preWindowOnCheck = false;

        zoomOutButton.SetActive(false);

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
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
            float touchX = Input.GetTouch(0).position.x;
            float touchY = Input.GetTouch(0).position.y;
            float posX = touchX - halfWidth - transform.localPosition.x;
            Debug.Log("Touch X =" + touchX + " Y=" + touchY);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch begin.");
                prePosX = touchX;
                oneTouchState = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log("Touch slide. Rotate camera.");
                movePosX = prePosX - touchX;
                cameraParent.transform.Rotate(0, movePosX * Time.deltaTime, 0);
                prePosX = touchX;
                oneTouchState = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("Touch end.");
                //zoom in
                if (oneTouchState && !zoomInState)
                {
                    Debug.Log("Zoom in camera.");
                    prePos_zoom = Camera.main.transform.position;
                    Camera.main.transform.Translate(posX * Time.deltaTime * 0.2f, 0, 0);
                    Camera.main.fieldOfView = Camera.main.fieldOfView * 0.5f;

                    zoomInState = true;
                    oneTouchState = false;

                    zoomOutButton.SetActive(true);
                }
            }
        }

        preWindowOnCheck = windowOnCheck;
    }

    public void ZoomOutButton() //zoom out
    {
        Debug.Log("Zoom out camera.");
        Camera.main.transform.position = prePos_zoom;
        Camera.main.fieldOfView = defaultZoom;

        zoomOutButton.SetActive(false);
        zoomInState = false;
        touchDelay = 10;
    }
}
