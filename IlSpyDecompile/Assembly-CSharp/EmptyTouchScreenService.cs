using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class EmptyTouchScreenService : ITouchScreenService, IService
{
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
	}

	public void ShowKeyboard()
	{
	}

	public void HideKeyboard()
	{
	}

	public void ShowOSK()
	{
	}

	public string GetIntelDeviceName()
	{
		return null;
	}

	public PowerSource GetBatteryMode()
	{
		return PowerSource.UndefinedPower;
	}

	public int GetPercentBatteryLife()
	{
		return -1;
	}

	public bool IsVirtualKeyboardVisible()
	{
		return false;
	}

	public bool GetTouch(int touchCount)
	{
		return false;
	}

	public bool GetTouchDown(int touchCount)
	{
		return false;
	}

	public bool GetTouchUp(int touchCount)
	{
		return false;
	}

	public Vector3 GetTouchPosition()
	{
		return new Vector3(0f, 0f, 0f);
	}

	public Vector2 GetTouchDelta()
	{
		return new Vector2(0f, 0f);
	}

	public Vector3 GetTouchPositionForGUI()
	{
		return new Vector3(0f, 0f, 0f);
	}

	public bool IsTouchSupported()
	{
		return false;
	}

	public void AddOnVirtualKeyboardShowListener(Action listener)
	{
	}

	public void RemoveOnVirtualKeyboardShowListener(Action listener)
	{
	}

	public void AddOnVirtualKeyboardHideListener(Action listener)
	{
	}

	public void RemoveOnVirtualKeyboardHideListener(Action listener)
	{
	}
}
