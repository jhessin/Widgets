using UnityEngine;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine.UI;

namespace GrillbrickStudios
{
	//Widget_Animation: Animation State Manager for Widget.
	//controls layers, blends, and play cues for all imported animations
	[AddComponentMenu("Player/Widget'AnimationManager")]
	public class Widget_Animation : MonoBehaviour
	{
		private float nextPlayIdle = 0.0f;
		public float waitTime = 10.0f;

		private Widget_Controller playerController;
		private Animation anim;

		public void Awake()
		{
			playerController = GetComponent<Widget_Controller>();
			anim = GetComponent<Animation>();
		}

		public void Start()
		{
			// set up layers - high numbers receive priority when blending
			anim["Widget_Idle"].layer = -1;
			anim["Idle"].layer = 0;

			// we want to make sure that the rolls are synced together
			anim["SlowRoll"].layer = 1;
			anim["FastRoll"].layer = 1;
			anim["Duck"].layer = 1;
			anim.SyncLayer(1);

			anim["Taser"].layer = 3;
			anim["Jump"].layer = 5;

			//these should take priority over all others
			anim["FallDown"].layer = 7;
			anim["GotHit"].layer = 8;
			anim["Die"].layer = 10;

			anim["Widget_Idle"].wrapMode = WrapMode.PingPong;
			anim["Duck"].wrapMode = WrapMode.Loop;
			anim["Jump"].wrapMode = WrapMode.ClampForever;
			anim["FallDown"].wrapMode = WrapMode.ClampForever;

			anim.Stop();
			anim.Play("Idle");
		}

		public void Update()
		{
			if (playerController.IsGrounded())
			{
				anim.Blend("FallDown", 0, 0.2f);
				anim.Blend("Jump", 0, 0.2f);

				// if boosting
				if (playerController.IsBoosting())
				{
					anim.CrossFade("FastRoll", 0.5f);
					nextPlayIdle = Time.time + waitTime;
				}
				else if (playerController.IsDucking())
				{
					anim.CrossFade("Duck", 0.2f);
					nextPlayIdle = Time.time + waitTime;
				}
				// Fade in normal roll
				else if (playerController.IsMoving())
				{
					anim.CrossFade("SlowRoll", 0.5f);
					nextPlayIdle = Time.time + waitTime;
				}
				// Fade out walk and run
				else
				{
					anim.Blend("FastRoll", 0.0f, 0.3f);
					anim.Blend("SlowRoll", 0.0f, 0.3f);
					anim.Blend("Duck", 0.0f, 0.3f);
					if (Time.time > nextPlayIdle)
					{
						nextPlayIdle = Time.time + waitTime;
						PlayIdle();
					}
					else
						anim.CrossFade("Widget_Idle", 0.2f);
				}
			}
			// in air animations
			else
			{
				if (Input.GetButtonDown("Jump"))
				{
					anim.CrossFade("Jump");
				}
				if (!playerController.IsGrounded())
				{
					anim.CrossFade("FallDown", 0.5f);
				}
			}

			// test for idle
			if (Input.anyKey)
			{
				nextPlayIdle = Time.time + waitTime;
			}
		}

		// Other methods
		public void PlayTaser()
		{
			anim.CrossFade("Taser", 0.2f);
		}

		public void PlayIdle()
		{
			anim.CrossFade("Idle", 0.2f);
		}

		public void GetHit( )
		{
			anim.CrossFade("GotHit", 0.2f);
		}

		public void PlayDie( )
		{
			anim.CrossFade("Die", 0.2f);
		}
	}
}