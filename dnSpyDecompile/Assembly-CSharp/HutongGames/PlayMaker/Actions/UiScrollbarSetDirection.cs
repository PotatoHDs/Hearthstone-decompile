using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E86 RID: 3718
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the direction of a UI Scrollbar component.")]
	public class UiScrollbarSetDirection : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		// Token: 0x0600A93A RID: 43322 RVA: 0x00352105 File Offset: 0x00350305
		public override void Reset()
		{
			this.gameObject = null;
			this.direction = UnityEngine.UI.Scrollbar.Direction.LeftToRight;
			this.includeRectLayouts = new FsmBool
			{
				UseVariable = true
			};
			this.resetOnExit = null;
		}

		// Token: 0x0600A93B RID: 43323 RVA: 0x00352138 File Offset: 0x00350338
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollbar = this.cachedComponent;
			}
			if (this.resetOnExit.Value)
			{
				this.originalValue = this.scrollbar.direction;
			}
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A93C RID: 43324 RVA: 0x00352198 File Offset: 0x00350398
		private void DoSetValue()
		{
			if (this.scrollbar == null)
			{
				return;
			}
			if (this.includeRectLayouts.IsNone)
			{
				this.scrollbar.direction = (UnityEngine.UI.Scrollbar.Direction)this.direction.Value;
				return;
			}
			this.scrollbar.SetDirection((UnityEngine.UI.Scrollbar.Direction)this.direction.Value, this.includeRectLayouts.Value);
		}

		// Token: 0x0600A93D RID: 43325 RVA: 0x00352204 File Offset: 0x00350404
		public override void OnExit()
		{
			if (this.scrollbar == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				if (this.includeRectLayouts.IsNone)
				{
					this.scrollbar.direction = this.originalValue;
					return;
				}
				this.scrollbar.SetDirection(this.originalValue, this.includeRectLayouts.Value);
			}
		}

		// Token: 0x04008FE2 RID: 36834
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FE3 RID: 36835
		[RequiredField]
		[Tooltip("The direction of the UI Scrollbar.")]
		[ObjectType(typeof(UnityEngine.UI.Scrollbar.Direction))]
		public FsmEnum direction;

		// Token: 0x04008FE4 RID: 36836
		[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		public FsmBool includeRectLayouts;

		// Token: 0x04008FE5 RID: 36837
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FE6 RID: 36838
		private UnityEngine.UI.Scrollbar scrollbar;

		// Token: 0x04008FE7 RID: 36839
		private UnityEngine.UI.Scrollbar.Direction originalValue;
	}
}
