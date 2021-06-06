using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ScreenResizeDetector : MonoBehaviour
{
	public delegate void SizeChangedCallback(object userData);

	private class SizeChangedListener : EventListener<SizeChangedCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	private const float _1x1 = 1f;

	private const float _5x4 = 1.25f;

	private const float _4x3 = 1.33333337f;

	private const float _3x2 = 1.5f;

	private const float _16x10 = 1.6f;

	private const float _16x9 = 1.77777779f;

	private const float _21x9 = 2.33333325f;

	private const float ExtraWide = 2.37037039f;

	private const float AspectRatioTolerance = 0.005f;

	private float m_screenWidth;

	private float m_screenHeight;

	private List<SizeChangedListener> m_sizeChangedListeners = new List<SizeChangedListener>();

	private void Awake()
	{
		SaveScreenSize();
		UpdateDeviceDataModel();
	}

	private void OnPreCull()
	{
		float b = Screen.width;
		float b2 = Screen.height;
		if (!Mathf.Approximately(m_screenWidth, b) || !Mathf.Approximately(m_screenHeight, b2))
		{
			SaveScreenSize();
			UpdateDeviceDataModel();
			FireSizeChangedEvent();
		}
	}

	public bool AddSizeChangedListener(SizeChangedCallback callback)
	{
		return AddSizeChangedListener(callback, null);
	}

	public bool AddSizeChangedListener(SizeChangedCallback callback, object userData)
	{
		SizeChangedListener sizeChangedListener = new SizeChangedListener();
		sizeChangedListener.SetCallback(callback);
		sizeChangedListener.SetUserData(userData);
		if (m_sizeChangedListeners.Contains(sizeChangedListener))
		{
			return false;
		}
		m_sizeChangedListeners.Add(sizeChangedListener);
		return true;
	}

	public bool RemoveSizeChangedListener(SizeChangedCallback callback)
	{
		return RemoveSizeChangedListener(callback, null);
	}

	public bool RemoveSizeChangedListener(SizeChangedCallback callback, object userData)
	{
		SizeChangedListener sizeChangedListener = new SizeChangedListener();
		sizeChangedListener.SetCallback(callback);
		sizeChangedListener.SetUserData(userData);
		return m_sizeChangedListeners.Remove(sizeChangedListener);
	}

	private void SaveScreenSize()
	{
		m_screenWidth = Screen.width;
		m_screenHeight = Screen.height;
	}

	private void FireSizeChangedEvent()
	{
		SizeChangedListener[] array = m_sizeChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void UpdateDeviceDataModel()
	{
		if (GlobalDataContext.Get().GetDataModel(0, out var model))
		{
			((DeviceDataModel)model).AspectRatio = GetNextBestAspectRatio();
		}
	}

	private AspectRatio GetNextBestAspectRatio()
	{
		float num = 1f;
		num = ((PlatformSettings.Screen != ScreenCategory.Phone) ? (m_screenWidth / m_screenHeight) : (Screen.safeArea.width / Screen.safeArea.height));
		if (NarrowerThanTargetRatio(num, 1f))
		{
			return AspectRatio.Unknown;
		}
		if (NarrowerThanTargetRatio(num, 1.25f))
		{
			return AspectRatio._1x1;
		}
		if (NarrowerThanTargetRatio(num, 1.33333337f))
		{
			return AspectRatio._5x4;
		}
		if (NarrowerThanTargetRatio(num, 1.5f))
		{
			return AspectRatio._4x3;
		}
		if (NarrowerThanTargetRatio(num, 1.6f))
		{
			return AspectRatio._3x2;
		}
		if (NarrowerThanTargetRatio(num, 1.77777779f))
		{
			return AspectRatio._16x10;
		}
		if (NarrowerThanTargetRatio(num, 2.33333325f))
		{
			return AspectRatio._16x9;
		}
		if (NarrowerThanTargetRatio(num, 2.37037039f))
		{
			return AspectRatio._21x9;
		}
		return AspectRatio.ExtraWide;
	}

	private bool NarrowerThanTargetRatio(float value, float target)
	{
		return value < target - 0.005f;
	}
}
