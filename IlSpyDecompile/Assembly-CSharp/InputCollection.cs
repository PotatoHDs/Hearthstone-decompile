using System.Collections.Generic;
using UnityEngine;

public static class InputCollection
{
	private static List<IInput> m_Inputs;

	static InputCollection()
	{
		m_Inputs = new List<IInput>();
		m_Inputs.Add(new UnityInput());
	}

	public static Vector3 GetMousePosition()
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetMousePosition(out var position))
			{
				return position;
			}
		}
		return Vector3.zero;
	}

	public static bool GetAnyKey()
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetAnyKey(out var value))
			{
				return value;
			}
		}
		return false;
	}

	public static bool GetAnyKeyDown()
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetAnyKeyDown(out var value))
			{
				return value;
			}
		}
		return false;
	}

	public static bool GetKey(KeyCode keycode)
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetKey(keycode, out var value))
			{
				return value;
			}
		}
		return false;
	}

	public static bool GetKeyDown(KeyCode keycode)
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetKeyDown(keycode, out var value))
			{
				return value;
			}
		}
		return false;
	}

	public static bool GetKeyUp(KeyCode keycode)
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetKeyUp(keycode, out var value))
			{
				return value;
			}
		}
		return false;
	}

	public static bool GetMouseButton(int button)
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetMouseButton(button, out var value))
			{
				return value;
			}
		}
		return false;
	}

	public static bool GetMouseButtonDown(int button)
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetMouseButtonDown(button, out var value))
			{
				return value;
			}
		}
		return false;
	}

	public static bool GetMouseButtonUp(int button)
	{
		for (int i = 0; i < m_Inputs.Count; i++)
		{
			if (m_Inputs[i].GetMouseButtonUp(button, out var value))
			{
				return value;
			}
		}
		return false;
	}
}
