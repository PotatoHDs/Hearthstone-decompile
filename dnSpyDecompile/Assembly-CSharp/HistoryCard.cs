using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class HistoryCard : HistoryItem
{
	// Token: 0x06002C4E RID: 11342 RVA: 0x000DE8B0 File Offset: 0x000DCAB0
	public void LoadMainCardActor()
	{
		string text;
		if (this.m_fatigue)
		{
			text = "Card_Hand_Fatigue.prefab:ae394ca0bb29a964eb4c7eeb555f2fae";
		}
		else if (this.m_burned)
		{
			text = "Card_Hand_BurnAway.prefab:869912636c30bc244bace332571afc94";
		}
		else
		{
			text = ActorNames.GetHistoryActor(this.m_entity, this.m_historyInfoType);
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadMainCardActor() - FAILED to load actor \"{0}\"", new object[]
			{
				text
			});
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadMainCardActor() - ERROR actor \"{0}\" has no Actor component", new object[]
			{
				text
			});
			return;
		}
		this.m_mainCardActor = component;
		if (this.m_fatigue)
		{
			this.m_mainCardActor.GetPowersText().Text = GameStrings.Get("GAMEPLAY_FATIGUE_HISTORY_TEXT");
		}
		else if (this.m_burned)
		{
			this.m_mainCardActor.GetPowersText().Text = GameStrings.Get("GAMEPLAY_BURNED_CARDS_HISTORY_TEXT");
		}
		else
		{
			this.m_mainCardActor.SetCardDefFromEntity(this.m_entity);
			this.m_mainCardActor.SetPremium(this.m_entity.GetPremiumType());
			this.m_mainCardActor.SetWatermarkCardSetOverride(this.m_entity.GetWatermarkCardSetOverride());
		}
		this.m_mainCardActor.SetHistoryItem(this);
		this.m_mainCardActor.UpdateAllComponents();
		this.InitDisplayedCreator();
	}

	// Token: 0x06002C4F RID: 11343 RVA: 0x000DE9F0 File Offset: 0x000DCBF0
	private void InitDisplayedCreator()
	{
		if (this.m_entity == null)
		{
			return;
		}
		string displayedCreatorName = this.m_entity.GetDisplayedCreatorName();
		if (string.IsNullOrEmpty(displayedCreatorName))
		{
			return;
		}
		GameObject gameObject = this.m_mainCardActor.FindBone("HistoryCreatedByBone");
		if (!gameObject)
		{
			Error.AddDevWarning("Missing Bone", "Missing {0} on {1}", new object[]
			{
				"HistoryCreatedByBone",
				this.m_mainCardActor
			});
			return;
		}
		this.m_createdByText.Text = GameStrings.Format("GAMEPLAY_HISTORY_CREATED_BY", new object[]
		{
			displayedCreatorName
		});
		this.m_createdByText.transform.parent = this.m_mainCardActor.GetRootObject().transform;
		this.m_createdByText.gameObject.SetActive(true);
		TransformUtil.SetPoint(this.m_createdByText, new Vector3(0.5f, 0f, 1f), gameObject, new Vector3(0.5f, 0f, 0f));
		this.m_createdByText.gameObject.SetActive(false);
		this.m_haveDisplayedCreator = true;
	}

	// Token: 0x06002C50 RID: 11344 RVA: 0x000DEAF7 File Offset: 0x000DCCF7
	private void ShowDisplayedCreator()
	{
		this.m_createdByText.gameObject.SetActive(this.m_haveDisplayedCreator);
	}

	// Token: 0x06002C51 RID: 11345 RVA: 0x000DEB0F File Offset: 0x000DCD0F
	public bool HasBeenShown()
	{
		return this.m_hasBeenShown;
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x000DEB17 File Offset: 0x000DCD17
	public void MarkAsShown()
	{
		if (this.m_hasBeenShown)
		{
			return;
		}
		this.m_hasBeenShown = true;
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x000DEB29 File Offset: 0x000DCD29
	public bool IsHalfSize()
	{
		return this.m_halfSize;
	}

	// Token: 0x06002C54 RID: 11348 RVA: 0x000DEB31 File Offset: 0x000DCD31
	public float GetTileSize()
	{
		return this.m_tileSize;
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x000DEB3C File Offset: 0x000DCD3C
	public void LoadTile(HistoryTileInitInfo info)
	{
		this.m_childInfos = info.m_childInfos;
		if (info.m_fatigueTexture != null)
		{
			this.m_portraitTexture = info.m_fatigueTexture;
			this.m_fatigue = true;
		}
		else if (info.m_burnedCardsTexture != null)
		{
			this.m_portraitTexture = info.m_burnedCardsTexture;
			this.m_burned = true;
		}
		else
		{
			this.m_entity = info.m_entity;
			this.m_portraitTexture = info.m_portraitTexture;
			this.m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
			base.SetCardDef(info.m_cardDef);
			this.m_fullTileMaterial = info.m_fullTileMaterial;
			this.m_halfTileMaterial = info.m_halfTileMaterial;
			this.m_splatAmount = info.m_splatAmount;
			this.m_isPoisonous = info.m_isPoisonous;
			this.m_dead = info.m_dead;
		}
		this.m_historyInfoType = info.m_type;
		switch (info.m_type)
		{
		case HistoryInfoType.NONE:
		case HistoryInfoType.WEAPON_PLAYED:
		case HistoryInfoType.CARD_PLAYED:
		case HistoryInfoType.FATIGUE:
		case HistoryInfoType.BURNED_CARDS:
			this.LoadPlayTile();
			return;
		case HistoryInfoType.ATTACK:
			this.LoadAttackTile();
			return;
		case HistoryInfoType.TRIGGER:
			this.LoadTriggerTile();
			return;
		case HistoryInfoType.WEAPON_BREAK:
			this.LoadWeaponBreak();
			return;
		default:
			return;
		}
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x000DEC60 File Offset: 0x000DCE60
	public void NotifyMousedOver()
	{
		if (this.m_mousedOver)
		{
			return;
		}
		if (this == HistoryManager.Get().GetCurrentBigCard())
		{
			return;
		}
		this.LoadChildCardsFromInfos();
		this.m_mousedOver = true;
		SoundManager.Get().LoadAndPlay("history_event_mouseover.prefab:0bc4f1638257a264a9b02e811c0a61b5", this.m_tileActor.gameObject);
		if (!this.m_mainCardActor)
		{
			this.LoadMainCardActor();
			SceneUtils.SetLayer(this.m_mainCardActor, GameLayer.Tooltip);
		}
		this.ShowTile();
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x000DECDC File Offset: 0x000DCEDC
	public void NotifyMousedOut()
	{
		if (!this.m_mousedOver)
		{
			return;
		}
		this.m_mousedOver = false;
		if (this.m_gameEntityMousedOver)
		{
			GameState.Get().GetGameEntity().NotifyOfHistoryTokenMousedOut();
			this.m_gameEntityMousedOver = false;
		}
		TooltipPanelManager.Get().HideKeywordHelp();
		if (this.m_mainCardActor)
		{
			this.m_mainCardActor.ActivateAllSpellsDeathStates();
			this.m_mainCardActor.Hide();
		}
		for (int i = 0; i < this.m_historyChildren.Count; i++)
		{
			if (!(this.m_historyChildren[i].m_mainCardActor == null))
			{
				this.m_historyChildren[i].m_mainCardActor.ActivateAllSpellsDeathStates();
				this.m_historyChildren[i].m_mainCardActor.Hide();
			}
		}
		if (this.m_separator)
		{
			this.m_separator.Hide();
		}
		HistoryManager.Get().UpdateLayout();
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x000DEDC1 File Offset: 0x000DCFC1
	private void LoadPlayTile()
	{
		this.m_halfSize = false;
		this.LoadTileImpl("HistoryTile_Card.prefab:df3002d4532e4dd40b37101e83202db4");
		this.LoadArrowSeparator();
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x000DEDDB File Offset: 0x000DCFDB
	private void LoadAttackTile()
	{
		this.m_halfSize = true;
		this.LoadTileImpl("HistoryTile_Attack.prefab:816bc6c1f4d8f0c439e981d30bf5b57b");
		this.LoadSwordsSeparator();
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x000DEDF5 File Offset: 0x000DCFF5
	private void LoadWeaponBreak()
	{
		this.m_halfSize = true;
		this.LoadTileImpl("HistoryTile_Attack.prefab:816bc6c1f4d8f0c439e981d30bf5b57b");
	}

	// Token: 0x06002C5B RID: 11355 RVA: 0x000DEE09 File Offset: 0x000DD009
	private void LoadTriggerTile()
	{
		this.m_halfSize = true;
		this.LoadTileImpl("HistoryTile_Trigger.prefab:14cb236519ac3744b8c7c1274a379c94");
		this.LoadArrowSeparator();
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x000DEE24 File Offset: 0x000DD024
	private void LoadTileImpl(string actorPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - FAILED to load actor \"{0}\"", new object[]
			{
				actorPath
			});
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - ERROR actor \"{0}\" has no Actor component", new object[]
			{
				actorPath
			});
			return;
		}
		this.m_tileActor = component;
		this.m_tileActor.transform.parent = base.transform;
		TransformUtil.Identity(this.m_tileActor.transform);
		this.m_tileActor.transform.localScale = HistoryManager.Get().transform.localScale;
		Material[] array = new Material[2];
		array[0] = this.m_tileActor.GetMeshRenderer(false).GetMaterial();
		if (this.m_halfSize)
		{
			if (this.m_halfTileMaterial != null)
			{
				array[1] = this.m_halfTileMaterial;
				this.m_tileActor.GetMeshRenderer(false).SetMaterials(array);
			}
			else
			{
				this.m_tileActor.GetMeshRenderer(false).GetMaterial(1).mainTexture = this.m_portraitTexture;
			}
		}
		else if (this.m_fullTileMaterial != null)
		{
			array[1] = this.m_fullTileMaterial;
			this.m_tileActor.GetMeshRenderer(false).SetMaterials(array);
		}
		else
		{
			this.m_tileActor.GetMeshRenderer(false).GetMaterial(1).mainTexture = this.m_portraitTexture;
		}
		Color color = Color.white;
		if (Board.Get() != null)
		{
			color = Board.Get().m_HistoryTileColor;
		}
		if (!this.m_fatigue && !this.m_burned)
		{
			if (this.m_entity.IsControlledByFriendlySidePlayer())
			{
				color *= this.FRIENDLY_COLOR;
			}
			else
			{
				color *= this.OPPONENT_COLOR;
			}
		}
		else if (this.AffectsFriendlySidePlayer())
		{
			color *= this.FRIENDLY_COLOR;
		}
		else
		{
			color *= this.OPPONENT_COLOR;
		}
		foreach (Renderer renderer in this.m_tileActor.GetMeshRenderer(false).GetComponentsInChildren<Renderer>())
		{
			if (!(renderer.tag == "FakeShadow"))
			{
				renderer.GetMaterial().color = Board.Get().m_HistoryTileColor;
			}
		}
		List<Material> materials = this.m_tileActor.GetMeshRenderer(false).GetMaterials();
		materials[0].color = color;
		materials[1].color = Board.Get().m_HistoryTileColor;
		if (base.GetTileCollider() != null)
		{
			this.m_tileSize = base.GetTileCollider().bounds.size.z;
		}
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x000DF0BC File Offset: 0x000DD2BC
	private bool AffectsFriendlySidePlayer()
	{
		return this.m_childInfos != null && this.m_childInfos.Count != 0 && this.m_childInfos[0] != null && (this.m_childInfos[0].GetDuplicatedEntity() != null && this.m_childInfos[0].GetDuplicatedEntity().IsControlledByFriendlySidePlayer());
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x000DF120 File Offset: 0x000DD320
	private void LoadSwordsSeparator()
	{
		this.LoadSeparator("History_Swords.prefab:361feac100313e443b68055167e5088c");
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x000DF12D File Offset: 0x000DD32D
	private void LoadArrowSeparator()
	{
		if (this.m_childInfos == null)
		{
			return;
		}
		if (this.m_childInfos.Count == 0)
		{
			return;
		}
		this.LoadSeparator("History_Arrow.prefab:a9ef1ff267ab0a24c9cdef7f3678b5a4");
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x000DF154 File Offset: 0x000DD354
	private void LoadSeparator(string actorPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning(string.Format("HistoryCard.LoadSeparator() - FAILED to load actor \"{0}\"", actorPath));
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("HistoryCard.LoadSeparator() - ERROR actor \"{0}\" has no Actor component", actorPath));
			return;
		}
		this.m_separator = component;
		MeshRenderer component2 = this.m_separator.GetRootObject().transform.Find("Blue").gameObject.GetComponent<MeshRenderer>();
		MeshRenderer component3 = this.m_separator.GetRootObject().transform.Find("Red").gameObject.GetComponent<MeshRenderer>();
		if (this.m_fatigue || this.m_burned)
		{
			component3.enabled = true;
			component2.enabled = false;
		}
		else
		{
			bool flag = this.m_entity.IsControlledByFriendlySidePlayer();
			component2.enabled = flag;
			component3.enabled = !flag;
		}
		this.m_separator.transform.parent = base.transform;
		TransformUtil.Identity(this.m_separator.transform);
		if (this.m_separator.GetRootObject() != null)
		{
			TransformUtil.Identity(this.m_separator.GetRootObject().transform);
		}
		this.m_separator.Hide();
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x000DF298 File Offset: 0x000DD498
	private void LoadChildCardsFromInfos()
	{
		if (this.m_childInfos == null)
		{
			return;
		}
		foreach (HistoryInfo historyInfo in this.m_childInfos)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("HistoryChildCard.prefab:f85dbd296f9764f4e9c6a2c638a024d3", AssetLoadingOptions.IgnorePrefabPosition);
			HistoryChildCard component = gameObject.GetComponent<HistoryChildCard>();
			Entity duplicatedEntity = historyInfo.GetDuplicatedEntity();
			if (duplicatedEntity == null)
			{
				Log.Gameplay.PrintError("{0}.LoadChildCardsFromInfos(): childInfo {1} has a null duplicated entity!", new object[]
				{
					this,
					historyInfo
				});
			}
			else
			{
				using (DefLoader.DisposableCardDef disposableCardDef = duplicatedEntity.ShareDisposableCardDef())
				{
					if (!(((disposableCardDef != null) ? disposableCardDef.CardDef : null) == null))
					{
						component.SetCardInfo(duplicatedEntity, disposableCardDef, historyInfo.GetSplatAmount(), historyInfo.HasDied(), historyInfo.m_isBurnedCard, historyInfo.m_isPoisonous);
						component.transform.parent = base.transform;
						component.LoadMainCardActor();
						Actor componentInChildren = gameObject.GetComponentInChildren<Actor>();
						if (!(componentInChildren == null))
						{
							this.m_historyChildren.Add(component);
							componentInChildren.SetEntity(duplicatedEntity);
							componentInChildren.SetCardDef(disposableCardDef);
							componentInChildren.UpdateAllComponents();
						}
					}
				}
			}
		}
		this.m_childInfos = null;
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x000DF410 File Offset: 0x000DD610
	private void ShowTile()
	{
		if (!this.m_mousedOver)
		{
			this.m_mainCardActor.Hide();
			return;
		}
		this.m_mainCardActor.Show();
		this.ShowDisplayedCreator();
		base.InitializeMainCardActor();
		base.DisplaySpells();
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_mainCardActor.transform.position = new Vector3(base.transform.position.x + this.MOUSE_OVER_X_OFFSET, base.transform.position.y + 7.524521f, this.GetZOffsetForThisTilesMouseOverCard());
		}
		else
		{
			this.m_mainCardActor.transform.position = new Vector3(base.transform.position.x + this.MOUSE_OVER_X_OFFSET, base.transform.position.y + 7.524521f, base.transform.position.z + this.GetZOffsetForThisTilesMouseOverCard());
		}
		this.m_mainCardActor.transform.localScale = new Vector3(this.MOUSE_OVER_SCALE, 1f, this.MOUSE_OVER_SCALE);
		if (UniversalInputManager.UsePhoneUI && (this.m_fatigue || this.m_burned))
		{
			this.m_mainCardActor.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (!this.m_gameEntityMousedOver)
		{
			this.m_gameEntityMousedOver = true;
			GameState.Get().GetGameEntity().NotifyOfHistoryTokenMousedOver(base.gameObject);
		}
		if (!this.m_fatigue && !this.m_burned)
		{
			TooltipPanelManager.Get().UpdateKeywordHelpForHistoryCard(this.m_entity, this.m_mainCardActor, this.m_createdByText);
		}
		if (this.m_historyChildren.Count <= 0)
		{
			return;
		}
		float max = 1f;
		float num = 1f;
		if (this.m_historyChildren.Count > 4 && this.m_historyChildren.Count < 9)
		{
			num = 2f;
			max = 0.5f;
		}
		else if (this.m_historyChildren.Count >= 9)
		{
			num = 3f;
			max = 0.3f;
		}
		int num2 = Mathf.CeilToInt((float)this.m_historyChildren.Count / num);
		float num3 = (float)num2 * this.X_SIZE_OF_MOUSE_OVER_CHILD;
		float num4 = 5f / num3;
		num4 = Mathf.Clamp(num4, 0.1f, max);
		int num5 = 0;
		int num6 = 1;
		for (int i = 0; i < this.m_historyChildren.Count; i++)
		{
			this.m_historyChildren[i].m_mainCardActor.Show();
			this.m_historyChildren[i].InitializeMainCardActor();
			this.m_historyChildren[i].DisplaySpells();
			this.m_historyChildren[i].m_mainCardActor.UpdateAllComponents();
			float num7 = this.m_mainCardActor.transform.position.z;
			if (num == 2f)
			{
				if (num6 == 1)
				{
					num7 += 0.78f;
				}
				else
				{
					num7 -= 0.78f;
				}
			}
			else if (num == 3f)
			{
				if (num6 == 1)
				{
					num7 += 0.98f;
				}
				else if (num6 == 3)
				{
					num7 -= 0.93f;
				}
			}
			float num8 = this.m_mainCardActor.transform.position.x + this.X_SIZE_OF_MOUSE_OVER_CHILD * (1f + num4) / 2f;
			this.m_historyChildren[i].m_mainCardActor.transform.position = new Vector3(num8 + this.X_SIZE_OF_MOUSE_OVER_CHILD * (float)num5 * num4, this.m_mainCardActor.transform.position.y, num7);
			this.m_historyChildren[i].m_mainCardActor.transform.localScale = new Vector3(num4, num4, num4);
			num5++;
			if (num5 >= num2)
			{
				num5 = 0;
				num6++;
			}
		}
		if (this.m_separator != null)
		{
			float num9 = 0.4f;
			float num10 = this.X_SIZE_OF_MOUSE_OVER_CHILD / 2f;
			this.m_separator.Show();
			this.m_separator.transform.position = new Vector3(this.m_mainCardActor.transform.position.x + num10, this.m_mainCardActor.transform.position.y + num9, this.m_mainCardActor.transform.position.z);
		}
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x000DF888 File Offset: 0x000DDA88
	private float GetZOffsetForThisTilesMouseOverCard()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			float num = Mathf.Abs(-1.5726469f);
			HistoryManager historyManager = HistoryManager.Get();
			float num2 = num / (float)historyManager.GetNumHistoryTiles();
			int num3 = historyManager.GetNumHistoryTiles() - historyManager.GetIndexForTile(this) - 1;
			return -1.404475f + num2 * (float)num3;
		}
		if (this.m_entity != null && this.m_entity.IsSecret() && this.m_entity.IsHidden())
		{
			return -4.3f;
		}
		if (this.m_haveDisplayedCreator)
		{
			return -4.3f;
		}
		return -4.75f;
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x000DF914 File Offset: 0x000DDB14
	public void LoadBigCard(HistoryBigCardInitInfo info)
	{
		this.m_entity = info.m_entity;
		this.m_historyInfoType = info.m_historyInfoType;
		this.m_portraitTexture = info.m_portraitTexture;
		base.SetCardDef(info.m_cardDef);
		this.m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
		this.m_bigCardFinishedCallback = info.m_finishedCallback;
		this.m_bigCardCountered = info.m_countered;
		this.m_bigCardWaitingForSecret = info.m_waitForSecretSpell;
		this.m_bigCardFromMetaData = info.m_fromMetaData;
		this.m_bigCardPostTransformedEntity = info.m_postTransformedEntity;
		this.m_displayTimeMS = info.m_displayTimeMS;
		this.LoadMainCardActor();
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x000DF9AC File Offset: 0x000DDBAC
	public void LoadBigCardPostTransformedEntity()
	{
		if (this.m_bigCardPostTransformedEntity == null)
		{
			return;
		}
		this.m_entity = this.m_bigCardPostTransformedEntity;
		Card card = this.m_entity.GetCard();
		this.m_portraitTexture = card.GetPortraitTexture();
		this.m_portraitGoldenMaterial = card.GetGoldenMaterial();
		using (DefLoader.DisposableCardDef disposableCardDef = card.ShareDisposableCardDef())
		{
			base.SetCardDef(disposableCardDef);
		}
		this.LoadMainCardActor();
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x000DFA24 File Offset: 0x000DDC24
	public HistoryManager.BigCardFinishedCallback GetBigCardFinishedCallback()
	{
		return this.m_bigCardFinishedCallback;
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x000DFA2C File Offset: 0x000DDC2C
	public void RunBigCardFinishedCallback()
	{
		if (this.m_bigCardFinishedCallbackHasRun)
		{
			return;
		}
		this.m_bigCardFinishedCallbackHasRun = true;
		if (this.m_bigCardFinishedCallback != null)
		{
			this.m_bigCardFinishedCallback();
		}
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x000DFA51 File Offset: 0x000DDC51
	public bool WasBigCardCountered()
	{
		return this.m_bigCardCountered;
	}

	// Token: 0x06002C69 RID: 11369 RVA: 0x000DFA59 File Offset: 0x000DDC59
	public int GetDisplayTimeMS()
	{
		return this.m_displayTimeMS;
	}

	// Token: 0x06002C6A RID: 11370 RVA: 0x000DFA61 File Offset: 0x000DDC61
	public bool IsBigCardWaitingForSecret()
	{
		return this.m_bigCardWaitingForSecret;
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x000DFA69 File Offset: 0x000DDC69
	public bool IsBigCardFromMetaData()
	{
		return this.m_bigCardFromMetaData;
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x000DFA71 File Offset: 0x000DDC71
	public Entity GetBigCardPostTransformedEntity()
	{
		return this.m_bigCardPostTransformedEntity;
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x000DFA79 File Offset: 0x000DDC79
	public bool HasBigCardPostTransformedEntity()
	{
		return this.m_bigCardPostTransformedEntity != null;
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x000DFA84 File Offset: 0x000DDC84
	public void ShowBigCard(Vector3[] pathToFollow)
	{
		float num = 1f;
		if (this.m_displayTimeMS > 0)
		{
			float b = (float)this.m_displayTimeMS / 1000f;
			num = Mathf.Min(1f, b);
		}
		this.m_mainCardActor.transform.localScale = new Vector3(1.03f, 1.03f, 1.03f);
		Entity entity = this.m_entity;
		if (this.HasBigCardPostTransformedEntity())
		{
			entity = this.m_bigCardPostTransformedEntity;
		}
		if (entity != null)
		{
			if (entity.GetCardType() == TAG_CARDTYPE.SPELL || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER || this.m_bigCardFromMetaData)
			{
				pathToFollow[0] = this.m_mainCardActor.transform.position;
				Hashtable args = iTween.Hash(new object[]
				{
					"path",
					pathToFollow,
					"time",
					num,
					"oncomplete",
					"OnBigCardPathComplete",
					"oncompletetarget",
					base.gameObject
				});
				iTween.MoveTo(this.m_mainCardActor.gameObject, args);
				iTween.ScaleTo(base.gameObject, new Vector3(1f, 1f, 1f), num);
				SoundManager.Get().LoadAndPlay("play_card_from_hand_1.prefab:ac4be75e319a97947a68308a08e54e88");
				return;
			}
			this.ShowDisplayedCreator();
		}
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x000DFBC5 File Offset: 0x000DDDC5
	private void OnBigCardPathComplete()
	{
		this.ShowDisplayedCreator();
	}

	// Token: 0x0400186F RID: 6255
	public UberText m_createdByText;

	// Token: 0x04001870 RID: 6256
	private readonly Color OPPONENT_COLOR = new Color(0.7137f, 0.2f, 0.1333f, 1f);

	// Token: 0x04001871 RID: 6257
	private readonly Color FRIENDLY_COLOR = new Color(0.6509f, 0.6705f, 0.9843f, 1f);

	// Token: 0x04001872 RID: 6258
	private const float ABILITY_CARD_ANIMATE_TO_BIG_CARD_AREA_TIME = 1f;

	// Token: 0x04001873 RID: 6259
	private const float BIG_CARD_SCALE = 1.03f;

	// Token: 0x04001874 RID: 6260
	private const float MOUSE_OVER_Z_OFFSET_TOP = -1.404475f;

	// Token: 0x04001875 RID: 6261
	private const float MOUSE_OVER_Z_OFFSET_BOTTOM = 0.1681719f;

	// Token: 0x04001876 RID: 6262
	private const float MOUSE_OVER_Z_OFFSET_PHONE = -4.75f;

	// Token: 0x04001877 RID: 6263
	private const float MOUSE_OVER_Z_OFFSET_SECRET_PHONE = -4.3f;

	// Token: 0x04001878 RID: 6264
	private const float MOUSE_OVER_Z_OFFSET_WITH_CREATOR_PHONE = -4.3f;

	// Token: 0x04001879 RID: 6265
	private const float MOUSE_OVER_HEIGHT_OFFSET = 7.524521f;

	// Token: 0x0400187A RID: 6266
	private PlatformDependentValue<float> MOUSE_OVER_X_OFFSET = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 4.326718f,
		Tablet = 4.7f,
		Phone = 5.4f
	};

	// Token: 0x0400187B RID: 6267
	private PlatformDependentValue<float> MOUSE_OVER_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 1f,
		Tablet = 1f,
		Phone = 1f
	};

	// Token: 0x0400187C RID: 6268
	private PlatformDependentValue<float> X_SIZE_OF_MOUSE_OVER_CHILD = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 2.5f,
		Tablet = 2.5f,
		Phone = 2.5f
	};

	// Token: 0x0400187D RID: 6269
	private const float MAX_WIDTH_OF_CHILDREN = 5f;

	// Token: 0x0400187E RID: 6270
	private const string CREATED_BY_BONE_NAME = "HistoryCreatedByBone";

	// Token: 0x0400187F RID: 6271
	private Material m_fullTileMaterial;

	// Token: 0x04001880 RID: 6272
	private Material m_halfTileMaterial;

	// Token: 0x04001881 RID: 6273
	private bool m_mousedOver;

	// Token: 0x04001882 RID: 6274
	private bool m_halfSize;

	// Token: 0x04001883 RID: 6275
	private bool m_hasBeenShown;

	// Token: 0x04001884 RID: 6276
	private Actor m_separator;

	// Token: 0x04001885 RID: 6277
	private bool m_haveDisplayedCreator;

	// Token: 0x04001886 RID: 6278
	private bool m_gameEntityMousedOver;

	// Token: 0x04001887 RID: 6279
	private List<HistoryInfo> m_childInfos;

	// Token: 0x04001888 RID: 6280
	private List<HistoryChildCard> m_historyChildren = new List<HistoryChildCard>();

	// Token: 0x04001889 RID: 6281
	private bool m_bigCardFinishedCallbackHasRun;

	// Token: 0x0400188A RID: 6282
	private HistoryManager.BigCardFinishedCallback m_bigCardFinishedCallback;

	// Token: 0x0400188B RID: 6283
	private bool m_bigCardCountered;

	// Token: 0x0400188C RID: 6284
	private bool m_bigCardWaitingForSecret;

	// Token: 0x0400188D RID: 6285
	private bool m_bigCardFromMetaData;

	// Token: 0x0400188E RID: 6286
	private Entity m_bigCardPostTransformedEntity;

	// Token: 0x0400188F RID: 6287
	private float m_tileSize;

	// Token: 0x04001890 RID: 6288
	private int m_displayTimeMS;

	// Token: 0x04001891 RID: 6289
	private HistoryInfoType m_historyInfoType;
}
