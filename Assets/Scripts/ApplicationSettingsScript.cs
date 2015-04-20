using UnityEngine;
using System.Collections;

public class ApplicationSettingsScript : MonoBehaviour {
    public int targetFrameRate = 60;

    void Awake() {
        Application.targetFrameRate = targetFrameRate;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
