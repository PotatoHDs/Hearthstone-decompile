using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C17 RID: 3095
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Draws a line from a Start point to an End point. Specify the points as Game Objects or Vector3 world positions. If both are specified, position is used as a local offset from the Object's position.")]
	public class DrawDebugLine : FsmStateAction
	{
		// Token: 0x06009DEF RID: 40431 RVA: 0x0032A1B8 File Offset: 0x003283B8
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
			this.toObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.toPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.color = Color.white;
		}

		// Token: 0x06009DF0 RID: 40432 RVA: 0x0032A220 File Offset: 0x00328420
		public override void OnUpdate()
		{
			Vector3 position = ActionHelpers.GetPosition(this.fromObject, this.fromPosition);
			Vector3 position2 = ActionHelpers.GetPosition(this.toObject, this.toPosition);
			Debug.DrawLine(position, position2, this.color.Value);
		}

		// Token: 0x04008343 RID: 33603
		[Tooltip("Draw line from a GameObject.")]
		public FsmGameObject fromObject;

		// Token: 0x04008344 RID: 33604
		[Tooltip("Draw line from a world position, or local offset from GameObject if provided.")]
		public FsmVector3 fromPosition;

		// Token: 0x04008345 RID: 33605
		[Tooltip("Draw line to a GameObject.")]
		public FsmGameObject toObject;

		// Token: 0x04008346 RID: 33606
		[Tooltip("Draw line to a world position, or local offset from GameObject if provided.")]
		public FsmVector3 toPosition;

		// Token: 0x04008347 RID: 33607
		[Tooltip("The color of the line.")]
		public FsmColor color;
	}
}
