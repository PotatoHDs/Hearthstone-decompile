using System;
using UnityEngine;

public class DoneWithBamView : ShopView.IComponent
{
	private StoreDoneWithBAM m_doneWithBam;

	public bool IsLoaded => m_doneWithBam != null;

	public bool IsShown
	{
		get
		{
			if (IsLoaded)
			{
				return m_doneWithBam.IsShown();
			}
			return false;
		}
	}

	public event Action OnComponentReady = delegate
	{
	};

	public event Action OnOkay = delegate
	{
	};

	public void Load(IAssetLoader assetLoader)
	{
		if (!IsLoaded)
		{
			assetLoader.InstantiatePrefab((string)ShopPrefabs.ShopDoneWithBamPrefab, OnLoaded);
		}
	}

	public void Unload()
	{
		if (IsLoaded)
		{
			m_doneWithBam.RemoveOkayListener(OnOkayListener);
			UnityEngine.Object.Destroy(m_doneWithBam.gameObject);
			m_doneWithBam = null;
		}
	}

	public void Show()
	{
		if (IsLoaded)
		{
			m_doneWithBam.Show();
		}
	}

	public void Hide()
	{
		if (IsShown)
		{
			m_doneWithBam.Hide();
		}
	}

	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("DoneWithBamView.OnLoaded(): go is null!");
			return;
		}
		m_doneWithBam = go.GetComponent<StoreDoneWithBAM>();
		if (m_doneWithBam == null)
		{
			Debug.LogError("DoneWithBamView.OnLoaded(): go has no StoreDoneWithBAM component");
			return;
		}
		m_doneWithBam.Hide();
		m_doneWithBam.RegisterOkayListener(OnOkayListener);
		this.OnComponentReady();
	}

	private void OnOkayListener()
	{
		this.OnOkay();
	}
}
