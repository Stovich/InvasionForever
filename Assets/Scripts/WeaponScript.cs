using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

    public AudioClip shootSound;
    public Transform shotPrefab;
    public float projectileSpeed = 1f;
    public float spreadAngle = 0f;
    public float shootingRate = 0.25f;
    public float volleyReloadRate = 3f;
    public float volleyLength = 1f;
    public float tracerLength = 1.5f;
    public float spawnCooldownPenalty = 1f;
    public float minVolume = 0.1f;
    public float maxVolume = 0.15f;
    public bool isShotSoundEnabled = true;
    public bool mustReload = false;
    public bool hasBeamTracer = false;

    
    
    private AudioSource audioSource;
    private float shootCooldown;
    private float volleyCooldown;
    private float spawnPenaltyCounter;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

	void Start () {
        //set how long before newly spawned entity can attack.
        shootCooldown = spawnCooldownPenalty;
        volleyCooldown = (volleyReloadRate + Random.Range(0f, spawnCooldownPenalty));
        spawnPenaltyCounter = spawnCooldownPenalty;
	}
	
	void Update () {
        //tick remaining reload time down
        if (spawnPenaltyCounter <= 0) {
            if (shootCooldown > 0) {
                shootCooldown = shootCooldown - Time.deltaTime;
            }
            volleyCooldown = volleyCooldown - Time.deltaTime;
        }
        else
            spawnPenaltyCounter = spawnPenaltyCounter - Time.deltaTime;
	}

    public void Attack(bool isEnemy) {
        //fire the weapon
        if (spawnPenaltyCounter <= 0f && isLoaded() && CanAttack) {
            shootCooldown = shootingRate;
            //Create a new shot
            Transform shotTransform = Instantiate(shotPrefab) as Transform;
            //Assign position
            shotTransform.position = transform.position;
            
            
            //Is it an enemy shot?
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null) {
                shot.isEnemyShot = isEnemy;
            }

            //compute movement pattern of the projectile
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null) {
                Vector2 shotDirection = this.transform.right;
                //if spreadAngle is set, offset the shot by a random angle within the spreadAngle arc
                if (spreadAngle != 0f)
                    shotDirection.y = Random.Range(-spreadAngle, spreadAngle);
                shotDirection.x = shotDirection.x * projectileSpeed;
                move.direction = shotDirection;
            }
            
            //play sound
            if(isShotSoundEnabled)
                audioSource.PlayOneShot(shootSound, Random.Range(minVolume, maxVolume));
        }
    }

    public bool isLoaded() {
        if (mustReload == true) {

            if (volleyCooldown <= 0f) {
                volleyCooldown = volleyReloadRate + volleyLength;
                return false;
            }
            else if (volleyCooldown <= volleyLength) {
                if (hasBeamTracer)
                    GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                return true;
            }
            else if (volleyCooldown <= volleyLength + tracerLength) {
                if (hasBeamTracer)
                    GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                return false;
            }
            else {
                if (hasBeamTracer)
                    GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                return false;
            }
        }
        else
            return true;
    }

    public bool CanAttack {
        get {
            return shootCooldown <= 0f;
        }
    }
}
