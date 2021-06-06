using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF1 RID: 3569
	[ActionCategory(ActionCategory.UnityObject)]
	[ActionTarget(typeof(Component), "targetProperty", false)]
	[ActionTarget(typeof(GameObject), "targetProperty", false)]
	[Tooltip("Sets the value of any public property or field on the targeted Unity Object. E.g., Drag and drop any component attached to a Game Object to access its properties.")]
	public class SetProperty : FsmStateAction
	{
		// Token: 0x0600A683 RID: 42627 RVA: 0x00349743 File Offset: 0x00347943
		public override void Reset()
		{
			this.targetProperty = new FsmProperty
			{
				setProperty = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A684 RID: 42628 RVA: 0x0034975E File Offset: 0x0034795E
		public override void OnEnter()
		{
			this.targetProperty.SetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A685 RID: 42629 RVA: 0x00349779 File Offset: 0x00347979
		public override void OnUpdate()
		{
			this.targetProperty.SetValue();
		}

		// Token: 0x04008D07 RID: 36103
		public FsmProperty targetProperty;

		// Token: 0x04008D08 RID: 36104
		public bool everyFrame;
	}
}
