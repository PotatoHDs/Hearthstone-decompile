using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class HistoryChildCard : HistoryItem
{
	// Token: 0x06002C71 RID: 11377 RVA: 0x000DFCB4 File Offset: 0x000DDEB4
	public void SetCardInfo(Entity entity, DefLoader.DisposableCardDef cardDef, int splatAmount, bool isDead, bool isBurnedCard, bool isPoisonous)
	{
		this.m_entity = entity;
		this.m_portraitTexture = cardDef.CardDef.GetPortraitTexture();
		this.m_portraitGoldenMaterial = cardDef.CardDef.GetPremiumPortraitMaterial();
		base.SetCardDef(cardDef);
		this.m_splatAmount = splatAmount;
		this.m_dead = isDead;
		this.m_burned = isBurnedCard;
		this.m_isPoisonous = isPoisonous;
	}

	// Token: 0x06002C72 RID: 11378 RVA: 0x000DFD10 File Offset: 0x000DDF10
	public void LoadMainCardActor()
	{
		string historyActor = ActorNames.GetHistoryActor(this.m_entity, HistoryInfoType.NONE);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(historyActor, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryChildCard.LoadActorCallback() - FAILED to load actor \"{0}\"", new object[]
			{
				historyActor
			});
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryChildCard.LoadActorCallback() - ERROR actor \"{0}\" has no Actor component", new object[]
			{
				historyActor
			});
			return;
		}
		this.m_mainCardActor = component;
		this.m_mainCardActor.SetPremium(this.m_entity.GetPremiumType());
		this.m_mainCardActor.SetWatermarkCardSetOverride(this.m_entity.GetWatermarkCardSetOverride());
		this.m_mainCardActor.SetHistoryItem(this);
		this.m_mainCardActor.UpdateAllComponents();
		SceneUtils.SetLayer(this.m_mainCardActor.gameObject, GameLayer.Tooltip);
		this.m_mainCardActor.Hide();
	}
}
