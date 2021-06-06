using System;
using Hearthstone.UI;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F9C RID: 3996
	[ActionCategory("Pegasus")]
	[Tooltip("Hides a widget on a game object.")]
	public class WidgetHideAction : FsmStateAction
	{
		// Token: 0x0600AE03 RID: 44547 RVA: 0x00362E8D File Offset: 0x0036108D
		public override void Reset()
		{
			this.widgetObject = null;
		}

		// Token: 0x0600AE04 RID: 44548 RVA: 0x00362E96 File Offset: 0x00361096
		public override void OnEnter()
		{
			this.Hide();
			base.Finish();
		}

		// Token: 0x0600AE05 RID: 44549 RVA: 0x00362EA4 File Offset: 0x003610A4
		private void Hide()
		{
			if (this.widgetObject == null || this.widgetObject.Value == null)
			{
				Debug.LogError("WidgetHideAction.Hide() - Widget Object is null.");
				return;
			}
			Widget component = this.widgetObject.Value.GetComponent<Widget>();
			if (component == null)
			{
				Debug.LogError(string.Format("WidgetHideAction.Hide() - Game Object {0} does not have a Widget component.", this.widgetObject));
				return;
			}
			component.Hide();
		}

		// Token: 0x040094D0 RID: 38096
		[RequiredField]
		[Tooltip("Game object containing the widget to hide.")]
		public FsmGameObject widgetObject;
	}
}
