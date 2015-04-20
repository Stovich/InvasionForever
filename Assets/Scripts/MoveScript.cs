using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2((float)-0.5, 0);
    private Vector2 movement;
	
	void Update () {
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
	}

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
