using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000863 RID: 2147
[CustomEditClass]
public class Board : MonoBehaviour
{
	// Token: 0x060073DD RID: 29661 RVA: 0x00252C94 File Offset: 0x00250E94
	private void Awake()
	{
		Board.s_instance = this;
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
		if (this.m_FriendlyHeroTray == null)
		{
			Debug.LogError("Friendly Hero Tray is not assigned!");
		}
		if (this.m_OpponentHeroTray == null)
		{
			Debug.LogError("Opponent Hero Tray is not assigned!");
		}
	}

	// Token: 0x060073DE RID: 29662 RVA: 0x00252CF4 File Offset: 0x00250EF4
	private void OnDestroy()
	{
		Board.s_instance = null;
		AssetHandle.SafeDispose<Texture>(ref this.m_friendlyHeroTrayTexture);
		AssetHandle.SafeDispose<Texture>(ref this.m_friendlyHeroPhoneTrayTexture);
		AssetHandle.SafeDispose<Texture>(ref this.m_opponentHeroTrayTexture);
		AssetHandle.SafeDispose<Texture>(ref this.m_opponentHeroPhoneTrayTexture);
	}

	// Token: 0x060073DF RID: 29663 RVA: 0x00252D28 File Offset: 0x00250F28
	private void Start()
	{
		ProjectedShadow.SetShadowColor(this.m_ShadowColor);
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>()[base.GetComponent<Animation>().clip.name].normalizedTime = 0.25f;
			base.GetComponent<Animation>()[base.GetComponent<Animation>().clip.name].speed = -3f;
			base.GetComponent<Animation>().Play(base.GetComponent<Animation>().clip.name);
		}
		base.StartCoroutine(this.GoldenHeroes());
		if (!GameMgr.Get().IsTutorial())
		{
			foreach (Board.BoardSpecialEvents boardSpecialEvents in this.m_SpecialEvents)
			{
				if (SpecialEventManager.Get().IsEventActive(boardSpecialEvents.EventType, false))
				{
					this.LoadBoardSpecialEvent(boardSpecialEvents);
				}
			}
		}
	}

	// Token: 0x060073E0 RID: 29664 RVA: 0x00252E28 File Offset: 0x00251028
	public static Board Get()
	{
		return Board.s_instance;
	}

	// Token: 0x060073E1 RID: 29665 RVA: 0x00252E2F File Offset: 0x0025102F
	public void SetBoardDbId(int id)
	{
		this.m_boardDbId = id;
	}

	// Token: 0x060073E2 RID: 29666 RVA: 0x00252E38 File Offset: 0x00251038
	public void ResetAmbientColor()
	{
		RenderSettings.ambientLight = this.m_AmbientColor;
	}

	// Token: 0x060073E3 RID: 29667 RVA: 0x00252E45 File Offset: 0x00251045
	[ContextMenu("RaiseTheLights")]
	public void RaiseTheLights()
	{
		this.RaiseTheLights(1f);
	}

	// Token: 0x060073E4 RID: 29668 RVA: 0x00252E52 File Offset: 0x00251052
	public void RaiseTheLightsQuickly()
	{
		this.RaiseTheLights(5f);
	}

	// Token: 0x060073E5 RID: 29669 RVA: 0x00252E60 File Offset: 0x00251060
	public void RaiseTheLights(float speed)
	{
		if (this.m_raisedLights)
		{
			return;
		}
		float num = 3f / speed;
		Action<object> action = delegate(object amount)
		{
			RenderSettings.ambientLight = (Color)amount;
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			RenderSettings.ambientLight,
			"to",
			this.m_AmbientColor,
			"time",
			num,
			"easeType",
			iTween.EaseType.easeInOutQuad,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args);
		Action<object> action2 = delegate(object amount)
		{
			this.m_DirectionalLight.intensity = (float)amount;
		};
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			this.m_DirectionalLight.intensity,
			"to",
			this.m_DirectionalLightIntensity,
			"time",
			num,
			"easeType",
			iTween.EaseType.easeInOutQuad,
			"onupdate",
			action2,
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args2);
		this.m_raisedLights = true;
	}

	// Token: 0x060073E6 RID: 29670 RVA: 0x00252FC6 File Offset: 0x002511C6
	public void SetMulliganLighting()
	{
		RenderSettings.ambientLight = this.MULLIGAN_AMBIENT_LIGHT_COLOR;
		this.m_DirectionalLight.intensity = 0f;
	}

	// Token: 0x060073E7 RID: 29671 RVA: 0x00252FE3 File Offset: 0x002511E3
	public void DimTheLights()
	{
		this.DimTheLights(5f);
	}

	// Token: 0x060073E8 RID: 29672 RVA: 0x00252FF0 File Offset: 0x002511F0
	public void DimTheLights(float speed)
	{
		if (!this.m_raisedLights)
		{
			return;
		}
		float num = 3f / speed;
		Action<object> action = delegate(object amount)
		{
			RenderSettings.ambientLight = (Color)amount;
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			RenderSettings.ambientLight,
			"to",
			this.MULLIGAN_AMBIENT_LIGHT_COLOR,
			"time",
			num,
			"easeType",
			iTween.EaseType.easeInOutQuad,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args);
		Action<object> action2 = delegate(object amount)
		{
			this.m_DirectionalLight.intensity = (float)amount;
		};
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			this.m_DirectionalLight.intensity,
			"to",
			0f,
			"time",
			num,
			"easeType",
			iTween.EaseType.easeInOutQuad,
			"onupdate",
			action2,
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args2);
		this.m_raisedLights = false;
	}

	// Token: 0x060073E9 RID: 29673 RVA: 0x00253158 File Offset: 0x00251358
	public Transform FindBone(string name)
	{
		if (this.m_BoneParent != null)
		{
			Transform transform = this.m_BoneParent.Find(name);
			if (transform != null)
			{
				return transform;
			}
		}
		return Gameplay.Get().GetBoardLayout().FindBone(name);
	}

	// Token: 0x060073EA RID: 29674 RVA: 0x0025319C File Offset: 0x0025139C
	public Collider FindCollider(string name)
	{
		if (this.m_ColliderParent != null)
		{
			Transform transform = this.m_ColliderParent.Find(name);
			if (transform != null)
			{
				if (!(transform == null))
				{
					return transform.GetComponent<Collider>();
				}
				return null;
			}
		}
		return Gameplay.Get().GetBoardLayout().FindCollider(name);
	}

	// Token: 0x060073EB RID: 29675 RVA: 0x002531EF File Offset: 0x002513EF
	public GameObject GetMouseClickDustEffectPrefab()
	{
		return this.m_MouseClickDustEffect;
	}

	// Token: 0x060073EC RID: 29676 RVA: 0x002531F7 File Offset: 0x002513F7
	public void CombinedSurface()
	{
		if (this.m_CombinedPlaySurface != null && this.m_SplitPlaySurface != null)
		{
			this.m_CombinedPlaySurface.SetActive(true);
			this.m_SplitPlaySurface.SetActive(false);
		}
	}

	// Token: 0x060073ED RID: 29677 RVA: 0x0025322D File Offset: 0x0025142D
	public void SplitSurface()
	{
		if (this.m_CombinedPlaySurface != null && this.m_SplitPlaySurface != null)
		{
			this.m_CombinedPlaySurface.SetActive(false);
			this.m_SplitPlaySurface.SetActive(true);
		}
	}

	// Token: 0x060073EE RID: 29678 RVA: 0x00253263 File Offset: 0x00251463
	public Spell GetFriendlyTraySpell()
	{
		return this.m_FriendlyTraySpellEffect;
	}

	// Token: 0x060073EF RID: 29679 RVA: 0x0025326B File Offset: 0x0025146B
	public Spell GetOpponentTraySpell()
	{
		return this.m_OpponentTraySpellEffect;
	}

	// Token: 0x060073F0 RID: 29680 RVA: 0x00253274 File Offset: 0x00251474
	public void ChangeBoardVisualState(TAG_BOARD_VISUAL_STATE boardState)
	{
		if (this.m_BoardStateChangingObjects == null || this.m_BoardStateChangingObjects.Count == 0)
		{
			return;
		}
		foreach (PlayMakerFSM playMakerFSM in this.m_BoardStateChangingObjects)
		{
			playMakerFSM.SetState(EnumUtils.GetString<TAG_BOARD_VISUAL_STATE>(boardState));
		}
	}

	// Token: 0x060073F1 RID: 29681 RVA: 0x002532E0 File Offset: 0x002514E0
	private IEnumerator GoldenHeroes()
	{
		bool friendlyHeroIsGolden = false;
		bool opposingHeroIsGolden = false;
		GameState gameState = GameState.Get();
		while (gameState == null)
		{
			gameState = GameState.Get();
			yield return null;
		}
		Player friendlyPlayer = gameState.GetFriendlySidePlayer();
		while (friendlyPlayer == null)
		{
			friendlyPlayer = gameState.GetFriendlySidePlayer();
			yield return null;
		}
		Player opposingPlayer = gameState.GetOpposingSidePlayer();
		Card friendlyHeroCard = friendlyPlayer.GetHeroCard();
		while (friendlyHeroCard == null)
		{
			friendlyHeroCard = friendlyPlayer.GetHeroCard();
			yield return null;
		}
		Card opposingHeroCard = opposingPlayer.GetHeroCard();
		while (opposingHeroCard == null)
		{
			opposingHeroCard = opposingPlayer.GetHeroCard();
			yield return null;
		}
		while (friendlyHeroCard.GetEntity() == null)
		{
			yield return null;
		}
		while (opposingHeroCard.GetEntity() == null)
		{
			yield return null;
		}
		if (friendlyHeroCard.GetPremium() == TAG_PREMIUM.GOLDEN)
		{
			friendlyHeroIsGolden = true;
		}
		if (opposingHeroCard.GetPremium() == TAG_PREMIUM.GOLDEN)
		{
			opposingHeroIsGolden = true;
		}
		if (friendlyHeroIsGolden)
		{
			AssetLoader.Get().InstantiatePrefab("HeroTray_Golden_Friendly.prefab:53559bff3e3c2414d8ea4c731e363ff7", new PrefabCallback<GameObject>(this.ShowFriendlyHeroTray), null, AssetLoadingOptions.None);
		}
		else
		{
			base.StartCoroutine(this.UpdateHeroTray(Player.Side.FRIENDLY, false));
		}
		if (opposingHeroIsGolden)
		{
			AssetLoader.Get().InstantiatePrefab("HeroTray_Golden_Opponent.prefab:073fa61999554054e9cc93c518349e15", new PrefabCallback<GameObject>(this.ShowOpponentHeroTray), null, AssetLoadingOptions.None);
		}
		else
		{
			base.StartCoroutine(this.UpdateHeroTray(Player.Side.OPPOSING, false));
		}
		yield break;
	}

	// Token: 0x060073F2 RID: 29682 RVA: 0x002532F0 File Offset: 0x002514F0
	private void ShowFriendlyHeroTray(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).transform.position;
		go.SetActive(true);
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetMaterial().color = this.m_GoldenHeroTrayColor;
		}
		UnityEngine.Object.Destroy(this.m_FriendlyHeroTray);
		this.m_FriendlyHeroTray = go;
		base.StartCoroutine(this.UpdateHeroTray(Player.Side.FRIENDLY, true));
	}

	// Token: 0x060073F3 RID: 29683 RVA: 0x00253370 File Offset: 0x00251570
	private void ShowOpponentHeroTray(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).transform.position;
		go.SetActive(true);
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetMaterial().color = this.m_GoldenHeroTrayColor;
		}
		this.m_OpponentHeroTray.SetActive(false);
		UnityEngine.Object.Destroy(this.m_OpponentHeroTray);
		this.m_OpponentHeroTray = go;
		base.StartCoroutine(this.UpdateHeroTray(Player.Side.OPPOSING, true));
	}

	// Token: 0x060073F4 RID: 29684 RVA: 0x002533F9 File Offset: 0x002515F9
	private IEnumerator UpdateHeroTray(Player.Side side, bool isGolden)
	{
		while (GameState.Get().GetPlayerMap().Count == 0)
		{
			yield return null;
		}
		Player p = null;
		while (p == null)
		{
			foreach (Player player in GameState.Get().GetPlayerMap().Values)
			{
				if (player.GetSide() == side)
				{
					p = player;
					break;
				}
			}
			yield return null;
		}
		while (p.GetHero() == null)
		{
			yield return null;
		}
		Entity hero = p.GetHero();
		while (hero.IsLoadingAssets())
		{
			yield return null;
		}
		while (hero.GetCard() == null)
		{
			yield return null;
		}
		Card heroCard = hero.GetCard();
		while (!heroCard.HasCardDef)
		{
			yield return null;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			while (ManaCrystalMgr.Get() == null)
			{
				yield return null;
			}
			if (side == Player.Side.FRIENDLY)
			{
				if (!string.IsNullOrEmpty(heroCard.CustomHeroPhoneManaGem))
				{
					AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroPhoneManaGem, new AssetHandleCallback<Texture>(this.OnHeroSkinManaGemTextureLoaded), null, AssetLoadingOptions.None);
				}
				else if (this.m_GemManaPhoneTexture != null)
				{
					ManaCrystalMgr.Get().SetFriendlyManaGemTexture(new AssetHandle<Texture>(this.m_GemManaPhoneTexture.name, this.m_GemManaPhoneTexture));
				}
			}
		}
		for (int i = 0; i < heroCard.CustomHeroTraySettings.Count; i++)
		{
			if (this.m_boardDbId == (int)heroCard.CustomHeroTraySettings[i].m_Board)
			{
				this.m_TrayTint = heroCard.CustomHeroTraySettings[i].m_Tint;
			}
		}
		if (!string.IsNullOrEmpty(heroCard.CustomHeroTray))
		{
			while (heroCard.GetActor() == null)
			{
				yield return null;
			}
			if (heroCard.GetActor().GetPremium() == TAG_PREMIUM.GOLDEN && !string.IsNullOrEmpty(heroCard.CustomHeroTrayGolden))
			{
				AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroTrayGolden, new AssetHandleCallback<Texture>(this.OnHeroTrayTextureLoaded), side, AssetLoadingOptions.None);
			}
			else
			{
				AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroTray, new AssetHandleCallback<Texture>(this.OnHeroTrayTextureLoaded), side, AssetLoadingOptions.None);
			}
		}
		if (UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(heroCard.CustomHeroPhoneTray))
		{
			AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroPhoneTray, new AssetHandleCallback<Texture>(this.OnHeroPhoneTrayTextureLoaded), side, AssetLoadingOptions.None);
		}
		yield break;
	}

	// Token: 0x060073F5 RID: 29685 RVA: 0x00253410 File Offset: 0x00251610
	private void OnHeroSkinManaGemTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		try
		{
			if (!texture)
			{
				Debug.LogError("OnHeroSkinManaGemTextureLoaded() loaded texture is null!");
			}
			else
			{
				ManaCrystalMgr.Get().SetFriendlyManaGemTexture(texture);
				ManaCrystalMgr.Get().SetFriendlyManaGemTint(this.m_TrayTint);
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

	// Token: 0x060073F6 RID: 29686 RVA: 0x0025346C File Offset: 0x0025166C
	private void OnHeroTrayTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		try
		{
			if (!texture)
			{
				Debug.LogError("Board.OnHeroTrayTextureLoaded() loaded texture is null!");
			}
			else if ((Player.Side)callbackData == Player.Side.FRIENDLY)
			{
				AssetHandle.Set<Texture>(ref this.m_friendlyHeroTrayTexture, texture);
				Material material = this.m_FriendlyHeroTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
				material.mainTexture = this.m_friendlyHeroTrayTexture;
				material.color = this.m_TrayTint;
			}
			else
			{
				AssetHandle.Set<Texture>(ref this.m_opponentHeroTrayTexture, texture);
				Material material2 = this.m_OpponentHeroTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
				material2.mainTexture = this.m_opponentHeroTrayTexture;
				material2.color = this.m_TrayTint;
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

	// Token: 0x060073F7 RID: 29687 RVA: 0x0025352C File Offset: 0x0025172C
	private void OnHeroPhoneTrayTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		try
		{
			if (!texture)
			{
				Debug.LogError("Board.OnHeroTrayTextureLoaded() loaded texture is null!");
			}
			else if ((Player.Side)callbackData == Player.Side.FRIENDLY)
			{
				if (this.m_FriendlyHeroPhoneTray == null)
				{
					Debug.LogWarning("Friendly Hero Phone Tray Object on Board is null!");
				}
				else
				{
					AssetHandle.Set<Texture>(ref this.m_friendlyHeroPhoneTrayTexture, texture);
					Material material = this.m_FriendlyHeroPhoneTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
					material.mainTexture = this.m_friendlyHeroPhoneTrayTexture;
					material.color = this.m_TrayTint;
				}
			}
			else if (this.m_OpponentHeroPhoneTray == null)
			{
				Debug.LogWarning("Opponent Hero Phone Tray Object on Board is null!");
			}
			else
			{
				AssetHandle.Set<Texture>(ref this.m_opponentHeroPhoneTrayTexture, texture);
				Material material2 = this.m_OpponentHeroPhoneTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
				material2.mainTexture = this.m_opponentHeroPhoneTrayTexture;
				material2.color = this.m_TrayTint;
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

	// Token: 0x060073F8 RID: 29688 RVA: 0x00253620 File Offset: 0x00251820
	private void OnHeroTrayEffectLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("Board.OnHeroTrayEffectLoaded() Hero tray effect is null!");
			return;
		}
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			Debug.LogError("Board.OnHeroTrayEffectLoaded() Hero tray effect: could not find spell component!");
			return;
		}
		if ((Player.Side)callbackData == Player.Side.FRIENDLY)
		{
			go.transform.parent = base.transform;
			go.transform.position = this.FindBone("CustomSocketIn_Friendly").position;
			this.m_FriendlyTraySpellEffect = component;
			return;
		}
		go.transform.parent = base.transform;
		go.transform.position = this.FindBone("CustomSocketIn_Opposing").position;
		this.m_OpponentTraySpellEffect = component;
	}

	// Token: 0x060073F9 RID: 29689 RVA: 0x002536CC File Offset: 0x002518CC
	private void LoadBoardSpecialEvent(Board.BoardSpecialEvents boardSpecialEvent)
	{
		if (AssetLoader.Get().InstantiatePrefab(boardSpecialEvent.Prefab, AssetLoadingOptions.None) == null)
		{
			Debug.LogWarning(string.Format("Failed to load special board event: {0}", boardSpecialEvent.Prefab));
		}
		this.m_AmbientColor = boardSpecialEvent.AmbientColorOverride;
	}

	// Token: 0x04005C04 RID: 23556
	private const string GOLDEN_HERO_TRAY_FRIENDLY = "HeroTray_Golden_Friendly.prefab:53559bff3e3c2414d8ea4c731e363ff7";

	// Token: 0x04005C05 RID: 23557
	private const string GOLDEN_HERO_TRAY_OPPONENT = "HeroTray_Golden_Opponent.prefab:073fa61999554054e9cc93c518349e15";

	// Token: 0x04005C06 RID: 23558
	private readonly Color MULLIGAN_AMBIENT_LIGHT_COLOR = new Color(0.1607843f, 0.1921569f, 0.282353f, 1f);

	// Token: 0x04005C07 RID: 23559
	private const float MULLIGAN_LIGHT_INTENSITY = 0f;

	// Token: 0x04005C08 RID: 23560
	public Color m_AmbientColor = Color.white;

	// Token: 0x04005C09 RID: 23561
	public Light m_DirectionalLight;

	// Token: 0x04005C0A RID: 23562
	public float m_DirectionalLightIntensity = 0.275f;

	// Token: 0x04005C0B RID: 23563
	public GameObject m_FriendlyHeroTray;

	// Token: 0x04005C0C RID: 23564
	public GameObject m_OpponentHeroTray;

	// Token: 0x04005C0D RID: 23565
	public GameObject m_FriendlyHeroPhoneTray;

	// Token: 0x04005C0E RID: 23566
	public GameObject m_OpponentHeroPhoneTray;

	// Token: 0x04005C0F RID: 23567
	public Transform m_BoneParent;

	// Token: 0x04005C10 RID: 23568
	public GameObject m_SplitPlaySurface;

	// Token: 0x04005C11 RID: 23569
	public GameObject m_CombinedPlaySurface;

	// Token: 0x04005C12 RID: 23570
	public Transform m_ColliderParent;

	// Token: 0x04005C13 RID: 23571
	public GameObject m_MouseClickDustEffect;

	// Token: 0x04005C14 RID: 23572
	public Color m_ShadowColor = new Color(0.098f, 0.098f, 0.235f, 0.45f);

	// Token: 0x04005C15 RID: 23573
	public Color m_DeckColor = Color.white;

	// Token: 0x04005C16 RID: 23574
	public Color m_EndTurnButtonColor = Color.white;

	// Token: 0x04005C17 RID: 23575
	public Color m_HistoryTileColor = Color.white;

	// Token: 0x04005C18 RID: 23576
	public Color m_GoldenHeroTrayColor = Color.white;

	// Token: 0x04005C19 RID: 23577
	public List<PlayMakerFSM> m_BoardStateChangingObjects;

	// Token: 0x04005C1A RID: 23578
	public List<Board.BoardSpecialEvents> m_SpecialEvents;

	// Token: 0x04005C1B RID: 23579
	public MusicPlaylistType m_BoardMusic = MusicPlaylistType.InGame_Default;

	// Token: 0x04005C1C RID: 23580
	public Texture m_GemManaPhoneTexture;

	// Token: 0x04005C1D RID: 23581
	private static Board s_instance;

	// Token: 0x04005C1E RID: 23582
	private bool m_raisedLights;

	// Token: 0x04005C1F RID: 23583
	private Spell m_FriendlyTraySpellEffect;

	// Token: 0x04005C20 RID: 23584
	private Spell m_OpponentTraySpellEffect;

	// Token: 0x04005C21 RID: 23585
	private int m_boardDbId;

	// Token: 0x04005C22 RID: 23586
	private Color m_TrayTint = Color.white;

	// Token: 0x04005C23 RID: 23587
	private AssetHandle<Texture> m_friendlyHeroTrayTexture;

	// Token: 0x04005C24 RID: 23588
	private AssetHandle<Texture> m_friendlyHeroPhoneTrayTexture;

	// Token: 0x04005C25 RID: 23589
	private AssetHandle<Texture> m_opponentHeroTrayTexture;

	// Token: 0x04005C26 RID: 23590
	private AssetHandle<Texture> m_opponentHeroPhoneTrayTexture;

	// Token: 0x0200245B RID: 9307
	[Serializable]
	public class CustomTraySettings
	{
		// Token: 0x0400E9EE RID: 59886
		public BoardDdId m_Board;

		// Token: 0x0400E9EF RID: 59887
		public Color m_Tint = Color.white;
	}

	// Token: 0x0200245C RID: 9308
	[Serializable]
	public class BoardSpecialEvents
	{
		// Token: 0x0400E9F0 RID: 59888
		public SpecialEventType EventType;

		// Token: 0x0400E9F1 RID: 59889
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string Prefab;

		// Token: 0x0400E9F2 RID: 59890
		public Color AmbientColorOverride = Color.white;
	}
}
