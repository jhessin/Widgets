using UnityEngine;
using System.Collections;

namespace GrillbrickStudios
{
	[AddComponentMenu("Environment Props/DamageTrigger")]
	public class DamageTrigger : MonoBehaviour
	{
		public float damage = 20.0f;
		public Widget_Status playerStatus;

		public void OnTriggerEnter(Collider other)
		{
			print("ow!");
			playerStatus = GameObject.FindWithTag("Player").GetComponent<Widget_Status>();
			playerStatus.ApplyDamage(damage);
		}
	}
}