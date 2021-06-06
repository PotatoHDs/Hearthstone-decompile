using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C24 RID: 3108
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds the Child of a GameObject by Name.\nNote, you can specify a path to the child, e.g., LeftShoulder/Arm/Hand/Finger. If you need to specify a tag, use GetChild.")]
	public class FindChild : FsmStateAction
	{
		// Token: 0x06009E26 RID: 40486 RVA: 0x0032AC30 File Offset: 0x00328E30
		public override void Reset()
		{
			this.gameObject = null;
			this.childName = "";
			this.storeResult = null;
		}

		// Token: 0x06009E27 RID: 40487 RVA: 0x0032AC50 File Offset: 0x00328E50
		public override void OnEnter()
		{
			this.DoFindChild();
			base.Finish();
		}

		// Token: 0x06009E28 RID: 40488 RVA: 0x0032AC60 File Offset: 0x00328E60
		private void DoFindChild()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Transform transform = ownerDefaultTarget.transform.Find(this.childName.Value);
			this.storeResult.Value = ((transform != null) ? transform.gameObject : null);
		}

		// Token: 0x0400837F RID: 33663
		[RequiredField]
		[Tooltip("The GameObject to search.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008380 RID: 33664
		[RequiredField]
		[Tooltip("The name of the child. Note, you can specify a path to the child, e.g., LeftShoulder/Arm/Hand/Finger")]
		public FsmString childName;

		// Token: 0x04008381 RID: 33665
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the child in a GameObject variable.")]
		public FsmGameObject storeResult;
	}
}
