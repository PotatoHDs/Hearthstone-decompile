using System;
using UnityEngine;

public class SummaryView : ShopView.IComponent
{
	private StoreSummary m_summary;

	public bool IsLoaded => m_summary != null;

	public bool IsShown
	{
		get
		{
			if (IsLoaded)
			{
				return m_summary.IsShown();
			}
			return false;
		}
	}

	public event Action OnComponentReady = delegate
	{
	};

	public event Action<int, object> OnSummaryConfirm = delegate
	{
	};

	public event Action<object> OnSummaryCancel = delegate
	{
	};

	public event Action<object> OnSummaryInfo = delegate
	{
	};

	public event Action<object> OnSummaryPaymentAndTos = delegate
	{
	};

	public void Load(IAssetLoader assetLoader)
	{
		if (!IsLoaded)
		{
			assetLoader.InstantiatePrefab((string)ShopPrefabs.ShopSummaryPrefab, OnLoaded);
		}
	}

	public void Unload()
	{
		if (IsLoaded)
		{
			m_summary.RemoveConfirmListener(ConfirmListener);
			m_summary.RemoveCancelListener(CancelListener);
			m_summary.RemoveInfoListener(InfoListener);
			m_summary.RemovePaymentAndTOSListener(PaymentAndTosListener);
			UnityEngine.Object.Destroy(m_summary.gameObject);
			m_summary = null;
		}
	}

	public void Show(long pmtProductID, int quantity, string paymentMethodName)
	{
		if (IsLoaded)
		{
			m_summary.Show(pmtProductID, quantity, paymentMethodName);
		}
	}

	public void Hide()
	{
		if (IsShown)
		{
			m_summary.Hide();
		}
	}

	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("SummaryView.OnLoaded(): go is null!");
			return;
		}
		m_summary = go.GetComponent<StoreSummary>();
		if (m_summary == null)
		{
			Debug.LogError("SummaryView.OnLoaded(): go has no StoreSummary component");
			return;
		}
		m_summary.Hide();
		m_summary.RegisterConfirmListener(ConfirmListener);
		m_summary.RegisterCancelListener(CancelListener);
		m_summary.RegisterInfoListener(InfoListener);
		m_summary.RegisterPaymentAndTOSListener(PaymentAndTosListener);
		this.OnComponentReady();
	}

	private void ConfirmListener(int quantity, object userData)
	{
		this.OnSummaryConfirm(quantity, userData);
	}

	private void CancelListener(object userData)
	{
		this.OnSummaryCancel(userData);
	}

	private void InfoListener(object userData)
	{
		this.OnSummaryInfo(userData);
	}

	private void PaymentAndTosListener(object userData)
	{
		this.OnSummaryPaymentAndTos(userData);
	}
}
