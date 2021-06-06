using System;
using Hearthstone;
using UnityEngine;

// Token: 0x02000874 RID: 2164
public class CardSound
{
	// Token: 0x060075F4 RID: 30196 RVA: 0x0025DB55 File Offset: 0x0025BD55
	public CardSound(string path, Card owner, bool alwaysValid)
	{
		this.m_path = path;
		this.m_owner = owner;
		this.m_alwaysValid = alwaysValid;
	}

	// Token: 0x060075F5 RID: 30197 RVA: 0x0025DB72 File Offset: 0x0025BD72
	public AudioSource GetSound(bool loadIfNeeded = true)
	{
		if (this.m_source == null && loadIfNeeded)
		{
			this.LoadSound();
		}
		return this.m_source;
	}

	// Token: 0x060075F6 RID: 30198 RVA: 0x0025DB90 File Offset: 0x0025BD90
	public void Clear()
	{
		if (this.m_source == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_source.gameObject);
	}

	// Token: 0x060075F7 RID: 30199 RVA: 0x0025DBB4 File Offset: 0x0025BDB4
	private void LoadSound()
	{
		if (string.IsNullOrEmpty(this.m_path) || !AssetLoader.Get().IsAssetAvailable(this.m_path))
		{
			return;
		}
		GameObject gameObject = SoundLoader.LoadSound(this.m_path);
		if (gameObject == null)
		{
			if (this.m_alwaysValid)
			{
				string message = string.Format("CardSound.LoadSound() - Failed to load \"{0}\"", this.m_path);
				if (HearthstoneApplication.UseDevWorkarounds())
				{
					Debug.LogError(message);
					return;
				}
				Error.AddDevFatal(message, Array.Empty<object>());
			}
			return;
		}
		this.m_source = gameObject.GetComponent<AudioSource>();
		if (this.m_source == null)
		{
			UnityEngine.Object.Destroy(gameObject);
			if (this.m_alwaysValid)
			{
				string message2 = string.Format("CardSound.LoadSound() - \"{0}\" does not have an AudioSource component.", this.m_path);
				if (HearthstoneApplication.UseDevWorkarounds())
				{
					Debug.LogError(message2);
					return;
				}
				Error.AddDevFatal(message2, Array.Empty<object>());
			}
			return;
		}
		this.SetupSound();
	}

	// Token: 0x060075F8 RID: 30200 RVA: 0x0025DC8C File Offset: 0x0025BE8C
	private void SetupSound()
	{
		if (this.m_source == null)
		{
			return;
		}
		if (this.m_owner == null)
		{
			return;
		}
		this.m_source.transform.parent = this.m_owner.transform;
		TransformUtil.Identity(this.m_source.transform);
	}

	// Token: 0x04005D29 RID: 23849
	private string m_path;

	// Token: 0x04005D2A RID: 23850
	private AudioSource m_source;

	// Token: 0x04005D2B RID: 23851
	private Card m_owner;

	// Token: 0x04005D2C RID: 23852
	private bool m_alwaysValid;
}
