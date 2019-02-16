using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody2D rb;
	public float vforce = 2f;
	Interactions inter_script = null;

	void OnTriggerEnter2D(Collider2D obj) {

		print ("Player was triggered");

		if (obj.gameObject.tag == "interobj") {
			inter_script = obj.gameObject.GetComponent<Interactions>();
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
	}

	// Update is called once per frame
	void Update () {

		float moveh = Input.GetAxis ("Horizontal");
		float movev = Input.GetAxis ("Vertical");

		if (Input.GetKey ("down")) {
			transform.Translate (Time.deltaTime * 0.25f, -Time.deltaTime * 0.2f, 0);
		}
		if (Input.GetKey ("up")) {
			transform.Translate (-Time.deltaTime * 0.25f, Time.deltaTime * 0.2f, 0);
		}
			
		transform.Translate (moveh * Time.deltaTime * 0.5f, 0, 0);

			
		if (inter_script != null) {
			print ("Do some triggering");
			if (inter_script.isAlive ()) {
				inter_script.makeDead ();
			} else {
				inter_script.makeAlive ();
			}
			inter_script = null;
		}

		// Move up down
		//rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Horizontal"));
		// Move left right
		//rb.velocity = new Vector2(rb.velocity.y, Input.GetAxis("Vertical"));
		
	}
}
