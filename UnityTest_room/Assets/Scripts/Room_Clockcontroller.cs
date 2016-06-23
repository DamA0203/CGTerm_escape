using UnityEngine;
using System.Collections;

public class Room_Clockcontroller : MonoBehaviour {
    public GameObject ending;
    GameObject soundObject;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        soundObject = GameObject.Find("Clock");
        audioSource = soundObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ending.activeInHierarchy) audioSource.Pause();
	}
}
