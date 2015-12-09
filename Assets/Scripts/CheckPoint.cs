using UnityEngine;

// checkpoints in the level - active for the last selected one and first for the initial one at startup
//the static declaration makes the isActivePt variable global across all instances of this script in the game.

namespace GrillbrickStudios
{
	[AddComponentMenu("Environment Props/CheckPt")]
	public class CheckPoint : MonoBehaviour
	{
		public static CheckPoint isActivePt;

		//special effects
		public ParticleEmitter activeEmitter;
		public CheckPoint firstPt;

		//audio

		private Widget_Status playerStatus;

		public void Awake()
		{
			playerStatus = GameObject.FindWithTag("Player").GetComponent<Widget_Status>();
		}

		public void Start()
		{
			//initialize first point
			isActivePt = firstPt;

			if (isActivePt == this)
			{
				BeActive();
			}
		}

		//When the player encounters a point, this is called when the collision occurs
		public void OnTriggerEnter(Collider other)
		{
			//first turn off the old respawn point if this is a newly encountered one
			if (isActivePt != this)
			{
				isActivePt.BeInactive();

				//then set the new one
				isActivePt = this;
				BeActive();
			}
			playerStatus.AddHealth(playerStatus.maxHealth);
			playerStatus.AddEnergy(playerStatus.maxEnergy);
		}

		// Activates the particles.
		private void BeActive()
		{
			activeEmitter.emit = true;
		}

		// Deactivates the particles.
		private void BeInactive()
		{
			activeEmitter.emit = false;
		}
	}
}