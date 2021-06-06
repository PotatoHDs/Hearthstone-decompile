using Hearthstone.UI;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Shows a widget on a game object.")]
	public class WidgetShowAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Game object containing the widget to show.")]
		public FsmGameObject widgetObject;

		public override void Reset()
		{
			widgetObject = null;
		}

		public override void OnEnter()
		{
			Show();
			Finish();
		}

		private void Show()
		{
			if (widgetObject == null || widgetObject.Value == null)
			{
				Debug.LogError("WidgetShowAction.Show() - Widget Object is null.");
				return;
			}
			Widget component = widgetObject.Value.GetComponent<Widget>();
			if (component == null)
			{
				Debug.LogError($"WidgetShowAction.Show() - Game Object {widgetObject} does not have a Widget component.");
			}
			else
			{
				component.Show();
			}
		}
	}
}
