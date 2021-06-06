using System;
using Blizzard.T5.Services;
using UnityEngine;

public interface ITouchScreenService : IService
{
	void ShowKeyboard();

	void HideKeyboard();

	void ShowOSK();

	string GetIntelDeviceName();

	PowerSource GetBatteryMode();

	int GetPercentBatteryLife();

	bool IsVirtualKeyboardVisible();

	bool GetTouch(int touchCount);

	bool GetTouchDown(int touchCount);

	bool GetTouchUp(int touchCount);

	Vector3 GetTouchPosition();

	Vector2 GetTouchDelta();

	Vector3 GetTouchPositionForGUI();

	bool IsTouchSupported();

	void AddOnVirtualKeyboardShowListener(Action listener);

	void RemoveOnVirtualKeyboardShowListener(Action listener);

	void AddOnVirtualKeyboardHideListener(Action listener);

	void RemoveOnVirtualKeyboardHideListener(Action listener);
}
