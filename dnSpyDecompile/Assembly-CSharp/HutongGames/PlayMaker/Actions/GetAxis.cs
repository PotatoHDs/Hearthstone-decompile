using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C45 RID: 3141
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the value of the specified Input Axis and stores it in a Float Variable. See Unity Input Manager docs.")]
	public class GetAxis : FsmStateAction
	{
		// Token: 0x06009EBC RID: 40636 RVA: 0x0032C3A2 File Offset: 0x0032A5A2
		public override void Reset()
		{
			this.axisName = "";
			this.multiplier = 1f;
			this.store = null;
			this.everyFrame = true;
		}

		// Token: 0x06009EBD RID: 40637 RVA: 0x0032C3D2 File Offset: 0x0032A5D2
		public override void OnEnter()
		{
			this.DoGetAxis();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EBE RID: 40638 RVA: 0x0032C3E8 File Offset: 0x0032A5E8
		public override void OnUpdate()
		{
			this.DoGetAxis();
		}

		// Token: 0x06009EBF RID: 40639 RVA: 0x0032C3F0 File Offset: 0x0032A5F0
		private void DoGetAxis()
		{
			if (FsmString.IsNullOrEmpty(this.axisName))
			{
				return;
			}
			float num = Input.GetAxis(this.axisName.Value);
			if (!this.multiplier.IsNone)
			{
				num *= this.multiplier.Value;
			}
			this.store.Value = num;
		}

		// Token: 0x04008419 RID: 33817
		[RequiredField]
		[Tooltip("The name of the axis. Set in the Unity Input Manager.")]
		public FsmString axisName;

		// Token: 0x0400841A RID: 33818
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range.")]
		public FsmFloat multiplier;

		// Token: 0x0400841B RID: 33819
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a float variable.")]
		public FsmFloat store;

		// Token: 0x0400841C RID: 33820
		[Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;
	}
}
