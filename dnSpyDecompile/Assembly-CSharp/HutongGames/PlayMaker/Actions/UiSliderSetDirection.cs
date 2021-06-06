using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E93 RID: 3731
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the direction of a UI Slider component.")]
	public class UiSliderSetDirection : ComponentAction<Slider>
	{
		// Token: 0x0600A97F RID: 43391 RVA: 0x00352C0D File Offset: 0x00350E0D
		public override void Reset()
		{
			this.gameObject = null;
			this.direction = Slider.Direction.LeftToRight;
			this.includeRectLayouts = new FsmBool
			{
				UseVariable = true
			};
			this.resetOnExit = null;
		}

		// Token: 0x0600A980 RID: 43392 RVA: 0x00352C40 File Offset: 0x00350E40
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.originalValue = this.slider.direction;
			this.DoSetValue();
		}

		// Token: 0x0600A981 RID: 43393 RVA: 0x00352C8C File Offset: 0x00350E8C
		private void DoSetValue()
		{
			if (this.slider == null)
			{
				return;
			}
			if (this.includeRectLayouts.IsNone)
			{
				this.slider.direction = (Slider.Direction)this.direction.Value;
				return;
			}
			this.slider.SetDirection((Slider.Direction)this.direction.Value, this.includeRectLayouts.Value);
		}

		// Token: 0x0600A982 RID: 43394 RVA: 0x00352CF8 File Offset: 0x00350EF8
		public override void OnExit()
		{
			if (this.slider == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				if (this.includeRectLayouts.IsNone)
				{
					this.slider.direction = this.originalValue;
					return;
				}
				this.slider.SetDirection(this.originalValue, this.includeRectLayouts.Value);
			}
		}

		// Token: 0x04009028 RID: 36904
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009029 RID: 36905
		[RequiredField]
		[Tooltip("The direction of the UI Slider component.")]
		[ObjectType(typeof(Slider.Direction))]
		public FsmEnum direction;

		// Token: 0x0400902A RID: 36906
		[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		public FsmBool includeRectLayouts;

		// Token: 0x0400902B RID: 36907
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x0400902C RID: 36908
		private Slider slider;

		// Token: 0x0400902D RID: 36909
		private Slider.Direction originalValue;
	}
}
