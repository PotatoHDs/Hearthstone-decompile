using System;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x020005E8 RID: 1512
public interface ITouchScreenService : IService
{
	// Token: 0x060052C3 RID: 21187
	void ShowKeyboard();

	// Token: 0x060052C4 RID: 21188
	void HideKeyboard();

	// Token: 0x060052C5 RID: 21189
	void ShowOSK();

	// Token: 0x060052C6 RID: 21190
	string GetIntelDeviceName();

	// Token: 0x060052C7 RID: 21191
	PowerSource GetBatteryMode();

	// Token: 0x060052C8 RID: 21192
	int GetPercentBatteryLife();

	// Token: 0x060052C9 RID: 21193
	bool IsVirtualKeyboardVisible();

	// Token: 0x060052CA RID: 21194
	bool GetTouch(int touchCount);

	// Token: 0x060052CB RID: 21195
	bool GetTouchDown(int touchCount);

	// Token: 0x060052CC RID: 21196
	bool GetTouchUp(int touchCount);

	// Token: 0x060052CD RID: 21197
	Vector3 GetTouchPosition();

	// Token: 0x060052CE RID: 21198
	Vector2 GetTouchDelta();

	// Token: 0x060052CF RID: 21199
	Vector3 GetTouchPositionForGUI();

	// Token: 0x060052D0 RID: 21200
	bool IsTouchSupported();

	// Token: 0x060052D1 RID: 21201
	void AddOnVirtualKeyboardShowListener(Action listener);

	// Token: 0x060052D2 RID: 21202
	void RemoveOnVirtualKeyboardShowListener(Action listener);

	// Token: 0x060052D3 RID: 21203
	void AddOnVirtualKeyboardHideListener(Action listener);

	// Token: 0x060052D4 RID: 21204
	void RemoveOnVirtualKeyboardHideListener(Action listener);
}
