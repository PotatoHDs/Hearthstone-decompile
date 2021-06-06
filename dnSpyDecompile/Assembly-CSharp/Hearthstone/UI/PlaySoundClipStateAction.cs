using System;
using Assets;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001023 RID: 4131
	public class PlaySoundClipStateAction : StateActionImplementation
	{
		// Token: 0x0600B346 RID: 45894 RVA: 0x0037354A File Offset: 0x0037174A
		public override void Run(bool loadSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B347 RID: 45895 RVA: 0x00373568 File Offset: 0x00371768
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			WeakAssetReference assetAtIndex = base.GetAssetAtIndex(PlaySoundClipStateAction.s_SoundDefIndex);
			if (string.IsNullOrEmpty(assetAtIndex.AssetString))
			{
				Hearthstone.UI.Logging.Log.Get().AddMessage("Tried playing sound but sound def was missing.", this, LogLevel.Error, "");
				base.Complete(false);
				return;
			}
			GameObject parentObject;
			base.GetOverride(0).Resolve(out parentObject);
			SoundPlayClipArgs args = new SoundPlayClipArgs
			{
				m_category = new Global.SoundCategory?((Global.SoundCategory)base.GetIntValueAtIndex(PlaySoundClipStateAction.s_SoundCategoryIndex)),
				m_volume = new float?(base.GetFloatValueAtIndex(PlaySoundClipStateAction.s_VolumeIndex)),
				m_pitch = new float?(base.GetFloatValueAtIndex(PlaySoundClipStateAction.s_PitchIndex)),
				m_spatialBlend = new float?(base.GetFloatValueAtIndex(PlaySoundClipStateAction.s_SpatialBlendIndex)),
				m_parentObject = parentObject
			};
			bool success = false;
			if (SoundManager.Get() != null)
			{
				success = SoundManager.Get().LoadAndPlayClip(AssetReference.CreateFromAssetString(assetAtIndex.AssetString), args);
			}
			base.Complete(success);
		}

		// Token: 0x04009670 RID: 38512
		public static readonly int s_SoundDefIndex = 0;

		// Token: 0x04009671 RID: 38513
		public static readonly int s_SoundCategoryIndex = 0;

		// Token: 0x04009672 RID: 38514
		public static readonly int s_VolumeIndex = 0;

		// Token: 0x04009673 RID: 38515
		public static readonly int s_PitchIndex = 1;

		// Token: 0x04009674 RID: 38516
		public static readonly int s_SpatialBlendIndex = 2;

		// Token: 0x04009675 RID: 38517
		public static readonly int s_DelayIndex = 3;
	}
}
