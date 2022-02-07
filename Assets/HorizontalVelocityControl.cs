using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVelocityControl : MonoBehaviour
{
	private Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		// float steering = ((float)Input.mousePosition.x / Screen.width - 0.5f) / 0.5f;
		// Debug.Log("steering: " + steering);
		// rb.AddForce(steering * 10f, 0, 0, ForceMode.Force);
		if (Input.GetKey("right"))
		{
			rb.AddForce(50f, 0, 0, ForceMode.Force);
		}
		if (Input.GetKey("left"))
		{
			rb.AddForce(-50f, 0, 0, ForceMode.Force);
		}
	}
}
