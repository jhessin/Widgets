using System.Collections;
using UnityEngine;

//EBunny_AIController: Handles Both the electronic bunny's AI and animations
//Since we don't need to be checking for the player's input or actions, playing the proper animations is much more straightforward and can be handled
//in the same script.

//------------------------------------------------------------------

namespace GrillbrickStudios
{
	[AddComponentMenu("Enemies/Bunny'sAIController")]
	public class EBunny_AIController : MonoBehaviour
	{
		private Animation anim;
		public float attackDistance = 15.0f;
		public Vector3 attackPosution = new Vector3(0, 1, 0);

		public float attackRadius = 2.5f;
		public float attackRotateSpeed = 80.0f;
		public float attackSpeed = 8.0f;
		public float attackTurnTime = 1.0f;

		// Cache the controller
		private CharacterController charController;

		public int damage = 1;

		public float directionTraveltime = 2.0f;
		private Vector3 distanceToPlayer;
		public float idleTime = 1.5f;

		//-----------------------------------------

		private bool isAttacking;
		private float lastAttackTime;
		private float nextPauseTime = 0.0f;
		public float rotateSpeed = 30.0f;

		public Transform target;
		private float timeToNewDirection;
		public float viewAngle = 20.0f;
		public float walkSpeed = 3.0f;

		public void Awake()
		{
			charController = GetComponent<CharacterController>();
			anim = GetComponent<Animation>();
		}

		//-------------------------------------------
		public void Start()
		{
			if (!target)
				target = GameObject.FindWithTag("Player").transform;

			// Setup animations----------------------------------------------------
			anim.wrapMode = WrapMode.Loop;
			anim["EBunny_Death"].wrapMode = WrapMode.Once;
			anim["EBunny_Attack"].layer = 1;
			anim["EBunny_Hit"].layer = 3;
			anim["EBunny_Death"].layer = 5;

			// Rather than placing this in an update function, we can start the AI's behavior now and
			// use coroutines to handle the changes
			StartCoroutine(EnemyAI());
		}

		private IEnumerator EnemyAI()
		{
			yield return new WaitForSeconds(idleTime);

			while (true)
			{
				// Idle around and wait for the player
				yield return StartCoroutine(idle());

				// Player has been located, prepare for the attack.
				yield return StartCoroutine(Attack());
			}
		}

		private IEnumerator idle()
		{
			// Walk around and pause in random directions unless the player is within range
			while (true)
			{
				// Find a new direction to move
				if (Time.time > timeToNewDirection)
				{
					yield return new WaitForSeconds(idleTime);

					var RandomDirection = Random.value;
					if (RandomDirection > 0.5)
						transform.Rotate(new Vector3(0, 5, 0), rotateSpeed);
					else
						transform.Rotate(new Vector3(0, -5, 0), rotateSpeed);
					timeToNewDirection = Time.time + directionTraveltime;
				}

				var walkForward = transform.TransformDirection(Vector3.forward);
				charController.SimpleMove(walkForward*walkSpeed);

				distanceToPlayer = transform.position - target.position;

				//We found the player! Stop wasting time and go after him
				if (distanceToPlayer.magnitude < attackDistance)
					break;
				yield return null;
			}
		}

		private IEnumerator Attack()
		{
			isAttacking = true;
			anim.Play("EBunny_Attack");

			// We need to turn to face the player now that he's in range
			var angle = 0.0f;
			var time = 0.0f;
			while (angle > viewAngle || time < attackTurnTime)
			{
				time += Time.deltaTime;
				angle = Mathf.Abs(FacePlayer(target.position, attackRotateSpeed));
				var move = Mathf.Clamp01((90 - angle)/90);

				// depending on the angle, start moving
				anim["EBunny_Attack"].weight = anim["EBunny_Attack"].speed = move;
				var direction = transform.TransformDirection(Vector3.forward*attackSpeed*move);
				charController.SimpleMove(direction);

				yield return null;
			}

			// attack if can see player
			var lostSight = false;
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse modified in loop
			while (!lostSight)
			{
				angle = FacePlayer(target.position, attackRotateSpeed);

				// Check to ensure that the target is within the Bunny's eyesight
				if (Mathf.Abs(angle) > viewAngle)
					lostSight = true;

				// If bunny loses site of the player he jumps out of here.
				if (lostSight)
					break;

				// Check to see if we're close enough to the player to bite 'em.
				var location = transform.TransformPoint(attackPosution) - target.position;
				if (Time.time > lastAttackTime + 1.0 && location.magnitude < attackRadius)
				{
					// deal damage
					target.SendMessage("ApplyDamage", damage);

					lastAttackTime = Time.time;
				}

				if (location.magnitude > attackRadius)
					break;

				// Check to make sure our current direction didn't collide us with something
				if (charController.velocity.magnitude < attackSpeed*0.3)
					break;

				// yield for one frame
				yield return null;
			}

			isAttacking = false;
		}

		private float FacePlayer(Vector3 targetLocation, float rotateSpeed)
		{
			// Find the relative place in the world where the player is located
			var relativeLocation = transform.InverseTransformPoint(targetLocation);
			var angle = Mathf.Atan2(relativeLocation.x, relativeLocation.z)*Mathf.Rad2Deg;

			// Clamp it with the max rotation speed so he doesn't move too fast
			var maxRotation = rotateSpeed*Time.deltaTime;
			var clampedAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);

			// Rotate
			transform.Rotate(0, clampedAngle, 0);
			//Return the current angle
			return angle;
		}
	}
}