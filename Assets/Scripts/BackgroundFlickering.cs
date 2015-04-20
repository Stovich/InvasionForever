using UnityEngine;
using System.Collections;

public class BackgroundFlickering : MonoBehaviour {
    public float backgroundOpacity = 1f;
    public float fadeSpeed = 0.1f;
    public bool isFading = true;
    private SpriteRenderer backgroundSprite;
	// Use this for initialization
	void Start () {
        backgroundSprite = GetComponent<SpriteRenderer>();
        backgroundSprite.color = new Color(1f, 1f, 1f, backgroundOpacity);
	}
	
	// Update is called once per frame
	void Update () {
        if (isFading) {
            backgroundSprite.color = new Color(1f, 1f, 1f, backgroundSprite.color.a - fadeSpeed);
            if (backgroundSprite.color.a <= 0.0f)
                isFading = false;
        }
        else {
            backgroundSprite.color = new Color(1f, 1f, 1f, backgroundSprite.color.a + fadeSpeed);
            if (backgroundSprite.color.a >= 1.0f)
                isFading = true;
        }
	}
}
