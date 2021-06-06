using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E55 RID: 3669
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the navigation mode of a UI Selectable component.")]
	public class UiNavigationSetMode : ComponentAction<Selectable>
	{
		// Token: 0x0600A84B RID: 43083 RVA: 0x0034F666 File Offset: 0x0034D866
		public override void Reset()
		{
			this.gameObject = null;
			this.navigationMode = UnityEngine.UI.Navigation.Mode.Automatic;
			this.resetOnExit = false;
		}

		// Token: 0x0600A84C RID: 43084 RVA: 0x0034F684 File Offset: 0x0034D884
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			if (this.selectable != null && this.resetOnExit.Value)
			{
				this.originalValue = this.selectable.navigation.mode;
			}
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A84D RID: 43085 RVA: 0x0034F6F8 File Offset: 0x0034D8F8
		private void DoSetValue()
		{
			if (this.selectable != null)
			{
				this._navigation = this.selectable.navigation;
				this._navigation.mode = this.navigationMode;
				this.selectable.navigation = this._navigation;
			}
		}

		// Token: 0x0600A84E RID: 43086 RVA: 0x0034F748 File Offset: 0x0034D948
		public override void OnExit()
		{
			if (this.selectable == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this._navigation = this.selectable.navigation;
				this._navigation.mode = this.originalValue;
				this.selectable.navigation = this._navigation;
			}
		}

		// Token: 0x04008ED9 RID: 36569
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EDA RID: 36570
		[Tooltip("The navigation mode value")]
		public UnityEngine.UI.Navigation.Mode navigationMode;

		// Token: 0x04008EDB RID: 36571
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008EDC RID: 36572
		private Selectable selectable;

		// Token: 0x04008EDD RID: 36573
		private UnityEngine.UI.Navigation _navigation;

		// Token: 0x04008EDE RID: 36574
		private UnityEngine.UI.Navigation.Mode originalValue;
	}
}
