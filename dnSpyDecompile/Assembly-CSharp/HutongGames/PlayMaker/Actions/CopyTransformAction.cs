using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F2D RID: 3885
	[ActionCategory("Pegasus")]
	[Tooltip("Copies a game object's transform to another game object.")]
	public class CopyTransformAction : FsmStateAction
	{
		// Token: 0x0600AC3B RID: 44091 RVA: 0x0035BCA4 File Offset: 0x00359EA4
		public override void Reset()
		{
			this.m_SourceObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_TargetObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_Position = true;
			this.m_Rotation = true;
			this.m_Scale = true;
			this.m_WorldSpace = true;
		}

		// Token: 0x0600AC3C RID: 44092 RVA: 0x0035BD08 File Offset: 0x00359F08
		public override void OnEnter()
		{
			if (this.m_SourceObject == null || this.m_SourceObject.IsNone || !this.m_SourceObject.Value || this.m_TargetObject == null || this.m_TargetObject.IsNone || !this.m_TargetObject.Value)
			{
				base.Finish();
				return;
			}
			Transform transform = this.m_SourceObject.Value.transform;
			Transform transform2 = this.m_TargetObject.Value.transform;
			if (this.m_WorldSpace.Value)
			{
				if (this.m_Position.Value)
				{
					transform2.position = transform.position;
				}
				if (this.m_Rotation.Value)
				{
					transform2.rotation = transform.rotation;
				}
				if (this.m_Scale.Value)
				{
					TransformUtil.CopyWorldScale(transform2, transform);
				}
			}
			else
			{
				if (this.m_Position.Value)
				{
					transform2.localPosition = transform.localPosition;
				}
				if (this.m_Rotation.Value)
				{
					transform2.localRotation = transform.localRotation;
				}
				if (this.m_Scale.Value)
				{
					transform2.localScale = transform.localScale;
				}
			}
			base.Finish();
		}

		// Token: 0x0400930C RID: 37644
		[RequiredField]
		public FsmGameObject m_SourceObject;

		// Token: 0x0400930D RID: 37645
		[RequiredField]
		public FsmGameObject m_TargetObject;

		// Token: 0x0400930E RID: 37646
		public FsmBool m_Position;

		// Token: 0x0400930F RID: 37647
		public FsmBool m_Rotation;

		// Token: 0x04009310 RID: 37648
		public FsmBool m_Scale;

		// Token: 0x04009311 RID: 37649
		[Tooltip("Copies the transform in world space if checked, otherwise copies in local space.")]
		public FsmBool m_WorldSpace;
	}
}
