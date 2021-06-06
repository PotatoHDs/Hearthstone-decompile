using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010F4 RID: 4340
	[Serializable]
	public class RandomSeedBehaviour : PlayableBehaviour
	{
		// Token: 0x0600BE46 RID: 48710 RVA: 0x003A08A4 File Offset: 0x0039EAA4
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (this.m_triggered || !Application.isPlaying)
			{
				return;
			}
			uint num = (uint)this.m_random.Next(65536);
			uint num2 = (uint)this.m_random.Next(65536);
			ParticleSystem particleSystem = playerData as ParticleSystem;
			particleSystem.Clear();
			particleSystem.Stop();
			particleSystem.randomSeed = (num << 16 | num2);
			particleSystem.Simulate(0f, true, true);
			particleSystem.Play();
			this.m_triggered = true;
		}

		// Token: 0x04009AFC RID: 39676
		private System.Random m_random = new System.Random();

		// Token: 0x04009AFD RID: 39677
		private bool m_triggered;
	}
}
