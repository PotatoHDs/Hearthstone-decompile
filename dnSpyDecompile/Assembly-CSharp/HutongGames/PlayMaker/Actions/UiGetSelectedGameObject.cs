using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E3A RID: 3642
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Returns the EventSystem's currently select GameObject.")]
	public class UiGetSelectedGameObject : FsmStateAction
	{
		// Token: 0x0600A7D8 RID: 42968 RVA: 0x0034E3B5 File Offset: 0x0034C5B5
		public override void Reset()
		{
			this.StoreGameObject = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7D9 RID: 42969 RVA: 0x0034E3C5 File Offset: 0x0034C5C5
		public override void OnEnter()
		{
			this.GetCurrentSelectedGameObject();
			this.lastGameObject = this.StoreGameObject.Value;
		}

		// Token: 0x0600A7DA RID: 42970 RVA: 0x0034E3E0 File Offset: 0x0034C5E0
		public override void OnUpdate()
		{
			this.GetCurrentSelectedGameObject();
			if (this.StoreGameObject.Value != this.lastGameObject && this.ObjectChangedEvent != null)
			{
				base.Fsm.Event(this.ObjectChangedEvent);
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7DB RID: 42971 RVA: 0x0034E432 File Offset: 0x0034C632
		private void GetCurrentSelectedGameObject()
		{
			this.StoreGameObject.Value = EventSystem.current.currentSelectedGameObject;
		}

		// Token: 0x04008E7E RID: 36478
		[UIHint(UIHint.Variable)]
		[Tooltip("The currently selected GameObject")]
		public FsmGameObject StoreGameObject;

		// Token: 0x04008E7F RID: 36479
		[UIHint(UIHint.Variable)]
		[Tooltip("Event when the selected GameObject changes")]
		public FsmEvent ObjectChangedEvent;

		// Token: 0x04008E80 RID: 36480
		[UIHint(UIHint.Variable)]
		[Tooltip("If true, each frame will check the currently selected GameObject")]
		public bool everyFrame;

		// Token: 0x04008E81 RID: 36481
		private GameObject lastGameObject;
	}
}
