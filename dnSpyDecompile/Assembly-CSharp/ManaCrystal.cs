using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000323 RID: 803
public class ManaCrystal : MonoBehaviour
{
	// Token: 0x06002D8E RID: 11662 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06002D8F RID: 11663 RVA: 0x000E7948 File Offset: 0x000E5B48
	private void Update()
	{
		ManaCrystal.State state = this.state;
		if (state == this.m_visibleState)
		{
			return;
		}
		if (state == ManaCrystal.State.DESTROYED)
		{
			return;
		}
		string transitionAnimName = this.GetTransitionAnimName(this.m_visibleState, state);
		this.PlayGemAnimation(transitionAnimName, state);
	}

	// Token: 0x17000506 RID: 1286
	// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000E7981 File Offset: 0x000E5B81
	// (set) Token: 0x06002D91 RID: 11665 RVA: 0x000E7989 File Offset: 0x000E5B89
	public ManaCrystal.State state
	{
		get
		{
			return this.m_state;
		}
		set
		{
			if (this.m_state == ManaCrystal.State.DESTROYED)
			{
				return;
			}
			if (value == ManaCrystal.State.DESTROYED)
			{
				this.Destroy();
				return;
			}
			this.m_state = value;
		}
	}

	// Token: 0x06002D92 RID: 11666 RVA: 0x000E79A7 File Offset: 0x000E5BA7
	public void MarkAsNotInGame()
	{
		this.m_isInGame = false;
	}

	// Token: 0x06002D93 RID: 11667 RVA: 0x000E79B0 File Offset: 0x000E5BB0
	public void MarkAsTemp()
	{
		this.m_isTemp = true;
		ManaCrystalMgr manaCrystalMgr = ManaCrystalMgr.Get();
		this.gem.GetComponentInChildren<MeshRenderer>().SetMaterial(manaCrystalMgr.GetTemporaryManaCrystalMaterial());
		this.gem.transform.Find("Proposed_Quad").gameObject.GetComponent<MeshRenderer>().SetMaterial(manaCrystalMgr.GetTemporaryManaCrystalProposedQuadMaterial());
	}

	// Token: 0x06002D94 RID: 11668 RVA: 0x000E7A0C File Offset: 0x000E5C0C
	public void PlayCreateAnimation()
	{
		this.spawnEffects.SetActive(!this.m_isTemp);
		this.tempSpawnEffects.SetActive(this.m_isTemp);
		if (this.m_isTemp)
		{
			this.tempSpawnEffects.GetComponent<Animation>().Play(this.ANIM_TEMP_SPAWN_EFFECTS);
			this.PlayGemAnimation(this.ANIM_TEMP_MANA_GEM_BIRTH, ManaCrystal.State.READY);
			return;
		}
		this.spawnEffects.GetComponent<Animation>().Play(this.ANIM_SPAWN_EFFECTS);
		this.PlayGemAnimation(this.ANIM_MANA_GEM_BIRTH, ManaCrystal.State.READY);
	}

	// Token: 0x06002D95 RID: 11669 RVA: 0x000E7A8F File Offset: 0x000E5C8F
	public void Destroy()
	{
		this.m_state = ManaCrystal.State.DESTROYED;
		base.StartCoroutine(this.WaitThenDestroy());
	}

	// Token: 0x06002D96 RID: 11670 RVA: 0x000E7AA5 File Offset: 0x000E5CA5
	public bool IsOverloaded()
	{
		return this.m_overloadPaidSpell != null;
	}

	// Token: 0x06002D97 RID: 11671 RVA: 0x000E7AB3 File Offset: 0x000E5CB3
	public bool IsOwedForOverload()
	{
		return this.m_overloadOwedSpell != null;
	}

	// Token: 0x06002D98 RID: 11672 RVA: 0x000E7AC1 File Offset: 0x000E5CC1
	public void MarkAsOwedForOverload()
	{
		this.MarkAsOwedForOverload(false);
	}

	// Token: 0x06002D99 RID: 11673 RVA: 0x000E7ACC File Offset: 0x000E5CCC
	public void ReclaimOverload()
	{
		if (!this.IsOwedForOverload())
		{
			return;
		}
		this.m_overloadOwedSpell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnOverloadBirthCompletePayOverload));
		this.m_overloadOwedSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnOverloadUnlockedAnimComplete));
		this.m_overloadOwedSpell.ActivateState(SpellStateType.DEATH);
		this.m_overloadOwedSpell = null;
	}

	// Token: 0x06002D9A RID: 11674 RVA: 0x000E7B24 File Offset: 0x000E5D24
	public void PayOverload()
	{
		if (!this.IsOwedForOverload())
		{
			this.state = ManaCrystal.State.USED;
			this.MarkAsOwedForOverload(true);
			return;
		}
		this.m_overloadPaidSpell = this.m_overloadOwedSpell;
		this.m_overloadOwedSpell = null;
		this.m_overloadPaidSpell.ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x06002D9B RID: 11675 RVA: 0x000E7B5C File Offset: 0x000E5D5C
	public void UnlockOverload()
	{
		if (!this.IsOverloaded())
		{
			return;
		}
		this.m_overloadPaidSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnOverloadUnlockedAnimComplete));
		this.m_overloadPaidSpell.ActivateState(SpellStateType.DEATH);
		this.m_overloadPaidSpell = null;
	}

	// Token: 0x06002D9C RID: 11676 RVA: 0x000E7B94 File Offset: 0x000E5D94
	private void PlayGemAnimation(string animName, ManaCrystal.State newVisibleState)
	{
		if (this.m_isInGame && !this.m_birthAnimationPlayed)
		{
			if (!animName.Equals(this.ANIM_MANA_GEM_BIRTH) && !animName.Equals(this.ANIM_TEMP_MANA_GEM_BIRTH))
			{
				return;
			}
			this.m_birthAnimationPlayed = true;
		}
		if (!this.gem.GetComponent<Animation>()[animName])
		{
			Debug.LogWarning(string.Format("Mana gem animation named '{0}' doesn't exist.", animName));
			return;
		}
		if (this.state == ManaCrystal.State.DESTROYED)
		{
			return;
		}
		if (this.m_playingAnimation)
		{
			return;
		}
		this.m_playingAnimation = true;
		this.gem.GetComponent<Animation>().cullingType = AnimationCullingType.BasedOnRenderers;
		this.gem.GetComponent<Animation>()[animName].normalizedTime = 1f;
		this.gem.GetComponent<Animation>()[animName].time = 0f;
		this.gem.GetComponent<Animation>()[animName].speed = 1f;
		this.gem.GetComponent<Animation>().Play(animName);
		if (!base.gameObject.activeInHierarchy)
		{
			this.m_playingAnimation = false;
			this.m_visibleState = newVisibleState;
			return;
		}
		base.StartCoroutine(this.WaitForAnimation(animName, newVisibleState));
	}

	// Token: 0x06002D9D RID: 11677 RVA: 0x000E7CBA File Offset: 0x000E5EBA
	private IEnumerator WaitForAnimation(string animName, ManaCrystal.State newVisibleState)
	{
		yield return new WaitForSeconds(this.gem.GetComponent<Animation>()[animName].length);
		this.m_visibleState = newVisibleState;
		this.m_playingAnimation = false;
		yield break;
	}

	// Token: 0x06002D9E RID: 11678 RVA: 0x000E7CD8 File Offset: 0x000E5ED8
	private string GetTransitionAnimName(ManaCrystal.State oldState, ManaCrystal.State newState)
	{
		string result = "";
		switch (oldState)
		{
		case ManaCrystal.State.READY:
			if (newState == ManaCrystal.State.PROPOSED)
			{
				result = (this.m_isTemp ? this.ANIM_TEMP_READY_TO_PROPOSED : this.ANIM_READY_TO_PROPOSED);
			}
			else if (newState == ManaCrystal.State.USED)
			{
				result = this.ANIM_READY_TO_USED;
			}
			break;
		case ManaCrystal.State.USED:
			if (newState == ManaCrystal.State.READY)
			{
				result = this.ANIM_USED_TO_READY;
			}
			else if (newState == ManaCrystal.State.PROPOSED)
			{
				result = this.ANIM_USED_TO_PROPOSED;
			}
			break;
		case ManaCrystal.State.PROPOSED:
			if (newState == ManaCrystal.State.READY)
			{
				result = (this.m_isTemp ? this.ANIM_TEMP_PROPOSED_TO_READY : this.ANIM_PROPOSED_TO_READY);
			}
			else if (newState == ManaCrystal.State.USED)
			{
				result = this.ANIM_PROPOSED_TO_USED;
			}
			break;
		case ManaCrystal.State.DESTROYED:
			Log.Gameplay.Print("Trying to get an anim name for a mana that's been destroyed!!!", Array.Empty<object>());
			break;
		}
		return result;
	}

	// Token: 0x06002D9F RID: 11679 RVA: 0x000E7D87 File Offset: 0x000E5F87
	private IEnumerator WaitThenDestroy()
	{
		while (this.m_playingAnimation)
		{
			yield return null;
		}
		Spell spell = this.m_isTemp ? this.tempGemDestroy.GetComponent<Spell>() : this.gemDestroy.GetComponent<Spell>();
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnGemDestroyedAnimComplete));
		spell.Activate();
		yield break;
	}

	// Token: 0x06002DA0 RID: 11680 RVA: 0x000E7D96 File Offset: 0x000E5F96
	private void OnGemDestroyedAnimComplete(Spell spell, SpellStateType spellStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06002DA1 RID: 11681 RVA: 0x000E7DAC File Offset: 0x000E5FAC
	private void OnOverloadUnlockedAnimComplete(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.transform.parent.gameObject);
	}

	// Token: 0x06002DA2 RID: 11682 RVA: 0x000E7DCC File Offset: 0x000E5FCC
	private void OnOverloadBirthCompletePayOverload(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.IDLE)
		{
			return;
		}
		spell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnOverloadBirthCompletePayOverload));
		this.PayOverload();
	}

	// Token: 0x06002DA3 RID: 11683 RVA: 0x000E7DF4 File Offset: 0x000E5FF4
	public void MarkAsOwedForOverload(bool immediatelyLockForOverload)
	{
		if (this.IsOwedForOverload())
		{
			if (immediatelyLockForOverload)
			{
				this.PayOverload();
			}
			return;
		}
		GameObject gameObject = (GameObject)GameUtils.InstantiateGameObject(ManaCrystalMgr.Get().manaLockPrefab, base.gameObject, false);
		if (UniversalInputManager.UsePhoneUI)
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
		this.m_overloadOwedSpell = gameObject.transform.Find("Lock_Mana").GetComponent<Spell>();
		this.m_overloadOwedSpell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnOverloadUnlockedAnimComplete));
		if (immediatelyLockForOverload)
		{
			this.m_overloadOwedSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnOverloadBirthCompletePayOverload));
		}
		this.m_overloadOwedSpell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x04001907 RID: 6407
	public GameObject gem;

	// Token: 0x04001908 RID: 6408
	public GameObject spawnEffects;

	// Token: 0x04001909 RID: 6409
	public GameObject gemDestroy;

	// Token: 0x0400190A RID: 6410
	public GameObject tempSpawnEffects;

	// Token: 0x0400190B RID: 6411
	public GameObject tempGemDestroy;

	// Token: 0x0400190C RID: 6412
	private readonly string ANIM_SPAWN_EFFECTS = "mana_spawn_edit";

	// Token: 0x0400190D RID: 6413
	private readonly string ANIM_TEMP_SPAWN_EFFECTS = "mana_spawn_edit_temp";

	// Token: 0x0400190E RID: 6414
	private readonly string ANIM_MANA_GEM_BIRTH = "ManaGemBirth";

	// Token: 0x0400190F RID: 6415
	private readonly string ANIM_TEMP_MANA_GEM_BIRTH = "ManaGemBirth_Temp";

	// Token: 0x04001910 RID: 6416
	private readonly string ANIM_READY_TO_USED = "ManaGemUsed";

	// Token: 0x04001911 RID: 6417
	private readonly string ANIM_USED_TO_READY = "ManaGem_Restore";

	// Token: 0x04001912 RID: 6418
	private readonly string ANIM_READY_TO_PROPOSED = "ManaGemProposed";

	// Token: 0x04001913 RID: 6419
	private readonly string ANIM_TEMP_READY_TO_PROPOSED = "ManaGemProposed_Temp";

	// Token: 0x04001914 RID: 6420
	private readonly string ANIM_PROPOSED_TO_READY = "ManaGemProposed_Cancel";

	// Token: 0x04001915 RID: 6421
	private readonly string ANIM_TEMP_PROPOSED_TO_READY = "ManaGemProposed_Cancel_Temp";

	// Token: 0x04001916 RID: 6422
	private readonly string ANIM_USED_TO_PROPOSED = "ManaGemUsed_Proposed";

	// Token: 0x04001917 RID: 6423
	private readonly string ANIM_PROPOSED_TO_USED = "ManaGemProposed_Used";

	// Token: 0x04001918 RID: 6424
	private bool m_isInGame = true;

	// Token: 0x04001919 RID: 6425
	private bool m_birthAnimationPlayed;

	// Token: 0x0400191A RID: 6426
	private bool m_playingAnimation;

	// Token: 0x0400191B RID: 6427
	private bool m_isTemp;

	// Token: 0x0400191C RID: 6428
	private Spell m_overloadOwedSpell;

	// Token: 0x0400191D RID: 6429
	private Spell m_overloadPaidSpell;

	// Token: 0x0400191E RID: 6430
	private ManaCrystal.State m_state;

	// Token: 0x0400191F RID: 6431
	private ManaCrystal.State m_visibleState;

	// Token: 0x020016A7 RID: 5799
	public enum State
	{
		// Token: 0x0400B140 RID: 45376
		READY,
		// Token: 0x0400B141 RID: 45377
		USED,
		// Token: 0x0400B142 RID: 45378
		PROPOSED,
		// Token: 0x0400B143 RID: 45379
		DESTROYED
	}
}
