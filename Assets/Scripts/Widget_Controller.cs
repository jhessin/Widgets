using UnityEngine;
using System.Collections;
using System;

namespace GrillbrickStudios
{
	//Widget_Controller: Handles Widget's movement and player input
	[AddComponentMenu("Player/Widget's Controller")]
	public class Widget_Controller : MonoBehaviour
	{
		//Widget's Movement Variables------------------------------------------------------------------
		//These can be changed in the Inspector
		public float rollSpeed = 6.0f;
		public float fastRollSpeed = 2.0f;
		public float jumpSpeed = 8.0f;
		public float gravity = 20.0f;
		public float rotateSpeed = 4.0f;
		public float duckSpeed = 0.5f;

		//private, helper variables-----------------------------------------------------------------------
		private Vector3 moveDirection = Vector3.zero;
		private bool grounded = false;
		private float moveHorz = 0.0f;
		private float normalHeight = 2.0f;
		private float duckHeight = 1.0f;
		private Vector3 rotateDirection = Vector3.zero;

		private bool isDucking = false;
		private bool isBoosting = false;

		public bool isControllable = true;

		//cache controller so we only have to find it once----------------------------------------------
		private CharacterController controller;
		private Widget_Status widgetStatus;

		public void Awake()
		{
			controller = GetComponent<CharacterController>();
			widgetStatus = GetComponent<Widget_Status>();
		}

		public void FixedUpdate()
		{
			// check to make sure the character is controllable and not dead
			if (!isControllable)
				Input.ResetInputAxes();

			else
			{
				if (grounded)
				{
					// Since we're touching something solid, like the ground, allow movement
					// calculate movement directly from Input Axes
					moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
					moveDirection = transform.TransformDirection(moveDirection);
					moveDirection *= rollSpeed;

					// Find rotation based upon axes if need to turn
					moveHorz = Input.GetAxis("Horizontal");
					if (moveHorz > 0)									//right turn
						rotateDirection = new Vector3(0, 1, 0);
					else if (moveHorz < 0)								//left turn
						rotateDirection = new Vector3(0, -1, 0);
					else												//not turning
						rotateDirection = new Vector3(0, 0, 0);

					//Jump Controls
					if (Input.GetButton("Jump"))
					{
						moveDirection.y = jumpSpeed;
					}

					//Apply any Boosted Speed
					if (Input.GetButton("Boost"))
						if (widgetStatus)
							if (widgetStatus.energy > 0)
							{
								moveDirection *= fastRollSpeed;
								widgetStatus.energy -= widgetStatus.widgetBoostUsage*Time.deltaTime;
							}
				}
				
				//Duck the controller
				if (Input.GetButton("Duck"))
				{
					controller.height = duckHeight;
					controller.center = new Vector3(controller.center.x, controller.height/2 + 0.25f, controller.center.z);
					moveDirection *= duckSpeed;
					isDucking = true;
				}

				if (Input.GetButtonUp("Duck"))
				{
					controller.height = normalHeight;
					controller.center = new Vector3(controller.center.x, controller.height/2, controller.center.z);
					isDucking = false;
				}

				if (Input.GetButtonUp("Boost"))
				{
					isBoosting = false;
				}
				// Apply gravity to end Jump, enable falling, and make sure he's touching the ground
				moveDirection.y -= gravity*Time.deltaTime;

				// Move and rotate the controller
				CollisionFlags flags = controller.Move(moveDirection*Time.deltaTime);
				controller.transform.Rotate(rotateDirection*Time.deltaTime, rotateSpeed);
				grounded = ((flags & CollisionFlags.CollidedBelow) != 0);
			}

		}

		public bool IsMoving()
		{
			return moveDirection.magnitude > 0.5f;
		}

		public bool IsDucking()
		{
			return isDucking;
		}

		public bool IsBoosting()
		{
			return isBoosting;
		}

		public bool IsGrounded()
		{
			return grounded;
		}
	}
}