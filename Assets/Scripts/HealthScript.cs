using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public int hp = 1;
    public bool isEnemy = true;
    public AudioClip ricochetSound01;
    public AudioClip ricochetSound02;
    public AudioClip ricochetSound03;
    public AudioClip ricochetSound04;
    public int ricochetsPerSound = 5;
    public int ricochetSoundCount = 4;
    public float ricochetSoundVolume = 0.1f;
    public int bigExplosionsCount = 3;
    public int smallExplosionsCount = 12;
    public Transform bigExplosionPrefab;
    public Transform smallExplosionPrefab;
    public Transform explosionSoundPrefab;

    private AudioSource audioSource;
    private int ricochetCounter = 0;
    private Transform transform;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        transform = GetComponent<Transform>();
    }

    public void Damage(int damageCount) {
        hp = hp - damageCount;
        if (hp <= 0) {
            Explode();
            Destroy(gameObject);
        }
    }

    public void Explode() {
        Transform explodeSound = Instantiate(explosionSoundPrefab) as Transform;
        for (int i = 0; i < bigExplosionsCount; i++) {
            Transform bigExplosionTransform = Instantiate(bigExplosionPrefab) as Transform;
            bigExplosionTransform.position = transform.position;
        }
        for (int i = 0; i < smallExplosionsCount; i++) {
            Transform smallExplosionTransform = Instantiate(smallExplosionPrefab) as Transform;
            smallExplosionTransform.position = transform.position;
        }
    }

	public void OnTriggerEnter2D(Collider2D otherCollider) {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null) {
            if (shot.isEnemyShot != isEnemy) {
                Damage(shot.damage);
                Destroy(shot.gameObject);
                if (isEnemy && ricochetCounter == 0) {
                    ricochetCounter = ricochetsPerSound;
                    int ricochetSelector = Random.Range(1, ricochetSoundCount);
                    switch (ricochetSelector) {
                        case 1:
                            audioSource.PlayOneShot(ricochetSound01, ricochetSoundVolume);
                            break;
                        case 2:
                            audioSource.PlayOneShot(ricochetSound02, ricochetSoundVolume);
                            break;
                        case 3:
                            audioSource.PlayOneShot(ricochetSound03, ricochetSoundVolume);
                            break;
                        case 4:
                            audioSource.PlayOneShot(ricochetSound04, ricochetSoundVolume);
                            break;
                    }
                }
                else
                    ricochetCounter = ricochetCounter - 1;
            }
        }
	}
}
