using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class UnityInput : IInput
{
	// Token: 0x0600316C RID: 12652 RVA: 0x000FDD80 File Offset: 0x000FBF80
	public bool GetMousePosition(out Vector3 position)
	{
		position = Input.mousePosition;
		return true;
	}

	// Token: 0x0600316D RID: 12653 RVA: 0x000FDD8E File Offset: 0x000FBF8E
	public bool GetAnyKey(out bool value)
	{
		value = Input.anyKey;
		return value;
	}

	// Token: 0x0600316E RID: 12654 RVA: 0x000FDD99 File Offset: 0x000FBF99
	public bool GetAnyKeyDown(out bool value)
	{
		value = Input.anyKeyDown;
		return value;
	}

	// Token: 0x0600316F RID: 12655 RVA: 0x000FDDA4 File Offset: 0x000FBFA4
	public bool GetKey(KeyCode keycode, out bool value)
	{
		value = Input.GetKey(keycode);
		return value;
	}

	// Token: 0x06003170 RID: 12656 RVA: 0x000FDDB0 File Offset: 0x000FBFB0
	public bool GetKeyDown(KeyCode keycode, out bool value)
	{
		value = Input.GetKeyDown(keycode);
		return value;
	}

	// Token: 0x06003171 RID: 12657 RVA: 0x000FDDBC File Offset: 0x000FBFBC
	public bool GetKeyUp(KeyCode keycode, out bool value)
	{
		value = Input.GetKeyUp(keycode);
		return value;
	}

	// Token: 0x06003172 RID: 12658 RVA: 0x000FDDC8 File Offset: 0x000FBFC8
	public bool GetMouseButton(int button, out bool value)
	{
		value = Input.GetMouseButton(button);
		return value;
	}

	// Token: 0x06003173 RID: 12659 RVA: 0x000FDDD4 File Offset: 0x000FBFD4
	public bool GetMouseButtonDown(int button, out bool value)
	{
		value = Input.GetMouseButtonDown(button);
		return value;
	}

	// Token: 0x06003174 RID: 12660 RVA: 0x000FDDE0 File Offset: 0x000FBFE0
	public bool GetMouseButtonUp(int button, out bool value)
	{
		value = Input.GetMouseButtonUp(button);
		return value;
	}
}
