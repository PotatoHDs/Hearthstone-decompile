using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Serializable]
	[Obsolete("Use CameraOverlay instead.", false)]
	public class CameraAmbientColorBehaviour : PlayableBehaviour
	{
		public Color m_ambientColor = new Color(0.2f, 0.2f, 0.2f);

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			Color black = Color.black;
			int inputCount = playable.GetInputCount();
			if (inputCount != 0)
			{
				for (int i = 0; i < inputCount; i++)
				{
					float inputWeight = playable.GetInputWeight(i);
					CameraAmbientColorBehaviour behaviour = ((ScriptPlayable<CameraAmbientColorBehaviour>)playable.GetInput(i)).GetBehaviour();
					black += behaviour.m_ambientColor * inputWeight;
				}
				RenderSettings.ambientLight = black;
			}
		}
	}
}
