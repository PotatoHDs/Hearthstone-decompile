using System;
using Hearthstone.UI;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F9E RID: 3998
	[ActionCategory("Pegasus")]
	[Tooltip("Shows a widget on a game object.")]
	public class WidgetShowAction : FsmStateAction
	{
		// Token: 0x0600AE0B RID: 44555 RVA: 0x00363000 File Offset: 0x00361200
		public override void Reset()
		{
			this.widgetObject = null;
		}

		// Token: 0x0600AE0C RID: 44556 RVA: 0x00363009 File Offset: 0x00361209
		public override void OnEnter()
		{
			this.Show();
			base.Finish();
		}

		// Token: 0x0600AE0D RID: 44557 RVA: 0x00363018 File Offset: 0x00361218
		private void Show()
		{
			if (this.widgetObject == null || this.widgetObject.Value == null)
			{
				Debug.LogError("WidgetShowAction.Show() - Widget Object is null.");
				return;
			}
			Widget component = this.widgetObject.Value.GetComponent<Widget>();
			if (component == null)
			{
				Debug.LogError(string.Format("WidgetShowAction.Show() - Game Object {0} does not have a Widget component.", this.widgetObject));
				return;
			}
			component.Show();
		}

		// Token: 0x040094D3 RID: 38099
		[RequiredField]
		[Tooltip("Game object containing the widget to show.")]
		public FsmGameObject widgetObject;
	}
}
