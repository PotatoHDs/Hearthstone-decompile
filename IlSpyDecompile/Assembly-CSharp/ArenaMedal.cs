using Blizzard.T5.AssetManager;
using UnityEngine;

public class ArenaMedal : PegUIElement
{
	public GameObject m_rankMedal;

	private int m_medal;

	private AssetHandle<Texture> m_medalTexture;

	public const string MEDAL_NAME_PREFIX = "GLOBAL_ARENA_MEDAL_";

	private static readonly AssetReference[] s_arenaMedalTextures = new AssetReference[13]
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

	protected override void Awake()
	{
		base.Awake();
		AddEventListener(UIEventType.ROLLOVER, MedalOver);
		AddEventListener(UIEventType.ROLLOUT, MedalOut);
	}

	protected override void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_medalTexture);
		base.OnDestroy();
	}

	public void SetMedal(int medal)
	{
		m_medal = medal;
		AssetLoader.Get().LoadAsset<Texture>(s_arenaMedalTextures[medal], OnTextureLoaded);
	}

	private void MedalOver(UIEvent e)
	{
		bool flag = SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB);
		string headline;
		string bodytext;
		if (Options.Get().GetBool(Option.IN_RANKED_PLAY_MODE) || flag)
		{
			headline = GetMedalName();
			bodytext = GameStrings.Format("GLOBAL_MEDAL_ARENA_TOOLTIP_BODY", m_medal);
		}
		else
		{
			headline = GameStrings.Get("GLUE_TOURNAMENT_UNRANKED_MODE");
			bodytext = GameStrings.Get("GLUE_TOURNAMENT_UNRANKED_DESC");
		}
		base.gameObject.GetComponent<TooltipZone>().ShowLayerTooltip(headline, bodytext);
	}

	private void MedalOut(UIEvent e)
	{
		base.gameObject.GetComponent<TooltipZone>().HideTooltip();
	}

	private void OnTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		using (texture)
		{
			if (!texture)
			{
				Debug.LogWarning($"ArenaMedal.OnTextureLoaded(): asset for {assetRef} is null!");
				return;
			}
			AssetHandle.Set(ref m_medalTexture, texture);
			m_rankMedal.GetComponent<Renderer>().GetMaterial().mainTexture = m_medalTexture;
		}
	}

	private string GetMedalName()
	{
		return GameStrings.Get("GLOBAL_ARENA_MEDAL_" + m_medal);
	}

	private string GetNextMedalName()
	{
		string text = "GLOBAL_ARENA_MEDAL_" + (m_medal + 1);
		string text2 = GameStrings.Get(text);
		if (text2 == text)
		{
			return "";
		}
		return text2;
	}
}
