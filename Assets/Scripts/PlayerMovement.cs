using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//public Rigidbody2D rb;
	public float vforce = 2f;
    public float speedX;
    public float speedY;
    public float speedH;

	private CatGraphicsController cat_controller;

	public KeyCode interact;

	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	private bool UpKeyWorking = true;
	private bool DownKeyWorking = true;
	private bool LeftKeyWorking = true;
	private bool RightKeyWorking = true;

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "background_up") {
			UpKeyWorking = false;
		}
		if (coll.gameObject.tag == "background_down") {
			DownKeyWorking = false;
		}
		if (coll.gameObject.tag == "background_left") {
			LeftKeyWorking = false;
		}
		if (coll.gameObject.tag == "background_right") {
			RightKeyWorking = false;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "background_up") {
			UpKeyWorking = true;
		}
		if (coll.gameObject.tag == "background_down") {
			DownKeyWorking = true;
		}
		if (coll.gameObject.tag == "background_left") {
			LeftKeyWorking = true;
		}
		if (coll.gameObject.tag == "background_right") {
			RightKeyWorking = true;
		}
	}

    public float deadAliveMultiplier;

    Interactable inter_script = null;

    void OnTriggerEnter2D(Collider2D obj) {

		print ("Player was triggered");

		if (obj.gameObject.tag == "interobj") {
			inter_script = obj.gameObject.GetComponent<Interactable>();
		}
	}

	void OnTriggerExit2D(Collider2D obj) {

		print ("Player exited trigger mode");

		if (obj.gameObject.tag == "interobj") {
			inter_script = null;
		}
	}

	// Use this for initialization
	void Start () {
		Physics2D.gravity = Vector2.zero;
		cat_controller = GetComponent<CatGraphicsController>();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (down) && DownKeyWorking) {
			transform.Translate (Time.deltaTime * speedX, -Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionRight();
            cat_controller.ChangeAnimationWalking();
		}
		if (Input.GetKey (up) && UpKeyWorking) {
			transform.Translate (-Time.deltaTime * speedX, Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionLeft();
            cat_controller.ChangeAnimationWalking();
        }
		if (Input.GetKey (right) && RightKeyWorking) {
			transform.Translate (Time.deltaTime * speedH, 0, 0);
			cat_controller.ChangeDirectionRight();
		}
		if (Input.GetKey (left) && LeftKeyWorking) {
			transform.Translate (-Time.deltaTime * speedH, 0, 0);
			cat_controller.ChangeDirectionLeft();
            cat_controller.ChangeAnimationWalking();
        }
        else if (Input.anyKey == false) cat_controller.ChangeAnimationStanding();

        if (inter_script != null)
        {
            if (Input.GetKey(interact))
            {
                inter_script.Interact(deadAliveMultiplier);
                cat_controller.ChangeAnimationScratching();
            }
        }
    }
}
