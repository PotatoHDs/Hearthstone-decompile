using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000320 RID: 800
public static class InputCollection
{
	// Token: 0x06002CF7 RID: 11511 RVA: 0x000E23C5 File Offset: 0x000E05C5
	static InputCollection()
	{
		InputCollection.m_Inputs.Add(new UnityInput());
	}

	// Token: 0x06002CF8 RID: 11512 RVA: 0x000E23E0 File Offset: 0x000E05E0
	public static Vector3 GetMousePosition()
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			Vector3 result;
			if (InputCollection.m_Inputs[i].GetMousePosition(out result))
			{
				return result;
			}
		}
		return Vector3.zero;
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x000E2420 File Offset: 0x000E0620
	public static bool GetAnyKey()
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetAnyKey(out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x000E245C File Offset: 0x000E065C
	public static bool GetAnyKeyDown()
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetAnyKeyDown(out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x06002CFB RID: 11515 RVA: 0x000E2498 File Offset: 0x000E0698
	public static bool GetKey(KeyCode keycode)
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetKey(keycode, out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x06002CFC RID: 11516 RVA: 0x000E24D4 File Offset: 0x000E06D4
	public static bool GetKeyDown(KeyCode keycode)
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetKeyDown(keycode, out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x06002CFD RID: 11517 RVA: 0x000E2510 File Offset: 0x000E0710
	public static bool GetKeyUp(KeyCode keycode)
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetKeyUp(keycode, out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x06002CFE RID: 11518 RVA: 0x000E254C File Offset: 0x000E074C
	public static bool GetMouseButton(int button)
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetMouseButton(button, out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x06002CFF RID: 11519 RVA: 0x000E2588 File Offset: 0x000E0788
	public static bool GetMouseButtonDown(int button)
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetMouseButtonDown(button, out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x06002D00 RID: 11520 RVA: 0x000E25C4 File Offset: 0x000E07C4
	public static bool GetMouseButtonUp(int button)
	{
		for (int i = 0; i < InputCollection.m_Inputs.Count; i++)
		{
			bool result;
			if (InputCollection.m_Inputs[i].GetMouseButtonUp(button, out result))
			{
				return result;
			}
		}
		return false;
	}

	// Token: 0x040018D5 RID: 6357
	private static List<IInput> m_Inputs = new List<IInput>();
}
