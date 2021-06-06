using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E82 RID: 3714
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Rebuild a UI Graphic component.")]
	public class UiRebuild : ComponentAction<Graphic>
	{
		// Token: 0x0600A926 RID: 43302 RVA: 0x00351E72 File Offset: 0x00350072
		public override void Reset()
		{
			this.gameObject = null;
			this.canvasUpdate = CanvasUpdate.LatePreRender;
			this.rebuildOnExit = false;
		}

		// Token: 0x0600A927 RID: 43303 RVA: 0x00351E8C File Offset: 0x0035008C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.graphic = this.cachedComponent;
			}
			if (!this.rebuildOnExit)
			{
				this.DoAction();
			}
			base.Finish();
		}

		// Token: 0x0600A928 RID: 43304 RVA: 0x00351ED4 File Offset: 0x003500D4
		private void DoAction()
		{
			if (this.graphic != null)
			{
				this.graphic.Rebuild(this.canvasUpdate);
			}
		}

		// Token: 0x0600A929 RID: 43305 RVA: 0x00351EF5 File Offset: 0x003500F5
		public override void OnExit()
		{
			if (this.rebuildOnExit)
			{
				this.DoAction();
			}
		}

		// Token: 0x04008FD1 RID: 36817
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with the UI Graphic component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FD2 RID: 36818
		public CanvasUpdate canvasUpdate;

		// Token: 0x04008FD3 RID: 36819
		[Tooltip("Only Rebuild when state exits.")]
		public bool rebuildOnExit;

		// Token: 0x04008FD4 RID: 36820
		private Graphic graphic;
	}
}
