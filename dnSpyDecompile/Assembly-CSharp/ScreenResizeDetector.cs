using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000A85 RID: 2693
public class ScreenResizeDetector : MonoBehaviour
{
	// Token: 0x06009050 RID: 36944 RVA: 0x002ED963 File Offset: 0x002EBB63
	private void Awake()
	{
		this.SaveScreenSize();
		this.UpdateDeviceDataModel();
	}

	// Token: 0x06009051 RID: 36945 RVA: 0x002ED974 File Offset: 0x002EBB74
	private void OnPreCull()
	{
		float b = (float)Screen.width;
		float b2 = (float)Screen.height;
		if (Mathf.Approximately(this.m_screenWidth, b) && Mathf.Approximately(this.m_screenHeight, b2))
		{
			return;
		}
		this.SaveScreenSize();
		this.UpdateDeviceDataModel();
		this.FireSizeChangedEvent();
	}

	// Token: 0x06009052 RID: 36946 RVA: 0x002ED9BE File Offset: 0x002EBBBE
	public bool AddSizeChangedListener(ScreenResizeDetector.SizeChangedCallback callback)
	{
		return this.AddSizeChangedListener(callback, null);
	}

	// Token: 0x06009053 RID: 36947 RVA: 0x002ED9C8 File Offset: 0x002EBBC8
	public bool AddSizeChangedListener(ScreenResizeDetector.SizeChangedCallback callback, object userData)
	{
		ScreenResizeDetector.SizeChangedListener sizeChangedListener = new ScreenResizeDetector.SizeChangedListener();
		sizeChangedListener.SetCallback(callback);
		sizeChangedListener.SetUserData(userData);
		if (this.m_sizeChangedListeners.Contains(sizeChangedListener))
		{
			return false;
		}
		this.m_sizeChangedListeners.Add(sizeChangedListener);
		return true;
	}

	// Token: 0x06009054 RID: 36948 RVA: 0x002EDA06 File Offset: 0x002EBC06
	public bool RemoveSizeChangedListener(ScreenResizeDetector.SizeChangedCallback callback)
	{
		return this.RemoveSizeChangedListener(callback, null);
	}

	// Token: 0x06009055 RID: 36949 RVA: 0x002EDA10 File Offset: 0x002EBC10
	public bool RemoveSizeChangedListener(ScreenResizeDetector.SizeChangedCallback callback, object userData)
	{
		ScreenResizeDetector.SizeChangedListener sizeChangedListener = new ScreenResizeDetector.SizeChangedListener();
		sizeChangedListener.SetCallback(callback);
		sizeChangedListener.SetUserData(userData);
		return this.m_sizeChangedListeners.Remove(sizeChangedListener);
	}

	// Token: 0x06009056 RID: 36950 RVA: 0x002EDA3D File Offset: 0x002EBC3D
	private void SaveScreenSize()
	{
		this.m_screenWidth = (float)Screen.width;
		this.m_screenHeight = (float)Screen.height;
	}

	// Token: 0x06009057 RID: 36951 RVA: 0x002EDA58 File Offset: 0x002EBC58
	private void FireSizeChangedEvent()
	{
		ScreenResizeDetector.SizeChangedListener[] array = this.m_sizeChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x06009058 RID: 36952 RVA: 0x002EDA88 File Offset: 0x002EBC88
	private void UpdateDeviceDataModel()
	{
		IDataModel dataModel;
		if (GlobalDataContext.Get().GetDataModel(0, out dataModel))
		{
			((DeviceDataModel)dataModel).AspectRatio = this.GetNextBestAspectRatio();
		}
	}

	// Token: 0x06009059 RID: 36953 RVA: 0x002EDAB8 File Offset: 0x002EBCB8
	private AspectRatio GetNextBestAspectRatio()
	{
		float value;
		if (PlatformSettings.Screen == ScreenCategory.Phone)
		{
			value = Screen.safeArea.width / Screen.safeArea.height;
		}
		else
		{
			value = this.m_screenWidth / this.m_screenHeight;
		}
		if (this.NarrowerThanTargetRatio(value, 1f))
		{
			return AspectRatio.Unknown;
		}
		if (this.NarrowerThanTargetRatio(value, 1.25f))
		{
			return AspectRatio._1x1;
		}
		if (this.NarrowerThanTargetRatio(value, 1.3333334f))
		{
			return AspectRatio._5x4;
		}
		if (this.NarrowerThanTargetRatio(value, 1.5f))
		{
			return AspectRatio._4x3;
		}
		if (this.NarrowerThanTargetRatio(value, 1.6f))
		{
			return AspectRatio._3x2;
		}
		if (this.NarrowerThanTargetRatio(value, 1.7777778f))
		{
			return AspectRatio._16x10;
		}
		if (this.NarrowerThanTargetRatio(value, 2.3333333f))
		{
			return AspectRatio._16x9;
		}
		if (this.NarrowerThanTargetRatio(value, 2.3703704f))
		{
			return AspectRatio._21x9;
		}
		return AspectRatio.ExtraWide;
	}

	// Token: 0x0600905A RID: 36954 RVA: 0x002EDB80 File Offset: 0x002EBD80
	private bool NarrowerThanTargetRatio(float value, float target)
	{
		return value < target - 0.005f;
	}

	// Token: 0x04007932 RID: 31026
	private const float _1x1 = 1f;

	// Token: 0x04007933 RID: 31027
	private const float _5x4 = 1.25f;

	// Token: 0x04007934 RID: 31028
	private const float _4x3 = 1.3333334f;

	// Token: 0x04007935 RID: 31029
	private const float _3x2 = 1.5f;

	// Token: 0x04007936 RID: 31030
	private const float _16x10 = 1.6f;

	// Token: 0x04007937 RID: 31031
	private const float _16x9 = 1.7777778f;

	// Token: 0x04007938 RID: 31032
	private const float _21x9 = 2.3333333f;

	// Token: 0x04007939 RID: 31033
	private const float ExtraWide = 2.3703704f;

	// Token: 0x0400793A RID: 31034
	private const float AspectRatioTolerance = 0.005f;

	// Token: 0x0400793B RID: 31035
	private float m_screenWidth;

	// Token: 0x0400793C RID: 31036
	private float m_screenHeight;

	// Token: 0x0400793D RID: 31037
	private List<ScreenResizeDetector.SizeChangedListener> m_sizeChangedListeners = new List<ScreenResizeDetector.SizeChangedListener>();

	// Token: 0x020026D7 RID: 9943
	// (Invoke) Token: 0x0601386D RID: 79981
	public delegate void SizeChangedCallback(object userData);

	// Token: 0x020026D8 RID: 9944
	private class SizeChangedListener : EventListener<ScreenResizeDetector.SizeChangedCallback>
	{
		// Token: 0x06013870 RID: 79984 RVA: 0x0053708E File Offset: 0x0053528E
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
