using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F77 RID: 3959
	[ActionCategory("Pegasus")]
	[Tooltip("Sets a named float in multiple game object's material.")]
	public class SetMaterialFloatMultiAction : ComponentAction<Renderer>
	{
		// Token: 0x0600AD66 RID: 44390 RVA: 0x00360E26 File Offset: 0x0035F026
		public override void Reset()
		{
			this.gameObjectList = null;
			this.namedFloat = "";
			this.floatValue = 0f;
			this.everyFrame = false;
		}

		// Token: 0x0600AD67 RID: 44391 RVA: 0x00360E56 File Offset: 0x0035F056
		public override void OnEnter()
		{
			this.DoSetMaterialFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD68 RID: 44392 RVA: 0x00360E6C File Offset: 0x0035F06C
		public override void OnUpdate()
		{
			this.DoSetMaterialFloat();
		}

		// Token: 0x0600AD69 RID: 44393 RVA: 0x00360E74 File Offset: 0x0035F074
		private void DoSetMaterialFloat()
		{
			for (int i = 0; i < this.gameObjectList.Length; i++)
			{
				this.gameObjectList[i].Value.GetComponent<Renderer>().GetMaterial().SetFloat(this.namedFloat.Value, this.floatValue.Value);
			}
		}

		// Token: 0x04009444 RID: 37956
		[Tooltip("The GameObjects that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmGameObject[] gameObjectList;

		// Token: 0x04009445 RID: 37957
		[RequiredField]
		[Tooltip("A named float parameter in the shader.")]
		public FsmString namedFloat;

		// Token: 0x04009446 RID: 37958
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmFloat floatValue;

		// Token: 0x04009447 RID: 37959
		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;
	}
}
