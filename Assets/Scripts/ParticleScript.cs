using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {
    public float lifeSpan = 4;

    void Start() {
        Destroy(gameObject, lifeSpan);
    }
}
