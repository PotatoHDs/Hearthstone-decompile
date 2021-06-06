using System;
using UnityEngine;

public class SendToBamView : ShopView.IComponent
{
	private StoreSendToBAM m_sendToBam;

	public bool IsLoaded => m_sendToBam != null;

	public bool IsShown
	{
		get
		{
			if (IsLoaded)
			{
				return m_sendToBam.IsShown();
			}
			return false;
		}
	}

	public event Action OnComponentReady = delegate
	{
	};

	public event Action<MoneyOrGTAPPTransaction, StoreSendToBAM.BAMReason> OnOkay = delegate
	{
	};

	public event Action<MoneyOrGTAPPTransaction> OnCancel = delegate
	{
	};

	public void Load(IAssetLoader assetLoader)
	{
		if (!IsLoaded)
		{
			assetLoader.InstantiatePrefab((string)ShopPrefabs.ShopSendToBamPrefab, OnLoaded);
		}
	}

	public void Unload()
	{
		if (IsLoaded)
		{
			m_sendToBam.RemoveOkayListener(OkayListener);
			m_sendToBam.RemoveCancelListener(CancelListener);
			UnityEngine.Object.Destroy(m_sendToBam.gameObject);
			m_sendToBam = null;
		}
	}

	public void Show(MoneyOrGTAPPTransaction transaction, StoreSendToBAM.BAMReason reason, string errorCode, bool fromPreviousPurchase)
	{
		if (IsLoaded)
		{
			m_sendToBam.Show(transaction, reason, errorCode, fromPreviousPurchase);
		}
	}

	public void Hide()
	{
		if (IsShown)
		{
			m_sendToBam.Hide();
		}
	}

	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("SendToBAMView.OnLoaded(): go is null!");
			return;
		}
		m_sendToBam = go.GetComponent<StoreSendToBAM>();
		if (m_sendToBam == null)
		{
			Debug.LogError("SendToBAMView.OnLoaded(): go has no StoreSendToBAM component");
			return;
		}
		m_sendToBam.Hide();
		m_sendToBam.RegisterOkayListener(OkayListener);
		m_sendToBam.RegisterCancelListener(CancelListener);
		this.OnComponentReady();
	}

	private void OkayListener(MoneyOrGTAPPTransaction transaction, StoreSendToBAM.BAMReason reason)
	{
		this.OnOkay(transaction, reason);
	}

	private void CancelListener(MoneyOrGTAPPTransaction transaction)
	{
		this.OnCancel(transaction);
	}
}
