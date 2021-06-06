using Assets;
using Hearthstone.UI.Logging;

namespace Hearthstone.UI
{
	public class PlaySoundClipStateAction : StateActionImplementation
	{
		public static readonly int s_SoundDefIndex = 0;

		public static readonly int s_SoundCategoryIndex = 0;

		public static readonly int s_VolumeIndex = 0;

		public static readonly int s_PitchIndex = 1;

		public static readonly int s_SpatialBlendIndex = 2;

		public static readonly int s_DelayIndex = 3;

		public override void Run(bool loadSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			WeakAssetReference assetAtIndex = GetAssetAtIndex(s_SoundDefIndex);
			if (string.IsNullOrEmpty(assetAtIndex.AssetString))
			{
				Hearthstone.UI.Logging.Log.Get().AddMessage("Tried playing sound but sound def was missing.", this, LogLevel.Error);
				Complete(success: false);
				return;
			}
			GetOverride(0).Resolve(out var gameObject);
			SoundPlayClipArgs args = new SoundPlayClipArgs
			{
				m_category = (Global.SoundCategory)GetIntValueAtIndex(s_SoundCategoryIndex),
				m_volume = GetFloatValueAtIndex(s_VolumeIndex),
				m_pitch = GetFloatValueAtIndex(s_PitchIndex),
				m_spatialBlend = GetFloatValueAtIndex(s_SpatialBlendIndex),
				m_parentObject = gameObject
			};
			bool success = false;
			if (SoundManager.Get() != null)
			{
				success = SoundManager.Get().LoadAndPlayClip(AssetReference.CreateFromAssetString(assetAtIndex.AssetString), args);
			}
			Complete(success);
		}
	}
}
