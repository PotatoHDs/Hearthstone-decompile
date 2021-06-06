using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E8B RID: 3723
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("The normalized scroll position as a Vector2 between (0,0) and (1,1) with (0,0) being the lower left corner.")]
	public class UiScrollRectSetNormalizedPosition : ComponentAction<ScrollRect>
	{
		// Token: 0x0600A957 RID: 43351 RVA: 0x003525D4 File Offset: 0x003507D4
		public override void Reset()
		{
			this.gameObject = null;
			this.normalizedPosition = null;
			this.horizontalPosition = new FsmFloat
			{
				UseVariable = true
			};
			this.verticalPosition = new FsmFloat
			{
				UseVariable = true
			};
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A958 RID: 43352 RVA: 0x00352624 File Offset: 0x00350824
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollRect = this.cachedComponent;
			}
			this.originalValue = this.scrollRect.normalizedPosition;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A959 RID: 43353 RVA: 0x0035267D File Offset: 0x0035087D
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A95A RID: 43354 RVA: 0x00352688 File Offset: 0x00350888
		private void DoSetValue()
		{
			if (this.scrollRect == null)
			{
				return;
			}
			Vector2 value = this.scrollRect.normalizedPosition;
			if (!this.normalizedPosition.IsNone)
			{
				value = this.normalizedPosition.Value;
			}
			if (!this.horizontalPosition.IsNone)
			{
				value.x = this.horizontalPosition.Value;
			}
			if (!this.verticalPosition.IsNone)
			{
				value.y = this.verticalPosition.Value;
			}
			this.scrollRect.normalizedPosition = value;
		}

		// Token: 0x0600A95B RID: 43355 RVA: 0x00352713 File Offset: 0x00350913
		public override void OnExit()
		{
			if (this.scrollRect == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.scrollRect.normalizedPosition = this.originalValue;
			}
		}

		// Token: 0x04009000 RID: 36864
		[RequiredField]
		[CheckForComponent(typeof(ScrollRect))]
		[Tooltip("The GameObject with the UI ScrollRect component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009001 RID: 36865
		[Tooltip("The position's value of the UI ScrollRect component. Ranges from 0.0 to 1.0.")]
		public FsmVector2 normalizedPosition;

		// Token: 0x04009002 RID: 36866
		[Tooltip("The horizontal position's value of the UI ScrollRect component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat horizontalPosition;

		// Token: 0x04009003 RID: 36867
		[Tooltip("The vertical position's value of the UI ScrollRect component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat verticalPosition;

		// Token: 0x04009004 RID: 36868
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04009005 RID: 36869
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04009006 RID: 36870
		private ScrollRect scrollRect;

		// Token: 0x04009007 RID: 36871
		private Vector2 originalValue;
	}
}
