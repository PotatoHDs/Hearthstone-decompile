using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E5D RID: 3677
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Add multiple options to the options of the Dropdown UI Component")]
	public class UiDropDownAddOptions : ComponentAction<Dropdown>
	{
		// Token: 0x0600A875 RID: 43125 RVA: 0x0035018F File Offset: 0x0034E38F
		public override void Reset()
		{
			this.gameObject = null;
			this.optionText = new FsmString[1];
			this.optionImage = new FsmObject[1];
		}

		// Token: 0x0600A876 RID: 43126 RVA: 0x003501B0 File Offset: 0x0034E3B0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.dropDown = this.cachedComponent;
			}
			this.DoAddOptions();
			base.Finish();
		}

		// Token: 0x0600A877 RID: 43127 RVA: 0x003501F0 File Offset: 0x0034E3F0
		private void DoAddOptions()
		{
			if (this.dropDown == null)
			{
				return;
			}
			this.options = new List<Dropdown.OptionData>();
			for (int i = 0; i < this.optionText.Length; i++)
			{
				FsmString fsmString = this.optionText[i];
				this.options.Add(new Dropdown.OptionData
				{
					text = fsmString.Value,
					image = (this.optionImage[i].RawValue as Sprite)
				});
			}
			this.dropDown.AddOptions(this.options);
		}

		// Token: 0x04008F11 RID: 36625
		[RequiredField]
		[CheckForComponent(typeof(Dropdown))]
		[Tooltip("The GameObject with the UI DropDown component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F12 RID: 36626
		[Tooltip("The Options.")]
		[CompoundArray("Options", "Text", "Image")]
		public FsmString[] optionText;

		// Token: 0x04008F13 RID: 36627
		[ObjectType(typeof(Sprite))]
		public FsmObject[] optionImage;

		// Token: 0x04008F14 RID: 36628
		private Dropdown dropDown;

		// Token: 0x04008F15 RID: 36629
		private List<Dropdown.OptionData> options;
	}
}
