using UnityEngine;
using System.Collections;

public class MoveRandomDirectionScript : MonoBehaviour {
    public float xMinimum = -1f;
    public float xMaximum = 1f;
    public float yMinimum = -1;
    public float yMaximum = 1;
    public float speedMultiplier = 0.1f;

    private float xSpeed, ySpeed;
    private Transform transform;

    void Awake() {
        xSpeed = speedMultiplier * Random.Range(xMinimum, xMaximum);
        ySpeed = speedMultiplier * Random.Range(yMinimum, yMaximum);
        transform = GetComponent<Transform>();
    }

	void Start () {
	
	}
	
	
	void Update () {
        transform.Translate(new Vector3(xSpeed, ySpeed, 0));
	}
}
