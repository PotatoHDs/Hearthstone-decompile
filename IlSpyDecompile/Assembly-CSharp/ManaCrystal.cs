using System.Collections;
using UnityEngine;

public class ManaCrystal : MonoBehaviour
{
	public enum State
	{
		READY,
		USED,
		PROPOSED,
		DESTROYED
	}

	public GameObject gem;

	public GameObject spawnEffects;

	public GameObject gemDestroy;

	public GameObject tempSpawnEffects;

	public GameObject tempGemDestroy;

	private readonly string ANIM_SPAWN_EFFECTS = "mana_spawn_edit";

	private readonly string ANIM_TEMP_SPAWN_EFFECTS = "mana_spawn_edit_temp";

	private readonly string ANIM_MANA_GEM_BIRTH = "ManaGemBirth";

	private readonly string ANIM_TEMP_MANA_GEM_BIRTH = "ManaGemBirth_Temp";

	private readonly string ANIM_READY_TO_USED = "ManaGemUsed";

	private readonly string ANIM_USED_TO_READY = "ManaGem_Restore";

	private readonly string ANIM_READY_TO_PROPOSED = "ManaGemProposed";

	private readonly string ANIM_TEMP_READY_TO_PROPOSED = "ManaGemProposed_Temp";

	private readonly string ANIM_PROPOSED_TO_READY = "ManaGemProposed_Cancel";

	private readonly string ANIM_TEMP_PROPOSED_TO_READY = "ManaGemProposed_Cancel_Temp";

	private readonly string ANIM_USED_TO_PROPOSED = "ManaGemUsed_Proposed";

	private readonly string ANIM_PROPOSED_TO_USED = "ManaGemProposed_Used";

	private bool m_isInGame = true;

	private bool m_birthAnimationPlayed;

	private bool m_playingAnimation;

	private bool m_isTemp;

	private Spell m_overloadOwedSpell;

	private Spell m_overloadPaidSpell;

	private State m_state;

	private State m_visibleState;

	public State state
	{
		get
		{
			return m_state;
		}
		set
		{
			if (m_state != State.DESTROYED)
			{
				if (value == State.DESTROYED)
				{
					Destroy();
				}
				else
				{
					m_state = value;
				}
			}
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		State state = this.state;
		if (state != m_visibleState && state != State.DESTROYED)
		{
			string transitionAnimName = GetTransitionAnimName(m_visibleState, state);
			PlayGemAnimation(transitionAnimName, state);
		}
	}

	public void MarkAsNotInGame()
	{
		m_isInGame = false;
	}

	public void MarkAsTemp()
	{
		m_isTemp = true;
		ManaCrystalMgr manaCrystalMgr = ManaCrystalMgr.Get();
		gem.GetComponentInChildren<MeshRenderer>().SetMaterial(manaCrystalMgr.GetTemporaryManaCrystalMaterial());
		gem.transform.Find("Proposed_Quad").gameObject.GetComponent<MeshRenderer>().SetMaterial(manaCrystalMgr.GetTemporaryManaCrystalProposedQuadMaterial());
	}

	public void PlayCreateAnimation()
	{
		spawnEffects.SetActive(!m_isTemp);
		tempSpawnEffects.SetActive(m_isTemp);
		if (m_isTemp)
		{
			tempSpawnEffects.GetComponent<Animation>().Play(ANIM_TEMP_SPAWN_EFFECTS);
			PlayGemAnimation(ANIM_TEMP_MANA_GEM_BIRTH, State.READY);
		}
		else
		{
			spawnEffects.GetComponent<Animation>().Play(ANIM_SPAWN_EFFECTS);
			PlayGemAnimation(ANIM_MANA_GEM_BIRTH, State.READY);
		}
	}

	public void Destroy()
	{
		m_state = State.DESTROYED;
		StartCoroutine(WaitThenDestroy());
	}

	public bool IsOverloaded()
	{
		return m_overloadPaidSpell != null;
	}

	public bool IsOwedForOverload()
	{
		return m_overloadOwedSpell != null;
	}

	public void MarkAsOwedForOverload()
	{
		MarkAsOwedForOverload(immediatelyLockForOverload: false);
	}

	public void ReclaimOverload()
	{
		if (IsOwedForOverload())
		{
			m_overloadOwedSpell.RemoveStateFinishedCallback(OnOverloadBirthCompletePayOverload);
			m_overloadOwedSpell.AddStateFinishedCallback(OnOverloadUnlockedAnimComplete);
			m_overloadOwedSpell.ActivateState(SpellStateType.DEATH);
			m_overloadOwedSpell = null;
		}
	}

	public void PayOverload()
	{
		if (!IsOwedForOverload())
		{
			state = State.USED;
			MarkAsOwedForOverload(immediatelyLockForOverload: true);
		}
		else
		{
			m_overloadPaidSpell = m_overloadOwedSpell;
			m_overloadOwedSpell = null;
			m_overloadPaidSpell.ActivateState(SpellStateType.ACTION);
		}
	}

	public void UnlockOverload()
	{
		if (IsOverloaded())
		{
			m_overloadPaidSpell.AddStateFinishedCallback(OnOverloadUnlockedAnimComplete);
			m_overloadPaidSpell.ActivateState(SpellStateType.DEATH);
			m_overloadPaidSpell = null;
		}
	}

	private void PlayGemAnimation(string animName, State newVisibleState)
	{
		if (m_isInGame && !m_birthAnimationPlayed)
		{
			if (!animName.Equals(ANIM_MANA_GEM_BIRTH) && !animName.Equals(ANIM_TEMP_MANA_GEM_BIRTH))
			{
				return;
			}
			m_birthAnimationPlayed = true;
		}
		if (!gem.GetComponent<Animation>()[animName])
		{
			Debug.LogWarning($"Mana gem animation named '{animName}' doesn't exist.");
		}
		else if (state != State.DESTROYED && !m_playingAnimation)
		{
			m_playingAnimation = true;
			gem.GetComponent<Animation>().cullingType = AnimationCullingType.BasedOnRenderers;
			gem.GetComponent<Animation>()[animName].normalizedTime = 1f;
			gem.GetComponent<Animation>()[animName].time = 0f;
			gem.GetComponent<Animation>()[animName].speed = 1f;
			gem.GetComponent<Animation>().Play(animName);
			if (!base.gameObject.activeInHierarchy)
			{
				m_playingAnimation = false;
				m_visibleState = newVisibleState;
			}
			else
			{
				StartCoroutine(WaitForAnimation(animName, newVisibleState));
			}
		}
	}

	private IEnumerator WaitForAnimation(string animName, State newVisibleState)
	{
		yield return new WaitForSeconds(gem.GetComponent<Animation>()[animName].length);
		m_visibleState = newVisibleState;
		m_playingAnimation = false;
	}

	private string GetTransitionAnimName(State oldState, State newState)
	{
		string result = "";
		switch (oldState)
		{
		case State.READY:
			switch (newState)
			{
			case State.PROPOSED:
				result = (m_isTemp ? ANIM_TEMP_READY_TO_PROPOSED : ANIM_READY_TO_PROPOSED);
				break;
			case State.USED:
				result = ANIM_READY_TO_USED;
				break;
			}
			break;
		case State.PROPOSED:
			switch (newState)
			{
			case State.READY:
				result = (m_isTemp ? ANIM_TEMP_PROPOSED_TO_READY : ANIM_PROPOSED_TO_READY);
				break;
			case State.USED:
				result = ANIM_PROPOSED_TO_USED;
				break;
			}
			break;
		case State.USED:
			switch (newState)
			{
			case State.READY:
				result = ANIM_USED_TO_READY;
				break;
			case State.PROPOSED:
				result = ANIM_USED_TO_PROPOSED;
				break;
			}
			break;
		case State.DESTROYED:
			Log.Gameplay.Print("Trying to get an anim name for a mana that's been destroyed!!!");
			break;
		}
		return result;
	}

	private IEnumerator WaitThenDestroy()
	{
		while (m_playingAnimation)
		{
			yield return null;
		}
		Spell obj = (m_isTemp ? tempGemDestroy.GetComponent<Spell>() : gemDestroy.GetComponent<Spell>());
		obj.AddStateFinishedCallback(OnGemDestroyedAnimComplete);
		obj.Activate();
	}

	private void OnGemDestroyedAnimComplete(Spell spell, SpellStateType spellStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnOverloadUnlockedAnimComplete(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell.transform.parent.gameObject);
		}
	}

	private void OnOverloadBirthCompletePayOverload(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.IDLE)
		{
			spell.RemoveStateFinishedCallback(OnOverloadBirthCompletePayOverload);
			PayOverload();
		}
	}

	public void MarkAsOwedForOverload(bool immediatelyLockForOverload)
	{
		if (IsOwedForOverload())
		{
			if (immediatelyLockForOverload)
			{
				PayOverload();
			}
			return;
		}
		GameObject gameObject = (GameObject)GameUtils.InstantiateGameObject(ManaCrystalMgr.Get().manaLockPrefab, base.gameObject);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			gameObject.transform.localPosition = new Vector3(0f, 0.1f, 0f);
			float num = 1.1f;
			gameObject.transform.localScale = new Vector3(num, num, num);
		}
		else
		{
			float num2 = 1f / base.transform.localScale.x;
			gameObject.transform.localScale = new Vector3(num2, num2, num2);
		}
		m_overloadOwedSpell = gameObject.transform.Find("Lock_Mana").GetComponent<Spell>();
		m_overloadOwedSpell.RemoveStateFinishedCallback(OnOverloadUnlockedAnimComplete);
		if (immediatelyLockForOverload)
		{
			m_overloadOwedSpell.AddStateFinishedCallback(OnOverloadBirthCompletePayOverload);
		}
		m_overloadOwedSpell.ActivateState(SpellStateType.BIRTH);
	}
}
