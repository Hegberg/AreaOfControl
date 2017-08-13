using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	private void Move()
	{
		if (Input.GetKey("a"))
		{
			MoveLeft();
		}
		if (Input.GetKey("d"))
		{
			MoveRight();
		}
			

	}

	private void MoveLeft()
	{
		transform.Translate(new Vector3(-0.08f, 0, 0));
		//rb2D.MovePosition (transform.position - new Vector3 (0.08f, rb2D.velocity.y/100));
	}

	private void MoveRight()
	{
		transform.Translate(new Vector3(0.08f, 0, 0));
		//rb2D.MovePosition (transform.position + new Vector3 (0.08f, rb2D.velocity.y/100));
	}
}
