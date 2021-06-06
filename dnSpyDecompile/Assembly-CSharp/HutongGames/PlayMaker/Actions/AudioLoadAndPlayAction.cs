using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F16 RID: 3862
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Loads and Plays a Sound Prefab.")]
	public class AudioLoadAndPlayAction : FsmStateAction
	{
		// Token: 0x0600ABD6 RID: 43990 RVA: 0x0035A320 File Offset: 0x00358520
		public override void Reset()
		{
			this.m_ParentObject = null;
			this.m_PrefabName = null;
			this.m_VolumeScale = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600ABD7 RID: 43991 RVA: 0x0035A344 File Offset: 0x00358544
		public override void OnEnter()
		{
			if (this.m_PrefabName == null)
			{
				base.Finish();
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_ParentObject);
			if (this.m_VolumeScale.IsNone)
			{
				SoundManager.Get().LoadAndPlay(this.m_PrefabName.Value, ownerDefaultTarget);
			}
			else
			{
				SoundManager.Get().LoadAndPlay(this.m_PrefabName.Value, ownerDefaultTarget, this.m_VolumeScale.Value);
			}
			base.Finish();
		}

		// Token: 0x0400928D RID: 37517
		[Tooltip("Optional. If specified, the generated Audio Source will be attached to this object.")]
		public FsmOwnerDefault m_ParentObject;

		// Token: 0x0400928E RID: 37518
		[RequiredField]
		public FsmString m_PrefabName;

		// Token: 0x0400928F RID: 37519
		[Tooltip("Optional. Scales the volume of the loaded sound.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;
	}
}
