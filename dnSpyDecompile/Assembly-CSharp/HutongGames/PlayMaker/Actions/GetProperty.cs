using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C7D RID: 3197
	[ActionCategory(ActionCategory.UnityObject)]
	[ActionTarget(typeof(Component), "targetProperty", false)]
	[ActionTarget(typeof(GameObject), "targetProperty", false)]
	[Tooltip("Gets the value of any public property or field on the targeted Unity Object and stores it in a variable. E.g., Drag and drop any component attached to a Game Object to access its properties.")]
	public class GetProperty : FsmStateAction
	{
		// Token: 0x06009FB4 RID: 40884 RVA: 0x0032F145 File Offset: 0x0032D345
		public override void Reset()
		{
			this.targetProperty = new FsmProperty
			{
				setProperty = false
			};
			this.everyFrame = false;
		}

		// Token: 0x06009FB5 RID: 40885 RVA: 0x0032F160 File Offset: 0x0032D360
		public override void OnEnter()
		{
			this.targetProperty.GetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FB6 RID: 40886 RVA: 0x0032F17B File Offset: 0x0032D37B
		public override void OnUpdate()
		{
			this.targetProperty.GetValue();
		}

		// Token: 0x04008543 RID: 34115
		public FsmProperty targetProperty;

		// Token: 0x04008544 RID: 34116
		public bool everyFrame;
	}
}
