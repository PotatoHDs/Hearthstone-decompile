using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set up multiple button events in a single action.")]
	public class UiButtonArray : FsmStateAction
	{
		[Tooltip("Where to send the events.")]
		public FsmEventTarget eventTarget;

		[CompoundArray("Buttons", "Button", "Click Event")]
		[CheckForComponent(typeof(Button))]
		[Tooltip("The GameObject with the UI button component.")]
		public FsmGameObject[] gameObjects;

		[Tooltip("Send this event when the button is Clicked.")]
		public FsmEvent[] clickEvents;

		[SerializeField]
		private Button[] buttons;

		[SerializeField]
		private GameObject[] cachedGameObjects;

		private UnityAction[] actions;

		private int clickedButton;

		public override void Reset()
		{
			gameObjects = new FsmGameObject[3];
			clickEvents = new FsmEvent[3];
		}

		public override void OnPreprocess()
		{
			if (gameObjects != null)
			{
				buttons = new Button[gameObjects.Length];
				cachedGameObjects = new GameObject[gameObjects.Length];
				actions = new UnityAction[gameObjects.Length];
				InitButtons();
			}
		}

		private void InitButtons()
		{
			if (gameObjects == null)
			{
				return;
			}
			if (cachedGameObjects == null || cachedGameObjects.Length != gameObjects.Length)
			{
				OnPreprocess();
			}
			for (int i = 0; i < gameObjects.Length; i++)
			{
				GameObject value = gameObjects[i].Value;
				if (value != null && cachedGameObjects[i] != value)
				{
					buttons[i] = value.GetComponent<Button>();
					cachedGameObjects[i] = value;
				}
			}
		}

		public override void OnEnter()
		{
			InitButtons();
			for (int i = 0; i < buttons.Length; i++)
			{
				Button button = buttons[i];
				if (!(button == null))
				{
					int index = i;
					actions[i] = delegate
					{
						OnClick(index);
					};
					button.onClick.AddListener(actions[i]);
				}
			}
		}

		public override void OnExit()
		{
			for (int i = 0; i < gameObjects.Length; i++)
			{
				FsmGameObject fsmGameObject = gameObjects[i];
				if (!(fsmGameObject.Value == null))
				{
					fsmGameObject.Value.GetComponent<Button>().onClick.RemoveListener(actions[i]);
				}
			}
		}

		public void OnClick(int index)
		{
			base.Fsm.Event(gameObjects[index].Value, eventTarget, clickEvents[index]);
		}
	}
}
