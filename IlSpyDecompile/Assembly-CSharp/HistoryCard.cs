using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryCard : HistoryItem
{
	public UberText m_createdByText;

	private readonly Color OPPONENT_COLOR = new Color(0.7137f, 0.2f, 0.1333f, 1f);

	private readonly Color FRIENDLY_COLOR = new Color(0.6509f, 0.6705f, 0.9843f, 1f);

	private const float ABILITY_CARD_ANIMATE_TO_BIG_CARD_AREA_TIME = 1f;

	private const float BIG_CARD_SCALE = 1.03f;

	private const float MOUSE_OVER_Z_OFFSET_TOP = -1.404475f;

	private const float MOUSE_OVER_Z_OFFSET_BOTTOM = 0.1681719f;

	private const float MOUSE_OVER_Z_OFFSET_PHONE = -4.75f;

	private const float MOUSE_OVER_Z_OFFSET_SECRET_PHONE = -4.3f;

	private const float MOUSE_OVER_Z_OFFSET_WITH_CREATOR_PHONE = -4.3f;

	private const float MOUSE_OVER_HEIGHT_OFFSET = 7.524521f;

	private PlatformDependentValue<float> MOUSE_OVER_X_OFFSET = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 4.326718f,
		Tablet = 4.7f,
		Phone = 5.4f
	};

	private PlatformDependentValue<float> MOUSE_OVER_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 1f,
		Tablet = 1f,
		Phone = 1f
	};

	private PlatformDependentValue<float> X_SIZE_OF_MOUSE_OVER_CHILD = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 2.5f,
		Tablet = 2.5f,
		Phone = 2.5f
	};

	private const float MAX_WIDTH_OF_CHILDREN = 5f;

	private const string CREATED_BY_BONE_NAME = "HistoryCreatedByBone";

	private Material m_fullTileMaterial;

	private Material m_halfTileMaterial;

	private bool m_mousedOver;

	private bool m_halfSize;

	private bool m_hasBeenShown;

	private Actor m_separator;

	private bool m_haveDisplayedCreator;

	private bool m_gameEntityMousedOver;

	private List<HistoryInfo> m_childInfos;

	private List<HistoryChildCard> m_historyChildren = new List<HistoryChildCard>();

	private bool m_bigCardFinishedCallbackHasRun;

	private HistoryManager.BigCardFinishedCallback m_bigCardFinishedCallback;

	private bool m_bigCardCountered;

	private bool m_bigCardWaitingForSecret;

	private bool m_bigCardFromMetaData;

	private Entity m_bigCardPostTransformedEntity;

	private float m_tileSize;

	private int m_displayTimeMS;

	private HistoryInfoType m_historyInfoType;

	public void LoadMainCardActor()
	{
		string text = (m_fatigue ? "Card_Hand_Fatigue.prefab:ae394ca0bb29a964eb4c7eeb555f2fae" : ((!m_burned) ? ActorNames.GetHistoryActor(m_entity, m_historyInfoType) : "Card_Hand_BurnAway.prefab:869912636c30bc244bace332571afc94"));
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadMainCardActor() - FAILED to load actor \"{0}\"", text);
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadMainCardActor() - ERROR actor \"{0}\" has no Actor component", text);
			return;
		}
		m_mainCardActor = component;
		if (m_fatigue)
		{
			m_mainCardActor.GetPowersText().Text = GameStrings.Get("GAMEPLAY_FATIGUE_HISTORY_TEXT");
		}
		else if (m_burned)
		{
			m_mainCardActor.GetPowersText().Text = GameStrings.Get("GAMEPLAY_BURNED_CARDS_HISTORY_TEXT");
		}
		else
		{
			m_mainCardActor.SetCardDefFromEntity(m_entity);
			m_mainCardActor.SetPremium(m_entity.GetPremiumType());
			m_mainCardActor.SetWatermarkCardSetOverride(m_entity.GetWatermarkCardSetOverride());
		}
		m_mainCardActor.SetHistoryItem(this);
		m_mainCardActor.UpdateAllComponents();
		InitDisplayedCreator();
	}

	private void InitDisplayedCreator()
	{
		if (m_entity == null)
		{
			return;
		}
		string displayedCreatorName = m_entity.GetDisplayedCreatorName();
		if (!string.IsNullOrEmpty(displayedCreatorName))
		{
			GameObject gameObject = m_mainCardActor.FindBone("HistoryCreatedByBone");
			if (!gameObject)
			{
				Error.AddDevWarning("Missing Bone", "Missing {0} on {1}", "HistoryCreatedByBone", m_mainCardActor);
				return;
			}
			m_createdByText.Text = GameStrings.Format("GAMEPLAY_HISTORY_CREATED_BY", displayedCreatorName);
			m_createdByText.transform.parent = m_mainCardActor.GetRootObject().transform;
			m_createdByText.gameObject.SetActive(value: true);
			TransformUtil.SetPoint(m_createdByText, new Vector3(0.5f, 0f, 1f), gameObject, new Vector3(0.5f, 0f, 0f));
			m_createdByText.gameObject.SetActive(value: false);
			m_haveDisplayedCreator = true;
		}
	}

	private void ShowDisplayedCreator()
	{
		m_createdByText.gameObject.SetActive(m_haveDisplayedCreator);
	}

	public bool HasBeenShown()
	{
		return m_hasBeenShown;
	}

	public void MarkAsShown()
	{
		if (!m_hasBeenShown)
		{
			m_hasBeenShown = true;
		}
	}

	public bool IsHalfSize()
	{
		return m_halfSize;
	}

	public float GetTileSize()
	{
		return m_tileSize;
	}

	public void LoadTile(HistoryTileInitInfo info)
	{
		m_childInfos = info.m_childInfos;
		if (info.m_fatigueTexture != null)
		{
			m_portraitTexture = info.m_fatigueTexture;
			m_fatigue = true;
		}
		else if (info.m_burnedCardsTexture != null)
		{
			m_portraitTexture = info.m_burnedCardsTexture;
			m_burned = true;
		}
		else
		{
			m_entity = info.m_entity;
			m_portraitTexture = info.m_portraitTexture;
			m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
			SetCardDef(info.m_cardDef);
			m_fullTileMaterial = info.m_fullTileMaterial;
			m_halfTileMaterial = info.m_halfTileMaterial;
			m_splatAmount = info.m_splatAmount;
			m_isPoisonous = info.m_isPoisonous;
			m_dead = info.m_dead;
		}
		m_historyInfoType = info.m_type;
		switch (info.m_type)
		{
		case HistoryInfoType.NONE:
		case HistoryInfoType.WEAPON_PLAYED:
		case HistoryInfoType.CARD_PLAYED:
		case HistoryInfoType.FATIGUE:
		case HistoryInfoType.BURNED_CARDS:
			LoadPlayTile();
			break;
		case HistoryInfoType.ATTACK:
			LoadAttackTile();
			break;
		case HistoryInfoType.TRIGGER:
			LoadTriggerTile();
			break;
		case HistoryInfoType.WEAPON_BREAK:
			LoadWeaponBreak();
			break;
		}
	}

	public void NotifyMousedOver()
	{
		if (!m_mousedOver && !(this == HistoryManager.Get().GetCurrentBigCard()))
		{
			LoadChildCardsFromInfos();
			m_mousedOver = true;
			SoundManager.Get().LoadAndPlay("history_event_mouseover.prefab:0bc4f1638257a264a9b02e811c0a61b5", m_tileActor.gameObject);
			if (!m_mainCardActor)
			{
				LoadMainCardActor();
				SceneUtils.SetLayer(m_mainCardActor, GameLayer.Tooltip);
			}
			ShowTile();
		}
	}

	public void NotifyMousedOut()
	{
		if (!m_mousedOver)
		{
			return;
		}
		m_mousedOver = false;
		if (m_gameEntityMousedOver)
		{
			GameState.Get().GetGameEntity().NotifyOfHistoryTokenMousedOut();
			m_gameEntityMousedOver = false;
		}
		TooltipPanelManager.Get().HideKeywordHelp();
		if ((bool)m_mainCardActor)
		{
			m_mainCardActor.ActivateAllSpellsDeathStates();
			m_mainCardActor.Hide();
		}
		for (int i = 0; i < m_historyChildren.Count; i++)
		{
			if (!(m_historyChildren[i].m_mainCardActor == null))
			{
				m_historyChildren[i].m_mainCardActor.ActivateAllSpellsDeathStates();
				m_historyChildren[i].m_mainCardActor.Hide();
			}
		}
		if ((bool)m_separator)
		{
			m_separator.Hide();
		}
		HistoryManager.Get().UpdateLayout();
	}

	private void LoadPlayTile()
	{
		m_halfSize = false;
		LoadTileImpl("HistoryTile_Card.prefab:df3002d4532e4dd40b37101e83202db4");
		LoadArrowSeparator();
	}

	private void LoadAttackTile()
	{
		m_halfSize = true;
		LoadTileImpl("HistoryTile_Attack.prefab:816bc6c1f4d8f0c439e981d30bf5b57b");
		LoadSwordsSeparator();
	}

	private void LoadWeaponBreak()
	{
		m_halfSize = true;
		LoadTileImpl("HistoryTile_Attack.prefab:816bc6c1f4d8f0c439e981d30bf5b57b");
	}

	private void LoadTriggerTile()
	{
		m_halfSize = true;
		LoadTileImpl("HistoryTile_Trigger.prefab:14cb236519ac3744b8c7c1274a379c94");
		LoadArrowSeparator();
	}

	private void LoadTileImpl(string actorPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - FAILED to load actor \"{0}\"", actorPath);
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - ERROR actor \"{0}\" has no Actor component", actorPath);
			return;
		}
		m_tileActor = component;
		m_tileActor.transform.parent = base.transform;
		TransformUtil.Identity(m_tileActor.transform);
		m_tileActor.transform.localScale = HistoryManager.Get().transform.localScale;
		Material[] array = new Material[2]
		{
			m_tileActor.GetMeshRenderer().GetMaterial(),
			null
		};
		if (m_halfSize)
		{
			if (m_halfTileMaterial != null)
			{
				array[1] = m_halfTileMaterial;
				m_tileActor.GetMeshRenderer().SetMaterials(array);
			}
			else
			{
				m_tileActor.GetMeshRenderer().GetMaterial(1).mainTexture = m_portraitTexture;
			}
		}
		else if (m_fullTileMaterial != null)
		{
			array[1] = m_fullTileMaterial;
			m_tileActor.GetMeshRenderer().SetMaterials(array);
		}
		else
		{
			m_tileActor.GetMeshRenderer().GetMaterial(1).mainTexture = m_portraitTexture;
		}
		Color color = Color.white;
		if (Board.Get() != null)
		{
			color = Board.Get().m_HistoryTileColor;
		}
		if (!m_fatigue && !m_burned)
		{
			if (m_entity.IsControlledByFriendlySidePlayer())
			{
				color *= FRIENDLY_COLOR;
			}
			else
			{
				color *= OPPONENT_COLOR;
			}
		}
		else if (AffectsFriendlySidePlayer())
		{
			color *= FRIENDLY_COLOR;
		}
		else
		{
			color *= OPPONENT_COLOR;
		}
		Renderer[] componentsInChildren = m_tileActor.GetMeshRenderer().GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			if (!(renderer.tag == "FakeShadow"))
			{
				renderer.GetMaterial().color = Board.Get().m_HistoryTileColor;
			}
		}
		List<Material> materials = m_tileActor.GetMeshRenderer().GetMaterials();
		materials[0].color = color;
		materials[1].color = Board.Get().m_HistoryTileColor;
		if (GetTileCollider() != null)
		{
			m_tileSize = GetTileCollider().bounds.size.z;
		}
	}

	private bool AffectsFriendlySidePlayer()
	{
		if (m_childInfos == null)
		{
			return false;
		}
		if (m_childInfos.Count == 0)
		{
			return false;
		}
		if (m_childInfos[0] == null)
		{
			return false;
		}
		if (m_childInfos[0].GetDuplicatedEntity() != null && m_childInfos[0].GetDuplicatedEntity().IsControlledByFriendlySidePlayer())
		{
			return true;
		}
		return false;
	}

	private void LoadSwordsSeparator()
	{
		LoadSeparator("History_Swords.prefab:361feac100313e443b68055167e5088c");
	}

	private void LoadArrowSeparator()
	{
		if (m_childInfos != null && m_childInfos.Count != 0)
		{
			LoadSeparator("History_Arrow.prefab:a9ef1ff267ab0a24c9cdef7f3678b5a4");
		}
	}

	private void LoadSeparator(string actorPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning($"HistoryCard.LoadSeparator() - FAILED to load actor \"{actorPath}\"");
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"HistoryCard.LoadSeparator() - ERROR actor \"{actorPath}\" has no Actor component");
			return;
		}
		m_separator = component;
		MeshRenderer component2 = m_separator.GetRootObject().transform.Find("Blue").gameObject.GetComponent<MeshRenderer>();
		MeshRenderer component3 = m_separator.GetRootObject().transform.Find("Red").gameObject.GetComponent<MeshRenderer>();
		if (m_fatigue || m_burned)
		{
			component3.enabled = true;
			component2.enabled = false;
		}
		else
		{
			bool flag2 = (component2.enabled = m_entity.IsControlledByFriendlySidePlayer());
			component3.enabled = !flag2;
		}
		m_separator.transform.parent = base.transform;
		TransformUtil.Identity(m_separator.transform);
		if (m_separator.GetRootObject() != null)
		{
			TransformUtil.Identity(m_separator.GetRootObject().transform);
		}
		m_separator.Hide();
	}

	private void LoadChildCardsFromInfos()
	{
		if (m_childInfos == null)
		{
			return;
		}
		foreach (HistoryInfo childInfo in m_childInfos)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("HistoryChildCard.prefab:f85dbd296f9764f4e9c6a2c638a024d3", AssetLoadingOptions.IgnorePrefabPosition);
			HistoryChildCard component = gameObject.GetComponent<HistoryChildCard>();
			Entity duplicatedEntity = childInfo.GetDuplicatedEntity();
			if (duplicatedEntity == null)
			{
				Log.Gameplay.PrintError("{0}.LoadChildCardsFromInfos(): childInfo {1} has a null duplicated entity!", this, childInfo);
				continue;
			}
			using DefLoader.DisposableCardDef disposableCardDef = duplicatedEntity.ShareDisposableCardDef();
			if (!(disposableCardDef?.CardDef == null))
			{
				component.SetCardInfo(duplicatedEntity, disposableCardDef, childInfo.GetSplatAmount(), childInfo.HasDied(), childInfo.m_isBurnedCard, childInfo.m_isPoisonous);
				component.transform.parent = base.transform;
				component.LoadMainCardActor();
				Actor componentInChildren = gameObject.GetComponentInChildren<Actor>();
				if (!(componentInChildren == null))
				{
					m_historyChildren.Add(component);
					componentInChildren.SetEntity(duplicatedEntity);
					componentInChildren.SetCardDef(disposableCardDef);
					componentInChildren.UpdateAllComponents();
				}
			}
		}
		m_childInfos = null;
	}

	private void ShowTile()
	{
		if (!m_mousedOver)
		{
			m_mainCardActor.Hide();
			return;
		}
		m_mainCardActor.Show();
		ShowDisplayedCreator();
		InitializeMainCardActor();
		DisplaySpells();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_mainCardActor.transform.position = new Vector3(base.transform.position.x + (float)MOUSE_OVER_X_OFFSET, base.transform.position.y + 7.524521f, GetZOffsetForThisTilesMouseOverCard());
		}
		else
		{
			m_mainCardActor.transform.position = new Vector3(base.transform.position.x + (float)MOUSE_OVER_X_OFFSET, base.transform.position.y + 7.524521f, base.transform.position.z + GetZOffsetForThisTilesMouseOverCard());
		}
		m_mainCardActor.transform.localScale = new Vector3(MOUSE_OVER_SCALE, 1f, MOUSE_OVER_SCALE);
		if ((bool)UniversalInputManager.UsePhoneUI && (m_fatigue || m_burned))
		{
			m_mainCardActor.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (!m_gameEntityMousedOver)
		{
			m_gameEntityMousedOver = true;
			GameState.Get().GetGameEntity().NotifyOfHistoryTokenMousedOver(base.gameObject);
		}
		if (!m_fatigue && !m_burned)
		{
			TooltipPanelManager.Get().UpdateKeywordHelpForHistoryCard(m_entity, m_mainCardActor, m_createdByText);
		}
		if (m_historyChildren.Count <= 0)
		{
			return;
		}
		float max = 1f;
		float num = 1f;
		if (m_historyChildren.Count > 4 && m_historyChildren.Count < 9)
		{
			num = 2f;
			max = 0.5f;
		}
		else if (m_historyChildren.Count >= 9)
		{
			num = 3f;
			max = 0.3f;
		}
		int num2 = Mathf.CeilToInt((float)m_historyChildren.Count / num);
		float num3 = (float)num2 * (float)X_SIZE_OF_MOUSE_OVER_CHILD;
		float value = 5f / num3;
		value = Mathf.Clamp(value, 0.1f, max);
		int num4 = 0;
		int num5 = 1;
		for (int i = 0; i < m_historyChildren.Count; i++)
		{
			m_historyChildren[i].m_mainCardActor.Show();
			m_historyChildren[i].InitializeMainCardActor();
			m_historyChildren[i].DisplaySpells();
			m_historyChildren[i].m_mainCardActor.UpdateAllComponents();
			float num6 = m_mainCardActor.transform.position.z;
			if (num == 2f)
			{
				num6 = ((num5 != 1) ? (num6 - 0.78f) : (num6 + 0.78f));
			}
			else if (num == 3f)
			{
				switch (num5)
				{
				case 1:
					num6 += 0.98f;
					break;
				case 3:
					num6 -= 0.93f;
					break;
				}
			}
			float num7 = m_mainCardActor.transform.position.x + (float)X_SIZE_OF_MOUSE_OVER_CHILD * (1f + value) / 2f;
			m_historyChildren[i].m_mainCardActor.transform.position = new Vector3(num7 + (float)X_SIZE_OF_MOUSE_OVER_CHILD * (float)num4 * value, m_mainCardActor.transform.position.y, num6);
			m_historyChildren[i].m_mainCardActor.transform.localScale = new Vector3(value, value, value);
			num4++;
			if (num4 >= num2)
			{
				num4 = 0;
				num5++;
			}
		}
		if (m_separator != null)
		{
			float num8 = 0.4f;
			float num9 = (float)X_SIZE_OF_MOUSE_OVER_CHILD / 2f;
			m_separator.Show();
			m_separator.transform.position = new Vector3(m_mainCardActor.transform.position.x + num9, m_mainCardActor.transform.position.y + num8, m_mainCardActor.transform.position.z);
		}
	}

	private float GetZOffsetForThisTilesMouseOverCard()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_entity != null && m_entity.IsSecret() && m_entity.IsHidden())
			{
				return -4.3f;
			}
			if (m_haveDisplayedCreator)
			{
				return -4.3f;
			}
			return -4.75f;
		}
		float num = Mathf.Abs(-1.57264686f);
		HistoryManager historyManager = HistoryManager.Get();
		float num2 = num / (float)historyManager.GetNumHistoryTiles();
		int num3 = historyManager.GetNumHistoryTiles() - historyManager.GetIndexForTile(this) - 1;
		return -1.404475f + num2 * (float)num3;
	}

	public void LoadBigCard(HistoryBigCardInitInfo info)
	{
		m_entity = info.m_entity;
		m_historyInfoType = info.m_historyInfoType;
		m_portraitTexture = info.m_portraitTexture;
		SetCardDef(info.m_cardDef);
		m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
		m_bigCardFinishedCallback = info.m_finishedCallback;
		m_bigCardCountered = info.m_countered;
		m_bigCardWaitingForSecret = info.m_waitForSecretSpell;
		m_bigCardFromMetaData = info.m_fromMetaData;
		m_bigCardPostTransformedEntity = info.m_postTransformedEntity;
		m_displayTimeMS = info.m_displayTimeMS;
		LoadMainCardActor();
	}

	public void LoadBigCardPostTransformedEntity()
	{
		if (m_bigCardPostTransformedEntity != null)
		{
			m_entity = m_bigCardPostTransformedEntity;
			Card card = m_entity.GetCard();
			m_portraitTexture = card.GetPortraitTexture();
			m_portraitGoldenMaterial = card.GetGoldenMaterial();
			using (DefLoader.DisposableCardDef cardDef = card.ShareDisposableCardDef())
			{
				SetCardDef(cardDef);
			}
			LoadMainCardActor();
		}
	}

	public HistoryManager.BigCardFinishedCallback GetBigCardFinishedCallback()
	{
		return m_bigCardFinishedCallback;
	}

	public void RunBigCardFinishedCallback()
	{
		if (!m_bigCardFinishedCallbackHasRun)
		{
			m_bigCardFinishedCallbackHasRun = true;
			if (m_bigCardFinishedCallback != null)
			{
				m_bigCardFinishedCallback();
			}
		}
	}

	public bool WasBigCardCountered()
	{
		return m_bigCardCountered;
	}

	public int GetDisplayTimeMS()
	{
		return m_displayTimeMS;
	}

	public bool IsBigCardWaitingForSecret()
	{
		return m_bigCardWaitingForSecret;
	}

	public bool IsBigCardFromMetaData()
	{
		return m_bigCardFromMetaData;
	}

	public Entity GetBigCardPostTransformedEntity()
	{
		return m_bigCardPostTransformedEntity;
	}

	public bool HasBigCardPostTransformedEntity()
	{
		return m_bigCardPostTransformedEntity != null;
	}

	public void ShowBigCard(Vector3[] pathToFollow)
	{
		float num = 1f;
		if (m_displayTimeMS > 0)
		{
			float b = (float)m_displayTimeMS / 1000f;
			num = Mathf.Min(1f, b);
		}
		m_mainCardActor.transform.localScale = new Vector3(1.03f, 1.03f, 1.03f);
		Entity entity = m_entity;
		if (HasBigCardPostTransformedEntity())
		{
			entity = m_bigCardPostTransformedEntity;
		}
		if (entity != null)
		{
			if (entity.GetCardType() == TAG_CARDTYPE.SPELL || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER || m_bigCardFromMetaData)
			{
				pathToFollow[0] = m_mainCardActor.transform.position;
				Hashtable args = iTween.Hash("path", pathToFollow, "time", num, "oncomplete", "OnBigCardPathComplete", "oncompletetarget", base.gameObject);
				iTween.MoveTo(m_mainCardActor.gameObject, args);
				iTween.ScaleTo(base.gameObject, new Vector3(1f, 1f, 1f), num);
				SoundManager.Get().LoadAndPlay("play_card_from_hand_1.prefab:ac4be75e319a97947a68308a08e54e88");
			}
			else
			{
				ShowDisplayedCreator();
			}
		}
	}

	private void OnBigCardPathComplete()
	{
		ShowDisplayedCreator();
	}
}
