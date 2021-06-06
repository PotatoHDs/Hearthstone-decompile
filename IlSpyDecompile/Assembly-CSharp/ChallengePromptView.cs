using System;
using PegasusUtil;
using UnityEngine;

public class ChallengePromptView : ShopView.IComponent
{
	private StoreChallengePrompt m_challengePrompt;

	public bool IsLoaded => m_challengePrompt != null;

	public bool IsShown
	{
		get
		{
			if (IsLoaded)
			{
				return m_challengePrompt.IsShown();
			}
			return false;
		}
	}

	public event Action OnComponentReady = delegate
	{
	};

	public event Action<string, bool, CancelPurchase.CancelReason?, string> OnComplete = delegate
	{
	};

	public event Action<string> OnCancel = delegate
	{
	};

	public void Load(IAssetLoader assetLoader)
	{
		if (!IsLoaded)
		{
			assetLoader.InstantiatePrefab((string)ShopPrefabs.ShopChallengePromptPrefab, OnLoaded);
		}
	}

	public void Unload()
	{
		if (IsLoaded)
		{
			m_challengePrompt.OnChallengeComplete -= CompleteListener;
			m_challengePrompt.OnCancel -= CancelListener;
			UnityEngine.Object.Destroy(m_challengePrompt.gameObject);
			m_challengePrompt = null;
		}
	}

	public void StartChallenge(string challengeId)
	{
		if (IsLoaded)
		{
			m_challengePrompt.StartCoroutine(m_challengePrompt.Show(challengeId));
		}
	}

	public void Hide()
	{
		if (IsShown)
		{
			m_challengePrompt.Hide();
		}
	}

	public bool Cancel(Action<string> onCancel)
	{
		string text = m_challengePrompt.HideChallenge();
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		onCancel(text);
		return true;
	}

	private void OnLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("ChallengePromptView.OnLoaded(): go is null!");
			return;
		}
		m_challengePrompt = go.GetComponent<StoreChallengePrompt>();
		if (m_challengePrompt == null)
		{
			Debug.LogError("ChallengePromptView.OnLoaded(): go has no StoreChallengePrompt component");
			return;
		}
		m_challengePrompt.Hide();
		m_challengePrompt.OnChallengeComplete += CompleteListener;
		m_challengePrompt.OnCancel += CancelListener;
		this.OnComponentReady();
	}

	private void CompleteListener(string challengeId, bool isSuccess, CancelPurchase.CancelReason? reason, string error)
	{
		this.OnComplete(challengeId, isSuccess, reason, error);
	}

	private void CancelListener(string challengeId)
	{
		this.OnCancel(challengeId);
	}
}
