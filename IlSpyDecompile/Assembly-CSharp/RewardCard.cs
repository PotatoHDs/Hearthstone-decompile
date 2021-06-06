using UnityEngine;

public class RewardCard : MonoBehaviour
{
	public string m_CardID = string.Empty;

	private bool m_Ready;

	private TAG_PREMIUM m_premium;

	private DefLoader.DisposableFullDef m_fullDef;

	private Actor m_actor;

	private GameLayer m_layer = GameLayer.IgnoreFullScreenEffects;

	private void OnDestroy()
	{
		m_Ready = false;
		m_fullDef?.Dispose();
		m_fullDef = null;
	}

	public bool IsReady()
	{
		return m_Ready;
	}

	public void LoadCard(CardRewardData cardData, GameLayer layer = GameLayer.IgnoreFullScreenEffects)
	{
		m_layer = layer;
		m_CardID = cardData.CardID;
		m_premium = cardData.Premium;
		DefLoader.Get().LoadFullDef(m_CardID, OnFullDefLoaded);
	}

	public void Death()
	{
		m_actor.ActivateSpellBirthState(SpellType.DEATH);
	}

	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		using (fullDef)
		{
			if (fullDef == null)
			{
				Debug.LogWarning($"RewardCard.OnFullDefLoaded() - FAILED to load \"{cardId}\"");
				return;
			}
			m_fullDef?.Dispose();
			m_fullDef = fullDef;
			string handActor = ActorNames.GetHandActor(m_fullDef?.EntityDef, m_premium);
			AssetLoader.Get().InstantiatePrefab(handActor, OnActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"RewardCard.OnActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"RewardCard.OnActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_actor = component;
		m_actor.TurnOffCollider();
		m_actor.SetEntityDef(m_fullDef?.EntityDef);
		m_actor.SetCardDef(m_fullDef?.DisposableCardDef);
		m_actor.SetPremium(m_premium);
		m_actor.UpdateAllComponents();
		SceneUtils.SetLayer(component.gameObject, m_layer);
		m_actor.transform.parent = base.transform;
		m_actor.transform.localPosition = Vector3.zero;
		m_actor.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
		m_actor.transform.localScale = Vector3.one;
		m_actor.Show();
		m_Ready = true;
	}
}
