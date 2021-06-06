using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x020005E6 RID: 1510
public class EmptyTouchScreenService : ITouchScreenService, IService
{
	// Token: 0x060052AD RID: 21165 RVA: 0x001B1A76 File Offset: 0x001AFC76
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x060052AE RID: 21166 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x060052AF RID: 21167 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060052B0 RID: 21168 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void ShowKeyboard()
	{
	}

	// Token: 0x060052B1 RID: 21169 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void HideKeyboard()
	{
	}

	// Token: 0x060052B2 RID: 21170 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void ShowOSK()
	{
	}

	// Token: 0x060052B3 RID: 21171 RVA: 0x00090064 File Offset: 0x0008E264
	public string GetIntelDeviceName()
	{
		return null;
	}

	// Token: 0x060052B4 RID: 21172 RVA: 0x001B1A7E File Offset: 0x001AFC7E
	public PowerSource GetBatteryMode()
	{
		return PowerSource.UndefinedPower;
	}

	// Token: 0x060052B5 RID: 21173 RVA: 0x001B1A85 File Offset: 0x001AFC85
	public int GetPercentBatteryLife()
	{
		return -1;
	}

	// Token: 0x060052B6 RID: 21174 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool IsVirtualKeyboardVisible()
	{
		return false;
	}

	// Token: 0x060052B7 RID: 21175 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool GetTouch(int touchCount)
	{
		return false;
	}

	// Token: 0x060052B8 RID: 21176 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool GetTouchDown(int touchCount)
	{
		return false;
	}

	// Token: 0x060052B9 RID: 21177 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool GetTouchUp(int touchCount)
	{
		return false;
	}

	// Token: 0x060052BA RID: 21178 RVA: 0x001B1A88 File Offset: 0x001AFC88
	public Vector3 GetTouchPosition()
	{
		return new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060052BB RID: 21179 RVA: 0x001B1A9E File Offset: 0x001AFC9E
	public Vector2 GetTouchDelta()
	{
		return new Vector2(0f, 0f);
	}

	// Token: 0x060052BC RID: 21180 RVA: 0x001B1A88 File Offset: 0x001AFC88
	public Vector3 GetTouchPositionForGUI()
	{
		return new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060052BD RID: 21181 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool IsTouchSupported()
	{
		return false;
	}

	// Token: 0x060052BE RID: 21182 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void AddOnVirtualKeyboardShowListener(Action listener)
	{
	}

	// Token: 0x060052BF RID: 21183 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void RemoveOnVirtualKeyboardShowListener(Action listener)
	{
	}

	// Token: 0x060052C0 RID: 21184 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void AddOnVirtualKeyboardHideListener(Action listener)
	{
	}

	// Token: 0x060052C1 RID: 21185 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void RemoveOnVirtualKeyboardHideListener(Action listener)
	{
	}
}
