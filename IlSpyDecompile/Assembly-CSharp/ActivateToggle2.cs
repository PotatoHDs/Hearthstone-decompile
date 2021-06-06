using UnityEngine;

public class ActivateToggle2 : MonoBehaviour
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

	public void ToggleActive2()
	{
		onoff = !onoff;
		if (obj != null)
		{
			obj.SetActive(onoff);
		}
	}

	public void ToggleOn2()
	{
		onoff = true;
		if (obj != null)
		{
			obj.SetActive(onoff);
		}
	}

	public void ToggleOff2()
	{
		onoff = false;
		if (obj != null)
		{
			obj.SetActive(onoff);
		}
	}
}
