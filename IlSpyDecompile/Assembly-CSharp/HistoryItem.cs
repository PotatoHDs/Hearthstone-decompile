using System.Collections;
using UnityEngine;

public class HistoryItem : MonoBehaviour
{
	public Actor m_tileActor;

	public Actor m_mainCardActor;

	protected bool m_dead;

	protected bool m_burned;

	protected bool m_isPoisonous;

	protected int m_splatAmount;

	protected Entity m_entity;

	protected Texture m_portraitTexture;

	protected Material m_portraitGoldenMaterial;

	protected DefLoader.DisposableCardDef m_cardDef;

	protected bool m_mainCardActorInitialized;

	protected bool m_fatigue;

	protected virtual void Awake()
	{
	}

	protected virtual void OnDestroy()
	{
		m_cardDef?.Dispose();
		m_cardDef = null;
	}

	public Entity GetEntity()
	{
		return m_entity;
	}

	public Texture GetPortraitTexture()
	{
		return m_portraitTexture;
	}

	public Material GetPortraitGoldenMaterial()
	{
		return m_portraitGoldenMaterial;
	}

	public Collider GetTileCollider()
	{
		if (m_tileActor == null)
		{
			return null;
		}
		if (m_tileActor.GetMeshRenderer() == null)
		{
			return null;
		}
		Transform transform = m_tileActor.GetMeshRenderer().transform.Find("Collider");
		if (transform == null)
		{
			return null;
		}
		return transform.GetComponent<Collider>();
	}

	public bool IsMainCardActorInitialized()
	{
		return m_mainCardActorInitialized;
	}

	public void InitializeMainCardActor()
	{
		if (!m_mainCardActorInitialized)
		{
			m_mainCardActor.TurnOffCollider();
			m_mainCardActor.SetActorState(ActorStateType.CARD_HISTORY);
			m_mainCardActorInitialized = true;
		}
	}

	public void DisplaySpells()
	{
		if (m_fatigue)
		{
			return;
		}
		if (m_burned)
		{
			DisplayFlameOnActor(m_mainCardActor);
		}
		else if (m_entity.IsCharacter() || m_entity.IsWeapon())
		{
			if (m_dead && !m_isPoisonous)
			{
				DisplaySkullOnActor(m_mainCardActor);
			}
			else if (m_splatAmount != 0 || m_isPoisonous)
			{
				DisplaySplatOnActor(m_mainCardActor, m_splatAmount, m_isPoisonous);
			}
		}
	}

	private void DisplaySplatOnActor(Actor actor, int damage, bool isPoisonous)
	{
		Spell spell = actor.GetSpell(SpellType.DAMAGE);
		if (!(spell == null))
		{
			DamageSplatSpell obj = (DamageSplatSpell)spell;
			obj.SetDamage(damage);
			obj.SetPoisonous(isPoisonous);
			obj.ActivateState(SpellStateType.IDLE);
			FadeHistoryOverlay(spell.gameObject);
		}
	}

	private void DisplaySkullOnActor(Actor actor)
	{
		Spell spell = actor.GetSpell(SpellType.SKULL);
		if (!(spell == null))
		{
			spell.ActivateState(SpellStateType.IDLE);
			FadeHistoryOverlay(spell.gameObject);
		}
	}

	private void DisplayFlameOnActor(Actor actor)
	{
		Spell spell = actor.GetSpell(SpellType.FLAME_SYMBOL);
		if (!(spell == null))
		{
			spell.ActivateState(SpellStateType.IDLE);
			FadeHistoryOverlay(spell.gameObject);
		}
	}

	private void FadeHistoryOverlay(GameObject gameObject)
	{
		StopAllCoroutines();
		iTween.Stop(gameObject);
		StartCoroutine(FadeHistoryOverlayCoroutine(gameObject));
	}

	private IEnumerator FadeHistoryOverlayCoroutine(GameObject gameObject)
	{
		iTween.FadeTo(gameObject, 1f, 0f);
		yield return new WaitForSeconds(1.5f);
		iTween.FadeTo(gameObject, 0f, 0.5f);
	}

	protected void SetCardDef(DefLoader.DisposableCardDef cardDef)
	{
		m_cardDef?.Dispose();
		m_cardDef = cardDef?.Share();
	}
}
