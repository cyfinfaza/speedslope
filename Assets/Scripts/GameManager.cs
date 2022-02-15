using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{

	public GameObject player;
	public GameObject scoreTextObject;
	public GameObject highScoreTextObject;
	private Text scoreText;
	private Text highScoreText;
	private Rigidbody playerBody;
	private float maxVelocity = 0;
	// Start is called before the first frame update
	void Start()
	{
		scoreText = scoreTextObject.GetComponent<Text>();
		highScoreText = highScoreTextObject.GetComponent<Text>();
		playerBody = player.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (playerBody.velocity.z > maxVelocity)
		{
			maxVelocity = playerBody.velocity.z;
		}
		scoreText.text = String.Format("{0:0.0}", playerBody.velocity.z) + " m/s";
		highScoreText.text = "max " + String.Format("{0:0.0}", maxVelocity) + " m/s";
	}
}
