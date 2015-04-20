using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour {
    
    public AudioClip explosion01;
    public AudioClip explosion02;
    public AudioClip explosion03;

    private AudioSource audioSource;
    private int explosionSoundsCount = 3;
	
	public void Explosion() {
	    int whichExplosion = Random.Range(1, explosionSoundsCount);
        
        switch (whichExplosion) {
            case 1:
                audioSource.PlayOneShot(explosion01);
                break;
            case 2:
                audioSource.PlayOneShot(explosion02);
                break;
            case 3:
                audioSource.PlayOneShot(explosion03);
                break;
        }
    }
}
