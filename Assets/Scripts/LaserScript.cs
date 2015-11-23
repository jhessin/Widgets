using UnityEngine;
using System.Collections;

namespace GrillbrickStudios
{
	[AddComponentMenu("Effects/Laser")]
	[RequireComponent(typeof(LineRenderer))]
	public class LaserScript : MonoBehaviour
	{
		public float LaserDamage = 3;
		private LineRenderer _line;
		private Light _light;
		private Renderer _renderer;
		
		public void OnDisable()
		{
			_light.enabled = false;
		}

		public void Start()
		{
			_line = GetComponent<LineRenderer>();
			_line.enabled = false;
			_light = GetComponent<Light>();
			_light.enabled = false;
			_renderer = _line.GetComponent<Renderer>();
		}

		public void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				StopCoroutine("FireLaser");
				StartCoroutine("FireLaser");
			}
		}

		IEnumerator FireLaser()
		{
			_line.enabled = true;
			_light.enabled = true;

			while (Input.GetButton("Fire1"))
			{
				_renderer.material.mainTextureOffset = new Vector2(Time.time, Time.time);

				Ray ray = new Ray(transform.position, transform.forward);
				RaycastHit hit;

				_line.SetPosition(0, ray.origin);

				if (Physics.Raycast(ray, out hit, 100))
				{
					_line.SetPosition(1, hit.point);
					EBunny_Status status = hit.collider.GetComponent<EBunny_Status>();
					if (status)
					{
						status.ApplyDamage(LaserDamage);
					}
				}
				else
					_line.SetPosition(1, ray.GetPoint(100));

				yield return null;
			}

			_line.enabled = false;
			_light.enabled = false;
		}
	}
}