using System;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public class RewardCard : MonoBehaviour
{
	// Token: 0x06002511 RID: 9489 RVA: 0x000BAAA7 File Offset: 0x000B8CA7
	private void OnDestroy()
	{
		this.m_Ready = false;
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef != null)
		{
			fullDef.Dispose();
		}
		this.m_fullDef = null;
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x000BAAC8 File Offset: 0x000B8CC8
	public bool IsReady()
	{
		return this.m_Ready;
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x000BAAD0 File Offset: 0x000B8CD0
	public void LoadCard(CardRewardData cardData, GameLayer layer = GameLayer.IgnoreFullScreenEffects)
	{
		this.m_layer = layer;
		this.m_CardID = cardData.CardID;
		this.m_premium = cardData.Premium;
		DefLoader.Get().LoadFullDef(this.m_CardID, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x000BAB0F File Offset: 0x000B8D0F
	public void Death()
	{
		this.m_actor.ActivateSpellBirthState(SpellType.DEATH);
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x000BAB20 File Offset: 0x000B8D20
	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		try
		{
			if (fullDef == null)
			{
				Debug.LogWarning(string.Format("RewardCard.OnFullDefLoaded() - FAILED to load \"{0}\"", cardId));
			}
			else
			{
				DefLoader.DisposableFullDef fullDef2 = this.m_fullDef;
				if (fullDef2 != null)
				{
					fullDef2.Dispose();
				}
				this.m_fullDef = fullDef;
				DefLoader.DisposableFullDef fullDef3 = this.m_fullDef;
				string handActor = ActorNames.GetHandActor((fullDef3 != null) ? fullDef3.EntityDef : null, this.m_premium);
				AssetLoader.Get().InstantiatePrefab(handActor, new PrefabCallback<GameObject>(this.OnActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
			}
		}
		finally
		{
			if (fullDef != null)
			{
				((IDisposable)fullDef).Dispose();
			}
		}
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x000BABB8 File Offset: 0x000B8DB8
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("RewardCard.OnActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("RewardCard.OnActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_actor = component;
		this.m_actor.TurnOffCollider();
		Actor actor = this.m_actor;
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		actor.SetEntityDef((fullDef != null) ? fullDef.EntityDef : null);
		Actor actor2 = this.m_actor;
		DefLoader.DisposableFullDef fullDef2 = this.m_fullDef;
		actor2.SetCardDef((fullDef2 != null) ? fullDef2.DisposableCardDef : null);
		this.m_actor.SetPremium(this.m_premium);
		this.m_actor.UpdateAllComponents();
		SceneUtils.SetLayer(component.gameObject, this.m_layer);
		this.m_actor.transform.parent = base.transform;
		this.m_actor.transform.localPosition = Vector3.zero;
		this.m_actor.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
		this.m_actor.transform.localScale = Vector3.one;
		this.m_actor.Show();
		this.m_Ready = true;
	}

	// Token: 0x040014AE RID: 5294
	public string m_CardID = string.Empty;

	// Token: 0x040014AF RID: 5295
	private bool m_Ready;

	// Token: 0x040014B0 RID: 5296
	private TAG_PREMIUM m_premium;

	// Token: 0x040014B1 RID: 5297
	private DefLoader.DisposableFullDef m_fullDef;

	// Token: 0x040014B2 RID: 5298
	private Actor m_actor;

	// Token: 0x040014B3 RID: 5299
	private GameLayer m_layer = GameLayer.IgnoreFullScreenEffects;
}
