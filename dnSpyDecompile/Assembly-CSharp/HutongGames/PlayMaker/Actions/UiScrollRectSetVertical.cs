using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E8C RID: 3724
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the UI ScrollRect vertical flag")]
	public class UiScrollRectSetVertical : ComponentAction<ScrollRect>
	{
		// Token: 0x0600A95D RID: 43357 RVA: 0x00352742 File Offset: 0x00350942
		public override void Reset()
		{
			this.gameObject = null;
			this.vertical = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A95E RID: 43358 RVA: 0x00352760 File Offset: 0x00350960
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollRect = this.cachedComponent;
			}
			this.originalValue = this.scrollRect.vertical;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A95F RID: 43359 RVA: 0x003527B9 File Offset: 0x003509B9
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A960 RID: 43360 RVA: 0x003527C1 File Offset: 0x003509C1
		private void DoSetValue()
		{
			if (this.scrollRect != null)
			{
				this.scrollRect.vertical = this.vertical.Value;
			}
		}

		// Token: 0x0600A961 RID: 43361 RVA: 0x003527E7 File Offset: 0x003509E7
		public override void OnExit()
		{
			if (this.scrollRect == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.scrollRect.vertical = this.originalValue;
			}
		}

		// Token: 0x04009008 RID: 36872
		[RequiredField]
		[CheckForComponent(typeof(ScrollRect))]
		[Tooltip("The GameObject with the UI ScrollRect component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009009 RID: 36873
		[Tooltip("The vertical flag")]
		public FsmBool vertical;

		// Token: 0x0400900A RID: 36874
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x0400900B RID: 36875
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x0400900C RID: 36876
		private ScrollRect scrollRect;

		// Token: 0x0400900D RID: 36877
		private bool originalValue;
	}
}
