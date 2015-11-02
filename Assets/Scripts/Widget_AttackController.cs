using UnityEngine;
using System.Collections;

namespace GrillbrickStudios
{
	//Widget_AttackController: handles the player's attack input and deals damage to the targeted enemy

	//---------------------------------------------------------------------------
	[AddComponentMenu("Player/Widget's Attack Controller")]
	public class Widget_AttackController : MonoBehaviour
	{
		//---Public---
		public float attackHitTime = 0.2f;
		public float attackTime = 0.5f;
		public Vector3 attackPosition = new Vector3(0, 1, 0);
		public float attackRadius = 2.0f;
		public float damage = 1.0f;

		public ParticleEmitter attackEmitter;
		public AudioClip attackSound;

		//---Private---
		bool busy = false;
		Vector3 ourLocation;
		GameObject[] enemies;

		Widget_Controller controller;
		private Animation anim;
		private new AudioSource audio;

		public void Awake()
		{
			controller = GetComponent<Widget_Controller>();
			anim = GetComponent<Animation>();
			audio = GetComponent<AudioSource>();
		}

		public void Update()
		{
			if (!busy && Input.GetButtonDown("Attack") && controller.IsGrounded() && !controller.IsMoving())
			{
				StartCoroutine(DidAttack());
				busy = true;
			}
		}

		private IEnumerator DidAttack()
		{
			// Play the animation regardless of whether we hit something or not
			anim.CrossFadeQueued("Taser", 0.1f, QueueMode.PlayNow);
			yield return new WaitForSeconds(attackHitTime);

			// Play effects
			StartCoroutine(PlayParticles());
			if (attackSound)
			{
				audio.clip = attackSound;
				audio.Play();
			}
			ourLocation = transform.TransformPoint(attackPosition);
			enemies = GameObject.FindGameObjectsWithTag("Enemy");

			//See if any enemies are within range of the attack. This will hit all in range.
			foreach (GameObject enemy in enemies)
			{
				EBunny_Status enemyStatus = enemy.GetComponent<EBunny_Status>();
				if (enemyStatus == null)
				{
					continue;
				}

				if (Vector3.Distance(enemy.transform.position, ourLocation) < attackRadius)
				{
					//apply damage for hitting
					enemyStatus.ApplyDamage(damage);
				}
			}
			yield return new WaitForSeconds(attackTime - attackHitTime);
			busy = false;
		}

		public GameObject GetClosestEnemy()
		{
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			float distanceToEnemy = Mathf.Infinity;
			GameObject wantedEnemy = null;
			float newDistanceToEnemy;

			foreach (GameObject enemy in enemies)
			{
				newDistanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
				if (newDistanceToEnemy < distanceToEnemy)
				{
					distanceToEnemy = newDistanceToEnemy;
					wantedEnemy = enemy;
				}
			}
			return wantedEnemy;
		}

		private IEnumerator PlayParticles()
		{
			attackEmitter.emit = true;
			yield return new WaitForSeconds(attackTime);
			attackEmitter.emit = false;
/*
			attackEmitter.Play();
			yield return new WaitForSeconds(attackEmitter.duration);
			attackEmitter.Stop();
*/
		}
	}
}