using UnityEngine;
using System.Collections;

public class Room_CameraControl : MonoBehaviour {
    GameObject cameraParent;

    Vector3 defaultPosition;
    Quaternion defaultRotation;
    float defaultZoom;

    bool zoomInState;
    bool touchState;

    float halfWidth;
    float halfHeight;

    float movePosX, prePosX;

    Vector3 prePos_zoom;

    // Use this for initialization
    void Start () {
        //Get parent of camera
        cameraParent = GameObject.Find("CameraParent");

        //Store default camera position
        defaultPosition = Camera.main.transform.position;
        defaultRotation = cameraParent.transform.rotation;
        defaultZoom = Camera.main.fieldOfView;

        zoomInState = false;
        touchState = false;

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)   //touch occur
        {
            //get touch coordinate
            float touchX = Input.GetTouch(0).position.x;
            float posX = touchX - halfWidth - transform.localPosition.x;

            //start touch
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch begin.");
                prePosX = touchX;
                touchState = true;
            }

            //moving touch point => camera rotation
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log("Touch slide. Rotate camera.");
                movePosX = prePosX - touchX;
                cameraParent.transform.Rotate(0, movePosX * Time.deltaTime, 0);
                prePosX = touchX;
                touchState = false;
            }

            //end touch
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("Touch end.");
                //zoom in
                if (touchState && !zoomInState)
                {
                    Debug.Log("Zoom in camera.");
                    prePos_zoom = Camera.main.transform.position;
                    Camera.main.transform.Translate(posX * Time.deltaTime * 0.2f, 0, 0);
                    Camera.main.fieldOfView = Camera.main.fieldOfView * 0.5f;

                    zoomInState = true;
                    touchState = false;
                }

                //zoom out
                else if (touchState && zoomInState)
                {
                    Debug.Log("Zoom out camera.");
                    Camera.main.transform.position = prePos_zoom;
                    Camera.main.fieldOfView = defaultZoom;

                    zoomInState = false;
                    touchState = false;
                }
            }
        }
    }
}
