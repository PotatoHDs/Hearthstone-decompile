using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BBE RID: 3006
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Iterate through the items in an Array and run an FSM on each item. NOTE: The FSM has to Finish before being run on the next item.")]
	public class ArrayForEach : RunFSMAction
	{
		// Token: 0x06009C60 RID: 40032 RVA: 0x0032518C File Offset: 0x0032338C
		public override void Reset()
		{
			this.array = null;
			this.fsmTemplateControl = new FsmTemplateControl();
			this.runFsm = null;
		}

		// Token: 0x06009C61 RID: 40033 RVA: 0x003251A7 File Offset: 0x003233A7
		public override void Awake()
		{
			if (this.array != null && this.fsmTemplateControl.fsmTemplate != null && Application.isPlaying)
			{
				this.runFsm = base.Fsm.CreateSubFsm(this.fsmTemplateControl);
			}
		}

		// Token: 0x06009C62 RID: 40034 RVA: 0x003251E2 File Offset: 0x003233E2
		public override void OnEnter()
		{
			if (this.array == null || this.runFsm == null)
			{
				base.Finish();
				return;
			}
			this.currentIndex = 0;
			this.StartFsm();
		}

		// Token: 0x06009C63 RID: 40035 RVA: 0x00325208 File Offset: 0x00323408
		public override void OnUpdate()
		{
			this.runFsm.Update();
			if (!this.runFsm.Finished)
			{
				return;
			}
			this.StartNextFsm();
		}

		// Token: 0x06009C64 RID: 40036 RVA: 0x00325229 File Offset: 0x00323429
		public override void OnFixedUpdate()
		{
			this.runFsm.FixedUpdate();
			if (!this.runFsm.Finished)
			{
				return;
			}
			this.StartNextFsm();
		}

		// Token: 0x06009C65 RID: 40037 RVA: 0x0032524A File Offset: 0x0032344A
		public override void OnLateUpdate()
		{
			this.runFsm.LateUpdate();
			if (!this.runFsm.Finished)
			{
				return;
			}
			this.StartNextFsm();
		}

		// Token: 0x06009C66 RID: 40038 RVA: 0x0032526B File Offset: 0x0032346B
		private void StartNextFsm()
		{
			this.currentIndex++;
			this.StartFsm();
		}

		// Token: 0x06009C67 RID: 40039 RVA: 0x00325284 File Offset: 0x00323484
		private void StartFsm()
		{
			while (this.currentIndex < this.array.Length)
			{
				this.DoStartFsm();
				if (!this.runFsm.Finished)
				{
					return;
				}
				this.currentIndex++;
			}
			base.Fsm.Event(this.finishEvent);
			base.Finish();
		}

		// Token: 0x06009C68 RID: 40040 RVA: 0x003252E0 File Offset: 0x003234E0
		private void DoStartFsm()
		{
			this.storeItem.SetValue(this.array.Values[this.currentIndex]);
			this.fsmTemplateControl.UpdateValues();
			this.fsmTemplateControl.ApplyOverrides(this.runFsm);
			this.runFsm.OnEnable();
			if (!this.runFsm.Started)
			{
				this.runFsm.Start();
			}
		}

		// Token: 0x06009C69 RID: 40041 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override void CheckIfFinished()
		{
		}

		// Token: 0x040081D9 RID: 33241
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Array to iterate through.")]
		public FsmArray array;

		// Token: 0x040081DA RID: 33242
		[HideTypeFilter]
		[MatchElementType("array")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the item in a variable")]
		public FsmVar storeItem;

		// Token: 0x040081DB RID: 33243
		[ActionSection("Run FSM")]
		public FsmTemplateControl fsmTemplateControl = new FsmTemplateControl();

		// Token: 0x040081DC RID: 33244
		[Tooltip("Event to send after iterating through all items in the Array.")]
		public FsmEvent finishEvent;

		// Token: 0x040081DD RID: 33245
		private int currentIndex;
	}
}
