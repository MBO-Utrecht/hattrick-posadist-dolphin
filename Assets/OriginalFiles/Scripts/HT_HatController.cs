using UnityEngine;
using System.Collections;
using UnityEditor.AnimatedValues;

public class HT_HatController : MonoBehaviour {

	public Camera cam;
	
	private float maxWidth;
	private bool canControl = false;

    private GameObject ball;
	private GameObject bomb;
	private float hatWidth;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - hatWidth;
	}
	
	// Update is called once per physics timestep
	void FixedUpdate () {
		if (canControl) {
			Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
			Vector3 targetPosition = new Vector3 (rawPosition.x, 0.0f, 0.0f);
			float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
			targetPosition = new Vector3 (targetWidth, targetPosition.y, targetPosition.z);
			GetComponent<Rigidbody2D>().MovePosition (targetPosition);
		}

		else
		{
			if (!ball)
			{
				if (GameObject.FindGameObjectWithTag("Ball"))
				{
                    ball = GameObject.FindGameObjectWithTag("Ball");
                }
				
			}

			if (!bomb)
			{
				if (GameObject.FindGameObjectWithTag("Bomb"))
				{
					bomb = GameObject.FindGameObjectWithTag("Bomb");
				}
			}
			
			if (bomb != null)
			{
				if ((Mathf.Abs(transform.localPosition.x) - Mathf.Abs(bomb.transform.localPosition.x)) < 1)
				{
					if (transform.localPosition.x <= 0)
					{
                        transform.localPosition -= new Vector3(10 * Time.deltaTime, 0 , 0);
                    }

					else
					{
                        transform.localPosition += new Vector3(10 * Time.deltaTime, 0, 0);
                    }
				}

            }

			else if (ball != null)
			{
                Vector3 targetPosition = new Vector3(ball.transform.position.x, 0f, 0f);
                if (targetPosition.x + hatWidth < transform.localPosition.x)
				{
					transform.localPosition -= new Vector3(10 * Time.deltaTime, 0, 0);
				}

                if (targetPosition.x - hatWidth > transform.localPosition.x)
                {
                    transform.localPosition += new Vector3(10 * Time.deltaTime, 0 , 0);
                }
            }


		}
	}

	public void ToggleControl (bool toggle) {
		canControl = toggle;
	}
}
