using UnityEngine;
using System.Collections;

public class PlayerRespawnScript : MonoBehaviour {
    public float playerRespawnDelay = 3f;
    public Transform playerShipPrefab;

    private float playerRespawnCounter;
    private Transform transform;

	void Start () {
        playerRespawnCounter = playerRespawnDelay;
        transform = GetComponent<Transform>();
	}
	

	void Update () {
        if (transform.childCount == 1) {
            if (playerRespawnCounter <= 0) {
                Transform playerTransform = Instantiate(playerShipPrefab) as Transform;
                playerTransform.transform.parent = transform;
                playerTransform.position = new Vector3(transform.position.x - 12, transform.position.y, transform.position.z);
                playerRespawnCounter = playerRespawnDelay;
            }
            else
                playerRespawnCounter = playerRespawnCounter - Time.deltaTime;
        }
	}
}
