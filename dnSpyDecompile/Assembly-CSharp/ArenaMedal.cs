using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000AC9 RID: 2761
public class ArenaMedal : PegUIElement
{
	// Token: 0x06009355 RID: 37717 RVA: 0x002FC7D0 File Offset: 0x002FA9D0
	protected override void Awake()
	{
		base.Awake();
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.MedalOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.MedalOut));
	}

	// Token: 0x06009356 RID: 37718 RVA: 0x002FC800 File Offset: 0x002FAA00
	protected override void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_medalTexture);
		base.OnDestroy();
	}

	// Token: 0x06009357 RID: 37719 RVA: 0x002FC813 File Offset: 0x002FAA13
	public void SetMedal(int medal)
	{
		this.m_medal = medal;
		AssetLoader.Get().LoadAsset<Texture>(ArenaMedal.s_arenaMedalTextures[medal], new AssetHandleCallback<Texture>(this.OnTextureLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06009358 RID: 37720 RVA: 0x002FC83C File Offset: 0x002FAA3C
	private void MedalOver(UIEvent e)
	{
		bool flag = SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB);
		string headline;
		string bodytext;
		if (Options.Get().GetBool(Option.IN_RANKED_PLAY_MODE) || flag)
		{
			headline = this.GetMedalName();
			bodytext = GameStrings.Format("GLOBAL_MEDAL_ARENA_TOOLTIP_BODY", new object[]
			{
				this.m_medal
			});
		}
		else
		{
			headline = GameStrings.Get("GLUE_TOURNAMENT_UNRANKED_MODE");
			bodytext = GameStrings.Get("GLUE_TOURNAMENT_UNRANKED_DESC");
		}
		base.gameObject.GetComponent<TooltipZone>().ShowLayerTooltip(headline, bodytext, 0);
	}

	// Token: 0x06009359 RID: 37721 RVA: 0x0028CE9B File Offset: 0x0028B09B
	private void MedalOut(UIEvent e)
	{
		base.gameObject.GetComponent<TooltipZone>().HideTooltip();
	}

	// Token: 0x0600935A RID: 37722 RVA: 0x002FC8BC File Offset: 0x002FAABC
	private void OnTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		try
		{
			if (!texture)
			{
				Debug.LogWarning(string.Format("ArenaMedal.OnTextureLoaded(): asset for {0} is null!", assetRef));
			}
			else
			{
				AssetHandle.Set<Texture>(ref this.m_medalTexture, texture);
				this.m_rankMedal.GetComponent<Renderer>().GetMaterial().mainTexture = this.m_medalTexture;
			}
		}
		finally
		{
			if (texture != null)
			{
				((IDisposable)texture).Dispose();
			}
		}
	}

	// Token: 0x0600935B RID: 37723 RVA: 0x002FC930 File Offset: 0x002FAB30
	private string GetMedalName()
	{
		return GameStrings.Get("GLOBAL_ARENA_MEDAL_" + this.m_medal);
	}

	// Token: 0x0600935C RID: 37724 RVA: 0x002FC94C File Offset: 0x002FAB4C
	private string GetNextMedalName()
	{
		string text = "GLOBAL_ARENA_MEDAL_" + (this.m_medal + 1);
		string text2 = GameStrings.Get(text);
		if (text2 == text)
		{
			return "";
		}
		return text2;
	}

	// Token: 0x04007B79 RID: 31609
	public GameObject m_rankMedal;

	// Token: 0x04007B7A RID: 31610
	private int m_medal;

	// Token: 0x04007B7B RID: 31611
	private AssetHandle<Texture> m_medalTexture;

	// Token: 0x04007B7C RID: 31612
	public const string MEDAL_NAME_PREFIX = "GLOBAL_ARENA_MEDAL_";

	// Token: 0x04007B7D RID: 31613
	private static readonly AssetReference[] s_arenaMedalTextures = new AssetReference[]
	{
		new AssetReference("Medal_Key_1.psd:2498bd0f3a6d16340be8676654a1cd1f"),
		new AssetReference("Medal_Key_2.psd:47f84fdbd66b87949ad1dbd29f7e1f96"),
		new AssetReference("Medal_Key_3.psd:5690610bd68943344a39c617e76ee1ad"),
		new AssetReference("Medal_Key_4.psd:b8d2d560c2b7b0d418cc7a1351de76aa"),
		new AssetReference("Medal_Key_5.psd:06af089ab00219b4d82fc0321fe8ec6f"),
		new AssetReference("Medal_Key_6.psd:1a7672de24da5bc4caea4d847cd690d0"),
		new AssetReference("Medal_Key_7.psd:5e7f88dc8b12ec9449d012bda24f852e"),
		new AssetReference("Medal_Key_8.psd:1ca047d0374edbb4aa969ec065d210f4"),
		new AssetReference("Medal_Key_9.psd:15eb648eb0922474d87e39b1a61fc7c2"),
		new AssetReference("Medal_Key_10.psd:841661205b2d54a47a44189a7fe6c93c"),
		new AssetReference("Medal_Key_11.psd:883cab168d52e6042a30123a65f5f5d9"),
		new AssetReference("Medal_Key_12.psd:1bf051dd0fb81a94595a5e7163ef6cfd"),
		new AssetReference("Medal_Key_13.psd:f979e44ef1d63324fad67769fe9fb8c2")
	};
}
