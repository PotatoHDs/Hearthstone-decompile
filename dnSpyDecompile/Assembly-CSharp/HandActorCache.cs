using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008D1 RID: 2257
public class HandActorCache
{
	// Token: 0x06007CD3 RID: 31955 RVA: 0x00289838 File Offset: 0x00287A38
	public void Initialize()
	{
		foreach (TAG_CARDTYPE tag_CARDTYPE in this.ACTOR_CARD_TYPES)
		{
			foreach (object obj in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				TAG_PREMIUM tag_PREMIUM = (TAG_PREMIUM)obj;
				if (tag_CARDTYPE == TAG_CARDTYPE.HERO)
				{
					string input = ActorNames.GetHeroSkinOrHandActor(tag_CARDTYPE, tag_PREMIUM);
					HandActorCache.ActorKey callbackData = this.MakeActorKey(tag_CARDTYPE, tag_PREMIUM, true);
					AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnActorLoaded), callbackData, AssetLoadingOptions.IgnorePrefabPosition);
					input = ActorNames.GetHandActor(tag_CARDTYPE, tag_PREMIUM, false);
					callbackData = this.MakeActorKey(tag_CARDTYPE, tag_PREMIUM, false);
					AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnActorLoaded), callbackData, AssetLoadingOptions.IgnorePrefabPosition);
				}
				else
				{
					string input = ActorNames.GetHeroSkinOrHandActor(tag_CARDTYPE, tag_PREMIUM);
					HandActorCache.ActorKey callbackData = this.MakeActorKey(tag_CARDTYPE, tag_PREMIUM, false);
					AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnActorLoaded), callbackData, AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
		}
	}

	// Token: 0x06007CD4 RID: 31956 RVA: 0x00289968 File Offset: 0x00287B68
	public bool IsInitializing()
	{
		foreach (TAG_CARDTYPE tag_CARDTYPE in this.ACTOR_CARD_TYPES)
		{
			foreach (object obj in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				TAG_PREMIUM premiumType = (TAG_PREMIUM)obj;
				if (tag_CARDTYPE == TAG_CARDTYPE.HERO)
				{
					HandActorCache.ActorKey key = this.MakeActorKey(tag_CARDTYPE, premiumType, true);
					HandActorCache.ActorKey key2 = this.MakeActorKey(tag_CARDTYPE, premiumType, false);
					if (!this.m_actorMap.ContainsKey(key) || !this.m_actorMap.ContainsKey(key2))
					{
						return true;
					}
				}
				else
				{
					HandActorCache.ActorKey key3 = this.MakeActorKey(tag_CARDTYPE, premiumType, false);
					if (!this.m_actorMap.ContainsKey(key3))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06007CD5 RID: 31957 RVA: 0x00289A4C File Offset: 0x00287C4C
	public Actor GetActor(EntityDef entityDef, TAG_PREMIUM premium)
	{
		HandActorCache.ActorKey key = this.MakeActorKey(entityDef, premium, entityDef.IsHeroSkin());
		Actor result;
		if (!this.m_actorMap.TryGetValue(key, out result))
		{
			Debug.LogError(string.Format("HandActorCache.GetActor() - FAILED to get actor with cardType={0} premiumType={1}", entityDef.GetCardType(), premium));
			return null;
		}
		return result;
	}

	// Token: 0x06007CD6 RID: 31958 RVA: 0x00289A9B File Offset: 0x00287C9B
	public void AddActorLoadedListener(HandActorCache.ActorLoadedCallback callback)
	{
		this.AddActorLoadedListener(callback, null);
	}

	// Token: 0x06007CD7 RID: 31959 RVA: 0x00289AA8 File Offset: 0x00287CA8
	public void AddActorLoadedListener(HandActorCache.ActorLoadedCallback callback, object userData)
	{
		HandActorCache.ActorLoadedListener actorLoadedListener = new HandActorCache.ActorLoadedListener();
		actorLoadedListener.SetCallback(callback);
		actorLoadedListener.SetUserData(userData);
		if (this.m_loadedListeners.Contains(actorLoadedListener))
		{
			return;
		}
		this.m_loadedListeners.Add(actorLoadedListener);
	}

	// Token: 0x06007CD8 RID: 31960 RVA: 0x00289AE4 File Offset: 0x00287CE4
	public bool RemoveActorLoadedListener(HandActorCache.ActorLoadedCallback callback)
	{
		return this.RemoveActorLoadedListener(callback, null);
	}

	// Token: 0x06007CD9 RID: 31961 RVA: 0x00289AF0 File Offset: 0x00287CF0
	public bool RemoveActorLoadedListener(HandActorCache.ActorLoadedCallback callback, object userData)
	{
		HandActorCache.ActorLoadedListener actorLoadedListener = new HandActorCache.ActorLoadedListener();
		actorLoadedListener.SetCallback(callback);
		actorLoadedListener.SetUserData(userData);
		return this.m_loadedListeners.Remove(actorLoadedListener);
	}

	// Token: 0x06007CDA RID: 31962 RVA: 0x00289B1D File Offset: 0x00287D1D
	private HandActorCache.ActorKey MakeActorKey(EntityDef entityDef, TAG_PREMIUM premium, bool isHeroSkin = false)
	{
		return this.MakeActorKey(entityDef.GetCardType(), premium, isHeroSkin);
	}

	// Token: 0x06007CDB RID: 31963 RVA: 0x00289B2D File Offset: 0x00287D2D
	private HandActorCache.ActorKey MakeActorKey(TAG_CARDTYPE cardType, TAG_PREMIUM premiumType, bool isHeroSkin = false)
	{
		return new HandActorCache.ActorKey
		{
			m_cardType = cardType,
			m_premiumType = premiumType,
			m_isHeroSkin = isHeroSkin
		};
	}

	// Token: 0x06007CDC RID: 31964 RVA: 0x00289B4C File Offset: 0x00287D4C
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("HandActorCache.OnActorLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("HandActorCache.OnActorLoaded() - ERROR \"{0}\" has no Actor component", assetRef));
			return;
		}
		go.transform.position = new Vector3(-99999f, -99999f, 99999f);
		HandActorCache.ActorKey key = (HandActorCache.ActorKey)callbackData;
		this.m_actorMap.Add(key, component);
		this.FireActorLoadedListeners(assetRef.ToString(), component);
	}

	// Token: 0x06007CDD RID: 31965 RVA: 0x00289BD4 File Offset: 0x00287DD4
	private void FireActorLoadedListeners(string assetRef, Actor actor)
	{
		HandActorCache.ActorLoadedListener[] array = this.m_loadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(assetRef, actor);
		}
	}

	// Token: 0x04006575 RID: 25973
	private readonly TAG_CARDTYPE[] ACTOR_CARD_TYPES = new TAG_CARDTYPE[]
	{
		TAG_CARDTYPE.MINION,
		TAG_CARDTYPE.SPELL,
		TAG_CARDTYPE.WEAPON,
		TAG_CARDTYPE.HERO
	};

	// Token: 0x04006576 RID: 25974
	private Map<HandActorCache.ActorKey, Actor> m_actorMap = new Map<HandActorCache.ActorKey, Actor>();

	// Token: 0x04006577 RID: 25975
	private List<HandActorCache.ActorLoadedListener> m_loadedListeners = new List<HandActorCache.ActorLoadedListener>();

	// Token: 0x0200255A RID: 9562
	// (Invoke) Token: 0x060132BB RID: 78523
	public delegate void ActorLoadedCallback(string name, Actor actor, object userData);

	// Token: 0x0200255B RID: 9563
	private class ActorLoadedListener : EventListener<HandActorCache.ActorLoadedCallback>
	{
		// Token: 0x060132BE RID: 78526 RVA: 0x0052B448 File Offset: 0x00529648
		public void Fire(string name, Actor actor)
		{
			this.m_callback(name, actor, this.m_userData);
		}
	}

	// Token: 0x0200255C RID: 9564
	private class ActorKey
	{
		// Token: 0x060132C0 RID: 78528 RVA: 0x0052B468 File Offset: 0x00529668
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			HandActorCache.ActorKey other = obj as HandActorCache.ActorKey;
			return this.Equals(other);
		}

		// Token: 0x060132C1 RID: 78529 RVA: 0x0052B488 File Offset: 0x00529688
		public bool Equals(HandActorCache.ActorKey other)
		{
			return other != null && (this.m_cardType == other.m_cardType && this.m_premiumType == other.m_premiumType) && this.m_isHeroSkin == other.m_isHeroSkin;
		}

		// Token: 0x060132C2 RID: 78530 RVA: 0x0052B4BB File Offset: 0x005296BB
		public override int GetHashCode()
		{
			return ((23 * 17 + this.m_cardType.GetHashCode()) * 17 + this.m_premiumType.GetHashCode()) * 17 + this.m_isHeroSkin.GetHashCode();
		}

		// Token: 0x060132C3 RID: 78531 RVA: 0x0052B4F8 File Offset: 0x005296F8
		public static bool operator ==(HandActorCache.ActorKey a, HandActorCache.ActorKey b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x060132C4 RID: 78532 RVA: 0x0052B50F File Offset: 0x0052970F
		public static bool operator !=(HandActorCache.ActorKey a, HandActorCache.ActorKey b)
		{
			return !(a == b);
		}

		// Token: 0x0400ED34 RID: 60724
		public TAG_CARDTYPE m_cardType;

		// Token: 0x0400ED35 RID: 60725
		public TAG_PREMIUM m_premiumType;

		// Token: 0x0400ED36 RID: 60726
		public bool m_isHeroSkin;
	}
}
