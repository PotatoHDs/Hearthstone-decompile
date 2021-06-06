using UnityEngine;

public class RewardCardBack : MonoBehaviour
{
	public GameObject m_cardbackBone;

	public UberText m_cardbackTitle;

	public UberText m_cardbackName;

	public int m_CardBackID = -1;

	private bool m_Ready;

	private Actor m_actor;

	private GameLayer m_layer = GameLayer.IgnoreFullScreenEffects;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
		m_Ready = false;
	}

	public bool IsReady()
	{
		return m_Ready;
	}

	public void LoadCardBack(CardBackRewardData cardbackData, GameLayer layer = GameLayer.IgnoreFullScreenEffects)
	{
		m_layer = layer;
		m_CardBackID = cardbackData.CardBackID;
		CardBackManager.Get().LoadCardBackByIndex(m_CardBackID, OnCardBackLoaded);
	}

	public void Death()
	{
		m_actor.ActivateSpellBirthState(SpellType.DEATH);
	}

	private void OnCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		GameObject gameObject = cardbackData.m_GameObject;
		gameObject.transform.parent = m_cardbackBone.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		gameObject.transform.localScale = Vector3.one;
		SceneUtils.SetLayer(gameObject, m_layer);
		m_actor = gameObject.GetComponent<Actor>();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_cardbackTitle.Text = "GLOBAL_SEASON_END_NEW_CARDBACK_TITLE_PHONE";
		}
		m_cardbackName.Text = cardbackData.m_Name;
		m_Ready = true;
	}
}
