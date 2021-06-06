using UnityEngine;

public interface IInput
{
	bool GetMousePosition(out Vector3 position);

	bool GetAnyKey(out bool value);

	bool GetAnyKeyDown(out bool value);

	bool GetKey(KeyCode keycode, out bool value);

	bool GetKeyDown(KeyCode keycode, out bool value);

	bool GetKeyUp(KeyCode keycode, out bool value);

	bool GetMouseButton(int button, out bool value);

	bool GetMouseButtonDown(int button, out bool value);

	bool GetMouseButtonUp(int button, out bool value);
}
