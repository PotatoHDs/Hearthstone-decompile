using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C7F RID: 3199
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Random Game Object from the scene.\nOptionally filter by Tag.")]
	public class GetRandomObject : FsmStateAction
	{
		// Token: 0x06009FBC RID: 40892 RVA: 0x0032F203 File Offset: 0x0032D403
		public override void Reset()
		{
			this.withTag = "Untagged";
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FBD RID: 40893 RVA: 0x0032F223 File Offset: 0x0032D423
		public override void OnEnter()
		{
			this.DoGetRandomObject();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FBE RID: 40894 RVA: 0x0032F239 File Offset: 0x0032D439
		public override void OnUpdate()
		{
			this.DoGetRandomObject();
		}

		// Token: 0x06009FBF RID: 40895 RVA: 0x0032F244 File Offset: 0x0032D444
		private void DoGetRandomObject()
		{
			GameObject[] array;
			if (this.withTag.Value != "Untagged")
			{
				array = GameObject.FindGameObjectsWithTag(this.withTag.Value);
			}
			else
			{
				array = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
			}
			if (array.Length != 0)
			{
				this.storeResult.Value = array[UnityEngine.Random.Range(0, array.Length)];
				return;
			}
			this.storeResult.Value = null;
		}

		// Token: 0x04008547 RID: 34119
		[UIHint(UIHint.Tag)]
		public FsmString withTag;

		// Token: 0x04008548 RID: 34120
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeResult;

		// Token: 0x04008549 RID: 34121
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
