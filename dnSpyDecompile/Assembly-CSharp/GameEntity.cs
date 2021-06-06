using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.Core.Streaming;
using Hearthstone.Progression;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class GameEntity : Entity
{
	// Token: 0x060029C8 RID: 10696 RVA: 0x000D4568 File Offset: 0x000D2768
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER,
				false
			},
			{
				GameEntityOption.MULLIGAN_IS_CHOOSE_ONE,
				false
			},
			{
				GameEntityOption.MULLIGAN_TIMER_HAS_ALTERNATE_POSITION,
				false
			},
			{
				GameEntityOption.HERO_POWER_TOOLTIP_SHIFTED_DURING_MULLIGAN,
				false
			},
			{
				GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION,
				true
			},
			{
				GameEntityOption.MULLIGAN_HAS_HERO_LOBBY,
				false
			},
			{
				GameEntityOption.DIM_OPPOSING_HERO_DURING_MULLIGAN,
				false
			},
			{
				GameEntityOption.HANDLE_COIN,
				true
			},
			{
				GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS,
				false
			},
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				true
			},
			{
				GameEntityOption.SUPPRESS_CLASS_NAMES,
				false
			},
			{
				GameEntityOption.USE_SECRET_CLASS_NAMES,
				true
			},
			{
				GameEntityOption.ALLOW_NAME_BANNER_MODE_ICONS,
				true
			},
			{
				GameEntityOption.USE_COMPACT_ENCHANTMENT_BANNERS,
				false
			},
			{
				GameEntityOption.ALLOW_FATIGUE,
				true
			},
			{
				GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN,
				false
			},
			{
				GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES,
				true
			},
			{
				GameEntityOption.ALLOW_SLEEP_FX,
				true
			},
			{
				GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR,
				false
			},
			{
				GameEntityOption.USES_PREMIUM_EMOTES,
				false
			},
			{
				GameEntityOption.CAN_SQUELCH_OPPONENT,
				true
			},
			{
				GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN,
				false
			},
			{
				GameEntityOption.SHOW_CRAZY_KEYWORD_TOOLTIP,
				false
			},
			{
				GameEntityOption.SHOW_HERO_TOOLTIPS,
				false
			},
			{
				GameEntityOption.USES_BIG_CARDS,
				true
			},
			{
				GameEntityOption.DISABLE_TOOLTIPS,
				false
			},
			{
				GameEntityOption.DELAY_CARD_SOUND_SPELLS,
				false
			},
			{
				GameEntityOption.DISPLAY_MULLIGAN_DETAIL_LABEL,
				false
			},
			{
				GameEntityOption.WAIT_FOR_RATING_INFO,
				true
			}
		};
	}

	// Token: 0x060029C9 RID: 10697 RVA: 0x000D4678 File Offset: 0x000D2878
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>
		{
			{
				GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME,
				null
			},
			{
				GameEntityOption.ALTERNATE_MULLIGAN_LOBBY_ACTOR_NAME,
				null
			},
			{
				GameEntityOption.VICTORY_SCREEN_PREFAB_PATH,
				"VictoryTwoScoop.prefab:b31e3c6c1e80ced4183c3e231c567669"
			},
			{
				GameEntityOption.DEFEAT_SCREEN_PREFAB_PATH,
				"DefeatTwoScoop.prefab:6535dd92d63fce1478220e9bc50e926b"
			},
			{
				GameEntityOption.RULEBOOK_POPUP_PREFAB_PATH,
				null
			},
			{
				GameEntityOption.VICTORY_AUDIO_PATH,
				"victory_jingle.prefab:23f19dd07c7a5114abe5f525099cbac4"
			},
			{
				GameEntityOption.DEFEAT_AUDIO_PATH,
				"defeat_jingle.prefab:0744a10f38e92f1438a02349c29a7b76"
			}
		};
	}

	// Token: 0x17000501 RID: 1281
	// (get) Token: 0x060029CA RID: 10698 RVA: 0x000D46D9 File Offset: 0x000D28D9
	// (set) Token: 0x060029CB RID: 10699 RVA: 0x000D46E1 File Offset: 0x000D28E1
	public string m_uuid { get; set; }

	// Token: 0x17000502 RID: 1282
	// (get) Token: 0x060029CC RID: 10700 RVA: 0x000D46EA File Offset: 0x000D28EA
	protected static MonoBehaviour Coroutines
	{
		get
		{
			if (GameEntity.s_coroutines == null)
			{
				GameEntity.s_coroutines = new GameObject().AddComponent<EmptyScript>();
			}
			return GameEntity.s_coroutines;
		}
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x000D470D File Offset: 0x000D290D
	public GameEntity()
	{
		this.PreloadAssets();
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnCreate()
	{
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnCreateGame()
	{
	}

	// Token: 0x060029D0 RID: 10704 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnDecommissionGame()
	{
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x000D473C File Offset: 0x000D293C
	public void FadeOutHeroActor(Actor actorToFade)
	{
		this.ToggleSpotLight(actorToFade.GetHeroSpotlight(), false);
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		Material heroMat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material heroFrameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		float @float = heroMat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			if (!heroMat || !heroFrameMat)
			{
				Log.Graphics.PrintWarning("Actor's portrait HeroMat or HeroFrameMat materials are null", Array.Empty<object>());
				return;
			}
			heroMat.SetFloat("_LightingBlend", (float)amount);
			heroFrameMat.SetFloat("_LightingBlend", (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			0.25f,
			"from",
			@float,
			"to",
			1f,
			"onupdate",
			action,
			"onupdatetarget",
			actorToFade.gameObject
		});
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	// Token: 0x060029D2 RID: 10706 RVA: 0x000D481C File Offset: 0x000D2A1C
	public void FadeOutActor(Actor actorToFade)
	{
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		Material mat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material frameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		float @float = mat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			mat.SetFloat("_LightingBlend", (float)amount);
			frameMat.SetFloat("_LightingBlend", (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			0.25f,
			"from",
			@float,
			"to",
			1f,
			"onupdate",
			action,
			"onupdatetarget",
			actorToFade.gameObject
		});
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	// Token: 0x060029D3 RID: 10707 RVA: 0x000D48F0 File Offset: 0x000D2AF0
	private void ToggleSpotLight(Light light, bool bOn)
	{
		float num = 0.1f;
		float num2 = 1.3f;
		Action<object> action = delegate(object amount)
		{
			light.intensity = (float)amount;
		};
		Action<object> action2 = delegate(object args)
		{
			light.enabled = false;
		};
		if (bOn)
		{
			light.enabled = true;
			light.intensity = 0f;
			Hashtable args3 = iTween.Hash(new object[]
			{
				"time",
				num,
				"from",
				0f,
				"to",
				num2,
				"onupdate",
				action,
				"onupdatetarget",
				light.gameObject
			});
			iTween.ValueTo(light.gameObject, args3);
			return;
		}
		Hashtable args2 = iTween.Hash(new object[]
		{
			"time",
			num,
			"from",
			light.intensity,
			"to",
			0f,
			"onupdate",
			action,
			"onupdatetarget",
			light.gameObject,
			"oncomplete",
			action2
		});
		iTween.ValueTo(light.gameObject, args2);
	}

	// Token: 0x060029D4 RID: 10708 RVA: 0x000D4A61 File Offset: 0x000D2C61
	public void FadeInHeroActor(Actor actorToFade)
	{
		this.FadeInHeroActor(actorToFade, 0f);
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x000D4A70 File Offset: 0x000D2C70
	public void FadeInHeroActor(Actor actorToFade, float lightBlendAmount)
	{
		if (!actorToFade)
		{
			Log.Graphics.PrintWarning("Actor to fade is null!", Array.Empty<object>());
			return;
		}
		this.ToggleSpotLight(actorToFade.GetHeroSpotlight(), true);
		if (!actorToFade.m_portraitMesh)
		{
			Log.Graphics.PrintWarning("Actor's portrait mesh is null!", Array.Empty<object>());
			return;
		}
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		if (!component)
		{
			Log.Graphics.PrintWarning("Actor's portrait mesh component render is null!", Array.Empty<object>());
			return;
		}
		Material heroMat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material heroFrameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		if (!heroMat || !heroFrameMat)
		{
			Log.Graphics.PrintWarning("Actor's portrait HeroMat or HeroFrameMat materials are null", Array.Empty<object>());
			return;
		}
		float @float = heroMat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			if (!heroMat || !heroFrameMat)
			{
				Log.Graphics.PrintWarning("Actor's portrait HeroMat or HeroFrameMat materials are null", Array.Empty<object>());
				return;
			}
			heroMat.SetFloat("_LightingBlend", (float)amount);
			heroFrameMat.SetFloat("_LightingBlend", (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			0.25f,
			"from",
			@float,
			"to",
			lightBlendAmount,
			"onupdate",
			action,
			"onupdatetarget",
			actorToFade.gameObject
		});
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x000D4BD9 File Offset: 0x000D2DD9
	public void FadeInActor(Actor actorToFade)
	{
		this.FadeInActor(actorToFade, 0f);
	}

	// Token: 0x060029D7 RID: 10711 RVA: 0x000D4BE8 File Offset: 0x000D2DE8
	public void FadeInActor(Actor actorToFade, float lightBlendAmount)
	{
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		Material mat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material frameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		float @float = mat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			mat.SetFloat("_LightingBlend", (float)amount);
			frameMat.SetFloat("_LightingBlend", (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			0.25f,
			"from",
			@float,
			"to",
			lightBlendAmount,
			"onupdate",
			action,
			"onupdatetarget",
			actorToFade.gameObject
		});
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	// Token: 0x060029D8 RID: 10712 RVA: 0x000D4CB5 File Offset: 0x000D2EB5
	public void PreloadSound(string soundPath)
	{
		this.m_preloadsNeeded++;
		SoundLoader.LoadSound(soundPath, new PrefabCallback<GameObject>(this.OnSoundLoaded), null, SoundManager.Get().GetPlaceholderSound());
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x000D4CE8 File Offset: 0x000D2EE8
	private void OnSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_preloadsNeeded--;
		if (assetRef == null)
		{
			Debug.LogWarning(string.Format("GameEntity.OnSoundLoaded() - ERROR missing Asset Ref for sound!", assetRef));
			return;
		}
		if (go == null)
		{
			Debug.LogWarning(string.Format("GameEntity.OnSoundLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("GameEntity.OnSoundLoaded() - ERROR \"{0}\" has no Spell component", assetRef));
			return;
		}
		this.m_preloadedSounds.Add(assetRef.ToString(), component);
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x000D4D64 File Offset: 0x000D2F64
	public void RemovePreloadedSound(string soundPath)
	{
		this.m_preloadedSounds.Remove(soundPath);
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x000D4D74 File Offset: 0x000D2F74
	public bool CheckPreloadedSound(string soundPath)
	{
		AudioSource audioSource;
		return this.m_preloadedSounds.TryGetValue(soundPath, out audioSource);
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x000D4D90 File Offset: 0x000D2F90
	public AudioSource GetPreloadedSound(string soundPath)
	{
		AudioSource result;
		if (this.m_preloadedSounds.TryGetValue(soundPath, out result))
		{
			return result;
		}
		Debug.LogError(string.Format("GameEntity.GetPreloadedSound() - \"{0}\" was not preloaded", soundPath));
		return null;
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x000D4DC0 File Offset: 0x000D2FC0
	public bool IsPreloadingAssets()
	{
		return this.m_preloadsNeeded > 0;
	}

	// Token: 0x060029DE RID: 10718 RVA: 0x000D4DCB File Offset: 0x000D2FCB
	public GameEntityOptions GetGameOptions()
	{
		return this.m_gameOptions;
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool HasValidDisplayName()
	{
		return false;
	}

	// Token: 0x060029E0 RID: 10720 RVA: 0x000D4DD3 File Offset: 0x000D2FD3
	public override string GetName()
	{
		return "GameEntity";
	}

	// Token: 0x060029E1 RID: 10721 RVA: 0x000D4DD3 File Offset: 0x000D2FD3
	public override string GetDebugName()
	{
		return "GameEntity";
	}

	// Token: 0x060029E2 RID: 10722 RVA: 0x000D4DDC File Offset: 0x000D2FDC
	public override void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		for (int i = 0; i < changeList.Count; i++)
		{
			TagDelta change = changeList[i];
			this.OnTagChanged(change);
		}
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x000D4E0C File Offset: 0x000D300C
	public override void InitRealTimeValues(List<Network.Entity.Tag> tags)
	{
		base.InitRealTimeValues(tags);
		foreach (Network.Entity.Tag tag in tags)
		{
			GAME_TAG name = (GAME_TAG)tag.Name;
			if (name <= GAME_TAG.TURN)
			{
				if (name != GAME_TAG.STEP)
				{
					if (name == GAME_TAG.TURN)
					{
						this.SetRealTimeTurn(tag.Value);
						GameState.Get().TriggerTurnTimerUpdateForTurn(tag.Value);
					}
				}
				else
				{
					this.SetRealTimeStep(tag.Value);
				}
			}
			else if (name != GAME_TAG.COIN_MANA_GEM)
			{
				if (name == GAME_TAG.BOARD_VISUAL_STATE)
				{
					if (tag.Value > 0)
					{
						Board.Get().ChangeBoardVisualState((TAG_BOARD_VISUAL_STATE)tag.Value);
					}
				}
			}
			else if (tag.Value != 0)
			{
				ManaCrystalMgr.Get().SetManaCrystalType(ManaCrystalType.COIN);
			}
		}
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x000D4EE4 File Offset: 0x000D30E4
	public override void OnRealTimeTagChanged(Network.HistTagChange change)
	{
		GAME_TAG tag = (GAME_TAG)change.Tag;
		if (tag <= GAME_TAG.STEP)
		{
			if (tag == GAME_TAG.MISSION_EVENT)
			{
				this.HandleRealTimeMissionEvent(change.Value);
				return;
			}
			if (tag != GAME_TAG.STEP)
			{
				return;
			}
			this.SetRealTimeStep(change.Value);
			return;
		}
		else
		{
			if (tag == GAME_TAG.TURN)
			{
				this.SetRealTimeTurn(change.Value);
				EndTurnButton.Get().OnTurnChanged();
				GameState.Get().UpdateOptionHighlights();
				return;
			}
			if (tag != GAME_TAG.COIN_MANA_GEM)
			{
				return;
			}
			if (change.Value != 0)
			{
				ManaCrystalMgr.Get().SetManaCrystalType(ManaCrystalType.COIN);
			}
			return;
		}
	}

	// Token: 0x060029E5 RID: 10725 RVA: 0x000D4F64 File Offset: 0x000D3164
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag <= GAME_TAG.END_TURN_BUTTON_ALTERNATIVE_APPEARANCE)
		{
			if (tag == GAME_TAG.TURN)
			{
				EndTurnButton.Get().OnTurnChanged();
				GameState.Get().UpdateOptionHighlights();
				return;
			}
			if (tag != GAME_TAG.END_TURN_BUTTON_ALTERNATIVE_APPEARANCE)
			{
				return;
			}
			EndTurnButton.Get().ApplyAlternativeAppearance();
			return;
		}
		else
		{
			if (tag == GAME_TAG.TURN_INDICATOR_ALTERNATIVE_APPEARANCE)
			{
				TurnStartManager.Get().ApplyAlternativeAppearance();
				return;
			}
			if (tag != GAME_TAG.BOARD_VISUAL_STATE)
			{
				return;
			}
			Board.Get().ChangeBoardVisualState((TAG_BOARD_VISUAL_STATE)change.newValue);
			return;
		}
	}

	// Token: 0x060029E6 RID: 10726 RVA: 0x000D4FE1 File Offset: 0x000D31E1
	private void SetRealTimeTurn(int turn)
	{
		this.m_realTimeTurn = turn;
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x000D4FEA File Offset: 0x000D31EA
	private void SetRealTimeStep(int step)
	{
		this.m_realTimeStep = step;
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x000D4FF3 File Offset: 0x000D31F3
	public bool IsCurrentTurnRealTime()
	{
		return this.m_realTimeTurn == base.GetTag(GAME_TAG.TURN);
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x000D5005 File Offset: 0x000D3205
	public bool IsMulliganActiveRealTime()
	{
		return this.m_realTimeStep <= 4;
	}

	// Token: 0x060029EA RID: 10730 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PreloadAssets()
	{
	}

	// Token: 0x060029EB RID: 10731 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfStartOfTurnEventsFinished()
	{
	}

	// Token: 0x060029EC RID: 10732 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool NotifyOfEndTurnButtonPushed()
	{
		return true;
	}

	// Token: 0x060029ED RID: 10733 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		return true;
	}

	// Token: 0x060029EE RID: 10734 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
	}

	// Token: 0x060029EF RID: 10735 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
	}

	// Token: 0x060029F0 RID: 10736 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool NotifyOfCardTooltipDisplayShow(Card card)
	{
		return true;
	}

	// Token: 0x060029F1 RID: 10737 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfCardTooltipDisplayHide(Card card)
	{
	}

	// Token: 0x060029F2 RID: 10738 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfCoinFlipResult()
	{
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		return false;
	}

	// Token: 0x060029F4 RID: 10740 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string[] NotifyOfKeywordHelpPanelDisplay(Entity entity)
	{
		return null;
	}

	// Token: 0x060029F5 RID: 10741 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfCardGrabbed(Entity entity)
	{
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfCardDropped(Entity entity)
	{
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfTargetModeCancelled()
	{
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfHelpPanelDisplay(int numPanels)
	{
	}

	// Token: 0x060029F9 RID: 10745 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfDebugCommand(int command)
	{
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfManaCrystalSpawned()
	{
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfEnemyManaCrystalSpawned()
	{
	}

	// Token: 0x060029FC RID: 10748 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfTooltipZoneMouseOver(TooltipZone tooltip)
	{
	}

	// Token: 0x060029FD RID: 10749 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfHistoryTokenMousedOver(GameObject mousedOverTile)
	{
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfHistoryTokenMousedOut()
	{
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfCustomIntroFinished()
	{
	}

	// Token: 0x06002A00 RID: 10752 RVA: 0x000D5014 File Offset: 0x000D3214
	public virtual void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_EndGameScreen);
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Gameplay.Get().SaveOriginalTimeScale();
		AchievementManager achievementManager = AchievementManager.Get();
		if (achievementManager != null)
		{
			achievementManager.PauseToastNotifications();
		}
		Spell enemyBlowUpSpell = null;
		Spell friendlyBlowUpSpell = null;
		if (this.ShouldPlayHeroBlowUpSpells(playState))
		{
			switch (playState)
			{
			case TAG_PLAYSTATE.WON:
			{
				string stringOption = this.GetGameOptions().GetStringOption(GameEntityOption.VICTORY_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				enemyBlowUpSpell = this.BlowUpHero(heroCard, SpellType.ENDGAME_WIN);
				break;
			}
			case TAG_PLAYSTATE.LOST:
			{
				string stringOption = this.GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				friendlyBlowUpSpell = this.BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				break;
			}
			case TAG_PLAYSTATE.TIED:
			{
				string stringOption = this.GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				enemyBlowUpSpell = this.BlowUpHero(heroCard, SpellType.ENDGAME_DRAW);
				friendlyBlowUpSpell = this.BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				break;
			}
			}
		}
		this.ShowEndGameScreen(playState, enemyBlowUpSpell, friendlyBlowUpSpell);
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfRealTimeTagChange(Entity entity, Network.HistTagChange tagChange)
	{
	}

	// Token: 0x06002A02 RID: 10754 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void ToggleAlternateMulliganActorHighlight(Card card, bool highlighted)
	{
	}

	// Token: 0x06002A03 RID: 10755 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool ToggleAlternateMulliganActorHighlight(Actor actor, bool? highlighted = null)
	{
		return false;
	}

	// Token: 0x06002A04 RID: 10756 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return true;
	}

	// Token: 0x06002A05 RID: 10757 RVA: 0x000D514C File Offset: 0x000D334C
	public virtual string GetVictoryScreenBannerText()
	{
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_VICTORY");
	}

	// Token: 0x06002A06 RID: 10758 RVA: 0x000D5158 File Offset: 0x000D3358
	public virtual string GetDefeatScreenBannerText()
	{
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_DEFEAT");
	}

	// Token: 0x06002A07 RID: 10759 RVA: 0x000D5164 File Offset: 0x000D3364
	public virtual string GetTieScreenBannerText()
	{
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_TIE");
	}

	// Token: 0x06002A08 RID: 10760 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfHeroesFinishedAnimatingInMulligan()
	{
	}

	// Token: 0x06002A09 RID: 10761 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool NotifyOfTooltipDisplay(TooltipZone tooltip)
	{
		return false;
	}

	// Token: 0x06002A0A RID: 10762 RVA: 0x000D5170 File Offset: 0x000D3370
	public virtual void NotifyOfMulliganInitialized()
	{
		if (GameMgr.Get().IsTutorial())
		{
			return;
		}
		AssetLoader.Get().InstantiatePrefab("EmoteHandler.prefab:5d44be0e8bb7fd14d9fbdbda6a74ab91", new PrefabCallback<GameObject>(this.EmoteHandlerDoneLoadingCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		if (GameMgr.Get().IsAI() || !this.GetGameOptions().GetBooleanOption(GameEntityOption.CAN_SQUELCH_OPPONENT))
		{
			return;
		}
		AssetLoader.Get().InstantiatePrefab("EnemyEmoteHandler.prefab:6ace3edd8826cad4aaa0d0e0eb085012", new PrefabCallback<GameObject>(this.EnemyEmoteHandlerDoneLoadingCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x000D51EC File Offset: 0x000D33EC
	public virtual AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		return heroCard.GetAnnouncerLine(type);
	}

	// Token: 0x06002A0C RID: 10764 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfMulliganEnded()
	{
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x000D51F5 File Offset: 0x000D33F5
	private void EmoteHandlerDoneLoadingCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).transform.position;
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x000D5217 File Offset: 0x000D3417
	private void EnemyEmoteHandlerDoneLoadingCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).transform.position;
	}

	// Token: 0x06002A0F RID: 10767 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfGamePackOpened()
	{
	}

	// Token: 0x06002A10 RID: 10768 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfDefeatCoinAnimation()
	{
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void SendCustomEvent(int eventID)
	{
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x000D5239 File Offset: 0x000D3439
	public virtual string GetTurnStartReminderText()
	{
		return "";
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool IsHeroMulliganLobbyFinished()
	{
		return true;
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual ActorStateType GetMulliganChoiceHighlightState()
	{
		return ActorStateType.CARD_IDLE;
	}

	// Token: 0x06002A15 RID: 10773 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldDelayShowingFakeHeroPowerTooltip()
	{
		return true;
	}

	// Token: 0x06002A16 RID: 10774 RVA: 0x000D5240 File Offset: 0x000D3440
	public virtual Vector3 NameBannerPosition(Player.Side side)
	{
		if (side == Player.Side.FRIENDLY)
		{
			return new Vector3(0f, 5f, 22f);
		}
		return new Vector3(0f, 5f, -10f);
	}

	// Token: 0x06002A17 RID: 10775 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PlayAlternateEnemyEmote(int playerId, EmoteType emoteType)
	{
	}

	// Token: 0x06002A18 RID: 10776 RVA: 0x000D526F File Offset: 0x000D346F
	public virtual Vector3 GetMulliganTimerAlternatePosition()
	{
		return Vector3.zero;
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x000D5276 File Offset: 0x000D3476
	private bool ShouldSkipMulligan()
	{
		return base.HasTag(GAME_TAG.SKIP_MULLIGAN);
	}

	// Token: 0x06002A1A RID: 10778 RVA: 0x000D5283 File Offset: 0x000D3483
	public virtual bool ShouldDoAlternateMulliganIntro()
	{
		return this.ShouldSkipMulligan();
	}

	// Token: 0x06002A1B RID: 10779 RVA: 0x000D528B File Offset: 0x000D348B
	public virtual bool DoAlternateMulliganIntro()
	{
		if (this.ShouldSkipMulligan())
		{
			GameEntity.Coroutines.StartCoroutine(this.SkipStandardMulliganWithTiming());
			return true;
		}
		return false;
	}

	// Token: 0x06002A1C RID: 10780 RVA: 0x000D52A9 File Offset: 0x000D34A9
	protected IEnumerator SkipStandardMulliganWithTiming()
	{
		GameState.Get().SetMulliganBusy(true);
		SceneMgr.Get().NotifySceneLoaded();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		MulliganManager.Get().SkipMulligan();
		yield break;
	}

	// Token: 0x06002A1D RID: 10781 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string GetMulliganDetailText()
	{
		return null;
	}

	// Token: 0x06002A1E RID: 10782 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnMulliganCardsDealt(List<Card> startingCards)
	{
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnMulliganBeginDealNewCards()
	{
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x000D52B1 File Offset: 0x000D34B1
	public virtual float GetAdditionalTimeToWaitForSpells()
	{
		return 0f;
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldShowBigCard()
	{
		return true;
	}

	// Token: 0x06002A22 RID: 10786 RVA: 0x000D52B8 File Offset: 0x000D34B8
	public virtual string GetBestNameForPlayer(int playerId)
	{
		string text = (GameState.Get().GetPlayerMap().ContainsKey(playerId) && GameState.Get().GetPlayerMap()[playerId] != null) ? GameState.Get().GetPlayerMap()[playerId].GetName() : null;
		bool flag = GameState.Get().GetPlayerMap().ContainsKey(playerId) && GameState.Get().GetPlayerMap()[playerId].IsFriendlySide();
		bool @bool = Options.Get().GetBool(Option.STREAMER_MODE);
		if (flag)
		{
			if (@bool || text == null)
			{
				return GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME");
			}
			return text;
		}
		else
		{
			if (@bool)
			{
				return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
			}
			if (text == null)
			{
				return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
			}
			return text;
		}
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual List<RewardData> GetCustomRewards()
	{
		return null;
	}

	// Token: 0x06002A24 RID: 10788 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void HandleRealTimeMissionEvent(int missionEvent)
	{
	}

	// Token: 0x06002A25 RID: 10789 RVA: 0x000D536C File Offset: 0x000D356C
	public virtual void OnPlayThinkEmote()
	{
		if (GameMgr.Get().IsAI())
		{
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x000D53CA File Offset: 0x000D35CA
	public virtual IEnumerator OnPlayThinkEmoteWithTiming()
	{
		if (GameMgr.Get().IsAI())
		{
			yield break;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		AudioSource activeAudioSource = GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType).GetActiveAudioSource();
		yield return new WaitForSeconds(activeAudioSource.clip.length);
		yield break;
	}

	// Token: 0x06002A27 RID: 10791 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnEmotePlayed(Card card, EmoteType emoteType, CardSoundSpell emoteSpell)
	{
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfOpponentWillPlayCard(string cardId)
	{
	}

	// Token: 0x06002A29 RID: 10793 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfOpponentPlayedCard(Entity entity)
	{
	}

	// Token: 0x06002A2A RID: 10794 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfFriendlyPlayedCard(Entity entity)
	{
	}

	// Token: 0x06002A2B RID: 10795 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfResetGameStarted()
	{
	}

	// Token: 0x06002A2C RID: 10796 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfEntityAttacked(Entity attacker, Entity defender)
	{
	}

	// Token: 0x06002A2E RID: 10798 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfMinionPlayed(Entity minion)
	{
	}

	// Token: 0x06002A2F RID: 10799 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfHeroChanged(Entity newHero)
	{
	}

	// Token: 0x06002A30 RID: 10800 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfWeaponEquipped(Entity weapon)
	{
	}

	// Token: 0x06002A31 RID: 10801 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfSpellPlayed(Entity spell, Entity target)
	{
	}

	// Token: 0x06002A32 RID: 10802 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfHeroPowerUsed(Entity heroPower, Entity target)
	{
	}

	// Token: 0x06002A33 RID: 10803 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfMinionDied(Entity minion)
	{
	}

	// Token: 0x06002A34 RID: 10804 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfHeroDied(Entity hero)
	{
	}

	// Token: 0x06002A35 RID: 10805 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void NotifyOfWeaponDestroyed(Entity weapon)
	{
	}

	// Token: 0x06002A36 RID: 10806 RVA: 0x000D53D2 File Offset: 0x000D35D2
	public virtual string UpdateCardText(Card card, Actor bigCardActor, string text)
	{
		return text;
	}

	// Token: 0x06002A37 RID: 10807 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void ApplyMulliganActorStateChanges(Actor baseActor)
	{
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void ApplyMulliganActorLobbyStateChanges(Actor baseActor)
	{
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void ClearMulliganActorStateChanges(Actor baseActor)
	{
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x000D53D5 File Offset: 0x000D35D5
	public virtual string GetMulliganBannerText()
	{
		return GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND");
	}

	// Token: 0x06002A3B RID: 10811 RVA: 0x000D53E1 File Offset: 0x000D35E1
	public virtual string GetMulliganBannerSubtitleText()
	{
		return GameStrings.Get("GAMEPLAY_MULLIGAN_SUBTITLE");
	}

	// Token: 0x06002A3C RID: 10812 RVA: 0x000D53D5 File Offset: 0x000D35D5
	public virtual string GetMulliganWaitingText()
	{
		return GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND");
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string GetMulliganWaitingSubtitleText()
	{
		return null;
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x000D53ED File Offset: 0x000D35ED
	public virtual Vector3 GetAlternateMulliganActorScale()
	{
		return new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual int GetNumberOfFakeMulliganCardsToShowOnLeft(int numOriginalCards)
	{
		return 0;
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual int GetNumberOfFakeMulliganCardsToShowOnRight(int numOriginalCards)
	{
		return 0;
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void ConfigureFakeMulliganCardActor(Actor actor, bool shown)
	{
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool IsGameSpeedupConditionInEffect()
	{
		return false;
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x000D5403 File Offset: 0x000D3603
	public virtual void StartMulliganSoundtracks(bool soft)
	{
		if (soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_MulliganSoft);
			return;
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Mulligan);
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x000D542C File Offset: 0x000D362C
	public virtual void StartGameplaySoundtracks()
	{
		Board board = Board.Get();
		MusicPlaylistType type = MusicPlaylistType.InGame_Default;
		bool flag = board == null;
		bool flag2 = GameDownloadManagerProvider.Get().IsReadyAssetsInTags(new string[]
		{
			DownloadTags.GetTagString(DownloadTags.Quality.MusicExpansion),
			DownloadTags.GetTagString(DownloadTags.Content.Base)
		});
		if (!(flag | !flag2))
		{
			type = board.m_BoardMusic;
		}
		MusicManager.Get().StartPlaylist(type);
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x000D5239 File Offset: 0x000D3439
	public virtual string GetAlternatePlayerName()
	{
		return "";
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void QueueEntityForRemoval(Entity entity)
	{
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x000D548E File Offset: 0x000D368E
	public virtual IEnumerator PlayMissionIntroLineAndWait()
	{
		yield break;
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x000D5496 File Offset: 0x000D3696
	public virtual IEnumerator DoActionsAfterIntroBeforeMulligan()
	{
		yield break;
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x000D549E File Offset: 0x000D369E
	public virtual IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		yield break;
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x000D54A6 File Offset: 0x000D36A6
	public virtual IEnumerator DoActionsAfterDealingBaseMulliganCards()
	{
		yield break;
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x000D54AE File Offset: 0x000D36AE
	public virtual IEnumerator DoActionsBeforeCoinFlip()
	{
		yield break;
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x000D54B6 File Offset: 0x000D36B6
	public virtual IEnumerator DoActionsAfterCoinFlip()
	{
		yield break;
	}

	// Token: 0x06002A4D RID: 10829 RVA: 0x000D54BE File Offset: 0x000D36BE
	public virtual IEnumerator DoActionsAfterDealingBonusCard()
	{
		yield break;
	}

	// Token: 0x06002A4E RID: 10830 RVA: 0x000D54C6 File Offset: 0x000D36C6
	public virtual IEnumerator DoActionsBeforeSpreadingMulliganCards()
	{
		yield break;
	}

	// Token: 0x06002A4F RID: 10831 RVA: 0x000D54CE File Offset: 0x000D36CE
	public virtual IEnumerator DoActionsAfterSpreadingMulliganCards()
	{
		yield break;
	}

	// Token: 0x06002A50 RID: 10832 RVA: 0x000D54D6 File Offset: 0x000D36D6
	public virtual IEnumerator DoGameSpecificPostIntroActions()
	{
		yield break;
	}

	// Token: 0x06002A51 RID: 10833 RVA: 0x000D54DE File Offset: 0x000D36DE
	public virtual IEnumerator DoCustomIntro(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
		yield break;
	}

	// Token: 0x06002A52 RID: 10834 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnCustomIntroCancelled(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldAllowCardGrab(Entity entity)
	{
		return true;
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string CustomChoiceBannerText()
	{
		return null;
	}

	// Token: 0x06002A55 RID: 10837 RVA: 0x000D54E6 File Offset: 0x000D36E6
	public virtual InputManager.ZoneTooltipSettings GetZoneTooltipSettings()
	{
		return new InputManager.ZoneTooltipSettings();
	}

	// Token: 0x06002A56 RID: 10838 RVA: 0x000D54F0 File Offset: 0x000D36F0
	protected virtual Spell BlowUpHero(Card card, SpellType spellType)
	{
		if (card == null)
		{
			return null;
		}
		Actor actor = card.GetActor();
		if (actor != null)
		{
			actor.ActivateAllSpellsDeathStates();
		}
		Spell result = card.ActivateActorSpell(spellType);
		Gameplay.Get().StartCoroutine(this.HideOtherElements(card));
		return result;
	}

	// Token: 0x06002A57 RID: 10839 RVA: 0x000D5537 File Offset: 0x000D3737
	protected IEnumerator HideOtherElements(Card card)
	{
		yield return new WaitForSeconds(0.5f);
		Player controller = card.GetEntity().GetController();
		if (controller.GetHeroPowerCard() != null)
		{
			controller.GetHeroPowerCard().HideCard();
			controller.GetHeroPowerCard().GetActor().ToggleForceIdle(true);
			controller.GetHeroPowerCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetHeroPowerCard().GetActor().DoCardDeathVisuals();
			controller.GetHeroPowerCard().DeactivateCustomKeywordEffect();
		}
		if (controller.GetWeaponCard() != null)
		{
			controller.GetWeaponCard().HideCard();
			controller.GetWeaponCard().GetActor().ToggleForceIdle(true);
			controller.GetWeaponCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetWeaponCard().GetActor().DoCardDeathVisuals();
		}
		card.GetActor().HideArmorSpell();
		card.GetActor().GetHealthObject().Hide();
		card.GetActor().GetAttackObject().Hide();
		card.GetActor().ToggleForceIdle(true);
		card.GetActor().SetActorState(ActorStateType.CARD_IDLE);
		yield break;
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x000D5548 File Offset: 0x000D3748
	protected void ShowEndGameScreen(TAG_PLAYSTATE playState, Spell enemyBlowUpSpell, Spell friendlyBlowUpSpell)
	{
		string text = null;
		if (playState != TAG_PLAYSTATE.WON)
		{
			if (playState - TAG_PLAYSTATE.LOST <= 1)
			{
				text = this.GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_SCREEN_PREFAB_PATH);
			}
		}
		else
		{
			text = this.GetGameOptions().GetStringOption(GameEntityOption.VICTORY_SCREEN_PREFAB_PATH);
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (!gameObject)
		{
			Debug.LogErrorFormat("GameEntity.ShowEndGameScreen() - FAILED to load \"{0}\"", new object[]
			{
				text
			});
			return;
		}
		EndGameScreen component = gameObject.GetComponent<EndGameScreen>();
		if (!component)
		{
			Debug.LogErrorFormat("GameEntity.ShowEndGameScreen() - \"{0}\" does not have an EndGameScreen component", new object[]
			{
				text
			});
			return;
		}
		GameEntity.EndGameScreenContext endGameScreenContext = new GameEntity.EndGameScreenContext();
		endGameScreenContext.m_screen = component;
		endGameScreenContext.m_enemyBlowUpSpell = enemyBlowUpSpell;
		endGameScreenContext.m_friendlyBlowUpSpell = friendlyBlowUpSpell;
		if (enemyBlowUpSpell && !enemyBlowUpSpell.IsFinished())
		{
			enemyBlowUpSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnBlowUpSpellFinished), endGameScreenContext);
		}
		if (friendlyBlowUpSpell && !friendlyBlowUpSpell.IsFinished())
		{
			friendlyBlowUpSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnBlowUpSpellFinished), endGameScreenContext);
		}
		this.ShowEndGameScreenAfterEffects(endGameScreenContext);
	}

	// Token: 0x06002A59 RID: 10841 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return true;
	}

	// Token: 0x06002A5A RID: 10842 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool ShouldUseAlternateNameForPlayer(Player.Side side)
	{
		return false;
	}

	// Token: 0x06002A5B RID: 10843 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string GetNameBannerOverride(Player.Side side)
	{
		return null;
	}

	// Token: 0x06002A5C RID: 10844 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return null;
	}

	// Token: 0x06002A5D RID: 10845 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string GetTurnTimerCountdownText(float timeRemainingInTurn)
	{
		return null;
	}

	// Token: 0x06002A5E RID: 10846 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string GetAttackSpellControllerOverride(Entity attacker)
	{
		return null;
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x000D563C File Offset: 0x000D383C
	private void OnBlowUpSpellFinished(Spell spell, object userData)
	{
		GameEntity.EndGameScreenContext context = (GameEntity.EndGameScreenContext)userData;
		this.ShowEndGameScreenAfterEffects(context);
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x000D5657 File Offset: 0x000D3857
	private void ShowEndGameScreenAfterEffects(GameEntity.EndGameScreenContext context)
	{
		if (this.AreBlowUpSpellsFinished(context))
		{
			Gameplay.Get().RestoreOriginalTimeScale();
			AchievementManager achievementManager = AchievementManager.Get();
			if (achievementManager != null)
			{
				achievementManager.UnpauseToastNotifications();
			}
			context.m_screen.Show();
		}
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x000D5687 File Offset: 0x000D3887
	private bool AreBlowUpSpellsFinished(GameEntity.EndGameScreenContext context)
	{
		return (!(context.m_enemyBlowUpSpell != null) || context.m_enemyBlowUpSpell.IsFinished()) && (!(context.m_friendlyBlowUpSpell != null) || context.m_friendlyBlowUpSpell.IsFinished());
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x000D56C4 File Offset: 0x000D38C4
	public virtual float? GetThinkEmoteDelayOverride()
	{
		return null;
	}

	// Token: 0x06002A63 RID: 10851 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual string[] GetOverrideBoardClickSounds()
	{
		return null;
	}

	// Token: 0x06002A64 RID: 10852 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnTurnStartManagerFinished()
	{
	}

	// Token: 0x06002A65 RID: 10853 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnTurnTimerEnded(bool isFriendlyPlayerTurnTimer)
	{
	}

	// Token: 0x040017B2 RID: 6066
	private static Map<GameEntityOption, bool> s_booleanOptions = GameEntity.InitBooleanOptions();

	// Token: 0x040017B3 RID: 6067
	private static Map<GameEntityOption, string> s_stringOptions = GameEntity.InitStringOptions();

	// Token: 0x040017B4 RID: 6068
	private Map<string, AudioSource> m_preloadedSounds = new Map<string, AudioSource>();

	// Token: 0x040017B5 RID: 6069
	private int m_preloadsNeeded;

	// Token: 0x040017B6 RID: 6070
	private int m_realTimeTurn;

	// Token: 0x040017B7 RID: 6071
	private int m_realTimeStep;

	// Token: 0x040017B9 RID: 6073
	private static MonoBehaviour s_coroutines;

	// Token: 0x040017BA RID: 6074
	protected GameEntityOptions m_gameOptions = new GameEntityOptions(GameEntity.s_booleanOptions, GameEntity.s_stringOptions);

	// Token: 0x02001646 RID: 5702
	protected class EndGameScreenContext
	{
		// Token: 0x0400B08B RID: 45195
		public EndGameScreen m_screen;

		// Token: 0x0400B08C RID: 45196
		public Spell m_enemyBlowUpSpell;

		// Token: 0x0400B08D RID: 45197
		public Spell m_friendlyBlowUpSpell;
	}
}
