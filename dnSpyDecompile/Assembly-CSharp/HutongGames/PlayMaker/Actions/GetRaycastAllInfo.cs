using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CFD RID: 3325
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last RaycastAll and store in array variables.")]
	public class GetRaycastAllInfo : FsmStateAction
	{
		// Token: 0x0600A1E0 RID: 41440 RVA: 0x0033951D File Offset: 0x0033771D
		public override void Reset()
		{
			this.storeHitObjects = null;
			this.points = null;
			this.normals = null;
			this.distances = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A1E1 RID: 41441 RVA: 0x00339544 File Offset: 0x00337744
		private void StoreRaycastAllInfo()
		{
			if (RaycastAll.RaycastAllHitInfo == null)
			{
				return;
			}
			this.storeHitObjects.Resize(RaycastAll.RaycastAllHitInfo.Length);
			this.points.Resize(RaycastAll.RaycastAllHitInfo.Length);
			this.normals.Resize(RaycastAll.RaycastAllHitInfo.Length);
			this.distances.Resize(RaycastAll.RaycastAllHitInfo.Length);
			for (int i = 0; i < RaycastAll.RaycastAllHitInfo.Length; i++)
			{
				this.storeHitObjects.Values[i] = RaycastAll.RaycastAllHitInfo[i].collider.gameObject;
				this.points.Values[i] = RaycastAll.RaycastAllHitInfo[i].point;
				this.normals.Values[i] = RaycastAll.RaycastAllHitInfo[i].normal;
				this.distances.Values[i] = RaycastAll.RaycastAllHitInfo[i].distance;
			}
		}

		// Token: 0x0600A1E2 RID: 41442 RVA: 0x00339641 File Offset: 0x00337841
		public override void OnEnter()
		{
			this.StoreRaycastAllInfo();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1E3 RID: 41443 RVA: 0x00339657 File Offset: 0x00337857
		public override void OnUpdate()
		{
			this.StoreRaycastAllInfo();
		}

		// Token: 0x040087EE RID: 34798
		[Tooltip("Store the GameObjects hit in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray storeHitObjects;

		// Token: 0x040087EF RID: 34799
		[Tooltip("Get the world position of all ray hit point and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Vector3, "", 0, 0, 65536)]
		public FsmArray points;

		// Token: 0x040087F0 RID: 34800
		[Tooltip("Get the normal at all hit points and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Vector3, "", 0, 0, 65536)]
		public FsmArray normals;

		// Token: 0x040087F1 RID: 34801
		[Tooltip("Get the distance along the ray to all hit points and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Float, "", 0, 0, 65536)]
		public FsmArray distances;

		// Token: 0x040087F2 RID: 34802
		[Tooltip("Repeat every frame. Warning, this could be affecting performances")]
		public bool everyFrame;
	}
}
