using UnityEngine;
using System.Collections;

public class Room_GameController : MonoBehaviour {
    public GameObject menuWindow;
    public GameObject titleWindow;
    public GameObject exitWindow;

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
        zoomInState = false;
        slideState = false;
        touchDelay = 10;

        windowOnCheck = false;
        preWindowOnCheck = false;

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
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
                    slideState = false;
                    break;

                //rotate camera
                case TouchPhase.Moved:
                    slideState = true;
                    break;

                case TouchPhase.Ended:
                    if (!slideState && !zoomInState)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touchPos.position);
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.name == "Closet")
                            {
                                //implement
                            }
                        }
                    }
                    break;
            }
        }
    }
}
