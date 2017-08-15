using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {

    public Object cloneCameraPrefab;

    private bool cameraDragging = true;
    private bool cameraDraggingUpDown = true;
    private float dragSpeed = 2;
    private Vector3 dragOrigin;

    private float dist;
    private Vector3 v3OrgMouse;

    private int cameraMinX = -10;
    private int cameraMaxX = 10;
    private int cameraMinY = -10;
    private int cameraMaxY = 10;


    // Use this for initialization
    void Start () {
        dist = transform.position.z;  // Distance camera is above map
    }
	
	// Update is called once per frame
	void Update () {
		Move ();
        CameraDrag();
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

    private void CameraDrag()
    {
        //when mouse pressed
        if (Input.GetMouseButtonDown(1))
        {
            /*
            to main camera preestablished i do not need this fix, 
            but for player cameras somehow the camera movement is reversed so

            affectedVector.x = affectedXVector.x;
            affectedVector.y = affectedYVector.y;

            needs to be

            affectedVector.x = -affectedXVector.x;
            affectedVector.y = -affectedYVector.y;

            on player cameras, but this causes camera to 
            reverse its x, y coordinates on click down so need a fix

            but I need to flip camera coordinates or else the drag script 
            will inicially flip the coordinates of the plaayers transform 
            beacause of the changed lines,
            but if I do that to the player camera it 
            switches fast between 2 spots, so looks glitchy
            this creates new camera at that spot, uses it for initial cooridnates 
            then destroys it so camera does not "glitch move"
            */
            GameObject newCamera = (GameObject)Instantiate(cloneCameraPrefab, new Vector3(-transform.position.x, -transform.position.y, -2), Quaternion.identity);
            v3OrgMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3OrgMouse = newCamera.GetComponent<Camera>().ScreenToWorldPoint(v3OrgMouse);
            Destroy(newCamera);

        }
        //when dragging
        else if (Input.GetMouseButton(1))
        {
            //getting distance moved
            //Debug.Log(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist));
            var v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            //Debug.Log(v3Pos);
            v3Pos = this.GetComponent<Camera>().ScreenToWorldPoint(v3Pos);
            //v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
            Vector3 checkVector = transform.position - (v3Pos - v3OrgMouse);
            //set temp and make it have no y effect
            Vector3 tempVector = v3Pos;
            tempVector.y = transform.position.y;
            //variable to do move calculation
            Vector3 affectedXVector = transform.position;
            Vector3 affectedYVector = transform.position;
            Vector3 affectedVector = new Vector3();

            //going right
            if (transform.position.x < checkVector.x &&
                transform.position.x < cameraMaxX)
            {
                affectedXVector -= (tempVector - v3OrgMouse);
            }

            //going left
            else if (transform.position.x > checkVector.x &&
                transform.position.x > cameraMinX)
            {
                affectedXVector -= (tempVector - v3OrgMouse);
            }

            //reset temp and make it have no x effect
            tempVector = v3Pos;
            tempVector.x = transform.position.x;

            //going up
            if (transform.position.y < checkVector.y &&
                transform.position.y < cameraMaxY)
            {
                affectedYVector -= (tempVector - v3OrgMouse);
            }

            //going down
            else if (transform.position.y > checkVector.y &&
                transform.position.y > cameraMinY)
            {
                affectedYVector -= (tempVector - v3OrgMouse);
            }

            affectedVector.x = -affectedXVector.x;
            affectedVector.y = -affectedYVector.y;
            affectedVector.z = transform.position.z;

            transform.position = affectedVector;
        }

    }
}
