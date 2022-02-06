using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowPlayer : MonoBehaviour
{

	public GameObject player;
	private Transform cameraTransform;

	// Start is called before the first frame update
	void Start()
	{
		cameraTransform = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		cameraTransform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15, player.transform.position.z - 15);
		cameraTransform.LookAt(player.transform);
	}
}
