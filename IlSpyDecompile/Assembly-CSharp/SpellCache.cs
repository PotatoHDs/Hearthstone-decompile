using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class SpellCache : IService
{
	private Map<string, SpellTable> m_spellTableCache = new Map<string, SpellTable>();

	private GameObject m_sceneObject;

	private GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("SpellCacheSceneObject", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield return new ServiceSoftDependency(typeof(SceneMgr), serviceLocator);
		if (serviceLocator.TryGetService<SceneMgr>(out var service))
		{
			service.RegisterScenePreLoadEvent(OnScenePreLoad);
		}
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(IAssetLoader) };
	}

	public void Shutdown()
	{
	}

	public static SpellCache Get()
	{
		if (!HearthstoneServices.TryGet<SpellCache>(out var service) && !Application.isEditor)
		{
			Debug.LogError("Attempting to access null SpellCache");
			return null;
		}
		return service;
	}

	public SpellTable GetSpellTable(string prefabPath)
	{
		if (!m_spellTableCache.TryGetValue(prefabPath, out var value))
		{
			return LoadSpellTable(prefabPath);
		}
		return value;
	}

	public void Clear()
	{
		foreach (KeyValuePair<string, SpellTable> item in m_spellTableCache)
		{
			item.Value.ReleaseAllSpells();
		}
	}

	private SpellTable LoadSpellTable(string prefabPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(prefabPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Error.AddDevFatal("SpellCache.LoadSpellTable() - SpellCache GameObject failed to load");
			return null;
		}
		SpellTable component = gameObject.GetComponent<SpellTable>();
		if (component == null)
		{
			Error.AddDevFatal("SpellCache.LoadSpellTable() - SpellCache has no SpellTable component ");
			return null;
		}
		component.transform.parent = SceneObject.transform;
		m_spellTableCache.Add(prefabPath, component);
		return component;
	}

	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
	{
		switch (mode)
		{
		case SceneMgr.Mode.COLLECTIONMANAGER:
		case SceneMgr.Mode.TAVERN_BRAWL:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			PreloadSpell("Card_Hand_Ally_SpellTable.prefab:78c9e9fcc292c4a82b35e4b168fb5200", SpellType.DEATHREVERSE);
			PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.DEATHREVERSE);
			PreloadSpell("Card_Hand_Weapon_SpellTable.prefab:548dce68672b046e1aec1fd629082eb2", SpellType.DEATHREVERSE);
			PreloadSpell("Card_Hand_Ally_SpellTable.prefab:78c9e9fcc292c4a82b35e4b168fb5200", SpellType.GHOSTCARD);
			PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.GHOSTCARD);
			PreloadSpell("Card_Hand_Weapon_SpellTable.prefab:548dce68672b046e1aec1fd629082eb2", SpellType.GHOSTCARD);
			break;
		case SceneMgr.Mode.GAMEPLAY:
			PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.SPELL_POWER_HINT_IDLE);
			PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.SPELL_POWER_HINT_BURST);
			PreloadSpell("Card_Hand_Ability_SpellTable.prefab:62c19ebc0789b4f00b9f393b17349cb2", SpellType.POWER_UP);
			PreloadSpell("Card_Hand_Ally_SpellTable.prefab:78c9e9fcc292c4a82b35e4b168fb5200", SpellType.SUMMON_OUT_MEDIUM);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.OPPONENT_ATTACK);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.STEALTH);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.DAMAGE);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.DEATH);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_OUT);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.FROZEN);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.FRIENDLY_ATTACK);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_IN_MEDIUM);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_IN);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.SUMMON_IN_OPPONENT);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.BATTLECRY);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.ENCHANT_POSITIVE);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.ENCHANT_NEGATIVE);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.ENCHANT_NEUTRAL);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.TAUNT_STEALTH);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.TRIGGER);
			PreloadSpell("Card_Play_Ally_SpellTable.prefab:81be4c58f5a9d454e815f1b0703d8179", SpellType.Zzz);
			PreloadSpell("Card_Hidden_SpellTable.prefab:c84aa502488424f6d9de41f07b4eb8c5", SpellType.SUMMON_OUT);
			PreloadSpell("Card_Hidden_SpellTable.prefab:c84aa502488424f6d9de41f07b4eb8c5", SpellType.SUMMON_IN);
			PreloadSpell("Card_Hidden_SpellTable.prefab:c84aa502488424f6d9de41f07b4eb8c5", SpellType.SUMMON_OUT_WEAPON);
			PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.ENDGAME_WIN);
			PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.OPPONENT_ATTACK);
			PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.FRIENDLY_ATTACK);
			PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.FROZEN);
			PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.DAMAGE);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.ENCHANT_POSITIVE);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.ENCHANT_NEUTRAL);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.ENCHANT_NEGATIVE);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.DAMAGE);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.DEATH);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.SHEATHE);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.UNSHEATHE);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.SUMMON_IN_OPPONENT);
			PreloadSpell("Card_Play_Weapon_SpellTable.prefab:9eb63fe4f82064b50b443e1b15a4825d", SpellType.SUMMON_IN_FRIENDLY);
			PreloadSpell("Card_Play_Hero_SpellTable.prefab:c7908ae61ab34412aa7f5a9bf3c89cf2", SpellType.FRIENDLY_ATTACK);
			break;
		}
	}

	private void PreloadSpell(string tableName, SpellType type)
	{
		SpellTable spellTable = GetSpellTable(tableName);
		if (spellTable == null)
		{
			Error.AddDevFatal("SpellCache.PreloadSpell() - Preloaded nonexistent SpellTable {0}", tableName);
			return;
		}
		SpellTableEntry spellTableEntry = spellTable.FindEntry(type);
		if (spellTableEntry == null)
		{
			Error.AddDevFatal("SpellCache.PreloadSpell() - SpellTable {0} has no spell of type {1}", tableName, type);
		}
		else
		{
			if (spellTableEntry.m_Spell != null)
			{
				return;
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(spellTableEntry.m_SpellPrefabName);
			if (gameObject == null)
			{
				Error.AddDevFatal("SpellCache.PreloadSpell() - Failed to load {0}", spellTableEntry.m_SpellPrefabName);
				return;
			}
			Spell component = gameObject.GetComponent<Spell>();
			if (component == null)
			{
				Error.AddDevFatal("SpellCache.PreloadSpell() - {0} does not have a Spell component", spellTableEntry.m_SpellPrefabName);
			}
			else
			{
				spellTable.SetSpell(type, component);
			}
		}
	}
}
