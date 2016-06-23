using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Room_Passcontroller : MonoBehaviour {

    Text finalPass;

	// Use this for initialization
	void Start () {
        finalPass = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!(Room_CameraControl.passZoom) || Room_GameController.openCloset) finalPass.enabled = false;
        else
        {
            finalPass.enabled = true;
            if (Room_GameController.password == 0) finalPass.text = "0000";
            else finalPass.text = Room_GameController.password.ToString();
        }
	}
}
