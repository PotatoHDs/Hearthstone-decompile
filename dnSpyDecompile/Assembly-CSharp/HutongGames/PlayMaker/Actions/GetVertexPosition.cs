using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C98 RID: 3224
	[ActionCategory("Mesh")]
	[Tooltip("Gets the position of a vertex in a GameObject's mesh. Hint: Use GetVertexCount to get the number of vertices in a mesh.")]
	public class GetVertexPosition : FsmStateAction
	{
		// Token: 0x0600A02F RID: 41007 RVA: 0x003302BA File Offset: 0x0032E4BA
		public override void Reset()
		{
			this.gameObject = null;
			this.space = Space.World;
			this.storePosition = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A030 RID: 41008 RVA: 0x003302D8 File Offset: 0x0032E4D8
		public override void OnEnter()
		{
			this.DoGetVertexPosition();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A031 RID: 41009 RVA: 0x003302EE File Offset: 0x0032E4EE
		public override void OnUpdate()
		{
			this.DoGetVertexPosition();
		}

		// Token: 0x0600A032 RID: 41010 RVA: 0x003302F8 File Offset: 0x0032E4F8
		private void DoGetVertexPosition()
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
				Space space = this.space;
				if (space == Space.World)
				{
					Vector3 position = component.mesh.vertices[this.vertexIndex.Value];
					this.storePosition.Value = ownerDefaultTarget.transform.TransformPoint(position);
					return;
				}
				if (space != Space.Self)
				{
					return;
				}
				this.storePosition.Value = component.mesh.vertices[this.vertexIndex.Value];
			}
		}

		// Token: 0x040085AB RID: 34219
		[RequiredField]
		[CheckForComponent(typeof(MeshFilter))]
		[Tooltip("The GameObject to check.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040085AC RID: 34220
		[RequiredField]
		[Tooltip("The index of the vertex.")]
		public FsmInt vertexIndex;

		// Token: 0x040085AD RID: 34221
		[Tooltip("Coordinate system to use.")]
		public Space space;

		// Token: 0x040085AE RID: 34222
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the vertex position in a variable.")]
		public FsmVector3 storePosition;

		// Token: 0x040085AF RID: 34223
		[Tooltip("Repeat every frame. Useful if the mesh is animated.")]
		public bool everyFrame;
	}
}
