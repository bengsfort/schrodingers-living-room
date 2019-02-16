using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

	public Sprite dead;
	public Sprite alive;
	public bool alive_bool = true;

	// Use this for initialization
	void Start () {
	}

	public bool isAlive() {
		return alive_bool;
	}

	public void makeDead() {
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = dead;
		alive_bool = false;
		//print ("make dead");
	}

	public void makeAlive() {
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = alive;
		alive_bool = true;
		//print ("make alive");
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
