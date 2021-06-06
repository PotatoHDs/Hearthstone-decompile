using System.Collections;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class RulebookController : MonoBehaviour
{
	private const string FriendlyBoneName = "FriendlyChoice";

	private Actor m_actor;

	private Entity m_entity;

	private WidgetInstance m_hoverPopupWidget;

	private void Awake()
	{
		m_actor = GetComponent<Actor>();
		if (m_actor == null)
		{
			Log.Gameplay.PrintError("RulebookController.Awake(): GameObject {0} does not have an Actor Component!", base.gameObject.name);
		}
	}

	public void OnDestroy()
	{
		NotifyMousedOut();
	}

	public void NotifyMousedOver()
	{
		StopCoroutine("WaitThenShowPopup");
		StartCoroutine("WaitThenShowPopup");
	}

	public void NotifyMousedOut()
	{
		StopCoroutine("WaitThenShowPopup");
		HidePopup();
	}

	private IEnumerator WaitThenShowPopup()
	{
		string widgetName = GameState.Get().GetStringGameOption(GameEntityOption.RULEBOOK_POPUP_PREFAB_PATH);
		if (m_hoverPopupWidget == null && !string.IsNullOrEmpty(widgetName))
		{
			m_hoverPopupWidget = WidgetInstance.Create(widgetName);
			m_hoverPopupWidget.transform.position = Vector3.up * 5000f;
			while (!m_hoverPopupWidget.IsReady)
			{
				yield return null;
			}
		}
		if (m_hoverPopupWidget == null)
		{
			Log.Gameplay.PrintError("RulebookIconController.WaitThenShowPopup: Invalid popup path: {0}", widgetName);
			yield break;
		}
		yield return new WaitForSeconds(InputManager.Get().m_MouseOverDelay);
		if (GetEntity() != null)
		{
			ShowPopup();
		}
	}

	private void ShowPopup()
	{
		m_hoverPopupWidget.Show();
		Vector3 position = Board.Get().FindBone("FriendlyChoice").position;
		m_hoverPopupWidget.transform.localPosition = position;
		Spell componentInChildren = m_hoverPopupWidget.GetComponentInChildren<Spell>();
		if ((bool)componentInChildren)
		{
			componentInChildren.AddStateFinishedCallback(OnIntroSpellStateFinished);
			componentInChildren.ActivateState(SpellStateType.BIRTH);
		}
	}

	private void HidePopup()
	{
		if (!(m_hoverPopupWidget == null))
		{
			m_hoverPopupWidget.Hide();
		}
	}

	private void OnIntroSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell);
			spell = null;
		}
	}

	private Entity GetEntity()
	{
		if (m_entity == null)
		{
			m_entity = m_actor.GetEntity();
		}
		return m_entity;
	}
}
