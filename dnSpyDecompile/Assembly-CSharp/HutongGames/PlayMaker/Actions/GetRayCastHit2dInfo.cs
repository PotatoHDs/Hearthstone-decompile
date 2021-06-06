using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D0A RID: 3338
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets info on the last 2d Raycast or LineCast and store in variables.")]
	public class GetRayCastHit2dInfo : FsmStateAction
	{
		// Token: 0x0600A22F RID: 41519 RVA: 0x0033B1C9 File Offset: 0x003393C9
		public override void Reset()
		{
			this.gameObjectHit = null;
			this.point = null;
			this.normal = null;
			this.distance = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A230 RID: 41520 RVA: 0x0033B1EE File Offset: 0x003393EE
		public override void OnEnter()
		{
			this.StoreRaycastInfo();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A231 RID: 41521 RVA: 0x0033B204 File Offset: 0x00339404
		public override void OnUpdate()
		{
			this.StoreRaycastInfo();
		}

		// Token: 0x0600A232 RID: 41522 RVA: 0x0033B20C File Offset: 0x0033940C
		private void StoreRaycastInfo()
		{
			RaycastHit2D lastRaycastHit2DInfo = Fsm.GetLastRaycastHit2DInfo(base.Fsm);
			if (lastRaycastHit2DInfo.collider != null)
			{
				this.gameObjectHit.Value = lastRaycastHit2DInfo.collider.gameObject;
				this.point.Value = lastRaycastHit2DInfo.point;
				this.normal.Value = lastRaycastHit2DInfo.normal;
				this.distance.Value = lastRaycastHit2DInfo.fraction;
			}
		}

		// Token: 0x0400886F RID: 34927
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit by the last Raycast and store it in a variable.")]
		public FsmGameObject gameObjectHit;

		// Token: 0x04008870 RID: 34928
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the ray hit point and store it in a variable.")]
		[Title("Hit Point")]
		public FsmVector2 point;

		// Token: 0x04008871 RID: 34929
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the normal at the hit point and store it in a variable.")]
		public FsmVector3 normal;

		// Token: 0x04008872 RID: 34930
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		public FsmFloat distance;

		// Token: 0x04008873 RID: 34931
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
