using UnityEngine;
using System.Collections;

public class Room1_CameraControl : MonoBehaviour {
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

        //Store default camera position
        defaultPosition = Camera.main.transform.position;
        defaultRotation = cameraParent.transform.rotation;
        defaultZoom = Camera.main.fieldOfView;

        zoomInState = false;
        slideState = false;
        touchDelay = 10;

        windowOnCheck = false;
        preWindowOnCheck = false;

        zoomOutButton.SetActive(false);

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit = new RaycastHit();

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
            Debug.Log("Touch X =" + touchPos.position.x + " Y=" + touchPos.position.y);

            switch(touchPos.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Touch begin.");
                    prePosX = touchPos.position.x;
                    slideState = false;

                    Ray ray = Camera.main.ScreenPointToRay(touchPos.position);
                    /*
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("Hit point: " + hit.point);
                        if (hit.collider.tag == "tag")
                        {                          
                            Debug.Log("Hit tag: " + hit.collider.tag);
                            //work
                        }
                        if(hit.collider.name == "name")
                        {
                            Debug.Log("Hit object: " + hit.collider.name);
                            //work
                        }
                    } 
                    */
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Touch slide. Rotate camera.");
                    movePosX = prePosX - touchPos.position.x;
                    cameraParent.transform.Rotate(0, movePosX * Time.deltaTime, 0);
                    prePosX = touchPos.position.x;
                    slideState = true;
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch end.");
                    //zoom in
                    if (!slideState && !zoomInState)
                    {
                        Debug.Log("Zoom in camera.");
                        prePos_zoom = Camera.main.transform.position;
                        Camera.main.transform.Translate(posX * Time.deltaTime * 0.2f, 0, 0);
                        Camera.main.fieldOfView = Camera.main.fieldOfView * 0.5f;

                        zoomInState = true;

                        zoomOutButton.SetActive(true);
                    }
                    break;
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
