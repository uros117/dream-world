using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public SwapController swapController;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;

	float jumpDur = 0;
	const float jumpDurMax = 0.35f;

	//bool dashAxis = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			jump = true;

			jumpDur = 0;
		}

		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			jumpDur = 0;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) && swapController.currDreamState == DreamState.Phobia)
		{
			dash = true;
		}


        /*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/

    }

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		if ((jumpDur < jumpDurMax) && Input.GetKey(KeyCode.UpArrow))
		{
			jumpDur += Time.fixedDeltaTime;
		}
		//Debug.Log(jumpDur);
		//else jumpDur = 0;
		//else jump = false;
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, (jumpDur > 0 && jumpDur < jumpDurMax), dash, swapController.currDreamState == DreamState.Fantasy);
		jump = false;
		dash = false;
	}
}
