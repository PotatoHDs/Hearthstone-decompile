using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010E6 RID: 4326
	[Obsolete("Use CameraOverlay instead.", false)]
	[Serializable]
	public class CameraAmbientColorBehaviour : PlayableBehaviour
	{
		// Token: 0x0600BE11 RID: 48657 RVA: 0x0039EED0 File Offset: 0x0039D0D0
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			Color color = Color.black;
			int inputCount = playable.GetInputCount<Playable>();
			if (inputCount == 0)
			{
				return;
			}
			for (int i = 0; i < inputCount; i++)
			{
				float inputWeight = playable.GetInputWeight(i);
				CameraAmbientColorBehaviour behaviour = ((ScriptPlayable<T>)playable.GetInput(i)).GetBehaviour();
				color += behaviour.m_ambientColor * inputWeight;
			}
			RenderSettings.ambientLight = color;
		}

		// Token: 0x04009AB9 RID: 39609
		public Color m_ambientColor = new Color(0.2f, 0.2f, 0.2f);
	}
}
