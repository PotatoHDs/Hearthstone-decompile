using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E8A RID: 3722
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the UI ScrollRect horizontal flag")]
	public class UiScrollRectSetHorizontal : ComponentAction<ScrollRect>
	{
		// Token: 0x0600A951 RID: 43345 RVA: 0x003524F6 File Offset: 0x003506F6
		public override void Reset()
		{
			this.gameObject = null;
			this.horizontal = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A952 RID: 43346 RVA: 0x00352514 File Offset: 0x00350714
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

		// Token: 0x0600A953 RID: 43347 RVA: 0x0035256D File Offset: 0x0035076D
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A954 RID: 43348 RVA: 0x00352575 File Offset: 0x00350775
		private void DoSetValue()
		{
			if (this.scrollRect != null)
			{
				this.scrollRect.horizontal = this.horizontal.Value;
			}
		}

		// Token: 0x0600A955 RID: 43349 RVA: 0x0035259B File Offset: 0x0035079B
		public override void OnExit()
		{
			if (this.scrollRect == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.scrollRect.horizontal = this.originalValue;
			}
		}

		// Token: 0x04008FFA RID: 36858
		[RequiredField]
		[CheckForComponent(typeof(ScrollRect))]
		[Tooltip("The GameObject with the UI ScrollRect component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FFB RID: 36859
		[Tooltip("The horizontal flag")]
		public FsmBool horizontal;

		// Token: 0x04008FFC RID: 36860
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FFD RID: 36861
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FFE RID: 36862
		private ScrollRect scrollRect;

		// Token: 0x04008FFF RID: 36863
		private bool originalValue;
	}
}
