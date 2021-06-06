using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x0200096E RID: 2414
public class SpellCache : IService
{
	// Token: 0x17000783 RID: 1923
	// (get) Token: 0x06008514 RID: 34068 RVA: 0x002AFD0D File Offset: 0x002ADF0D
	private GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("SpellCacheSceneObject", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x06008515 RID: 34069 RVA: 0x002AFD46 File Offset: 0x002ADF46
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield return new ServiceSoftDependency(typeof(SceneMgr), serviceLocator);
		SceneMgr sceneMgr;
		if (serviceLocator.TryGetService<SceneMgr>(out sceneMgr))
		{
			sceneMgr.RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnScenePreLoad));
		}
		yield break;
	}

	// Token: 0x06008516 RID: 34070 RVA: 0x002450CA File Offset: 0x002432CA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(IAssetLoader)
		};
	}

	// Token: 0x06008517 RID: 34071 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06008518 RID: 34072 RVA: 0x002AFD5C File Offset: 0x002ADF5C
	public static SpellCache Get()
	{
		SpellCache result;
		if (!HearthstoneServices.TryGet<SpellCache>(out result) && !Application.isEditor)
		{
			Debug.LogError("Attempting to access null SpellCache");
			return null;
		}
		return result;
	}

	// Token: 0x06008519 RID: 34073 RVA: 0x002AFD88 File Offset: 0x002ADF88
	public SpellTable GetSpellTable(string prefabPath)
	{
		SpellTable result;
		if (!this.m_spellTableCache.TryGetValue(prefabPath, out result))
		{
			result = this.LoadSpellTable(prefabPath);
		}
		return result;
	}

	// Token: 0x0600851A RID: 34074 RVA: 0x002AFDB0 File Offset: 0x002ADFB0
	public void Clear()
	{
		foreach (KeyValuePair<string, SpellTable> keyValuePair in this.m_spellTableCache)
		{
			keyValuePair.Value.ReleaseAllSpells();
		}
	}

	// Token: 0x0600851B RID: 34075 RVA: 0x002AFE08 File Offset: 0x002AE008
	private SpellTable LoadSpellTable(string prefabPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(prefabPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Error.AddDevFatal("SpellCache.LoadSpellTable() - SpellCache GameObject failed to load", Array.Empty<object>());
			return null;
		}
		SpellTable component = gameObject.GetComponent<SpellTable>();
		if (component == null)
		{
			Error.AddDevFatal("SpellCache.LoadSpellTable() - SpellCache has no SpellTable component ", Array.Empty<object>());
			return null;
		}
		component.transform.parent = this.SceneObject.transform;
		this.m_spellTableCache.Add(prefabPath, component);
		return component;
	}

	// Token: 0x0600851C RID: 34076 RVA: 0x002AFE88 File Offset: 0x002AE088
	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
	{
		if (mode != SceneMgr.Mode.GAMEPLAY)
		{
			if (mode == SceneMgr.Mode.COLLECTIONMANAGER || mode - SceneMgr.Mode.TAVERN_BRAWL <= 1)
			{
				this.PreloadSpell("Card_Hand_Ally_SpellTable.prefab:78c9e9fcc292c4a82b35e4b168fb5200", SpellType.DEATHREVERSE);
				this.PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.DEATHREVERSE);
				this.PreloadSpell("Card_Hand_Weapon_SpellTable.prefab:548dce68672b046e1aec1fd629082eb2", SpellType.DEATHREVERSE);
				this.PreloadSpell("Card_Hand_Ally_SpellTable.prefab:78c9e9fcc292c4a82b35e4b168fb5200", SpellType.GHOSTCARD);
				this.PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.GHOSTCARD);
				this.PreloadSpell("Card_Hand_Weapon_SpellTable.prefab:548dce68672b046e1aec1fd629082eb2", SpellType.GHOSTCARD);
				return;
			}
		}
		else
		{
			this.PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.SPELL_POWER_HINT_IDLE);
			this.PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.SPELL_POWER_HINT_BURST);
			this.PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.POWER_UP);
			this.PreloadSpell("Card_Hand_Ally_SpellTable.prefab:78c9e9fcc292c4a82b35e4b168fb5200", SpellType.SUMMON_OUT_MEDIUM);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.OPPONENT_ATTACK);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.STEALTH);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.DAMAGE);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.DEATH);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_OUT);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.FROZEN);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.FRIENDLY_ATTACK);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_IN_MEDIUM);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_IN);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_IN_OPPONENT);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.BATTLECRY);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.ENCHANT_POSITIVE);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.ENCHANT_NEGATIVE);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.ENCHANT_NEUTRAL);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.TAUNT_STEALTH);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.TRIGGER);
			this.PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.Zzz);
			this.PreloadSpell("Card_Hidden_SpellTable.prefab:c84aa502488424f6d9de41f07b4eb8c5", SpellType.SUMMON_OUT);
			this.PreloadSpell("Card_Hidden_SpellTable.prefab:c84aa502488424f6d9de41f07b4eb8c5", SpellType.SUMMON_IN);
			this.PreloadSpell("Card_Hidden_SpellTable.prefab:c84aa502488424f6d9de41f07b4eb8c5", SpellType.SUMMON_OUT_WEAPON);
			this.PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.ENDGAME_WIN);
			this.PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.OPPONENT_ATTACK);
			this.PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.FRIENDLY_ATTACK);
			this.PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.FROZEN);
			this.PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.DAMAGE);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.ENCHANT_POSITIVE);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.ENCHANT_NEUTRAL);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.ENCHANT_NEGATIVE);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.DAMAGE);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.DEATH);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.SHEATHE);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.UNSHEATHE);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.SUMMON_IN_OPPONENT);
			this.PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.SUMMON_IN_FRIENDLY);
			this.PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.FRIENDLY_ATTACK);
		}
	}

	// Token: 0x0600851D RID: 34077 RVA: 0x002B00E8 File Offset: 0x002AE2E8
	private void PreloadSpell(string tableName, SpellType type)
	{
		SpellTable spellTable = this.GetSpellTable(tableName);
		if (spellTable == null)
		{
			Error.AddDevFatal("SpellCache.PreloadSpell() - Preloaded nonexistent SpellTable {0}", new object[]
			{
				tableName
			});
			return;
		}
		SpellTableEntry spellTableEntry = spellTable.FindEntry(type);
		if (spellTableEntry == null)
		{
			Error.AddDevFatal("SpellCache.PreloadSpell() - SpellTable {0} has no spell of type {1}", new object[]
			{
				tableName,
				type
			});
			return;
		}
		if (spellTableEntry.m_Spell != null)
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(spellTableEntry.m_SpellPrefabName, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Error.AddDevFatal("SpellCache.PreloadSpell() - Failed to load {0}", new object[]
			{
				spellTableEntry.m_SpellPrefabName
			});
			return;
		}
		Spell component = gameObject.GetComponent<Spell>();
		if (component == null)
		{
			Error.AddDevFatal("SpellCache.PreloadSpell() - {0} does not have a Spell component", new object[]
			{
				spellTableEntry.m_SpellPrefabName
			});
			return;
		}
		spellTable.SetSpell(type, component);
	}

	// Token: 0x04006FA6 RID: 28582
	private Map<string, SpellTable> m_spellTableCache = new Map<string, SpellTable>();

	// Token: 0x04006FA7 RID: 28583
	private GameObject m_sceneObject;
}
