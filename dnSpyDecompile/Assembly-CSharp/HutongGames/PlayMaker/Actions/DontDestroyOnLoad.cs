using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C16 RID: 3094
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Makes the Game Object not be destroyed automatically when loading a new scene.")]
	public class DontDestroyOnLoad : FsmStateAction
	{
		// Token: 0x06009DEC RID: 40428 RVA: 0x0032A168 File Offset: 0x00328368
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x06009DED RID: 40429 RVA: 0x0032A174 File Offset: 0x00328374
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				UnityEngine.Object.DontDestroyOnLoad(ownerDefaultTarget.transform.root.gameObject);
			}
			base.Finish();
		}

		// Token: 0x04008342 RID: 33602
		[RequiredField]
		[Tooltip("GameObject to mark as DontDestroyOnLoad.")]
		public FsmOwnerDefault gameObject;
	}
}
