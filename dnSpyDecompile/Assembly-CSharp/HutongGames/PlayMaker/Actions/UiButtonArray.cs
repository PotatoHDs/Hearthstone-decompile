using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E5B RID: 3675
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set up multiple button events in a single action.")]
	public class UiButtonArray : FsmStateAction
	{
		// Token: 0x0600A869 RID: 43113 RVA: 0x0034FEA0 File Offset: 0x0034E0A0
		public override void Reset()
		{
			this.gameObjects = new FsmGameObject[3];
			this.clickEvents = new FsmEvent[3];
		}

		// Token: 0x0600A86A RID: 43114 RVA: 0x0034FEBC File Offset: 0x0034E0BC
		public override void OnPreprocess()
		{
			if (this.gameObjects == null)
			{
				return;
			}
			this.buttons = new Button[this.gameObjects.Length];
			this.cachedGameObjects = new GameObject[this.gameObjects.Length];
			this.actions = new UnityAction[this.gameObjects.Length];
			this.InitButtons();
		}

		// Token: 0x0600A86B RID: 43115 RVA: 0x0034FF14 File Offset: 0x0034E114
		private void InitButtons()
		{
			if (this.gameObjects == null)
			{
				return;
			}
			if (this.cachedGameObjects == null || this.cachedGameObjects.Length != this.gameObjects.Length)
			{
				this.OnPreprocess();
			}
			for (int i = 0; i < this.gameObjects.Length; i++)
			{
				GameObject value = this.gameObjects[i].Value;
				if (value != null && this.cachedGameObjects[i] != value)
				{
					this.buttons[i] = value.GetComponent<Button>();
					this.cachedGameObjects[i] = value;
				}
			}
		}

		// Token: 0x0600A86C RID: 43116 RVA: 0x0034FF9C File Offset: 0x0034E19C
		public override void OnEnter()
		{
			this.InitButtons();
			for (int i = 0; i < this.buttons.Length; i++)
			{
				Button button = this.buttons[i];
				if (!(button == null))
				{
					int index = i;
					this.actions[i] = delegate()
					{
						this.OnClick(index);
					};
					button.onClick.AddListener(this.actions[i]);
				}
			}
		}

		// Token: 0x0600A86D RID: 43117 RVA: 0x00350010 File Offset: 0x0034E210
		public override void OnExit()
		{
			for (int i = 0; i < this.gameObjects.Length; i++)
			{
				FsmGameObject fsmGameObject = this.gameObjects[i];
				if (!(fsmGameObject.Value == null))
				{
					fsmGameObject.Value.GetComponent<Button>().onClick.RemoveListener(this.actions[i]);
				}
			}
		}

		// Token: 0x0600A86E RID: 43118 RVA: 0x00350064 File Offset: 0x0034E264
		public void OnClick(int index)
		{
			base.Fsm.Event(this.gameObjects[index].Value, this.eventTarget, this.clickEvents[index]);
		}

		// Token: 0x04008F06 RID: 36614
		[Tooltip("Where to send the events.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008F07 RID: 36615
		[CompoundArray("Buttons", "Button", "Click Event")]
		[CheckForComponent(typeof(Button))]
		[Tooltip("The GameObject with the UI button component.")]
		public FsmGameObject[] gameObjects;

		// Token: 0x04008F08 RID: 36616
		[Tooltip("Send this event when the button is Clicked.")]
		public FsmEvent[] clickEvents;

		// Token: 0x04008F09 RID: 36617
		[SerializeField]
		private Button[] buttons;

		// Token: 0x04008F0A RID: 36618
		[SerializeField]
		private GameObject[] cachedGameObjects;

		// Token: 0x04008F0B RID: 36619
		private UnityAction[] actions;

		// Token: 0x04008F0C RID: 36620
		private int clickedButton;
	}
}
