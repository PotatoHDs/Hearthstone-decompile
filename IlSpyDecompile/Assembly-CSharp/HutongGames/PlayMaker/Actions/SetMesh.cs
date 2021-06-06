using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the mesh on a game object.")]
	public class SetMesh : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[ObjectType(typeof(Mesh))]
		public FsmObject mesh;

		public override void Reset()
		{
			gameObject = null;
			mesh = null;
		}

		public override void OnEnter()
		{
			DoSetMesh();
			Finish();
		}

		private void DoSetMesh()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				MeshFilter component = ownerDefaultTarget.GetComponent<MeshFilter>();
				if (component != null)
				{
					component.SetMesh(mesh.Value as Mesh);
				}
			}
		}
	}
}
