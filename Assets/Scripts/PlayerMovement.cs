using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//public Rigidbody2D rb;
	public float vforce = 2f;
    public float speedX;
    public float speedY;
    public float speedH;
    public float deadAliveMultiplier;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;

    Interactable inter_script = null;
    private CatGraphicsController cat_controller;

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

		if (Input.GetKey (down)) {
			transform.Translate (Time.deltaTime * speedX, -Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionRight();
            cat_controller.ChangeAnimationWalking();
		}
		else if (Input.GetKey (up)) {
			transform.Translate (-Time.deltaTime * speedX, Time.deltaTime * speedY, 0);
			cat_controller.ChangeDirectionLeft();
            cat_controller.ChangeAnimationWalking();
        }

		else if (Input.GetKey (right)) {
			transform.Translate (Time.deltaTime * speedH, 0, 0);
			cat_controller.ChangeDirectionRight();
            cat_controller.ChangeAnimationWalking();
        }
		else if (Input.GetKey (left)) {
			transform.Translate (-Time.deltaTime * speedH, 0, 0);
			cat_controller.ChangeDirectionLeft();
            cat_controller.ChangeAnimationWalking();
        }
        else cat_controller.ChangeAnimationStanding();

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
