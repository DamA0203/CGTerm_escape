using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Room_GameController : MonoBehaviour {
    public GameObject menuWindow;
    public GameObject titleWindow;
    public GameObject exitWindow;

    public GameObject key1;
    public GameObject drawer2;
    public GameObject paper;
    public GameObject paper_pianohint;
    public GameObject door;
    public GameObject driver;
    public GameObject picture_frame;
    public GameObject window;
    public GameObject door_left;
    public GameObject door_right;
    public GameObject hammer;

    public GameObject item1;
    public GameObject item1_s;
    public GameObject item2;
    public GameObject item2_s;
    public GameObject item3;
    public GameObject item3_s;
    public GameObject item4;
    public GameObject item4_s;
    public GameObject item5;
    public GameObject item5_s;

    public GameObject ending;

    int itemSelection;
    int itemCount;
    int pianoCorrectCount;
    int pwCorrectCount;

    float movePosX, prePosX;

    bool zoomInState;
    bool slideState;
    int touchDelay;

    bool openDrawer;
    bool openDoor;
    bool openWindow;
    bool openCloset;

    bool windowOnCheck;
    bool preWindowOnCheck;

    float halfWidth;
    float halfHeight;

    // Use this for initialization
    void Start () {
        item1.SetActive(false);
        item1_s.SetActive(false);
        item2.SetActive(false);
        item2_s.SetActive(false);
        item3.SetActive(false);
        item3_s.SetActive(false);
        item4.SetActive(false);
        item4_s.SetActive(false);
        item5.SetActive(false);
        item5_s.SetActive(false);
        ending.SetActive(false);

        paper_pianohint.SetActive(false);

        itemSelection = 0;
        itemCount = 0;
        pianoCorrectCount = 0;
        pwCorrectCount = 0;

        zoomInState = false;
        slideState = false;
        touchDelay = 5;

        openDrawer = false;
        openDoor = false;
        openWindow = false;
        openCloset = false;

        windowOnCheck = false;
        preWindowOnCheck = false;

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ending.activeSelf)
            {
                Debug.Log("[Touch]Move to title.");
                SceneManager.LoadScene("title");
            }
        }

        RaycastHit hit = new RaycastHit();

        //Touch delay setting
        windowOnCheck = menuWindow.activeSelf || titleWindow.activeSelf || exitWindow.activeSelf;
        if (windowOnCheck != preWindowOnCheck)
        {
            touchDelay = 5;
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
                    if (!slideState)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touchPos.position);
                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log("[Hit]Point: " + hit.point);
                            Debug.Log("[Hit]Object: " + hit.collider.name);

                            if (hit.collider.name == "Key1")
                            {
                                key1.SetActive(false);
                                item1.SetActive(true);
                                itemCount = 1;
                            }
                            else if (hit.collider.name == "Drawer2" && itemSelection == 1 && !openDrawer)
                            {
                                drawer2.transform.Translate(0, 0.5f, 0);
                                paper.SetActive(false);
                                item2.SetActive(true);
                                item1.SetActive(true);
                                item1_s.SetActive(false);
                                openDrawer = true;
                                itemCount = 2;
                            }
                            else if (hit.collider.name == "C4")
                            {
                                if (pianoCorrectCount == 0 || pianoCorrectCount == 3)
                                {
                                    pianoCorrectCount++;
                                }
                                else
                                {
                                    pianoCorrectCount = 0;
                                }

                            }
                            else if (hit.collider.name == "C#4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "D4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "D#4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "E4")
                            {
                                if (pianoCorrectCount == 4 || pianoCorrectCount == 5)
                                {
                                    pianoCorrectCount++;
                                }
                                else
                                {
                                    pianoCorrectCount = 0;
                                }
                            }
                            else if (hit.collider.name == "F4")
                            {
                                if (pianoCorrectCount == 2 || pianoCorrectCount == 6)
                                {
                                    pianoCorrectCount++;
                                }
                                else
                                {
                                    pianoCorrectCount = 0;
                                }
                            }
                            else if (hit.collider.name == "F#4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "G4")
                            {
                                if (pianoCorrectCount == 1)
                                {
                                    pianoCorrectCount++;
                                }
                                else
                                {
                                    pianoCorrectCount = 0;
                                }
                            }
                            else if (hit.collider.name == "G#4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "A4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "A#4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "B4")
                            {
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "Door" && itemSelection == 3 && !openDoor)
                            {
                                door.transform.Rotate(0, 0, -90.0f);
                                item3.SetActive(true);
                                item3_s.SetActive(false);
                                openDoor = true;
                            }
                            else if (hit.collider.name == "Driver")
                            {
                                driver.SetActive(false);
                                item4.SetActive(true);
                                itemCount = 4;
                            }
                            else if (hit.collider.name == "Picture_frame" && itemSelection == 4)
                            {
                                picture_frame.SetActive(false);
                                item4.SetActive(true);
                                item4_s.SetActive(false);
                            }
                            else if (hit.collider.name == "Window" && !openWindow)
                            {
                                window.transform.Rotate(0, 0, -90.0f);
                                openWindow = true;
                            }
                            else if (hit.collider.name == "num1")
                            {
                                if (pwCorrectCount == 2)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }
                            }
                            else if (hit.collider.name == "num2")
                            {
                                pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num3")
                            {
                                pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num4")
                            {
                                pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num5")
                            {
                                pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num6")
                            {
                                if (pwCorrectCount == 1)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }
                            }
                            else if (hit.collider.name == "num7")
                            {
                                if (pwCorrectCount == 0)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }
                            }
                            else if (hit.collider.name == "num8")
                            {
                                pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num9")
                            {
                                if (pwCorrectCount == 3)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }
                            }
                            else if (hit.collider.name == "Hammer")
                            {
                                hammer.SetActive(false);
                                item5.SetActive(true);
                                itemCount = 5;
                            }
                            else if (hit.collider.name == "Wall_fake1" && itemSelection == 5)
                            {
                                Debug.Log("Game End");
                                ending.SetActive(true);
                            }

                            else
                            {
                                if (pianoCorrectCount > 0)
                                {
                                    pianoCorrectCount = 0;
                                }
                                if (pwCorrectCount > 0)
                                {
                                    pwCorrectCount = 0;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        if (pianoCorrectCount == 7)
        {
            item3.SetActive(true);
            itemCount = 3;
        }
        if (pwCorrectCount == 4 && !openCloset)
        {
            door_left.transform.Rotate(0, 0, 90.0f);
            door_left.transform.Translate(-1.0f, 1.0f, 0);
            door_right.transform.Rotate(0, 0, -90.0f);
            openCloset = true;
        }
    }

    public void Ending()
    {
        Debug.Log("[Touch]Move to title.");
        SceneManager.LoadScene("title");
    }

    public void itemButton1()
    {
        if (touchDelay < 0)
        {
            if (itemSelection == 1)
            {
                Debug.Log("[Item1]Release.");
                itemSelection = 0;
                item1.SetActive(true);
                item1_s.SetActive(false);
            }
            else
            {
                Debug.Log("[Item1]Use.");
                itemSelection = 1;
                item1.SetActive(false);
                item1_s.SetActive(true);
                if (itemCount > 1)
                {
                    item2.SetActive(true);
                    item2_s.SetActive(false);
                    if (itemCount > 2)
                    {
                        item3.SetActive(true);
                        item3_s.SetActive(false);
                        if (itemCount > 3)
                        {
                            item4.SetActive(true);
                            item4_s.SetActive(false);
                            if (itemCount > 4)
                            {
                                item5.SetActive(true);
                                item5_s.SetActive(false);
                            }
                        }
                    }
                }   
            }
            touchDelay = 5;
        }
    }

    public void itemButton2()
    {
        if (touchDelay < 0)
        {
            if (itemSelection == 2)
            {
                Debug.Log("[Item2]Release.");
                itemSelection = 0;
                paper_pianohint.SetActive(false);
                item2.SetActive(true);
                item2_s.SetActive(false);
            }
            else
            {
                Debug.Log("[Item2]Use.");
                itemSelection = 2;
                paper_pianohint.SetActive(true);
                item1.SetActive(true);
                item1_s.SetActive(false);
                item2.SetActive(false);
                item2_s.SetActive(true);
                if (itemCount > 2)
                {
                    item3.SetActive(true);
                    item3_s.SetActive(false);
                    if (itemCount > 3)
                    {
                        item4.SetActive(true);
                        item4_s.SetActive(false);
                        if (itemCount > 4)
                        {
                            item5.SetActive(true);
                            item5_s.SetActive(false);
                        }
                    }
                } 
            }
            touchDelay = 5;
        }
    }

    public void itemButton3()
    {
        if (touchDelay < 0)
        {
            if (itemSelection == 3)
            {
                Debug.Log("[Item3]Release.");
                itemSelection = 0;
                item3.SetActive(true);
                item3_s.SetActive(false);
            }
            else
            {
                Debug.Log("[Item3]Use.");
                itemSelection = 3;
                item1.SetActive(true);
                item1_s.SetActive(false);
                item2.SetActive(true);
                item2_s.SetActive(false);
                item3.SetActive(false);
                item3_s.SetActive(true);
                if (itemCount > 3)
                {
                    item4.SetActive(true);
                    item4_s.SetActive(false);
                    if (itemCount > 4)
                    {
                        item5.SetActive(true);
                        item5_s.SetActive(false);
                    }
                }
            }
            touchDelay = 5;
        }
    }

    public void itemButton4()
    {
        if (touchDelay < 0)
        {
            if (itemSelection == 4)
            {
                Debug.Log("[Item4]Release.");
                itemSelection = 0;
                item4.SetActive(true);
                item4_s.SetActive(false);
            }
            else
            {
                Debug.Log("[Item4]Use.");
                itemSelection = 4;
                item1.SetActive(true);
                item1_s.SetActive(false);
                item2.SetActive(true);
                item2_s.SetActive(false);
                item3.SetActive(true);
                item3_s.SetActive(false);
                item4.SetActive(false);
                item4_s.SetActive(true);
                if (itemCount > 4)
                {
                    item5.SetActive(true);
                    item5_s.SetActive(false);
                }
            }
            touchDelay = 5;
        }
    }

    public void itemButton5()
    {
        if (touchDelay < 0)
        {
            if (itemSelection == 5)
            {
                Debug.Log("[Item5]Release.");
                itemSelection = 0;
                item5.SetActive(true);
                item5_s.SetActive(false);
            }
            else
            {
                Debug.Log("[Item5]Use.");
                itemSelection = 5;
                item1.SetActive(true);
                item1_s.SetActive(false);
                item2.SetActive(true);
                item2_s.SetActive(false);
                item3.SetActive(true);
                item3_s.SetActive(false);
                item4.SetActive(true);
                item4_s.SetActive(false);
                item5.SetActive(false);
                item5_s.SetActive(true);
            }
            touchDelay = 5;
        }
    }
}
