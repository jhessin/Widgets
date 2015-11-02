using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

//EBunny_Status:  controls the state information of the enemy bunny

namespace GrillbrickStudios
{
	[AddComponentMenu("Enemies/Bunny'sStateManager")]
	public class EBunny_Status : MonoBehaviour
	{
		public float health = 10.0f;
		public float energy = 10.0f;
		bool dead = false;

		public Texture2D charImage;
		public ParticleEmitter explosion;
		public ParticleEmitter smoke;

		//----------------------------
		public AudioClip deathSound;

		// PickupItems held-------------
		public int numHeldItemsMin = 1;
		public int numHeldItemsMax = 3;
		public GameObject pickup1;
		public GameObject pickup2;

		// Private components
		Animation anim;
		AudioSource aSource;

		public void Awake()
		{
			anim = GetComponent<Animation>();
			aSource = GetComponent<AudioSource>();
		}

		// State Functions---------------
		public void ApplyDamage(float damage)
		{
			if (health <= 0)
				return;

			health -= damage;

			anim.Play("EBunny_Hit");

			//check health and call Die if need to
			if(!dead && health <= 0)
			{
				health = 0; //for GUI
				dead = true;
				StartCoroutine(Die());
			}
		}

		private IEnumerator Die()
		{
			anim.Stop();
			anim.Play("EBunny_Death");

			Destroy(gameObject.GetComponent<EBunny_AIController>());
			yield return new WaitForSeconds(anim["EBunny_Death"].length - 0.5f);

			// Play effects
			StartCoroutine(PlayEffects());
			if (deathSound)
			{
				aSource.clip = deathSound;
				aSource.Play();
			}

			// Cache location of dead body for pickups
			Vector3 itemLocation = gameObject.transform.position;

			// drop a random number of reward pickups for the player
			yield return new WaitForSeconds(5);
			float rewardItems = Random.Range(numHeldItemsMin, numHeldItemsMax) + 1;

			for (int i = 0; i < rewardItems; i++)
			{
				Vector3 randomItemLocation = itemLocation;
				randomItemLocation.x += Random.Range(-2, 2);
				randomItemLocation.y += 1; // Keep it off the ground
				randomItemLocation.z += Random.Range(-2, 2);

				if (Random.value > 0.5)
					Instantiate(pickup1, randomItemLocation, pickup1.transform.rotation);
				else
					Instantiate(pickup2, randomItemLocation, pickup2.transform.rotation);
			}

			// Remove killed enemy from the scene
			Destroy(gameObject);
		}

		public bool IsDead()
		{
			return dead;
		}

		public Texture2D GetCharImage()
		{
			return charImage;
		}

		private IEnumerator PlayEffects()
		{
			explosion.emit = true;
			smoke.emit = true;
			yield return new WaitForSeconds(0.5f);
			GameObject.Find("root").GetComponent<SkinnedMeshRenderer>().enabled = false;
			yield return new WaitForSeconds(0.5f);
			explosion.emit = false;
			yield return new WaitForSeconds(3.5f);
			smoke.emit = false;
		}
	}
}