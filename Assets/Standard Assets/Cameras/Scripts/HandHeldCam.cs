using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class HandHeldCam : LookatTarget
	{
		[SerializeField] private readonly float m_BaseSwayAmount = .5f;
		[SerializeField] private readonly float m_SwaySpeed = .5f;
		[Range(-1, 1)] [SerializeField] private readonly float m_TrackingBias = 0;
		[SerializeField] private readonly float m_TrackingSwayAmount = .5f;


		protected override void FollowTarget(float deltaTime)
		{
			base.FollowTarget(deltaTime);

			var bx = (Mathf.PerlinNoise(0, Time.time*m_SwaySpeed) - 0.5f);
			var by = (Mathf.PerlinNoise(0, (Time.time*m_SwaySpeed) + 100)) - 0.5f;

			bx *= m_BaseSwayAmount;
			by *= m_BaseSwayAmount;

			var tx = (Mathf.PerlinNoise(0, Time.time*m_SwaySpeed) - 0.5f) + m_TrackingBias;
			var ty = ((Mathf.PerlinNoise(0, (Time.time*m_SwaySpeed) + 100)) - 0.5f) + m_TrackingBias;

			tx *= -m_TrackingSwayAmount*MFollowVelocity.x;
			ty *= m_TrackingSwayAmount*MFollowVelocity.y;

			transform.Rotate(bx + tx, by + ty, 0);
		}
	}
}