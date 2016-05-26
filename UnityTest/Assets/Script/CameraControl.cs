using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    GameObject cameraParent;

    //Store default camera position
    Vector3 defaultPosition;
    Quaternion defaultRotation;
    float defaultZoom;

    bool zoomInState;
    bool touchState;

    float halfWidth;
    float halfHeight;

    float movePosX, prePosX;

    Vector3 prePos_zoom;

	void Start () {
        //Get parent of camera
        cameraParent = GameObject.Find("CameraParent");

        //Store default position
        defaultPosition = Camera.main.transform.position;
        defaultRotation = cameraParent.transform.rotation;
        defaultZoom = Camera.main.fieldOfView;

        zoomInState = false;
        touchState = false;

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
	}
	
	void Update () {
        //exit game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

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

        if (Input.touchCount > 0) {
            float touchX = Input.GetTouch(0).position.x;
            float deltaX = touchX - halfWidth;
            float posX = touchX - halfWidth - transform.localPosition.x;

            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                prePosX = touchX;
                touchState = true;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                movePosX = prePosX - touchX;
                cameraParent.transform.Rotate(0, movePosX * Time.deltaTime, 0);
                prePosX = touchX;
                touchState = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                if (touchState && !zoomInState) {
                    prePos_zoom = Camera.main.transform.position;
                    Camera.main.transform.Translate(posX * Time.deltaTime * 0.2f, 0, 0);
                    Camera.main.fieldOfView = Camera.main.fieldOfView * 0.5f;

                    zoomInState = true;
                    touchState = true;
                }
                else if (touchState && zoomInState) {
                    Camera.main.transform.position = prePos_zoom;
                    Camera.main.fieldOfView = defaultZoom;

                    zoomInState = false;
                    touchState = true;
                }
            }
        }

        /* Camera postion initialization for PC
        if (Input.GetMouseButton(2)) {
            Camera.main.transform.position = defaultPosition;
            cameraParent.transform.rotation = defaultRotation;
            Camera.main.fieldOfView = defaultZoom;
        }
        */
    }
}
