using UnityEngine;
using System.Collections;

namespace GrillbrickStudios
{
	// Toggles on and off the different weapons.
	public class WeaponManager : MonoBehaviour
	{
		// Weapons
		private bool _taser = false;
		private bool _laser= false;
		private bool _doubleLaser= false;

		// getters and setters to equip only one weapon at a time.
		public bool HasTaser
		{
			get { return _taser; }
			set {
				if (value)
				{
					_taser = true;
					_laser = false;
					_doubleLaser = false;
				}
				else
				{
					if (!(_laser || _doubleLaser))
					{
						_taser = true;
					}
					else
					{
						_taser = false;
					}
				}
			}
		}

		public bool HasLaser
		{
			get { return _laser; }
			set
			{
				if (value)
				{
					_laser = true;
					_taser = false;
					_doubleLaser = false;
				}
				else
				{
					_laser = false;
					if (!(_taser || _doubleLaser))
					{
						_taser = true;
					}
				}
			}
		}

		public bool HasDoubleLaser
		{
			get { return _doubleLaser; }
			set
			{
				if (value)
				{
					_doubleLaser = true;
					_laser = true;
					_taser = false;
				}
				else
				{
					_doubleLaser = false;
					if (!(_laser || _taser))
					{
						_laser = true;
					}
				}
			}
		}

		// Assets to activate/deactivate each weapon.
		public GameObject Laser;
		public GameObject Taser;
		public GameObject DoubleLaser;

		public void Awake()
		{
			if (Laser == null)
			{
				Laser = GameObject.FindGameObjectWithTag("Laser");
			}
			if (Taser == null)
			{
				Taser = GameObject.FindGameObjectWithTag("Taser");
			}
			if (DoubleLaser == null)
			{
				DoubleLaser = GameObject.FindGameObjectWithTag("Laser2");
			}

		}

		public void Update()
		{
			foreach (EllipsoidParticleEmitter emitter in Taser.GetComponentsInChildren<EllipsoidParticleEmitter>())
			{
				emitter.enabled = HasTaser;
			}

			foreach (MeshRenderer mesh in Laser.GetComponentsInChildren<MeshRenderer>())
			{
				mesh.enabled = HasLaser;
			}

			foreach (LaserScript script in Laser.GetComponentsInChildren<LaserScript>())
			{
				script.enabled = HasLaser;
			}

			foreach (MeshRenderer mesh in DoubleLaser.GetComponentsInChildren<MeshRenderer>())
			{
				mesh.enabled = HasDoubleLaser;
			}

			foreach (LaserScript script in DoubleLaser.GetComponentsInChildren<LaserScript>())
			{
				script.enabled = HasDoubleLaser;
			}
		}
	}
}