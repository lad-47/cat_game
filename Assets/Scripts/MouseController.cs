using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {
	private Rigidbody2D rb;
	private GameObject mouse;
	private GameController gameController;
	private int currentOrientation;
	private int currentDistance;
	private int targetDistance;
	private int currentSpeed;
	public AudioSource audioSource;
	public AudioClip[] entryClips;
	public AudioClip[] exitClips;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D> ();
		mouse = this.gameObject;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (transform.position.x == 0) {
			currentOrientation = 0;
			transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
		} else if (transform.position.x == Screen.width) {
			currentOrientation = 1;
			transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
		}
		if (transform.position.y == 0) {
			currentOrientation = 2;
			transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
		} else if (transform.position.y == Screen.height) {
			currentOrientation = 3;
		}
		if (currentOrientation == 0 || currentOrientation == 1) {
			targetDistance = Random.Range(Screen.width/4, Screen.width/2);
		} else {
			targetDistance = Random.Range(Screen.height/4, Screen.height/2);
		}
		currentSpeed = Random.Range(1,4);
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		AudioClip entryClip = entryClips[Random.Range(0,entryClips.Length)];
		audioSource.PlayOneShot(entryClip, 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
        	Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
 
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
 
            if (hitInformation.collider != null) {
                GameObject touchedObject = hitInformation.transform.gameObject;
                gameController.update_score();
				AudioClip exitClip = exitClips[Random.Range(0,exitClips.Length)];
				audioSource.PlayOneShot(exitClip, 1.0f);
                Destroy(touchedObject);
            }
        }
    }

    void FixedUpdate() {
    	if (mouse.tag == "Clone") {
    		if (transform.position.x < 0 || 
    			transform.position.x > Screen.width ||
    			transform.position.y < 0 ||
    			transform.position.y > Screen.height) {
    			Destroy(mouse);
    		}
    		if (currentDistance >= targetDistance) {
    			currentOrientation = Random.Range(0,4);
    			currentDistance = 0;
    			if (currentOrientation == 0) {
    				transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
    			} else if (currentOrientation == 1) {
    				transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
    			} else if (currentOrientation == 2) {
    				transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
    			} else {
    				transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
    			}
				currentSpeed = Random.Range(1,4);
    		}
			Vector2 movement;
			if (currentOrientation == 0) {
				movement = new Vector2 (1, 0);
			} else if (currentOrientation == 1) {
				movement = new Vector2 (-1, 0);
			} else if (currentOrientation == 2) {
				movement = new Vector2 (0, 1);
			} else {
				movement = new Vector2 (0, -1);
			}
			rb.position += movement * currentSpeed;
			currentDistance += currentSpeed;
		}
    }
}
