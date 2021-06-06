using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E32 RID: 3634
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets properties of a UI CanvasGroup component.")]
	public class UiCanvasGroupSetProperties : ComponentAction<CanvasGroup>
	{
		// Token: 0x0600A7B4 RID: 42932 RVA: 0x0034D544 File Offset: 0x0034B744
		public override void Reset()
		{
			this.gameObject = null;
			this.alpha = new FsmFloat
			{
				UseVariable = true
			};
			this.interactable = new FsmBool
			{
				UseVariable = true
			};
			this.blocksRaycasts = new FsmBool
			{
				UseVariable = true
			};
			this.ignoreParentGroup = new FsmBool
			{
				UseVariable = true
			};
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7B5 RID: 42933 RVA: 0x0034D5B0 File Offset: 0x0034B7B0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.component = this.cachedComponent;
				if (this.component != null)
				{
					this.originalAlpha = this.component.alpha;
					this.originalInteractable = this.component.interactable;
					this.originalBlocksRaycasts = this.component.blocksRaycasts;
					this.originalIgnoreParentGroup = this.component.ignoreParentGroups;
				}
			}
			this.DoAction();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7B6 RID: 42934 RVA: 0x0034D64A File Offset: 0x0034B84A
		public override void OnUpdate()
		{
			this.DoAction();
		}

		// Token: 0x0600A7B7 RID: 42935 RVA: 0x0034D654 File Offset: 0x0034B854
		private void DoAction()
		{
			if (this.component == null)
			{
				return;
			}
			if (!this.alpha.IsNone)
			{
				this.component.alpha = this.alpha.Value;
			}
			if (!this.interactable.IsNone)
			{
				this.component.interactable = this.interactable.Value;
			}
			if (!this.blocksRaycasts.IsNone)
			{
				this.component.blocksRaycasts = this.blocksRaycasts.Value;
			}
			if (!this.ignoreParentGroup.IsNone)
			{
				this.component.ignoreParentGroups = this.ignoreParentGroup.Value;
			}
		}

		// Token: 0x0600A7B8 RID: 42936 RVA: 0x0034D6FC File Offset: 0x0034B8FC
		public override void OnExit()
		{
			if (this.component == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				if (!this.alpha.IsNone)
				{
					this.component.alpha = this.originalAlpha;
				}
				if (!this.interactable.IsNone)
				{
					this.component.interactable = this.originalInteractable;
				}
				if (!this.blocksRaycasts.IsNone)
				{
					this.component.blocksRaycasts = this.originalBlocksRaycasts;
				}
				if (!this.ignoreParentGroup.IsNone)
				{
					this.component.ignoreParentGroups = this.originalIgnoreParentGroup;
				}
			}
		}

		// Token: 0x04008E3E RID: 36414
		[RequiredField]
		[CheckForComponent(typeof(CanvasGroup))]
		[Tooltip("The GameObject with the UI CanvasGroup component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E3F RID: 36415
		[Tooltip("Canvas group alpha. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat alpha;

		// Token: 0x04008E40 RID: 36416
		[Tooltip("Is the group interactable (are the elements beneath the group enabled). Leave as None for no effect")]
		public FsmBool interactable;

		// Token: 0x04008E41 RID: 36417
		[Tooltip("Does this group block raycasting (allow collision). Leave as None for no effect")]
		public FsmBool blocksRaycasts;

		// Token: 0x04008E42 RID: 36418
		[Tooltip("Should the group ignore parent groups? Leave as None for no effect")]
		public FsmBool ignoreParentGroup;

		// Token: 0x04008E43 RID: 36419
		[Tooltip("Reset when exiting this state. Leave as None for no effect")]
		public FsmBool resetOnExit;

		// Token: 0x04008E44 RID: 36420
		public bool everyFrame;

		// Token: 0x04008E45 RID: 36421
		private CanvasGroup component;

		// Token: 0x04008E46 RID: 36422
		private float originalAlpha;

		// Token: 0x04008E47 RID: 36423
		private bool originalInteractable;

		// Token: 0x04008E48 RID: 36424
		private bool originalBlocksRaycasts;

		// Token: 0x04008E49 RID: 36425
		private bool originalIgnoreParentGroup;
	}
}
