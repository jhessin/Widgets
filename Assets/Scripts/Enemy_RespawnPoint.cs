using UnityEngine;
using System.Collections;

//Enemy_RespawnPoint: attach to a GO in the scene to serve as a respawn point for enemies.  When the player walks into the 
//specified area, a new enemy will respawn.

namespace GrillbrickStudios
{
	[AddComponentMenu("Enemies/Respawn Point")]
	public class Enemy_RespawnPoint : MonoBehaviour
	{
		//------------------------------
		public float spawnRange = 40.0f;
		public GameObject enemy;

		Transform target;
		GameObject currentEnemy;    // only allow one enemy at a time to spawn
		bool outsideRange = true;
		Vector3 distanceToPlayer;
		//------------------------------

		public void Start()
		{
			target = GameObject.FindWithTag("Player").transform;
		}

		public void Update()
		{
			distanceToPlayer = transform.position - target.position;

			// check to see if player encounters the respawn point.
			if (distanceToPlayer.magnitude < spawnRange)
			{
				if (!currentEnemy)
				{
					currentEnemy = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
				}

				// the player is now inside the respawn's range
				outsideRange = false;
			}

			// player is moving out of range, so get rid of the unnecessary enemy now
			else
			{
				if (currentEnemy)
					Destroy(currentEnemy);
			}
			outsideRange = true;
		}
	}
}