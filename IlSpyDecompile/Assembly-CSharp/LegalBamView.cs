using System;
using UnityEngine;

public class LegalBamView : ShopView.IComponent
{
	private StoreLegalBAMLinks m_legalBam;

	public bool IsLoaded => m_legalBam != null;

	public bool IsShown
	{
		get
		{
			if (IsLoaded)
			{
				return m_legalBam.IsShown();
			}
			return false;
		}
	}

	public event Action OnComponentReady = delegate
	{
	};

	public event Action<StoreLegalBAMLinks.BAMReason> OnOkay = delegate
	{
	};

	public event Action OnCancel = delegate
	{
	};

	public void Load(IAssetLoader assetLoader)
	{
		if (!IsLoaded)
		{
			assetLoader.InstantiatePrefab((string)ShopPrefabs.ShopLegalBamLinksPrefab, OnLoaded);
		}
	}

	public void Unload()
	{
		if (IsLoaded)
		{
			m_legalBam.RemoveSendToBAMListener(OkayListener);
			m_legalBam.RemoveCancelListener(CancelListener);
			UnityEngine.Object.Destroy(m_legalBam.gameObject);
			m_legalBam = null;
		}
	}

	public void Show()
	{
		if (IsLoaded)
		{
			m_legalBam.Show();
		}
	}

	public void Hide()
	{
		if (IsShown)
		{
			m_legalBam.Hide();
		}
	}

	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("LegalBamView.OnLoaded(): go is null!");
			return;
		}
		m_legalBam = go.GetComponent<StoreLegalBAMLinks>();
		if (m_legalBam == null)
		{
			Debug.LogError("LegalBamView.OnLoaded(): go has no StoreLegalBAMLinks component");
			return;
		}
		m_legalBam.Hide();
		m_legalBam.RegisterSendToBAMListener(OkayListener);
		m_legalBam.RegisterCancelListener(CancelListener);
		this.OnComponentReady();
	}

	private void OkayListener(StoreLegalBAMLinks.BAMReason reason)
	{
		this.OnOkay(reason);
	}

	private void CancelListener()
	{
		this.OnCancel();
	}
}
