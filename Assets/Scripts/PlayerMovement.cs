using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//public Rigidbody2D rb;
	public float vforce = 2f;
    public float speedX;
    public float speedY;
    public float speedH;
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	private CatGraphicsController cat_controller;

	// Use this for initialization
	void Start () {
		Physics2D.gravity = Vector2.zero;
		cat_controller = GetComponent<CatGraphicsController>();
	}

	// Update is called once per frame
	void Update () {

		//float moveh = Input.GetAxis ("Horizontal");
		//float movev = Input.GetAxis ("Vertical");

		if (Input.GetKey (down)) {
			transform.Translate (Time.deltaTime * speedX, -Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionRight();
		}
		if (Input.GetKey (up)) {
			transform.Translate (-Time.deltaTime * speedX, Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionLeft();
		}
		if (Input.GetKey (right)) {
			transform.Translate (Time.deltaTime * speedH, 0, 0);
			cat_controller.ChangeDirectionRight();
		}
		if (Input.GetKey (left)) {
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
