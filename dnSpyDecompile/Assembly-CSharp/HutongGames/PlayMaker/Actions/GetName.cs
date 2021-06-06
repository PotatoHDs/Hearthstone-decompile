using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C77 RID: 3191
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the name of a Game Object and stores it in a String Variable.")]
	public class GetName : FsmStateAction
	{
		// Token: 0x06009F9D RID: 40861 RVA: 0x0032EDCD File Offset: 0x0032CFCD
		public override void Reset()
		{
			this.gameObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.storeName = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F9E RID: 40862 RVA: 0x0032EDEF File Offset: 0x0032CFEF
		public override void OnEnter()
		{
			this.DoGetGameObjectName();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F9F RID: 40863 RVA: 0x0032EE05 File Offset: 0x0032D005
		public override void OnUpdate()
		{
			this.DoGetGameObjectName();
		}

		// Token: 0x06009FA0 RID: 40864 RVA: 0x0032EE10 File Offset: 0x0032D010
		private void DoGetGameObjectName()
		{
			GameObject value = this.gameObject.Value;
			this.storeName.Value = ((value != null) ? value.name : "");
		}

		// Token: 0x0400852E RID: 34094
		[RequiredField]
		public FsmGameObject gameObject;

		// Token: 0x0400852F RID: 34095
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeName;

		// Token: 0x04008530 RID: 34096
		public bool everyFrame;
	}
}
