using System;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class RewardCardBack : MonoBehaviour
{
	// Token: 0x06002518 RID: 9496 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x000BAD0A File Offset: 0x000B8F0A
	private void OnDestroy()
	{
		this.m_Ready = false;
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x000BAD13 File Offset: 0x000B8F13
	public bool IsReady()
	{
		return this.m_Ready;
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x000BAD1B File Offset: 0x000B8F1B
	public void LoadCardBack(CardBackRewardData cardbackData, GameLayer layer = GameLayer.IgnoreFullScreenEffects)
	{
		this.m_layer = layer;
		this.m_CardBackID = cardbackData.CardBackID;
		CardBackManager.Get().LoadCardBackByIndex(this.m_CardBackID, new CardBackManager.LoadCardBackData.LoadCardBackCallback(this.OnCardBackLoaded), null);
	}

	// Token: 0x0600251C RID: 9500 RVA: 0x000BAD4E File Offset: 0x000B8F4E
	public void Death()
	{
		this.m_actor.ActivateSpellBirthState(SpellType.DEATH);
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x000BAD60 File Offset: 0x000B8F60
	private void OnCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		GameObject gameObject = cardbackData.m_GameObject;
		gameObject.transform.parent = this.m_cardbackBone.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		gameObject.transform.localScale = Vector3.one;
		SceneUtils.SetLayer(gameObject, this.m_layer);
		this.m_actor = gameObject.GetComponent<Actor>();
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_cardbackTitle.Text = "GLOBAL_SEASON_END_NEW_CARDBACK_TITLE_PHONE";
		}
		this.m_cardbackName.Text = cardbackData.m_Name;
		this.m_Ready = true;
	}

	// Token: 0x040014B4 RID: 5300
	public GameObject m_cardbackBone;

	// Token: 0x040014B5 RID: 5301
	public UberText m_cardbackTitle;

	// Token: 0x040014B6 RID: 5302
	public UberText m_cardbackName;

	// Token: 0x040014B7 RID: 5303
	public int m_CardBackID = -1;

	// Token: 0x040014B8 RID: 5304
	private bool m_Ready;

	// Token: 0x040014B9 RID: 5305
	private Actor m_actor;

	// Token: 0x040014BA RID: 5306
	private GameLayer m_layer = GameLayer.IgnoreFullScreenEffects;
}
