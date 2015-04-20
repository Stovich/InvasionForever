using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    public int lifespan = 5;
    public int damage = 1;
    public bool isEnemyShot = false;
    public bool isBeam = false;
    public float beamPulseValue = 0.05f;
    private bool isPulsating = false;
    private Vector3 originalScale;
	void Start () {
        originalScale = GetComponent<Transform>().localScale;
        Destroy(gameObject, lifespan);
	}

    void Update() {
        if (isBeam) {
            if (isPulsating)
                GetComponent<Transform>().localScale = originalScale;
            else
                GetComponent<Transform>().localScale = new Vector3(originalScale.x, originalScale.y - beamPulseValue, originalScale.z);
        }
    }
}
