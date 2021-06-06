using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C18 RID: 3096
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Draws a line from a Start point in a direction. Specify the start point as Game Objects or Vector3 world positions. If both are specified, position is used as a local offset from the Object's position.")]
	public class DrawDebugRay : FsmStateAction
	{
		// Token: 0x06009DF2 RID: 40434 RVA: 0x0032A264 File Offset: 0x00328464
		public override void Reset()
		{
			this.fromObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.fromPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.direction = new FsmVector3
			{
				UseVariable = true
			};
			this.color = Color.white;
		}

		// Token: 0x06009DF3 RID: 40435 RVA: 0x0032A2B7 File Offset: 0x003284B7
		public override void OnUpdate()
		{
			Debug.DrawRay(ActionHelpers.GetPosition(this.fromObject, this.fromPosition), this.direction.Value, this.color.Value);
		}

		// Token: 0x04008348 RID: 33608
		[Tooltip("Draw ray from a GameObject.")]
		public FsmGameObject fromObject;

		// Token: 0x04008349 RID: 33609
		[Tooltip("Draw ray from a world position, or local offset from GameObject if provided.")]
		public FsmVector3 fromPosition;

		// Token: 0x0400834A RID: 33610
		[Tooltip("Direction vector of ray.")]
		public FsmVector3 direction;

		// Token: 0x0400834B RID: 33611
		[Tooltip("The color of the ray.")]
		public FsmColor color;
	}
}
