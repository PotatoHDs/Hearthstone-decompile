using Hearthstone.UI;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Hides a widget on a game object.")]
	public class WidgetHideAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Game object containing the widget to hide.")]
		public FsmGameObject widgetObject;

		public override void Reset()
		{
			widgetObject = null;
		}

		public override void OnEnter()
		{
			Hide();
			Finish();
		}

		private void Hide()
		{
			if (widgetObject == null || widgetObject.Value == null)
			{
				Debug.LogError("WidgetHideAction.Hide() - Widget Object is null.");
				return;
			}
			Widget component = widgetObject.Value.GetComponent<Widget>();
			if (component == null)
			{
				Debug.LogError($"WidgetHideAction.Hide() - Game Object {widgetObject} does not have a Widget component.");
			}
			else
			{
				component.Hide();
			}
		}
	}
}
