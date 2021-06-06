using UnityEngine;

public class ActivateToggle : MonoBehaviour
{
	public GameObject obj;

	private bool onoff;

	private void Start()
	{
		if (obj != null)
		{
			onoff = obj.activeSelf;
		}
	}

	public void ToggleActive()
	{
		onoff = !onoff;
		if (obj != null)
		{
			obj.SetActive(onoff);
		}
	}

	public void ToggleOn()
	{
		onoff = true;
		if (obj != null)
		{
			obj.SetActive(onoff);
		}
	}

	public void ToggleOff()
	{
		onoff = false;
		if (obj != null)
		{
			obj.SetActive(onoff);
		}
	}
}
