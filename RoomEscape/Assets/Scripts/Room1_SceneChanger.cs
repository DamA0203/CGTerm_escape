using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Room1_SceneChanger : MonoBehaviour {

    public GameObject box;
    public GameObject yesButton;
    public GameObject noButton;

    bool boxOnCheck;

    // Use this for initialization
    void Start () {
        Debug.Log("Now game start.");
        box.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        boxOnCheck = false;
    }
	
	// Update is called once per frame
	void Update () {
        //exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!boxOnCheck)
            {
                Debug.Log("Escape key. Open notification window.");
                box.SetActive(true);
                yesButton.SetActive(true);
                noButton.SetActive(true);
                boxOnCheck = true;
            }
            else
            {
                Debug.Log("Escape key. Close notification window.");
                box.SetActive(false);
                yesButton.SetActive(false);
                noButton.SetActive(false);
                boxOnCheck = false;
            }
        }
    }

    public void YesButton()
    {
        Debug.Log("Yes-button. Back to the title.");
        SceneManager.LoadScene("start");
    }

    public void NoButton()
    {
        Debug.Log("No-button. Close notification window.");
        box.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        boxOnCheck = false;
    }
}
