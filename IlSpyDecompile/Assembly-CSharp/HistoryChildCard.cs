using UnityEngine;

public class HistoryChildCard : HistoryItem
{
	public void SetCardInfo(Entity entity, DefLoader.DisposableCardDef cardDef, int splatAmount, bool isDead, bool isBurnedCard, bool isPoisonous)
	{
		m_entity = entity;
		m_portraitTexture = cardDef.CardDef.GetPortraitTexture();
		m_portraitGoldenMaterial = cardDef.CardDef.GetPremiumPortraitMaterial();
		SetCardDef(cardDef);
		m_splatAmount = splatAmount;
		m_dead = isDead;
		m_burned = isBurnedCard;
		m_isPoisonous = isPoisonous;
	}

	public void LoadMainCardActor()
	{
		string historyActor = ActorNames.GetHistoryActor(m_entity, HistoryInfoType.NONE);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(historyActor, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryChildCard.LoadActorCallback() - FAILED to load actor \"{0}\"", historyActor);
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryChildCard.LoadActorCallback() - ERROR actor \"{0}\" has no Actor component", historyActor);
			return;
		}
		m_mainCardActor = component;
		m_mainCardActor.SetPremium(m_entity.GetPremiumType());
		m_mainCardActor.SetWatermarkCardSetOverride(m_entity.GetWatermarkCardSetOverride());
		m_mainCardActor.SetHistoryItem(this);
		m_mainCardActor.UpdateAllComponents();
		SceneUtils.SetLayer(m_mainCardActor.gameObject, GameLayer.Tooltip);
		m_mainCardActor.Hide();
	}
}
