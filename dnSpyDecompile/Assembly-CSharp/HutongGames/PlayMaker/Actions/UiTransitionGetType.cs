using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E59 RID: 3673
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the transition type of a UI Selectable component.")]
	public class UiTransitionGetType : ComponentAction<Selectable>
	{
		// Token: 0x0600A860 RID: 43104 RVA: 0x0034FC99 File Offset: 0x0034DE99
		public override void Reset()
		{
			this.gameObject = null;
			this.transition = null;
			this.colorTintEvent = null;
			this.spriteSwapEvent = null;
			this.animationEvent = null;
			this.noTransitionEvent = null;
		}

		// Token: 0x0600A861 RID: 43105 RVA: 0x0034FCC8 File Offset: 0x0034DEC8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			this.DoGetValue();
			base.Finish();
		}

		// Token: 0x0600A862 RID: 43106 RVA: 0x0034FD08 File Offset: 0x0034DF08
		private void DoGetValue()
		{
			if (this.selectable == null)
			{
				return;
			}
			this.transition.Value = this.selectable.transition.ToString();
			if (this.selectable.transition == Selectable.Transition.None)
			{
				base.Fsm.Event(this.noTransitionEvent);
				return;
			}
			if (this.selectable.transition == Selectable.Transition.ColorTint)
			{
				base.Fsm.Event(this.colorTintEvent);
				return;
			}
			if (this.selectable.transition == Selectable.Transition.SpriteSwap)
			{
				base.Fsm.Event(this.spriteSwapEvent);
				return;
			}
			if (this.selectable.transition == Selectable.Transition.Animation)
			{
				base.Fsm.Event(this.animationEvent);
			}
		}

		// Token: 0x04008EF9 RID: 36601
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EFA RID: 36602
		[Tooltip("The transition value")]
		public FsmString transition;

		// Token: 0x04008EFB RID: 36603
		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent colorTintEvent;

		// Token: 0x04008EFC RID: 36604
		[Tooltip("Event sent if transition is SpriteSwap")]
		public FsmEvent spriteSwapEvent;

		// Token: 0x04008EFD RID: 36605
		[Tooltip("Event sent if transition is Animation")]
		public FsmEvent animationEvent;

		// Token: 0x04008EFE RID: 36606
		[Tooltip("Event sent if transition is none")]
		public FsmEvent noTransitionEvent;

		// Token: 0x04008EFF RID: 36607
		private Selectable selectable;

		// Token: 0x04008F00 RID: 36608
		private Selectable.Transition originalTransition;
	}
}
