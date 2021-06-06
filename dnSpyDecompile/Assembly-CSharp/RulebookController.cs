using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000907 RID: 2311
[RequireComponent(typeof(Actor))]
public class RulebookController : MonoBehaviour
{
	// Token: 0x060080C7 RID: 32967 RVA: 0x0029D848 File Offset: 0x0029BA48
	private void Awake()
	{
		this.m_actor = base.GetComponent<Actor>();
		if (this.m_actor == null)
		{
			Log.Gameplay.PrintError("RulebookController.Awake(): GameObject {0} does not have an Actor Component!", new object[]
			{
				base.gameObject.name
			});
		}
	}

	// Token: 0x060080C8 RID: 32968 RVA: 0x0029D887 File Offset: 0x0029BA87
	public void OnDestroy()
	{
		this.NotifyMousedOut();
	}

	// Token: 0x060080C9 RID: 32969 RVA: 0x0029D88F File Offset: 0x0029BA8F
	public void NotifyMousedOver()
	{
		base.StopCoroutine("WaitThenShowPopup");
		base.StartCoroutine("WaitThenShowPopup");
	}

	// Token: 0x060080CA RID: 32970 RVA: 0x0029D8A8 File Offset: 0x0029BAA8
	public void NotifyMousedOut()
	{
		base.StopCoroutine("WaitThenShowPopup");
		this.HidePopup();
	}

	// Token: 0x060080CB RID: 32971 RVA: 0x0029D8BB File Offset: 0x0029BABB
	private IEnumerator WaitThenShowPopup()
	{
		string widgetName = GameState.Get().GetStringGameOption(GameEntityOption.RULEBOOK_POPUP_PREFAB_PATH);
		if (this.m_hoverPopupWidget == null && !string.IsNullOrEmpty(widgetName))
		{
			this.m_hoverPopupWidget = WidgetInstance.Create(widgetName, false);
			this.m_hoverPopupWidget.transform.position = Vector3.up * 5000f;
			while (!this.m_hoverPopupWidget.IsReady)
			{
				yield return null;
			}
		}
		if (this.m_hoverPopupWidget == null)
		{
			Log.Gameplay.PrintError("RulebookIconController.WaitThenShowPopup: Invalid popup path: {0}", new object[]
			{
				widgetName
			});
			yield break;
		}
		yield return new WaitForSeconds(InputManager.Get().m_MouseOverDelay);
		if (this.GetEntity() == null)
		{
			yield break;
		}
		this.ShowPopup();
		yield break;
	}

	// Token: 0x060080CC RID: 32972 RVA: 0x0029D8CC File Offset: 0x0029BACC
	private void ShowPopup()
	{
		this.m_hoverPopupWidget.Show();
		Vector3 position = Board.Get().FindBone("FriendlyChoice").position;
		this.m_hoverPopupWidget.transform.localPosition = position;
		Spell componentInChildren = this.m_hoverPopupWidget.GetComponentInChildren<Spell>();
		if (!componentInChildren)
		{
			return;
		}
		componentInChildren.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnIntroSpellStateFinished));
		componentInChildren.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x060080CD RID: 32973 RVA: 0x0029D938 File Offset: 0x0029BB38
	private void HidePopup()
	{
		if (this.m_hoverPopupWidget == null)
		{
			return;
		}
		this.m_hoverPopupWidget.Hide();
	}

	// Token: 0x060080CE RID: 32974 RVA: 0x0029D954 File Offset: 0x0029BB54
	private void OnIntroSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(spell);
			spell = null;
		}
	}

	// Token: 0x060080CF RID: 32975 RVA: 0x0029D967 File Offset: 0x0029BB67
	private Entity GetEntity()
	{
		if (this.m_entity == null)
		{
			this.m_entity = this.m_actor.GetEntity();
		}
		return this.m_entity;
	}

	// Token: 0x0400698E RID: 27022
	private const string FriendlyBoneName = "FriendlyChoice";

	// Token: 0x0400698F RID: 27023
	private Actor m_actor;

	// Token: 0x04006990 RID: 27024
	private Entity m_entity;

	// Token: 0x04006991 RID: 27025
	private WidgetInstance m_hoverPopupWidget;
}
