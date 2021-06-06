using System;
using System.Collections.Generic;
using UnityEngine;

public class HandActorCache
{
	public delegate void ActorLoadedCallback(string name, Actor actor, object userData);

	private class ActorLoadedListener : EventListener<ActorLoadedCallback>
	{
		public void Fire(string name, Actor actor)
		{
			m_callback(name, actor, m_userData);
		}
	}

	private class ActorKey
	{
		public TAG_CARDTYPE m_cardType;

		public TAG_PREMIUM m_premiumType;

		public bool m_isHeroSkin;

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ActorKey other = obj as ActorKey;
			return Equals(other);
		}

		public bool Equals(ActorKey other)
		{
			if ((object)other == null)
			{
				return false;
			}
			if (m_cardType == other.m_cardType && m_premiumType == other.m_premiumType)
			{
				return m_isHeroSkin == other.m_isHeroSkin;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ((23 * 17 + m_cardType.GetHashCode()) * 17 + m_premiumType.GetHashCode()) * 17 + m_isHeroSkin.GetHashCode();
		}

		public static bool operator ==(ActorKey a, ActorKey b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public static bool operator !=(ActorKey a, ActorKey b)
		{
			return !(a == b);
		}
	}

	private readonly TAG_CARDTYPE[] ACTOR_CARD_TYPES = new TAG_CARDTYPE[4]
	{
		TAG_CARDTYPE.MINION,
		TAG_CARDTYPE.SPELL,
		TAG_CARDTYPE.WEAPON,
		TAG_CARDTYPE.HERO
	};

	private Map<ActorKey, Actor> m_actorMap = new Map<ActorKey, Actor>();

	private List<ActorLoadedListener> m_loadedListeners = new List<ActorLoadedListener>();

	public void Initialize()
	{
		TAG_CARDTYPE[] aCTOR_CARD_TYPES = ACTOR_CARD_TYPES;
		foreach (TAG_CARDTYPE tAG_CARDTYPE in aCTOR_CARD_TYPES)
		{
			foreach (TAG_PREMIUM value in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				if (tAG_CARDTYPE == TAG_CARDTYPE.HERO)
				{
					string heroSkinOrHandActor = ActorNames.GetHeroSkinOrHandActor(tAG_CARDTYPE, value);
					ActorKey callbackData = MakeActorKey(tAG_CARDTYPE, value, isHeroSkin: true);
					AssetLoader.Get().InstantiatePrefab(heroSkinOrHandActor, OnActorLoaded, callbackData, AssetLoadingOptions.IgnorePrefabPosition);
					heroSkinOrHandActor = ActorNames.GetHandActor(tAG_CARDTYPE, value);
					callbackData = MakeActorKey(tAG_CARDTYPE, value);
					AssetLoader.Get().InstantiatePrefab(heroSkinOrHandActor, OnActorLoaded, callbackData, AssetLoadingOptions.IgnorePrefabPosition);
				}
				else
				{
					string heroSkinOrHandActor = ActorNames.GetHeroSkinOrHandActor(tAG_CARDTYPE, value);
					ActorKey callbackData = MakeActorKey(tAG_CARDTYPE, value);
					AssetLoader.Get().InstantiatePrefab(heroSkinOrHandActor, OnActorLoaded, callbackData, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
		}
	}

	public bool IsInitializing()
	{
		TAG_CARDTYPE[] aCTOR_CARD_TYPES = ACTOR_CARD_TYPES;
		foreach (TAG_CARDTYPE tAG_CARDTYPE in aCTOR_CARD_TYPES)
		{
			foreach (TAG_PREMIUM value in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				if (tAG_CARDTYPE == TAG_CARDTYPE.HERO)
				{
					ActorKey key = MakeActorKey(tAG_CARDTYPE, value, isHeroSkin: true);
					ActorKey key2 = MakeActorKey(tAG_CARDTYPE, value);
					if (!m_actorMap.ContainsKey(key) || !m_actorMap.ContainsKey(key2))
					{
						return true;
					}
				}
				else
				{
					ActorKey key3 = MakeActorKey(tAG_CARDTYPE, value);
					if (!m_actorMap.ContainsKey(key3))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public Actor GetActor(EntityDef entityDef, TAG_PREMIUM premium)
	{
		ActorKey key = MakeActorKey(entityDef, premium, entityDef.IsHeroSkin());
		if (!m_actorMap.TryGetValue(key, out var value))
		{
			Debug.LogError($"HandActorCache.GetActor() - FAILED to get actor with cardType={entityDef.GetCardType()} premiumType={premium}");
			return null;
		}
		return value;
	}

	public void AddActorLoadedListener(ActorLoadedCallback callback)
	{
		AddActorLoadedListener(callback, null);
	}

	public void AddActorLoadedListener(ActorLoadedCallback callback, object userData)
	{
		ActorLoadedListener actorLoadedListener = new ActorLoadedListener();
		actorLoadedListener.SetCallback(callback);
		actorLoadedListener.SetUserData(userData);
		if (!m_loadedListeners.Contains(actorLoadedListener))
		{
			m_loadedListeners.Add(actorLoadedListener);
		}
	}

	public bool RemoveActorLoadedListener(ActorLoadedCallback callback)
	{
		return RemoveActorLoadedListener(callback, null);
	}

	public bool RemoveActorLoadedListener(ActorLoadedCallback callback, object userData)
	{
		ActorLoadedListener actorLoadedListener = new ActorLoadedListener();
		actorLoadedListener.SetCallback(callback);
		actorLoadedListener.SetUserData(userData);
		return m_loadedListeners.Remove(actorLoadedListener);
	}

	private ActorKey MakeActorKey(EntityDef entityDef, TAG_PREMIUM premium, bool isHeroSkin = false)
	{
		return MakeActorKey(entityDef.GetCardType(), premium, isHeroSkin);
	}

	private ActorKey MakeActorKey(TAG_CARDTYPE cardType, TAG_PREMIUM premiumType, bool isHeroSkin = false)
	{
		return new ActorKey
		{
			m_cardType = cardType,
			m_premiumType = premiumType,
			m_isHeroSkin = isHeroSkin
		};
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"HandActorCache.OnActorLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"HandActorCache.OnActorLoaded() - ERROR \"{assetRef}\" has no Actor component");
			return;
		}
		go.transform.position = new Vector3(-99999f, -99999f, 99999f);
		ActorKey key = (ActorKey)callbackData;
		m_actorMap.Add(key, component);
		FireActorLoadedListeners(assetRef.ToString(), component);
	}

	private void FireActorLoadedListeners(string assetRef, Actor actor)
	{
		ActorLoadedListener[] array = m_loadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(assetRef, actor);
		}
	}
}
