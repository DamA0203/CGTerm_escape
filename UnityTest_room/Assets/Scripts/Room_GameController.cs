using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Room_GameController : MonoBehaviour {
    public GameObject menuWindow;
    public GameObject titleWindow;
    public GameObject exitWindow;

    public GameObject key1;
    public GameObject glassdoor;
    public GameObject drawer1;
    public GameObject drawer2;
    public GameObject drawer3;
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

    private AudioSource pianosound;
    public AudioClip c4;
    public AudioClip cc4;
    public AudioClip d4;
    public AudioClip dd4;
    public AudioClip e4;
    public AudioClip f4;
    public AudioClip ff4;
    public AudioClip g4;
    public AudioClip gg4;
    public AudioClip a4;
    public AudioClip aa4;
    public AudioClip b4;
    public AudioClip getitem;
    public AudioClip numsound;
    public AudioClip opendrawer;
    public AudioClip opendoor;
    public AudioClip framedrop;
    public AudioClip wallbreak;
    public AudioClip openwindow;
    public AudioClip hooray;
    public AudioClip locked;
    public AudioClip guitarsound;
    public AudioClip error;

    public GameObject ending;

    int itemSelection;
    int itemCount;
    int pianoCorrectCount;
    int pwCorrectCount;
    int passcount;
    public static int password;

    float movePosX, prePosX;

    bool zoomInState;
    bool slideState;
    int touchDelay;

    bool openDrawer1;
    bool openDrawer;
    bool openDoor;
    bool doorkeytukatta;
    bool openWindow;
    public static bool openCloset;
    bool openGlass;

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
        passcount = 0;
        password = 0;

        zoomInState = false;
        slideState = false;
        touchDelay = 5;

        openGlass = false;
        openDrawer1 = false;
        openDrawer = false;
        openDoor = false;
        doorkeytukatta = false;
        openWindow = false;
        openCloset = false;

        windowOnCheck = false;
        preWindowOnCheck = false;

        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;

        pianosound = GetComponent<AudioSource>();
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
                                pianosound.PlayOneShot(getitem);
                                key1.SetActive(false);
                                item1.SetActive(true);
                                itemCount = 1;
                            }
                            else if (hit.collider.name == "Glassdoor")
                            {
                                if (!openGlass)
                                {
                                    pianosound.PlayOneShot(openwindow);
                                    glassdoor.transform.Rotate(0, 0, 90.0f);
                                    openGlass = true;
                                }
                                else
                                {
                                    pianosound.PlayOneShot(openwindow);
                                    glassdoor.transform.Rotate(0, 0, -90.0f);
                                    openGlass = false;
                                }
                            }
                            else if (hit.collider.name == "Guitar")
                            {
                                pianosound.PlayOneShot(guitarsound);
                            }
                            else if (hit.collider.name == "Drawer1") {
                                if (!openDrawer1)
                                {
                                    pianosound.PlayOneShot(opendrawer);
                                    drawer1.transform.Translate(0, 0.5f, 0);
                                    openDrawer1 = true;
                                }
                                else
                                {
                                    pianosound.PlayOneShot(opendrawer);
                                    drawer1.transform.Translate(0, -0.5f, 0);
                                    openDrawer1 = false;
                                }
                            }
                            else if (hit.collider.name == "Drawer2")
                            {
                                if (!openDrawer)
                                {
                                    if (itemCount == 0 || (itemCount == 1 && itemSelection != 1)) pianosound.PlayOneShot(locked);
                                    else if (itemCount == 1 && itemSelection == 1)
                                    {
                                        pianosound.PlayOneShot(opendrawer);
                                        drawer2.transform.Translate(0, 0.5f, 0);
                                        paper.SetActive(false);
                                        pianosound.PlayOneShot(getitem);
                                        item2.SetActive(true);
                                        item1.SetActive(true);
                                        item1_s.SetActive(false);
                                        openDrawer = true;
                                        itemCount = 2;
                                    }
                                    else if (itemCount >= 2)
                                    {
                                        pianosound.PlayOneShot(opendrawer);
                                        drawer2.transform.Translate(0, 0.5f, 0);
                                        openDrawer = true;
                                    }
                                }
                                else
                                {
                                    pianosound.PlayOneShot(opendrawer);
                                    drawer2.transform.Translate(0, -0.5f, 0);
                                    openDrawer = false;
                                }
                            }
                            else if (hit.collider.name == "Drawer3")
                            {
                                pianosound.PlayOneShot(locked);
                            }
                            else if (hit.collider.name == "C4")
                            {
                                pianosound.PlayOneShot(c4);
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
                                pianosound.PlayOneShot(cc4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "D4")
                            {
                                pianosound.PlayOneShot(d4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "D#4")
                            {
                                pianosound.PlayOneShot(dd4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "E4")
                            {
                                pianosound.PlayOneShot(e4);
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
                                pianosound.PlayOneShot(f4);
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
                                pianosound.PlayOneShot(ff4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "G4")
                            {
                                pianosound.PlayOneShot(g4);
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
                                pianosound.PlayOneShot(gg4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "A4")
                            {
                                pianosound.PlayOneShot(a4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "A#4")
                            {
                                pianosound.PlayOneShot(aa4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "B4")
                            {
                                pianosound.PlayOneShot(b4);
                                pianoCorrectCount = 0;
                            }
                            else if (hit.collider.name == "Door")
                            {
                                if (!openDoor)
                                {
                                    if (itemCount < 3 || (itemCount == 3 && itemSelection != 3)) pianosound.PlayOneShot(locked);
                                    else if (itemCount == 3 && itemSelection == 3 && !doorkeytukatta)
                                    {
                                        pianosound.PlayOneShot(opendoor);
                                        door.transform.Rotate(0, 0, -90.0f);
                                        item3.SetActive(true);
                                        item3_s.SetActive(false);
                                        openDoor = true;
                                        doorkeytukatta = true;
                                    }
                                    else if (itemCount >= 3 && !openDoor && doorkeytukatta)
                                    {
                                        pianosound.PlayOneShot(opendoor);
                                        door.transform.Rotate(0, 0, -90.0f);
                                        openDoor = true;
                                    }
                                }
                                else
                                {
                                    pianosound.PlayOneShot(opendoor);
                                    door.transform.Rotate(0, 0, 90.0f);
                                    openDoor = false;
                                }
                            }
                            else if (hit.collider.name == "Driver")
                            {
                                pianosound.PlayOneShot(getitem);
                                driver.SetActive(false);
                                item4.SetActive(true);
                                itemCount = 4;
                            }
                            else if (hit.collider.name == "Picture_frame" && itemSelection == 4)
                            {
                                pianosound.PlayOneShot(framedrop);
                                picture_frame.SetActive(false);
                                item4.SetActive(true);
                                item4_s.SetActive(false);
                            }
                            else if (hit.collider.name == "Window")
                            {
                                if (!openWindow)
                                {
                                    pianosound.PlayOneShot(openwindow);
                                    window.transform.Rotate(0, 0, -90.0f);
                                    openWindow = true;
                                }
                                else
                                {
                                    pianosound.PlayOneShot(openwindow);
                                    window.transform.Rotate(0, 0, 90.0f);
                                    openWindow = false;
                                }
                            }
                            else if (hit.collider.name == "num1")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 1;
                                passcount++;/*
                                if (pwCorrectCount == 2)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }*/
                            }
                            else if (hit.collider.name == "num2")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 2;
                                passcount++;
                                //pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num3")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 3;
                                passcount++;
                                //pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num4")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 4;
                                passcount++;
                                //pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num5")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 5;
                                passcount++;
                                //pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num6")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 6;
                                passcount++;/*
                                if (pwCorrectCount == 1)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }*/
                            }
                            else if (hit.collider.name == "num7")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 7;
                                passcount++;/*
                                if (pwCorrectCount == 0)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }*/
                            }
                            else if (hit.collider.name == "num8")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 8;
                                passcount++;
                                //pwCorrectCount = 0;
                            }
                            else if (hit.collider.name == "num9")
                            {
                                pianosound.PlayOneShot(numsound);
                                password = password * 10 + 9;
                                passcount++;/*
                                if (pwCorrectCount == 3)
                                {
                                    pwCorrectCount++;
                                }
                                else
                                {
                                    pwCorrectCount = 0;
                                }*/
                            }
                            else if (hit.collider.name == "Hammer")
                            {
                                pianosound.PlayOneShot(getitem);
                                hammer.SetActive(false);
                                item5.SetActive(true);
                                itemCount = 5;
                            }
                            else if (hit.collider.name == "Wall_fake1" && itemSelection == 5)
                            {
                                pianosound.PlayOneShot(wallbreak);
                                Debug.Log("Game End");
                                ending.SetActive(true);
                                pianosound.PlayOneShot(hooray);
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
            if(itemCount != 3) pianosound.PlayOneShot(getitem);
            item3.SetActive(true);
            itemCount = 3;
        }
        if (passcount == 4)
        {
            if (password == 7619)
            {
                if (!openCloset)
                {
                    pianosound.PlayOneShot(framedrop);
                    door_left.transform.Rotate(0, 0, 90.0f);
                    door_left.transform.Translate(-1.0f, 1.0f, 0);
                    door_right.transform.Rotate(0, 0, -90.0f);
                    openCloset = true;
                }
            }
            else
            {
                pianosound.PlayOneShot(error);
                passcount = 0;
                password = 0;
            }
        }/*
        if (pwCorrectCount == 4 && !openCloset)
        {
            pianosound.PlayOneShot(framedrop);
            door_left.transform.Rotate(0, 0, 90.0f);
            door_left.transform.Translate(-1.0f, 1.0f, 0);
            door_right.transform.Rotate(0, 0, -90.0f);
            openCloset = true;
        }*/
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
