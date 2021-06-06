using UnityEngine;

public class UnityInput : IInput
{
	public bool GetMousePosition(out Vector3 position)
	{
		position = Input.mousePosition;
		return true;
	}

	public bool GetAnyKey(out bool value)
	{
		value = Input.anyKey;
		return value;
	}

	public bool GetAnyKeyDown(out bool value)
	{
		value = Input.anyKeyDown;
		return value;
	}

	public bool GetKey(KeyCode keycode, out bool value)
	{
		value = Input.GetKey(keycode);
		return value;
	}

	public bool GetKeyDown(KeyCode keycode, out bool value)
	{
		value = Input.GetKeyDown(keycode);
		return value;
	}

	public bool GetKeyUp(KeyCode keycode, out bool value)
	{
		value = Input.GetKeyUp(keycode);
		return value;
	}

	public bool GetMouseButton(int button, out bool value)
	{
		value = Input.GetMouseButton(button);
		return value;
	}

	public bool GetMouseButtonDown(int button, out bool value)
	{
		value = Input.GetMouseButtonDown(button);
		return value;
	}

	public bool GetMouseButtonUp(int button, out bool value)
	{
		value = Input.GetMouseButtonUp(button);
		return value;
	}
}
