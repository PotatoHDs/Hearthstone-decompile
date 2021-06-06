using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C80 RID: 3200
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last Raycast and store in variables.")]
	public class GetRaycastHitInfo : FsmStateAction
	{
		// Token: 0x06009FC1 RID: 40897 RVA: 0x0032F2B7 File Offset: 0x0032D4B7
		public override void Reset()
		{
			this.gameObjectHit = null;
			this.point = null;
			this.normal = null;
			this.distance = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FC2 RID: 40898 RVA: 0x0032F2DC File Offset: 0x0032D4DC
		private void StoreRaycastInfo()
		{
			if (base.Fsm.RaycastHitInfo.collider != null)
			{
				this.gameObjectHit.Value = base.Fsm.RaycastHitInfo.collider.gameObject;
				this.point.Value = base.Fsm.RaycastHitInfo.point;
				this.normal.Value = base.Fsm.RaycastHitInfo.normal;
				this.distance.Value = base.Fsm.RaycastHitInfo.distance;
			}
		}

		// Token: 0x06009FC3 RID: 40899 RVA: 0x0032F381 File Offset: 0x0032D581
		public override void OnEnter()
		{
			this.StoreRaycastInfo();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FC4 RID: 40900 RVA: 0x0032F397 File Offset: 0x0032D597
		public override void OnUpdate()
		{
			this.StoreRaycastInfo();
		}

		// Token: 0x0400854A RID: 34122
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit by the last Raycast and store it in a variable.")]
		public FsmGameObject gameObjectHit;

		// Token: 0x0400854B RID: 34123
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the ray hit point and store it in a variable.")]
		[Title("Hit Point")]
		public FsmVector3 point;

		// Token: 0x0400854C RID: 34124
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the normal at the hit point and store it in a variable.")]
		public FsmVector3 normal;

		// Token: 0x0400854D RID: 34125
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		public FsmFloat distance;

		// Token: 0x0400854E RID: 34126
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
