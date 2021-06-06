using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E54 RID: 3668
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the navigation mode of a UI Selectable component.")]
	public class UiNavigationGetMode : ComponentAction<Selectable>
	{
		// Token: 0x0600A847 RID: 43079 RVA: 0x0034F50E File Offset: 0x0034D70E
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600A848 RID: 43080 RVA: 0x0034F518 File Offset: 0x0034D718
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

		// Token: 0x0600A849 RID: 43081 RVA: 0x0034F558 File Offset: 0x0034D758
		private void DoGetValue()
		{
			if (this.selectable == null)
			{
				return;
			}
			this.navigationMode.Value = this.selectable.navigation.mode.ToString();
			if (this.selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.None)
			{
				base.Fsm.Event(this.noNavigationEvent);
				return;
			}
			if (this.selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Automatic)
			{
				base.Fsm.Event(this.automaticEvent);
				return;
			}
			if (this.selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Vertical)
			{
				base.Fsm.Event(this.verticalEvent);
				return;
			}
			if (this.selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Horizontal)
			{
				base.Fsm.Event(this.horizontalEvent);
				return;
			}
			if (this.selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Explicit)
			{
				base.Fsm.Event(this.explicitEvent);
			}
		}

		// Token: 0x04008ED0 RID: 36560
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008ED1 RID: 36561
		[Tooltip("The navigation mode value")]
		public FsmString navigationMode;

		// Token: 0x04008ED2 RID: 36562
		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent automaticEvent;

		// Token: 0x04008ED3 RID: 36563
		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent horizontalEvent;

		// Token: 0x04008ED4 RID: 36564
		[Tooltip("Event sent if transition is SpriteSwap")]
		public FsmEvent verticalEvent;

		// Token: 0x04008ED5 RID: 36565
		[Tooltip("Event sent if transition is Animation")]
		public FsmEvent explicitEvent;

		// Token: 0x04008ED6 RID: 36566
		[Tooltip("Event sent if transition is none")]
		public FsmEvent noNavigationEvent;

		// Token: 0x04008ED7 RID: 36567
		private Selectable selectable;

		// Token: 0x04008ED8 RID: 36568
		private Selectable.Transition originalTransition;
	}
}
