using UnityEngine;
using System.Collections;

public class WarpEllipseScript : MonoBehaviour {
    public Transform enemyShipTransform01;
    public Transform enemyShipTransform02;
    public int currentWave = 1;
    public int fleetSize = 0;
    public int numberOfDifferentEnemyShips = 2;
    public float delayAfterFleetDestroyed = 2f;
    public float delayAfterWarpSphereAppears = 2f;
    private float fleetDestroyedCounter;
    private float warpSphereCounter;
    private bool isWarpSphereActive = false;
    private bool isFleetDestroyed = true;
    private float opacity = 0f;
    private Transform transform;
	
	void Start () {
        fleetDestroyedCounter = delayAfterFleetDestroyed;
        warpSphereCounter = delayAfterWarpSphereAppears;
        transform = GetComponent<Transform>();
	}
	
	void Update () {
        // hide and reset warp sphere, officially begin wave
        if (isWarpSphereActive && warpSphereCounter <= 0f) {
            isFleetDestroyed = false;
            

            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            isWarpSphereActive = false;
            warpSphereCounter = delayAfterWarpSphereAppears;
        }
        //spawn wave once
        else if (isWarpSphereActive && fleetSize == 0 && warpSphereCounter <= delayAfterWarpSphereAppears / 4f) {
            fleetSize = currentWave;
            float fleetPositionDistance = 1f / (float)fleetSize;
            for (int i = 0; i < fleetSize; i++) {
                Transform enemyTransform = null;
                int chooseShip = Random.Range(1, numberOfDifferentEnemyShips+1);
                switch (chooseShip) {
                    case 1:
                        enemyTransform = Instantiate(enemyShipTransform01) as Transform;
                        break;
                    case 2:
                        enemyTransform = Instantiate(enemyShipTransform02) as Transform;
                        break;
                    default:
                        enemyTransform = Instantiate(enemyShipTransform02) as Transform;
                        break;
                }
                enemyTransform.GetComponent<MoveElliptically>().currentPosition = fleetPositionDistance * i;
                enemyTransform.transform.parent = transform;
                //REMOVE + 7 ON .X LATER
                enemyTransform.position = new Vector3(transform.position.x + 7,transform.position.y,transform.position.z + 1);

            }
            warpSphereCounter = warpSphereCounter - Time.deltaTime;
        }
        //decrement warp event counter if not an event keyframe
        else if (isWarpSphereActive) {
            warpSphereCounter = warpSphereCounter - Time.deltaTime;
        }
        //end fleet destruction event, begin warping next wave
        else if (isFleetDestroyed && fleetDestroyedCounter <= 0f) {
            fleetDestroyedCounter = delayAfterFleetDestroyed;
            isWarpSphereActive = true;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        //decrement fleet destruction event counter if not an event keyframe
        else if (isFleetDestroyed) {
            fleetDestroyedCounter = fleetDestroyedCounter - Time.deltaTime;
        }
        //if enemy fleet wave destroyed, begin fleet destruction event
        else if (fleetSize == 0) {
            currentWave = currentWave + 1;
            isFleetDestroyed = true;
        }
        //check and store how many ships are left in enemy fleet
        else {
            fleetSize = transform.childCount;
        }
	}
}
