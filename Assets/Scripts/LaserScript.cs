using UnityEngine;
using System.Collections;

namespace GrillbrickStudios
{
	[AddComponentMenu("Effects/Laser")]
	[RequireComponent(typeof(LineRenderer))]
	public class LaserScript : MonoBehaviour
	{
		public float laserDamage = 3;
		private LineRenderer line;
		private Light light;
		private Renderer renderer;

		public void Start()
		{
			line = GetComponent<LineRenderer>();
			line.enabled = false;
			light = GetComponent<Light>();
			light.enabled = false;
			renderer = line.GetComponent<Renderer>();
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
			line.enabled = true;
			light.enabled = true;

			while (Input.GetButton("Fire1"))
			{
				renderer.material.mainTextureOffset = new Vector2(Time.time, Time.time);

				Ray ray = new Ray(transform.position, transform.forward);
				RaycastHit hit;

				line.SetPosition(0, ray.origin);

				if (Physics.Raycast(ray, out hit, 100))
				{
					line.SetPosition(1, hit.point);
					EBunny_Status status = hit.collider.GetComponent<EBunny_Status>();
					if (status)
					{
						status.ApplyDamage(laserDamage);
					}
				}
				else
					line.SetPosition(1,ray.GetPoint(100));

				yield return null;
			}

			line.enabled = false;
			light.enabled = false;
		}
	}
}