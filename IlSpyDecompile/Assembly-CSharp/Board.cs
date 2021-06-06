using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class Board : MonoBehaviour
{
	[Serializable]
	public class CustomTraySettings
	{
		public BoardDdId m_Board;

		public Color m_Tint = Color.white;
	}

	[Serializable]
	public class BoardSpecialEvents
	{
		public SpecialEventType EventType;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string Prefab;

		public Color AmbientColorOverride = Color.white;
	}

	private const string GOLDEN_HERO_TRAY_FRIENDLY = "HeroTray_Golden_Friendly.prefab:53559bff3e3c2414d8ea4c731e363ff7";

	private const string GOLDEN_HERO_TRAY_OPPONENT = "HeroTray_Golden_Opponent.prefab:073fa61999554054e9cc93c518349e15";

	private readonly Color MULLIGAN_AMBIENT_LIGHT_COLOR = new Color(0.1607843f, 0.1921569f, 0.282353f, 1f);

	private const float MULLIGAN_LIGHT_INTENSITY = 0f;

	public Color m_AmbientColor = Color.white;

	public Light m_DirectionalLight;

	public float m_DirectionalLightIntensity = 0.275f;

	public GameObject m_FriendlyHeroTray;

	public GameObject m_OpponentHeroTray;

	public GameObject m_FriendlyHeroPhoneTray;

	public GameObject m_OpponentHeroPhoneTray;

	public Transform m_BoneParent;

	public GameObject m_SplitPlaySurface;

	public GameObject m_CombinedPlaySurface;

	public Transform m_ColliderParent;

	public GameObject m_MouseClickDustEffect;

	public Color m_ShadowColor = new Color(0.098f, 0.098f, 0.235f, 0.45f);

	public Color m_DeckColor = Color.white;

	public Color m_EndTurnButtonColor = Color.white;

	public Color m_HistoryTileColor = Color.white;

	public Color m_GoldenHeroTrayColor = Color.white;

	public List<PlayMakerFSM> m_BoardStateChangingObjects;

	public List<BoardSpecialEvents> m_SpecialEvents;

	public MusicPlaylistType m_BoardMusic = MusicPlaylistType.InGame_Default;

	public Texture m_GemManaPhoneTexture;

	private static Board s_instance;

	private bool m_raisedLights;

	private Spell m_FriendlyTraySpellEffect;

	private Spell m_OpponentTraySpellEffect;

	private int m_boardDbId;

	private Color m_TrayTint = Color.white;

	private AssetHandle<Texture> m_friendlyHeroTrayTexture;

	private AssetHandle<Texture> m_friendlyHeroPhoneTrayTexture;

	private AssetHandle<Texture> m_opponentHeroTrayTexture;

	private AssetHandle<Texture> m_opponentHeroPhoneTrayTexture;

	private void Awake()
	{
		s_instance = this;
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
		if (m_FriendlyHeroTray == null)
		{
			Debug.LogError("Friendly Hero Tray is not assigned!");
		}
		if (m_OpponentHeroTray == null)
		{
			Debug.LogError("Opponent Hero Tray is not assigned!");
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
		AssetHandle.SafeDispose(ref m_friendlyHeroTrayTexture);
		AssetHandle.SafeDispose(ref m_friendlyHeroPhoneTrayTexture);
		AssetHandle.SafeDispose(ref m_opponentHeroTrayTexture);
		AssetHandle.SafeDispose(ref m_opponentHeroPhoneTrayTexture);
	}

	private void Start()
	{
		ProjectedShadow.SetShadowColor(m_ShadowColor);
		if (GetComponent<Animation>() != null)
		{
			GetComponent<Animation>()[GetComponent<Animation>().clip.name].normalizedTime = 0.25f;
			GetComponent<Animation>()[GetComponent<Animation>().clip.name].speed = -3f;
			GetComponent<Animation>().Play(GetComponent<Animation>().clip.name);
		}
		StartCoroutine(GoldenHeroes());
		if (GameMgr.Get().IsTutorial())
		{
			return;
		}
		foreach (BoardSpecialEvents specialEvent in m_SpecialEvents)
		{
			if (SpecialEventManager.Get().IsEventActive(specialEvent.EventType, activeIfDoesNotExist: false))
			{
				LoadBoardSpecialEvent(specialEvent);
			}
		}
	}

	public static Board Get()
	{
		return s_instance;
	}

	public void SetBoardDbId(int id)
	{
		m_boardDbId = id;
	}

	public void ResetAmbientColor()
	{
		RenderSettings.ambientLight = m_AmbientColor;
	}

	[ContextMenu("RaiseTheLights")]
	public void RaiseTheLights()
	{
		RaiseTheLights(1f);
	}

	public void RaiseTheLightsQuickly()
	{
		RaiseTheLights(5f);
	}

	public void RaiseTheLights(float speed)
	{
		if (!m_raisedLights)
		{
			float num = 3f / speed;
			Action<object> action = delegate(object amount)
			{
				RenderSettings.ambientLight = (Color)amount;
			};
			Hashtable args = iTween.Hash("from", RenderSettings.ambientLight, "to", m_AmbientColor, "time", num, "easeType", iTween.EaseType.easeInOutQuad, "onupdate", action, "onupdatetarget", base.gameObject);
			iTween.ValueTo(base.gameObject, args);
			Action<object> action2 = delegate(object amount)
			{
				m_DirectionalLight.intensity = (float)amount;
			};
			Hashtable args2 = iTween.Hash("from", m_DirectionalLight.intensity, "to", m_DirectionalLightIntensity, "time", num, "easeType", iTween.EaseType.easeInOutQuad, "onupdate", action2, "onupdatetarget", base.gameObject);
			iTween.ValueTo(base.gameObject, args2);
			m_raisedLights = true;
		}
	}

	public void SetMulliganLighting()
	{
		RenderSettings.ambientLight = MULLIGAN_AMBIENT_LIGHT_COLOR;
		m_DirectionalLight.intensity = 0f;
	}

	public void DimTheLights()
	{
		DimTheLights(5f);
	}

	public void DimTheLights(float speed)
	{
		if (m_raisedLights)
		{
			float num = 3f / speed;
			Action<object> action = delegate(object amount)
			{
				RenderSettings.ambientLight = (Color)amount;
			};
			Hashtable args = iTween.Hash("from", RenderSettings.ambientLight, "to", MULLIGAN_AMBIENT_LIGHT_COLOR, "time", num, "easeType", iTween.EaseType.easeInOutQuad, "onupdate", action, "onupdatetarget", base.gameObject);
			iTween.ValueTo(base.gameObject, args);
			Action<object> action2 = delegate(object amount)
			{
				m_DirectionalLight.intensity = (float)amount;
			};
			Hashtable args2 = iTween.Hash("from", m_DirectionalLight.intensity, "to", 0f, "time", num, "easeType", iTween.EaseType.easeInOutQuad, "onupdate", action2, "onupdatetarget", base.gameObject);
			iTween.ValueTo(base.gameObject, args2);
			m_raisedLights = false;
		}
	}

	public Transform FindBone(string name)
	{
		if (m_BoneParent != null)
		{
			Transform transform = m_BoneParent.Find(name);
			if (transform != null)
			{
				return transform;
			}
		}
		return Gameplay.Get().GetBoardLayout().FindBone(name);
	}

	public Collider FindCollider(string name)
	{
		if (m_ColliderParent != null)
		{
			Transform transform = m_ColliderParent.Find(name);
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

	public GameObject GetMouseClickDustEffectPrefab()
	{
		return m_MouseClickDustEffect;
	}

	public void CombinedSurface()
	{
		if (m_CombinedPlaySurface != null && m_SplitPlaySurface != null)
		{
			m_CombinedPlaySurface.SetActive(value: true);
			m_SplitPlaySurface.SetActive(value: false);
		}
	}

	public void SplitSurface()
	{
		if (m_CombinedPlaySurface != null && m_SplitPlaySurface != null)
		{
			m_CombinedPlaySurface.SetActive(value: false);
			m_SplitPlaySurface.SetActive(value: true);
		}
	}

	public Spell GetFriendlyTraySpell()
	{
		return m_FriendlyTraySpellEffect;
	}

	public Spell GetOpponentTraySpell()
	{
		return m_OpponentTraySpellEffect;
	}

	public void ChangeBoardVisualState(TAG_BOARD_VISUAL_STATE boardState)
	{
		if (m_BoardStateChangingObjects == null || m_BoardStateChangingObjects.Count == 0)
		{
			return;
		}
		foreach (PlayMakerFSM boardStateChangingObject in m_BoardStateChangingObjects)
		{
			boardStateChangingObject.SetState(EnumUtils.GetString(boardState));
		}
	}

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
			AssetLoader.Get().InstantiatePrefab("HeroTray_Golden_Friendly.prefab:53559bff3e3c2414d8ea4c731e363ff7", ShowFriendlyHeroTray);
		}
		else
		{
			StartCoroutine(UpdateHeroTray(Player.Side.FRIENDLY, isGolden: false));
		}
		if (opposingHeroIsGolden)
		{
			AssetLoader.Get().InstantiatePrefab("HeroTray_Golden_Opponent.prefab:073fa61999554054e9cc93c518349e15", ShowOpponentHeroTray);
		}
		else
		{
			StartCoroutine(UpdateHeroTray(Player.Side.OPPOSING, isGolden: false));
		}
	}

	private void ShowFriendlyHeroTray(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).transform.position;
		go.SetActive(value: true);
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetMaterial().color = m_GoldenHeroTrayColor;
		}
		UnityEngine.Object.Destroy(m_FriendlyHeroTray);
		m_FriendlyHeroTray = go;
		StartCoroutine(UpdateHeroTray(Player.Side.FRIENDLY, isGolden: true));
	}

	private void ShowOpponentHeroTray(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).transform.position;
		go.SetActive(value: true);
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetMaterial().color = m_GoldenHeroTrayColor;
		}
		m_OpponentHeroTray.SetActive(value: false);
		UnityEngine.Object.Destroy(m_OpponentHeroTray);
		m_OpponentHeroTray = go;
		StartCoroutine(UpdateHeroTray(Player.Side.OPPOSING, isGolden: true));
	}

	private IEnumerator UpdateHeroTray(Player.Side side, bool isGolden)
	{
		while (GameState.Get().GetPlayerMap().Count == 0)
		{
			yield return null;
		}
		Player p = null;
		while (p == null)
		{
			foreach (Player value in GameState.Get().GetPlayerMap().Values)
			{
				if (value.GetSide() == side)
				{
					p = value;
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
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			while (ManaCrystalMgr.Get() == null)
			{
				yield return null;
			}
			if (side == Player.Side.FRIENDLY)
			{
				if (!string.IsNullOrEmpty(heroCard.CustomHeroPhoneManaGem))
				{
					AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroPhoneManaGem, OnHeroSkinManaGemTextureLoaded);
				}
				else if (m_GemManaPhoneTexture != null)
				{
					ManaCrystalMgr.Get().SetFriendlyManaGemTexture(new AssetHandle<Texture>(m_GemManaPhoneTexture.name, m_GemManaPhoneTexture));
				}
			}
		}
		for (int i = 0; i < heroCard.CustomHeroTraySettings.Count; i++)
		{
			if (m_boardDbId == (int)heroCard.CustomHeroTraySettings[i].m_Board)
			{
				m_TrayTint = heroCard.CustomHeroTraySettings[i].m_Tint;
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
				AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroTrayGolden, OnHeroTrayTextureLoaded, side);
			}
			else
			{
				AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroTray, OnHeroTrayTextureLoaded, side);
			}
		}
		if ((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(heroCard.CustomHeroPhoneTray))
		{
			AssetLoader.Get().LoadAsset<Texture>(heroCard.CustomHeroPhoneTray, OnHeroPhoneTrayTextureLoaded, side);
		}
	}

	private void OnHeroSkinManaGemTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		using (texture)
		{
			if (!texture)
			{
				Debug.LogError("OnHeroSkinManaGemTextureLoaded() loaded texture is null!");
				return;
			}
			ManaCrystalMgr.Get().SetFriendlyManaGemTexture(texture);
			ManaCrystalMgr.Get().SetFriendlyManaGemTint(m_TrayTint);
		}
	}

	private void OnHeroTrayTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		using (texture)
		{
			if (!texture)
			{
				Debug.LogError("Board.OnHeroTrayTextureLoaded() loaded texture is null!");
			}
			else if ((Player.Side)callbackData == Player.Side.FRIENDLY)
			{
				AssetHandle.Set(ref m_friendlyHeroTrayTexture, texture);
				Material material = m_FriendlyHeroTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
				material.mainTexture = m_friendlyHeroTrayTexture;
				material.color = m_TrayTint;
			}
			else
			{
				AssetHandle.Set(ref m_opponentHeroTrayTexture, texture);
				Material material2 = m_OpponentHeroTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
				material2.mainTexture = m_opponentHeroTrayTexture;
				material2.color = m_TrayTint;
			}
		}
	}

	private void OnHeroPhoneTrayTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		using (texture)
		{
			if (!texture)
			{
				Debug.LogError("Board.OnHeroTrayTextureLoaded() loaded texture is null!");
			}
			else if ((Player.Side)callbackData == Player.Side.FRIENDLY)
			{
				if (m_FriendlyHeroPhoneTray == null)
				{
					Debug.LogWarning("Friendly Hero Phone Tray Object on Board is null!");
					return;
				}
				AssetHandle.Set(ref m_friendlyHeroPhoneTrayTexture, texture);
				Material material = m_FriendlyHeroPhoneTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
				material.mainTexture = m_friendlyHeroPhoneTrayTexture;
				material.color = m_TrayTint;
			}
			else if (m_OpponentHeroPhoneTray == null)
			{
				Debug.LogWarning("Opponent Hero Phone Tray Object on Board is null!");
			}
			else
			{
				AssetHandle.Set(ref m_opponentHeroPhoneTrayTexture, texture);
				Material material2 = m_OpponentHeroPhoneTray.GetComponentInChildren<MeshRenderer>().GetMaterial();
				material2.mainTexture = m_opponentHeroPhoneTrayTexture;
				material2.color = m_TrayTint;
			}
		}
	}

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
		}
		else if ((Player.Side)callbackData == Player.Side.FRIENDLY)
		{
			go.transform.parent = base.transform;
			go.transform.position = FindBone("CustomSocketIn_Friendly").position;
			m_FriendlyTraySpellEffect = component;
		}
		else
		{
			go.transform.parent = base.transform;
			go.transform.position = FindBone("CustomSocketIn_Opposing").position;
			m_OpponentTraySpellEffect = component;
		}
	}

	private void LoadBoardSpecialEvent(BoardSpecialEvents boardSpecialEvent)
	{
		if (AssetLoader.Get().InstantiatePrefab(boardSpecialEvent.Prefab) == null)
		{
			Debug.LogWarning($"Failed to load special board event: {boardSpecialEvent.Prefab}");
		}
		m_AmbientColor = boardSpecialEvent.AmbientColorOverride;
	}
}
