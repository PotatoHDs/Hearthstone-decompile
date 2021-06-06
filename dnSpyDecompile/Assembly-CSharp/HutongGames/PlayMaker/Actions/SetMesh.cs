using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DEB RID: 3563
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the mesh on a game object.")]
	public class SetMesh : ComponentAction<Renderer>
	{
		// Token: 0x0600A66A RID: 42602 RVA: 0x00349325 File Offset: 0x00347525
		public override void Reset()
		{
			this.gameObject = null;
			this.mesh = null;
		}

		// Token: 0x0600A66B RID: 42603 RVA: 0x00349335 File Offset: 0x00347535
		public override void OnEnter()
		{
			this.DoSetMesh();
			base.Finish();
		}

		// Token: 0x0600A66C RID: 42604 RVA: 0x00349344 File Offset: 0x00347544
		private void DoSetMesh()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			MeshFilter component = ownerDefaultTarget.GetComponent<MeshFilter>();
			if (component != null)
			{
				component.SetMesh(this.mesh.Value as Mesh);
			}
		}

		// Token: 0x04008CEF RID: 36079
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CF0 RID: 36080
		[RequiredField]
		[ObjectType(typeof(Mesh))]
		public FsmObject mesh;
	}
}
