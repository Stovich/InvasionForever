using UnityEngine;
using System.Collections;

public class MoveBackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Transform transform = GetComponent<Transform>();
        transform.Translate(-0.1f, 0f, 0f);
        if (transform.position.x <= -27.3f)
            transform.position = new Vector3(27.3f, transform.position.y, transform.position.z);
	}
}
