using UnityEngine;
using System.Collections;

public class MoveElliptically : MonoBehaviour {
    public float currentPosition = 0f; //1f = 360 degrees
    public float ellipseWidth = 3.5f;
    public float ellipseHeight = 10f;
    public float speed = 0.01f;
    public int circleXPosition;
    private Vector2 movement;

    void Start() {
        //REMOVE - 9 ON .X LATER
        circleXPosition = (int)GetComponentInParent<Transform>().position.x - 9;
    }
	
	// Update is called once per frame
	void Update () {
        //int fleetPosition = (int)GetComponentInParent<Transform>().position.x;
        //Transform transform = GetComponent<Transform>();
        //transform.position = new Vector3(ellipseWidth * Mathf.Cos(currentPosition * 2 * Mathf.PI) + 8f,
        //    ellipseHeight * Mathf.Sin(currentPosition * 2 * Mathf.PI),
        //    transform.position.z);
        if (currentPosition + speed > 1f)
            currentPosition = currentPosition + speed - 1f;
        else
            currentPosition = currentPosition + speed;

        float moveX = ((ellipseWidth * Mathf.Cos(currentPosition * 2 * Mathf.PI)) + circleXPosition) - GetComponent<Transform>().position.x;
        float moveY = ellipseHeight * Mathf.Sin(currentPosition * 2 * Mathf.PI) - GetComponent<Transform>().position.y;

        movement = new Vector2(moveX, moveY);
	}

    void FixedUpdate() {
        //Apply velocity change to rigid body
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
