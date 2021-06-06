using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Serializable]
	public class RandomSeedBehaviour : PlayableBehaviour
	{
		private System.Random m_random = new System.Random();

		private bool m_triggered;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (!m_triggered && Application.isPlaying)
			{
				uint num = (uint)m_random.Next(65536);
				uint num2 = (uint)m_random.Next(65536);
				ParticleSystem obj = playerData as ParticleSystem;
				obj.Clear();
				obj.Stop();
				obj.randomSeed = (num << 16) | num2;
				obj.Simulate(0f, withChildren: true, restart: true);
				obj.Play();
				m_triggered = true;
			}
		}
	}
}
