using Hearthstone.UI;
using UnityEngine;

public class MulliganButton : MonoBehaviour
{
	public UberText uberText;

	public GameObject buttonContainer;

	public void SetText(string text)
	{
		uberText.Text = text;
		uberText.UpdateText();
	}

	public void SetEnabled(bool active)
	{
		VisualController component = buttonContainer.GetComponent<VisualController>();
		if (active)
		{
			component.SetState("Active");
		}
		else
		{
			component.SetState("Inactive");
		}
	}

	public virtual bool AddEventListener(UIEventType type, UIEvent.Handler handler)
	{
		return buttonContainer.GetComponent<Clickable>().AddEventListener(type, handler);
	}
}
