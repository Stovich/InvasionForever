using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    
    public bool isShooting = false;                     //firing mode toggle variable
    public Vector2 acceleration = new Vector2(8, 8);    //acceleration scalar
    public Vector2 maxSpeed = new Vector2(2, 2);        //maximum speed of player ship
    public AudioClip shootSound;
    public float shootVolume = 0.05f;
    private Vector2 movement;                           //stores post-iteration velocity change
    private WeaponScript[] weapons;
    private AudioSource audioSource;
    

    void Awake() {
        weapons = GetComponentsInChildren<WeaponScript>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {

        //convert mouse position to world position (ray-cast from camera).
        float mouseCoordinateX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mouseCoordinateY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        //bound mouse-world position to the on-screen game area so we can't fly out of bounds.
        if (mouseCoordinateX > 13.5f) mouseCoordinateX = 13.5f;
        else if (mouseCoordinateX < -13.5f) mouseCoordinateX = -13.5f;
        if (mouseCoordinateY > 9.5f) mouseCoordinateY = 9.5f;
        else if (mouseCoordinateY < -9.5f) mouseCoordinateY = -9.5f;

        //compute player velocityby subtracting player position from mouse position.
        float inputX = mouseCoordinateX - GetComponent<Transform>().position.x;
        float inputY = mouseCoordinateY - GetComponent<Transform>().position.y;

        //ceiling velocity to a pre-defined maximum
        if (inputX > maxSpeed.x) inputX = maxSpeed.x;
        else if (inputX < -maxSpeed.x) inputX = -maxSpeed.x;
        if (inputY > maxSpeed.y) inputY = maxSpeed.y;
        else if (inputY < -maxSpeed.y) inputY = -maxSpeed.y;

        //set the new movement vector based upon the computed acceleration
        movement = new Vector2(acceleration.x * inputX, acceleration.y * inputY);

        //toggle firing mode if player clicks left mouse button
        if (Input.GetButtonDown("Fire1")) {
            isShooting = !isShooting;
            if (isShooting)
                audioSource.Play();
            else
                audioSource.Stop();
        }

        //shoot current weapon if firing mode is toggled on
        if (isShooting) {
            foreach (WeaponScript weapon in weapons) {
                if (weapon != null && weapon.CanAttack) {
                    weapon.Attack(false);
                }
            }
        }
    }

    void FixedUpdate() {
        //Apply velocity change to rigid body
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        bool damagePlayer = false;
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null) {
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null) enemyHealth.Damage(3);
            damagePlayer = true;
        }

        if (damagePlayer) {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) playerHealth.Damage(3);
        }
    }

    
}
