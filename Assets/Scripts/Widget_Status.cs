using UnityEngine;
using System.Collections;
using System;

namespace GrillbrickStudios
{
	//Widget_Status: Handles Widget's state machine.
	//Keep track of health, energy and all the chunky stuff
	[AddComponentMenu("Player/Widget's State Manager")]
	public class Widget_Status : MonoBehaviour
	{
		//vitals---------------------------------------------------------------------------
		public float health = 10.0f;
		public float maxHealth = 10.0f;
		public float energy = 10.0f;
		public float maxEnergy = 10.0f;
		public float energyUsageForTransform = 3.0f;
		public float widgetBoostUsage = 5.0f;

		//Sound Effects-----------------------------------------------------------------
		public AudioClip hitSound;
		public AudioClip deathSound;

		//Cache Controllers---------------------------------------------------------------------------
		private Widget_Controller playerController;
		private CharacterController controller;
		private AudioSource aSource;
		private Widget_Animation animationState;
		private Animation anim;

		//Cache meshes
		private SkinnedMeshRenderer bodyMesh;
		private SkinnedMeshRenderer wheelMesh;

		public void Awake()
		{
			playerController = GetComponent<Widget_Controller>();
			controller = GetComponent<CharacterController>();
			aSource = GetComponent<AudioSource>();
			animationState = GetComponent<Widget_Animation>();
			anim = GetComponent<Animation>();
			bodyMesh = GameObject.Find("Body").GetComponent<SkinnedMeshRenderer>();
			wheelMesh = GameObject.Find("Wheels").GetComponent<SkinnedMeshRenderer>();
		}

		internal void ApplyDamage(float damage)
		{
			health -= damage;

			// play hit sound if it exists
			if (hitSound)
			{
				aSource.clip = hitSound;
				aSource.Play();
			}
			// check health and call Die if need to
			if (health <= 0)
			{
				health = 0; //for GUI
				StartCoroutine(Die());
			}
		}

		internal void AddHealth(float boost)
		{
			// add health and set to min of (current health + boost) or health max
			health += boost;
			if (health >= maxHealth)
			{
				health = maxHealth;
			}
			print("added health: "+ boost);
		}

		internal void AddEnergy(float boost)
		{
			// add energy and set to min of (current energy + boost) or energy max
			energy += boost;
			if (energy >= maxEnergy)
			{
				energy = maxEnergy;
			}
			print("Added energy: " + boost);
		}

		private IEnumerator Die()
		{
			// play death sound if it exists
			if (deathSound)
			{
				aSource.clip = deathSound;
				aSource.Play();
			}
			print("DEAD!");
			playerController.isControllable = false;

			animationState.PlayDie();
			yield return new WaitForSeconds(anim["Die"].length - 0.2f);
			HideCharacter();

			yield return new WaitForSeconds(1);

			// restart player at last respawn check point and give max life
			if (CheckPoint.isActivePt)
			{
				controller.transform.position = CheckPoint.isActivePt.transform.position;
				controller.transform.position = 
					new Vector3(controller.transform.position.x, 
					controller.transform.position.y + 0.5f,
					controller.transform.position.z);
			}
			ShowCharacter();
			health = maxHealth;
		}

		private void HideCharacter()
		{
			bodyMesh.enabled = false;
			wheelMesh.enabled = false;
			playerController.isControllable = false;
		}

		private void ShowCharacter()
		{
			bodyMesh.enabled = true;
			wheelMesh.enabled = true;
			playerController.isControllable = true;
		}
	}
}