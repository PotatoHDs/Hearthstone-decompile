using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C3C RID: 3132
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if the value of a GameObject variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class GameObjectChanged : FsmStateAction
	{
		// Token: 0x06009E91 RID: 40593 RVA: 0x0032BD27 File Offset: 0x00329F27
		public override void Reset()
		{
			this.gameObjectVariable = null;
			this.changedEvent = null;
			this.storeResult = null;
		}

		// Token: 0x06009E92 RID: 40594 RVA: 0x0032BD3E File Offset: 0x00329F3E
		public override void OnEnter()
		{
			if (this.gameObjectVariable.IsNone)
			{
				base.Finish();
				return;
			}
			this.previousValue = this.gameObjectVariable.Value;
		}

		// Token: 0x06009E93 RID: 40595 RVA: 0x0032BD68 File Offset: 0x00329F68
		public override void OnUpdate()
		{
			this.storeResult.Value = false;
			if (this.gameObjectVariable.Value != this.previousValue)
			{
				this.storeResult.Value = true;
				base.Fsm.Event(this.changedEvent);
			}
		}

		// Token: 0x040083EB RID: 33771
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject variable to watch for a change.")]
		public FsmGameObject gameObjectVariable;

		// Token: 0x040083EC RID: 33772
		[Tooltip("Event to send if the variable changes.")]
		public FsmEvent changedEvent;

		// Token: 0x040083ED RID: 33773
		[UIHint(UIHint.Variable)]
		[Tooltip("Set to True if the variable changes.")]
		public FsmBool storeResult;

		// Token: 0x040083EE RID: 33774
		private GameObject previousValue;
	}
}
