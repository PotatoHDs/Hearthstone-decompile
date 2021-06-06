using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CBE RID: 3262
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Toolbar. NOTE: Arrays must be the same length as NumButtons or empty.")]
	public class GUILayoutToolbar : GUILayoutAction
	{
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600A0A3 RID: 41123 RVA: 0x00331CF4 File Offset: 0x0032FEF4
		public GUIContent[] Contents
		{
			get
			{
				if (this.contents == null)
				{
					this.SetButtonsContent();
				}
				return this.contents;
			}
		}

		// Token: 0x0600A0A4 RID: 41124 RVA: 0x00331D0C File Offset: 0x0032FF0C
		private void SetButtonsContent()
		{
			if (this.contents == null)
			{
				this.contents = new GUIContent[this.numButtons.Value];
			}
			for (int i = 0; i < this.numButtons.Value; i++)
			{
				this.contents[i] = new GUIContent();
			}
			for (int j = 0; j < this.imagesArray.Length; j++)
			{
				this.contents[j].image = this.imagesArray[j].Value;
			}
			for (int k = 0; k < this.textsArray.Length; k++)
			{
				this.contents[k].text = this.textsArray[k].Value;
			}
			for (int l = 0; l < this.tooltipsArray.Length; l++)
			{
				this.contents[l].tooltip = this.tooltipsArray[l].Value;
			}
		}

		// Token: 0x0600A0A5 RID: 41125 RVA: 0x00331DE4 File Offset: 0x0032FFE4
		public override void Reset()
		{
			base.Reset();
			this.numButtons = 0;
			this.selectedButton = null;
			this.buttonEventsArray = new FsmEvent[0];
			this.imagesArray = new FsmTexture[0];
			this.tooltipsArray = new FsmString[0];
			this.style = "Button";
			this.everyFrame = false;
		}

		// Token: 0x0600A0A6 RID: 41126 RVA: 0x00331E48 File Offset: 0x00330048
		public override void OnEnter()
		{
			string text = this.ErrorCheck();
			if (!string.IsNullOrEmpty(text))
			{
				base.LogError(text);
				base.Finish();
			}
		}

		// Token: 0x0600A0A7 RID: 41127 RVA: 0x00331E74 File Offset: 0x00330074
		public override void OnGUI()
		{
			if (this.everyFrame)
			{
				this.SetButtonsContent();
			}
			bool changed = GUI.changed;
			GUI.changed = false;
			this.selectedButton.Value = GUILayout.Toolbar(this.selectedButton.Value, this.Contents, this.style.Value, base.LayoutOptions);
			if (GUI.changed)
			{
				if (this.selectedButton.Value < this.buttonEventsArray.Length)
				{
					base.Fsm.Event(this.buttonEventsArray[this.selectedButton.Value]);
					GUIUtility.ExitGUI();
					return;
				}
			}
			else
			{
				GUI.changed = changed;
			}
		}

		// Token: 0x0600A0A8 RID: 41128 RVA: 0x00331F18 File Offset: 0x00330118
		public override string ErrorCheck()
		{
			string text = "";
			if (this.imagesArray.Length != 0 && this.imagesArray.Length != this.numButtons.Value)
			{
				text += "Images array doesn't match NumButtons.\n";
			}
			if (this.textsArray.Length != 0 && this.textsArray.Length != this.numButtons.Value)
			{
				text += "Texts array doesn't match NumButtons.\n";
			}
			if (this.tooltipsArray.Length != 0 && this.tooltipsArray.Length != this.numButtons.Value)
			{
				text += "Tooltips array doesn't match NumButtons.\n";
			}
			return text;
		}

		// Token: 0x04008630 RID: 34352
		[Tooltip("The number of buttons in the toolbar")]
		public FsmInt numButtons;

		// Token: 0x04008631 RID: 34353
		[Tooltip("Store the index of the selected button in an Integer Variable")]
		[UIHint(UIHint.Variable)]
		public FsmInt selectedButton;

		// Token: 0x04008632 RID: 34354
		[Tooltip("Event to send when each button is pressed.")]
		public FsmEvent[] buttonEventsArray;

		// Token: 0x04008633 RID: 34355
		[Tooltip("Image to use on each button.")]
		public FsmTexture[] imagesArray;

		// Token: 0x04008634 RID: 34356
		[Tooltip("Text to use on each button.")]
		public FsmString[] textsArray;

		// Token: 0x04008635 RID: 34357
		[Tooltip("Tooltip to use for each button.")]
		public FsmString[] tooltipsArray;

		// Token: 0x04008636 RID: 34358
		[Tooltip("A named GUIStyle to use for the toolbar buttons. Default is Button.")]
		public FsmString style;

		// Token: 0x04008637 RID: 34359
		[Tooltip("Update the content of the buttons every frame. Useful if the buttons are using variables that change.")]
		public bool everyFrame;

		// Token: 0x04008638 RID: 34360
		private GUIContent[] contents;
	}
}
