using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E51 RID: 3665
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the explicit navigation properties of a UI Selectable component. Note that it will have no effect until Navigation mode is set to 'Explicit'.")]
	public class UiNavigationExplicitSetProperties : ComponentAction<Selectable>
	{
		// Token: 0x0600A838 RID: 43064 RVA: 0x0034F038 File Offset: 0x0034D238
		public override void Reset()
		{
			this.gameObject = null;
			this.selectOnDown = new FsmGameObject
			{
				UseVariable = true
			};
			this.selectOnUp = new FsmGameObject
			{
				UseVariable = true
			};
			this.selectOnLeft = new FsmGameObject
			{
				UseVariable = true
			};
			this.selectOnRight = new FsmGameObject
			{
				UseVariable = true
			};
			this.resetOnExit = false;
		}

		// Token: 0x0600A839 RID: 43065 RVA: 0x0034F0A0 File Offset: 0x0034D2A0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			if (this.selectable != null && this.resetOnExit.Value)
			{
				this.originalState = this.selectable.navigation;
			}
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A83A RID: 43066 RVA: 0x0034F10C File Offset: 0x0034D30C
		private void DoSetValue()
		{
			if (this.selectable == null)
			{
				return;
			}
			this.navigation = this.selectable.navigation;
			if (!this.selectOnDown.IsNone)
			{
				this.navigation.selectOnDown = UiNavigationExplicitSetProperties.GetComponentFromFsmGameObject<Selectable>(this.selectOnDown);
			}
			if (!this.selectOnUp.IsNone)
			{
				this.navigation.selectOnUp = UiNavigationExplicitSetProperties.GetComponentFromFsmGameObject<Selectable>(this.selectOnUp);
			}
			if (!this.selectOnLeft.IsNone)
			{
				this.navigation.selectOnLeft = UiNavigationExplicitSetProperties.GetComponentFromFsmGameObject<Selectable>(this.selectOnLeft);
			}
			if (!this.selectOnRight.IsNone)
			{
				this.navigation.selectOnRight = UiNavigationExplicitSetProperties.GetComponentFromFsmGameObject<Selectable>(this.selectOnRight);
			}
			this.selectable.navigation = this.navigation;
		}

		// Token: 0x0600A83B RID: 43067 RVA: 0x0034F1D8 File Offset: 0x0034D3D8
		public override void OnExit()
		{
			if (this.selectable == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.navigation = this.selectable.navigation;
				this.navigation.selectOnDown = this.originalState.selectOnDown;
				this.navigation.selectOnLeft = this.originalState.selectOnLeft;
				this.navigation.selectOnRight = this.originalState.selectOnRight;
				this.navigation.selectOnUp = this.originalState.selectOnUp;
				this.selectable.navigation = this.navigation;
			}
		}

		// Token: 0x0600A83C RID: 43068 RVA: 0x0034F27C File Offset: 0x0034D47C
		private static T GetComponentFromFsmGameObject<T>(FsmGameObject variable) where T : Component
		{
			if (variable.Value != null)
			{
				return variable.Value.GetComponent<T>();
			}
			return default(T);
		}

		// Token: 0x04008EB8 RID: 36536
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EB9 RID: 36537
		[Tooltip("The down Selectable. Leave as None for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnDown;

		// Token: 0x04008EBA RID: 36538
		[Tooltip("The up Selectable.  Leave as None for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnUp;

		// Token: 0x04008EBB RID: 36539
		[Tooltip("The left Selectable.  Leave as None for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnLeft;

		// Token: 0x04008EBC RID: 36540
		[Tooltip("The right Selectable.  Leave as None for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnRight;

		// Token: 0x04008EBD RID: 36541
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008EBE RID: 36542
		private Selectable selectable;

		// Token: 0x04008EBF RID: 36543
		private UnityEngine.UI.Navigation navigation;

		// Token: 0x04008EC0 RID: 36544
		private UnityEngine.UI.Navigation originalState;
	}
}
