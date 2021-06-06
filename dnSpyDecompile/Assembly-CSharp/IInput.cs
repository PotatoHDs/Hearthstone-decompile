using System;
using UnityEngine;

// Token: 0x0200031F RID: 799
public interface IInput
{
	// Token: 0x06002CEE RID: 11502
	bool GetMousePosition(out Vector3 position);

	// Token: 0x06002CEF RID: 11503
	bool GetAnyKey(out bool value);

	// Token: 0x06002CF0 RID: 11504
	bool GetAnyKeyDown(out bool value);

	// Token: 0x06002CF1 RID: 11505
	bool GetKey(KeyCode keycode, out bool value);

	// Token: 0x06002CF2 RID: 11506
	bool GetKeyDown(KeyCode keycode, out bool value);

	// Token: 0x06002CF3 RID: 11507
	bool GetKeyUp(KeyCode keycode, out bool value);

	// Token: 0x06002CF4 RID: 11508
	bool GetMouseButton(int button, out bool value);

	// Token: 0x06002CF5 RID: 11509
	bool GetMouseButtonDown(int button, out bool value);

	// Token: 0x06002CF6 RID: 11510
	bool GetMouseButtonUp(int button, out bool value);
}
