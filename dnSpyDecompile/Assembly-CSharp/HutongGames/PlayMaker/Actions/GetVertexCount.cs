using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C97 RID: 3223
	[ActionCategory("Mesh")]
	[Tooltip("Gets the number of vertices in a GameObject's mesh. Useful in conjunction with GetVertexPosition.")]
	public class GetVertexCount : FsmStateAction
	{
		// Token: 0x0600A02A RID: 41002 RVA: 0x0033022A File Offset: 0x0032E42A
		public override void Reset()
		{
			this.gameObject = null;
			this.storeCount = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A02B RID: 41003 RVA: 0x00330241 File Offset: 0x0032E441
		public override void OnEnter()
		{
			this.DoGetVertexCount();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A02C RID: 41004 RVA: 0x00330257 File Offset: 0x0032E457
		public override void OnUpdate()
		{
			this.DoGetVertexCount();
		}

		// Token: 0x0600A02D RID: 41005 RVA: 0x00330260 File Offset: 0x0032E460
		private void DoGetVertexCount()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				MeshFilter component = ownerDefaultTarget.GetComponent<MeshFilter>();
				if (component == null)
				{
					base.LogError("Missing MeshFilter!");
					return;
				}
				this.storeCount.Value = component.mesh.vertexCount;
			}
		}

		// Token: 0x040085A8 RID: 34216
		[RequiredField]
		[CheckForComponent(typeof(MeshFilter))]
		[Tooltip("The GameObject to check.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040085A9 RID: 34217
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the vertex count in a variable.")]
		public FsmInt storeCount;

		// Token: 0x040085AA RID: 34218
		public bool everyFrame;
	}
}
