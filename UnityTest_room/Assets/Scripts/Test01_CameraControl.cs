using UnityEngine;
using System.Collections;

public class Test01_CameraControl : MonoBehaviour {
    GameObject cameraParent;

    public GameObject menuBox;
    public GameObject titleBox;
    public GameObject exitBox;

    public GameObject zoomOutButton;

    Vector3 defaultPosition;
    Quaternion defaultRotation;
    float defaultZoom;

    float movePosX, prePosX;
    Vector3 prePos_zoom;

    bool zoomInState;
    bool oneTouchState;
    int touchDelay;

    bool boxOnCheck;
    bool preBoxOnCheck;

    float halfWidth;
    float halfHeight;

    // Use this for initialization
    void Start () {
        //Get parent of camera
        cameraParent = GameObject.Find("CameraParent");

        //Store default position
        defaultPosition = Camera.main.transform.position;
        defaultRotation = cameraParent.transform.rotation;
        defaultZoom = Camera.main.fieldOfView;

        zoomInState = false;
        oneTouchState = false;
        touchDelay = 10;

        boxOnCheck = false;
        preBoxOnCheck = false;

        zoomOutButton.SetActive(false);

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
	}
	
	void Update () {
        /* Camera moving for PC
        if (Input.GetMouseButton(0)) {
            Camera.main.transform.Translate(Input.GetAxisRaw("Mouse X") / 10, Input.GetAxisRaw("Mouse Y") / 10, 0);
        }
        */

        /* Camera Rotation for PC
        if (Input.GetMouseButton(1)) {
            cameraParent.transform.Rotate(0, Input.GetAxisRaw("Mouse X"), 0);
        }
        */

        /* Zoom in & zoom out for PC
        Camera.main.fieldOfView += (-20 * Input.GetAxis("Mouse ScrollWheel"));
        if (Camera.main.fieldOfView < 10) {
            Camera.main.fieldOfView = 10;
        } else if (Camera.main.fieldOfView > 100) {
            Camera.main.fieldOfView = 100;
        }
        */

        boxOnCheck = menuBox.activeSelf || titleBox.activeSelf || exitBox.activeSelf;
        if (boxOnCheck != preBoxOnCheck)
        {
            touchDelay = 10;
        }

        touchDelay--;

        //touch occur
        if (Input.touchCount > 0 && !boxOnCheck && touchDelay < 0)
        {
            //get touch coordinate
            float touchX = Input.GetTouch(0).position.x;
            float touchY = Input.GetTouch(0).position.y;
            float posX = touchX - halfWidth - transform.localPosition.x;
            Debug.Log("Touch X=" + touchX + " Y=" + touchY);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch begin.");
                prePosX = touchX;
                oneTouchState = true;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
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
                    oneTouchState = true;

                    zoomOutButton.SetActive(true);
                }
            }
        }

        preBoxOnCheck = boxOnCheck;

        /* Camera postion initialization for PC
        if (Input.GetMouseButton(2)) {
            Camera.main.transform.position = defaultPosition;
            cameraParent.transform.rotation = defaultRotation;
            Camera.main.fieldOfView = defaultZoom;
        }
        */
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
