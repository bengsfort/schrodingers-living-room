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

	// Use this for initialization
	void Start () {
		Physics2D.gravity = Vector2.zero;
		cat_controller = GetComponent<CatGraphicsController>();
	}

	// Update is called once per frame
	void Update () {

		//float moveh = Input.GetAxis ("Horizontal");
		//float movev = Input.GetAxis ("Vertical");

		if (Input.GetKey (down) && DownKeyWorking) {
			transform.Translate (Time.deltaTime * speedX, -Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionRight();
		}
		if (Input.GetKey (up) && UpKeyWorking) {
			transform.Translate (-Time.deltaTime * speedX, Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionLeft();
		}
		if (Input.GetKey (right) && RightKeyWorking) {
			transform.Translate (Time.deltaTime * speedH, 0, 0);
			cat_controller.ChangeDirectionRight();
		}
		if (Input.GetKey (left) && LeftKeyWorking) {
			transform.Translate (-Time.deltaTime * speedH, 0, 0);
			cat_controller.ChangeDirectionLeft();
		}
			
		//transform.Translate (moveh * Time.deltaTime * speedH, 0, 0);

		// Move up down
		//rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Horizontal"));
		// Move left right
		//rb.velocity = new Vector2(rb.velocity.y, Input.GetAxis("Vertical"));
		
	}
}
