using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C92 RID: 3218
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Game Object's Transform and stores it in an Object Variable.")]
	public class GetTransform : FsmStateAction
	{
		// Token: 0x0600A013 RID: 40979 RVA: 0x0032FF36 File Offset: 0x0032E136
		public override void Reset()
		{
			this.gameObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.storeTransform = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A014 RID: 40980 RVA: 0x0032FF58 File Offset: 0x0032E158
		public override void OnEnter()
		{
			this.DoGetGameObjectName();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A015 RID: 40981 RVA: 0x0032FF6E File Offset: 0x0032E16E
		public override void OnUpdate()
		{
			this.DoGetGameObjectName();
		}

		// Token: 0x0600A016 RID: 40982 RVA: 0x0032FF78 File Offset: 0x0032E178
		private void DoGetGameObjectName()
		{
			GameObject value = this.gameObject.Value;
			this.storeTransform.Value = ((value != null) ? value.transform : null);
		}

		// Token: 0x04008595 RID: 34197
		[RequiredField]
		public FsmGameObject gameObject;

		// Token: 0x04008596 RID: 34198
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Transform))]
		public FsmObject storeTransform;

		// Token: 0x04008597 RID: 34199
		public bool everyFrame;
	}
}
