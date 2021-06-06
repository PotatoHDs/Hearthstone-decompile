using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD3 RID: 3539
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Text used by the GUIText Component attached to a Game Object.")]
	[Obsolete("GUIText is part of the legacy UI system and will be removed in a future release")]
	public class SetGUIText : ComponentAction<GUIText>
	{
		// Token: 0x0600A603 RID: 42499 RVA: 0x00348514 File Offset: 0x00346714
		public override void Reset()
		{
			this.gameObject = null;
			this.text = "";
		}

		// Token: 0x0600A604 RID: 42500 RVA: 0x0034852D File Offset: 0x0034672D
		public override void OnEnter()
		{
			this.DoSetGUIText();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A605 RID: 42501 RVA: 0x00348543 File Offset: 0x00346743
		public override void OnUpdate()
		{
			this.DoSetGUIText();
		}

		// Token: 0x0600A606 RID: 42502 RVA: 0x0034854C File Offset: 0x0034674C
		private void DoSetGUIText()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.guiText.text = this.text.Value;
			}
		}

		// Token: 0x04008CA8 RID: 36008
		[RequiredField]
		[CheckForComponent(typeof(GUIText))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CA9 RID: 36009
		[UIHint(UIHint.TextArea)]
		public FsmString text;

		// Token: 0x04008CAA RID: 36010
		public bool everyFrame;
	}
}
