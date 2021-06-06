using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000327 RID: 807
[CustomEditClass]
public class ManaCrystalMgr : MonoBehaviour
{
	// Token: 0x06002DA7 RID: 11687 RVA: 0x000E7FB2 File Offset: 0x000E61B2
	private void Awake()
	{
		ManaCrystalMgr.s_instance = this;
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
	}

	// Token: 0x06002DA8 RID: 11688 RVA: 0x000E7FD9 File Offset: 0x000E61D9
	private void OnDestroy()
	{
		ManaCrystalMgr.s_instance = null;
		AssetHandle.SafeDispose<Texture>(ref this.m_friendlyManaGemTexture);
	}

	// Token: 0x06002DA9 RID: 11689 RVA: 0x000E7FEC File Offset: 0x000E61EC
	private void Start()
	{
		this.m_permanentCrystals = new List<ManaCrystal>();
		this.m_temporaryCrystals = new List<ManaCrystal>();
		this.InitializePhoneManaGems();
	}

	// Token: 0x06002DAA RID: 11690 RVA: 0x000E800A File Offset: 0x000E620A
	public static ManaCrystalMgr Get()
	{
		return ManaCrystalMgr.s_instance;
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x000E8014 File Offset: 0x000E6214
	public void Reset()
	{
		base.StopAllCoroutines();
		this.DestroyManaCrystals(this.m_permanentCrystals.Count);
		this.DestroyTempManaCrystals(this.m_temporaryCrystals.Count);
		this.UnlockCrystals(this.m_additionalOverloadedCrystalsOwedThisTurn);
		this.ReclaimCrystalsOwedForOverload(this.m_additionalOverloadedCrystalsOwedNextTurn);
		this.m_manaCrystalType = ManaCrystalType.DEFAULT;
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x000E8068 File Offset: 0x000E6268
	public void ResetUnresolvedManaToBeReadied()
	{
		if (this.m_numQueuedToReady < 0)
		{
			this.m_numQueuedToReady = 0;
		}
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x000E807A File Offset: 0x000E627A
	public void SetManaCrystalType(ManaCrystalType type)
	{
		this.m_manaCrystalType = type;
		this.InitializePhoneManaGems();
	}

	// Token: 0x06002DAE RID: 11694 RVA: 0x000E8089 File Offset: 0x000E6289
	public Vector3 GetManaCrystalSpawnPosition()
	{
		return base.transform.position;
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x000E8098 File Offset: 0x000E6298
	public void AddManaCrystals(int numCrystals, bool isTurnStart)
	{
		for (int i = 0; i < numCrystals; i++)
		{
			GameState.Get().GetGameEntity().NotifyOfManaCrystalSpawned();
			base.StartCoroutine(this.WaitThenAddManaCrystal(false, isTurnStart));
		}
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x000E80D0 File Offset: 0x000E62D0
	public void AddTempManaCrystals(int numCrystals)
	{
		for (int i = 0; i < numCrystals; i++)
		{
			base.StartCoroutine(this.WaitThenAddManaCrystal(true, false));
		}
	}

	// Token: 0x06002DB1 RID: 11697 RVA: 0x000E80F8 File Offset: 0x000E62F8
	public void DestroyManaCrystals(int numCrystals)
	{
		base.StartCoroutine(this.WaitThenDestroyManaCrystals(false, numCrystals));
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x000E8109 File Offset: 0x000E6309
	public void DestroyTempManaCrystals(int numCrystals)
	{
		base.StartCoroutine(this.WaitThenDestroyManaCrystals(true, numCrystals));
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x000E811A File Offset: 0x000E631A
	public void UpdateSpentMana(int shownChangeAmount)
	{
		if (shownChangeAmount > 0)
		{
			this.SpendManaCrystals(shownChangeAmount);
			return;
		}
		if (GameState.Get().IsTurnStartManagerActive())
		{
			TurnStartManager.Get().NotifyOfManaCrystalFilled(-shownChangeAmount);
			return;
		}
		this.ReadyManaCrystals(-shownChangeAmount);
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x000E814C File Offset: 0x000E634C
	public void SpendManaCrystals(int numCrystals)
	{
		ManaCrystalAssetPaths manaCrystalAssetPaths = this.GetManaCrystalAssetPaths(this.m_manaCrystalType);
		SoundManager.Get().LoadAndPlay(manaCrystalAssetPaths.m_SoundOnSpendPath, base.gameObject);
		for (int i = 0; i < numCrystals; i++)
		{
			this.SpendManaCrystal();
		}
	}

	// Token: 0x06002DB5 RID: 11701 RVA: 0x000E8194 File Offset: 0x000E6394
	public void ReadyManaCrystals(int numCrystals)
	{
		for (int i = 0; i < numCrystals; i++)
		{
			this.ReadyManaCrystal();
		}
	}

	// Token: 0x06002DB6 RID: 11702 RVA: 0x000E81B4 File Offset: 0x000E63B4
	public int GetSpendableManaCrystals()
	{
		int num = 0;
		for (int i = 0; i < this.m_temporaryCrystals.Count; i++)
		{
			if (this.m_temporaryCrystals[i].state == ManaCrystal.State.READY)
			{
				num++;
			}
		}
		for (int j = 0; j < this.m_permanentCrystals.Count; j++)
		{
			ManaCrystal manaCrystal = this.m_permanentCrystals[j];
			if (manaCrystal.state == ManaCrystal.State.READY && !manaCrystal.IsOverloaded())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06002DB7 RID: 11703 RVA: 0x000E8228 File Offset: 0x000E6428
	public void CancelAllProposedMana(Entity entity)
	{
		if (entity == null)
		{
			return;
		}
		if (this.m_proposedManaSourceEntID != entity.GetEntityId())
		{
			return;
		}
		this.m_proposedManaSourceEntID = -1;
		this.m_eventSpells.m_proposeUsageSpell.ActivateState(SpellStateType.DEATH);
		for (int i = 0; i < this.m_temporaryCrystals.Count; i++)
		{
			if (this.m_temporaryCrystals[i].state == ManaCrystal.State.PROPOSED)
			{
				this.m_temporaryCrystals[i].state = ManaCrystal.State.READY;
			}
		}
		for (int j = this.m_permanentCrystals.Count - 1; j >= 0; j--)
		{
			if (this.m_permanentCrystals[j].state == ManaCrystal.State.PROPOSED)
			{
				this.m_permanentCrystals[j].state = ManaCrystal.State.READY;
			}
		}
	}

	// Token: 0x06002DB8 RID: 11704 RVA: 0x000E82DC File Offset: 0x000E64DC
	public void ProposeManaCrystalUsage(Entity entity)
	{
		if (entity == null)
		{
			return;
		}
		this.m_proposedManaSourceEntID = entity.GetEntityId();
		int cost = entity.GetCost();
		this.m_eventSpells.m_proposeUsageSpell.ActivateState(SpellStateType.BIRTH);
		int num = 0;
		for (int i = this.m_temporaryCrystals.Count - 1; i >= 0; i--)
		{
			if (this.m_temporaryCrystals[i].state == ManaCrystal.State.USED)
			{
				Log.Gameplay.Print("Found a SPENT temporary mana crystal... this shouldn't happen!", Array.Empty<object>());
			}
			else if (num < cost)
			{
				this.m_temporaryCrystals[i].state = ManaCrystal.State.PROPOSED;
				num++;
			}
			else
			{
				this.m_temporaryCrystals[i].state = ManaCrystal.State.READY;
			}
		}
		for (int j = 0; j < this.m_permanentCrystals.Count; j++)
		{
			if (this.m_permanentCrystals[j].state != ManaCrystal.State.USED && !this.m_permanentCrystals[j].IsOverloaded())
			{
				if (num < cost)
				{
					this.m_permanentCrystals[j].state = ManaCrystal.State.PROPOSED;
					num++;
				}
				else
				{
					this.m_permanentCrystals[j].state = ManaCrystal.State.READY;
				}
			}
		}
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x000E83EE File Offset: 0x000E65EE
	public void HandleSameTurnOverloadChanged(int crystalsChanged)
	{
		if (crystalsChanged > 0)
		{
			this.MarkCrystalsOwedForOverload(crystalsChanged);
			return;
		}
		if (crystalsChanged < 0)
		{
			this.ReclaimCrystalsOwedForOverload(-crystalsChanged);
		}
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x000E8408 File Offset: 0x000E6608
	public void SetCrystalsLockedForOverload(int numCrystals)
	{
		base.StartCoroutine(this.WaitForCrystalsToLoadThenLockThem(numCrystals));
	}

	// Token: 0x06002DBB RID: 11707 RVA: 0x000E8418 File Offset: 0x000E6618
	private IEnumerator WaitForCrystalsToLoadThenLockThem(int numCrystals)
	{
		while (this.m_numCrystalsLoading > 0)
		{
			yield return null;
		}
		for (int i = 0; i < numCrystals; i++)
		{
			if (i < this.m_permanentCrystals.Count)
			{
				this.m_permanentCrystals[i].PayOverload();
			}
		}
		yield break;
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x000E8430 File Offset: 0x000E6630
	public void MarkCrystalsOwedForOverload(int numCrystals)
	{
		if (numCrystals > 0)
		{
			this.m_overloadLocksAreShowing = true;
		}
		int num = 0;
		int num2 = 0;
		while (numCrystals != num)
		{
			if (num2 == this.m_permanentCrystals.Count)
			{
				this.m_additionalOverloadedCrystalsOwedNextTurn += numCrystals - num;
				return;
			}
			ManaCrystal manaCrystal = this.m_permanentCrystals[num2];
			if (!manaCrystal.IsOwedForOverload())
			{
				manaCrystal.MarkAsOwedForOverload();
				num++;
			}
			num2++;
		}
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x000E8494 File Offset: 0x000E6694
	public void ReclaimCrystalsOwedForOverload(int numCrystals)
	{
		int num = 0;
		int num2 = this.m_permanentCrystals.FindLastIndex((ManaCrystal crystal) => crystal.IsOwedForOverload());
		while (num < numCrystals && num2 >= 0)
		{
			this.m_permanentCrystals[num2].ReclaimOverload();
			num2--;
			num++;
		}
		this.m_additionalOverloadedCrystalsOwedNextTurn -= numCrystals - num;
		this.m_overloadLocksAreShowing = (num2 >= 0 || this.m_additionalOverloadedCrystalsOwedNextTurn > 0);
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x000E8518 File Offset: 0x000E6718
	public void UnlockCrystals(int numCrystals)
	{
		int num = 0;
		int num2 = this.m_permanentCrystals.FindLastIndex((ManaCrystal crystal) => crystal.IsOverloaded());
		while (num < numCrystals && num2 >= 0)
		{
			this.m_permanentCrystals[num2].UnlockOverload();
			num2--;
			num++;
		}
		this.m_additionalOverloadedCrystalsOwedThisTurn -= numCrystals - num;
		this.m_overloadLocksAreShowing = (num2 >= 0 || this.m_additionalOverloadedCrystalsOwedThisTurn > 0);
	}

	// Token: 0x06002DBF RID: 11711 RVA: 0x000E859C File Offset: 0x000E679C
	public void TurnCrystalsRed(int previous, int current)
	{
		int num = previous;
		while (num < current && num < this.m_permanentCrystals.Count)
		{
			this.m_permanentCrystals[num].gem.gameObject.GetComponent<Renderer>().GetMaterial().mainTexture = this.redCrystalTexture;
			num++;
		}
	}

	// Token: 0x06002DC0 RID: 11712 RVA: 0x000E85F0 File Offset: 0x000E67F0
	public void OnCurrentPlayerChanged()
	{
		this.m_additionalOverloadedCrystalsOwedThisTurn = this.m_additionalOverloadedCrystalsOwedNextTurn;
		this.m_additionalOverloadedCrystalsOwedNextTurn = 0;
		if (this.m_additionalOverloadedCrystalsOwedThisTurn > 0)
		{
			this.m_overloadLocksAreShowing = true;
		}
		else
		{
			this.m_overloadLocksAreShowing = false;
		}
		for (int i = 0; i < this.m_permanentCrystals.Count; i++)
		{
			ManaCrystal manaCrystal = this.m_permanentCrystals[i];
			if (manaCrystal.IsOverloaded())
			{
				manaCrystal.UnlockOverload();
			}
			if (manaCrystal.IsOwedForOverload())
			{
				this.m_overloadLocksAreShowing = true;
				manaCrystal.PayOverload();
			}
			else if (this.m_additionalOverloadedCrystalsOwedThisTurn > 0)
			{
				manaCrystal.PayOverload();
				this.m_additionalOverloadedCrystalsOwedThisTurn--;
			}
		}
	}

	// Token: 0x06002DC1 RID: 11713 RVA: 0x000E868E File Offset: 0x000E688E
	public bool ShouldShowTooltip(ManaCrystalType type)
	{
		return this.m_manaCrystalType == type;
	}

	// Token: 0x06002DC2 RID: 11714 RVA: 0x000E8699 File Offset: 0x000E6899
	public bool ShouldShowOverloadTooltip()
	{
		return this.m_overloadLocksAreShowing;
	}

	// Token: 0x06002DC3 RID: 11715 RVA: 0x000E86A1 File Offset: 0x000E68A1
	public void SetFriendlyManaGemTexture(AssetHandle<Texture> texture)
	{
		AssetHandle.Set<Texture>(ref this.m_friendlyManaGemTexture, texture);
		this.ApplyFriendlyManaGemTexture();
	}

	// Token: 0x06002DC4 RID: 11716 RVA: 0x000E86B5 File Offset: 0x000E68B5
	public void SetFriendlyManaGemTint(Color tint)
	{
		if (this.m_friendlyManaGem == null)
		{
			return;
		}
		this.m_friendlyManaGem.GetComponentInChildren<MeshRenderer>().GetMaterial().SetColor("_TintColor", tint);
	}

	// Token: 0x06002DC5 RID: 11717 RVA: 0x000E86E4 File Offset: 0x000E68E4
	public void ShowPhoneManaTray()
	{
		this.m_friendlyManaGem.GetComponent<Animation>()[this.GEM_FLIP_ANIM_NAME].speed = 1f;
		this.m_friendlyManaGem.GetComponent<Animation>().Play(this.GEM_FLIP_ANIM_NAME);
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			this.m_friendlyManaText.TextAlpha,
			"to",
			0f,
			"time",
			0.1f,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.m_friendlyManaText.TextAlpha = (float)newVal;
			})
		}));
		this.manaTrayPhone.ToggleTraySlider(true, null, true);
	}

	// Token: 0x06002DC6 RID: 11718 RVA: 0x000E87A8 File Offset: 0x000E69A8
	public void HidePhoneManaTray()
	{
		this.m_friendlyManaGem.GetComponent<Animation>()[this.GEM_FLIP_ANIM_NAME].speed = -1f;
		if (this.m_friendlyManaGem.GetComponent<Animation>()[this.GEM_FLIP_ANIM_NAME].time == 0f)
		{
			this.m_friendlyManaGem.GetComponent<Animation>()[this.GEM_FLIP_ANIM_NAME].time = this.m_friendlyManaGem.GetComponent<Animation>()[this.GEM_FLIP_ANIM_NAME].length;
		}
		this.m_friendlyManaGem.GetComponent<Animation>().Play(this.GEM_FLIP_ANIM_NAME);
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			this.m_friendlyManaText.TextAlpha,
			"to",
			1f,
			"time",
			0.1f,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.m_friendlyManaText.TextAlpha = (float)newVal;
			})
		}));
		this.manaTrayPhone.ToggleTraySlider(false, null, true);
	}

	// Token: 0x06002DC7 RID: 11719 RVA: 0x000E88C4 File Offset: 0x000E6AC4
	public Material GetTemporaryManaCrystalMaterial()
	{
		return this.m_ManaCrystalAssetTable[(int)this.m_manaCrystalType].m_tempManaCrystalMaterial;
	}

	// Token: 0x06002DC8 RID: 11720 RVA: 0x000E88DC File Offset: 0x000E6ADC
	public Material GetTemporaryManaCrystalProposedQuadMaterial()
	{
		return this.m_ManaCrystalAssetTable[(int)this.m_manaCrystalType].m_tempManaCrystalProposedQuadMaterial;
	}

	// Token: 0x06002DC9 RID: 11721 RVA: 0x000E88F4 File Offset: 0x000E6AF4
	public void SetEnemyManaCounterActive(bool active)
	{
		this.opposingManaCounter.GetComponent<ManaCounter>().enabled = active;
		this.opposingManaCounter.SetActive(active);
	}

	// Token: 0x06002DCA RID: 11722 RVA: 0x000E8914 File Offset: 0x000E6B14
	private void UpdateLayout()
	{
		Vector3 position = base.transform.position;
		if (UniversalInputManager.UsePhoneUI)
		{
			position = this.manaGemBone.transform.position;
		}
		for (int i = this.m_permanentCrystals.Count - 1; i >= 0; i--)
		{
			this.m_permanentCrystals[i].transform.position = position;
			if (UniversalInputManager.UsePhoneUI)
			{
				position.z += this.m_manaCrystalWidth;
			}
			else
			{
				position.x += this.m_manaCrystalWidth;
			}
		}
		for (int j = 0; j < this.m_temporaryCrystals.Count; j++)
		{
			this.m_temporaryCrystals[j].transform.position = position;
			if (UniversalInputManager.UsePhoneUI)
			{
				position.z += this.m_manaCrystalWidth;
			}
			else
			{
				position.x += this.m_manaCrystalWidth;
			}
		}
	}

	// Token: 0x06002DCB RID: 11723 RVA: 0x000E8A06 File Offset: 0x000E6C06
	private IEnumerator UpdatePermanentCrystalStates()
	{
		while (this.m_numQueuedToReady > 0 || this.m_numCrystalsLoading > 0 || this.m_numQueuedToSpend > 0)
		{
			yield return null;
		}
		int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.RESOURCES_USED);
		int tag2 = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
		int num = 0;
		while (num < tag && num != this.m_permanentCrystals.Count)
		{
			if (this.m_permanentCrystals[num].state != ManaCrystal.State.USED)
			{
				this.m_permanentCrystals[num].state = ManaCrystal.State.USED;
			}
			num++;
		}
		for (int i = num; i < this.m_permanentCrystals.Count; i++)
		{
			if (this.m_permanentCrystals[i].state != ManaCrystal.State.READY)
			{
				this.m_permanentCrystals[i].state = ManaCrystal.State.READY;
			}
		}
		for (int j = 0; j < Math.Min(this.m_permanentCrystals.Count, tag2); j++)
		{
			if (!this.m_permanentCrystals[j].IsOwedForOverload())
			{
				this.m_permanentCrystals[j].MarkAsOwedForOverload();
			}
		}
		yield break;
	}

	// Token: 0x06002DCC RID: 11724 RVA: 0x000E8A18 File Offset: 0x000E6C18
	private void LoadCrystalCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_numCrystalsLoading--;
		if (this.m_manaCrystalWidth <= 0f)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_manaCrystalWidth = 0.33f;
			}
			else
			{
				this.m_manaCrystalWidth = go.transform.Find("Gem_Mana").GetComponent<Renderer>().bounds.size.x;
			}
		}
		ManaCrystalMgr.LoadCrystalCallbackData loadCrystalCallbackData = callbackData as ManaCrystalMgr.LoadCrystalCallbackData;
		ManaCrystal component = go.GetComponent<ManaCrystal>();
		if (loadCrystalCallbackData.IsTempCrystal)
		{
			component.MarkAsTemp();
			this.m_temporaryCrystals.Add(component);
		}
		else
		{
			this.m_permanentCrystals.Add(component);
			if (loadCrystalCallbackData.IsTurnStart)
			{
				if (this.m_additionalOverloadedCrystalsOwedThisTurn > 0)
				{
					component.PayOverload();
					this.m_additionalOverloadedCrystalsOwedThisTurn--;
				}
			}
			else if (this.m_additionalOverloadedCrystalsOwedNextTurn > 0)
			{
				component.state = ManaCrystal.State.USED;
				component.MarkAsOwedForOverload();
				this.m_additionalOverloadedCrystalsOwedNextTurn--;
			}
			base.StartCoroutine(this.UpdatePermanentCrystalStates());
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			component.transform.parent = this.manaGemBone.transform.parent;
			component.transform.localRotation = this.manaGemBone.transform.localRotation;
			component.transform.localScale = this.manaGemBone.transform.localScale;
		}
		else
		{
			component.transform.parent = base.transform;
		}
		component.transform.localPosition = Vector3.zero;
		component.PlayCreateAnimation();
		ManaCrystalAssetPaths manaCrystalAssetPaths = this.GetManaCrystalAssetPaths(this.m_manaCrystalType);
		SoundManager.Get().LoadAndPlay(manaCrystalAssetPaths.m_SoundOnAddPath, base.gameObject);
		this.UpdateLayout();
	}

	// Token: 0x06002DCD RID: 11725 RVA: 0x000E8BC8 File Offset: 0x000E6DC8
	public float GetWidth()
	{
		if (this.m_permanentCrystals.Count == 0)
		{
			return 0f;
		}
		return this.m_permanentCrystals[0].transform.Find("Gem_Mana").GetComponent<Renderer>().bounds.size.x * (float)this.m_permanentCrystals.Count * (float)this.m_temporaryCrystals.Count;
	}

	// Token: 0x06002DCE RID: 11726 RVA: 0x000E8C34 File Offset: 0x000E6E34
	private ManaCrystalAssetPaths GetManaCrystalAssetPaths(ManaCrystalType type)
	{
		foreach (ManaCrystalAssetPaths manaCrystalAssetPaths in this.m_ManaCrystalAssetTable)
		{
			if (manaCrystalAssetPaths.m_Type == type)
			{
				return manaCrystalAssetPaths;
			}
		}
		return this.m_ManaCrystalAssetTable[0];
	}

	// Token: 0x06002DCF RID: 11727 RVA: 0x000E8C9C File Offset: 0x000E6E9C
	private IEnumerator WaitThenAddManaCrystal(bool isTemp, bool isTurnStart)
	{
		this.m_numCrystalsLoading++;
		this.m_numQueuedToSpawn++;
		yield return new WaitForSeconds((float)this.m_numQueuedToSpawn * 0.2f);
		ManaCrystalAssetPaths manaCrystalAssetPaths = this.GetManaCrystalAssetPaths(this.m_manaCrystalType);
		ManaCrystalMgr.LoadCrystalCallbackData callbackData = new ManaCrystalMgr.LoadCrystalCallbackData(isTemp, isTurnStart);
		AssetLoader.Get().InstantiatePrefab(manaCrystalAssetPaths.m_ResourcePath, new PrefabCallback<GameObject>(this.LoadCrystalCallback), callbackData, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_numQueuedToSpawn--;
		yield break;
	}

	// Token: 0x06002DD0 RID: 11728 RVA: 0x000E8CB9 File Offset: 0x000E6EB9
	private IEnumerator WaitThenDestroyManaCrystals(bool isTemp, int numCrystals)
	{
		while (this.m_numCrystalsLoading > 0)
		{
			yield return null;
		}
		for (int i = 0; i < numCrystals; i++)
		{
			if (isTemp)
			{
				this.DestroyTempManaCrystal();
			}
			else
			{
				this.DestroyManaCrystal();
			}
		}
		yield break;
	}

	// Token: 0x06002DD1 RID: 11729 RVA: 0x000E8CD6 File Offset: 0x000E6ED6
	private IEnumerator WaitThenReadyManaCrystal()
	{
		this.m_numQueuedToReady++;
		yield return new WaitForSeconds((float)this.m_numQueuedToReady * 0.2f);
		if (this.m_numQueuedToReady <= 0)
		{
			yield break;
		}
		if (this.m_permanentCrystals.Count > 0)
		{
			for (int i = this.m_permanentCrystals.Count - 1; i >= 0; i--)
			{
				if (this.m_permanentCrystals[i].state == ManaCrystal.State.USED)
				{
					ManaCrystalAssetPaths manaCrystalAssetPaths = this.GetManaCrystalAssetPaths(this.m_manaCrystalType);
					SoundManager.Get().LoadAndPlay(manaCrystalAssetPaths.m_SoundOnRefreshPath, base.gameObject);
					this.m_permanentCrystals[i].state = ManaCrystal.State.READY;
					break;
				}
			}
		}
		this.m_numQueuedToReady--;
		yield break;
	}

	// Token: 0x06002DD2 RID: 11730 RVA: 0x000E8CE5 File Offset: 0x000E6EE5
	private IEnumerator WaitThenSpendManaCrystal()
	{
		this.m_numQueuedToSpend++;
		yield return new WaitForSeconds((float)(this.m_numQueuedToSpend - 1) * 0.2f);
		if (this.m_numQueuedToSpend <= 0)
		{
			yield break;
		}
		bool flag = false;
		for (int i = 0; i < this.m_permanentCrystals.Count; i++)
		{
			if (this.m_permanentCrystals[i].state != ManaCrystal.State.USED)
			{
				this.m_permanentCrystals[i].state = ManaCrystal.State.USED;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			this.m_numQueuedToReady--;
		}
		this.m_numQueuedToSpend--;
		if (this.m_numQueuedToSpend > 0)
		{
			yield break;
		}
		InputManager.Get().OnManaCrystalMgrManaSpent();
		yield break;
	}

	// Token: 0x06002DD3 RID: 11731 RVA: 0x000E8CF4 File Offset: 0x000E6EF4
	private void DestroyManaCrystal()
	{
		if (this.m_permanentCrystals.Count <= 0)
		{
			return;
		}
		int index = 0;
		Component component = this.m_permanentCrystals[index];
		this.m_permanentCrystals.RemoveAt(index);
		component.GetComponent<ManaCrystal>().Destroy();
		this.UpdateLayout();
		base.StartCoroutine(this.UpdatePermanentCrystalStates());
	}

	// Token: 0x06002DD4 RID: 11732 RVA: 0x000E8D48 File Offset: 0x000E6F48
	private void DestroyTempManaCrystal()
	{
		if (this.m_temporaryCrystals.Count <= 0)
		{
			return;
		}
		int index = this.m_temporaryCrystals.Count - 1;
		Component component = this.m_temporaryCrystals[index];
		this.m_temporaryCrystals.RemoveAt(index);
		component.GetComponent<ManaCrystal>().Destroy();
		this.UpdateLayout();
	}

	// Token: 0x06002DD5 RID: 11733 RVA: 0x000E8D9A File Offset: 0x000E6F9A
	private void SpendManaCrystal()
	{
		base.StartCoroutine(this.WaitThenSpendManaCrystal());
	}

	// Token: 0x06002DD6 RID: 11734 RVA: 0x000E8DA9 File Offset: 0x000E6FA9
	private void ReadyManaCrystal()
	{
		base.StartCoroutine(this.WaitThenReadyManaCrystal());
	}

	// Token: 0x06002DD7 RID: 11735 RVA: 0x000E8DB8 File Offset: 0x000E6FB8
	private void InitializePhoneManaGems()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.m_friendlyManaText = this.friendlyManaCounter.GetComponent<UberText>();
		ManaCounter component = this.friendlyManaCounter.GetComponent<ManaCounter>();
		string phoneLargeResource = this.m_ManaCrystalAssetTable[(int)this.m_manaCrystalType].m_phoneLargeResource;
		component.InitializeLargeResourceGameObject(phoneLargeResource);
		if (this.opposingManaCounter.activeInHierarchy)
		{
			this.opposingManaCounter.GetComponent<ManaCounter>().InitializeLargeResourceGameObject(phoneLargeResource);
		}
		this.m_friendlyManaGem = component.GetPhoneGem();
		this.ApplyFriendlyManaGemTexture();
	}

	// Token: 0x06002DD8 RID: 11736 RVA: 0x000E8E3D File Offset: 0x000E703D
	private void ApplyFriendlyManaGemTexture()
	{
		if (this.m_friendlyManaGem == null || this.m_friendlyManaGemTexture == null)
		{
			return;
		}
		this.m_friendlyManaGem.GetComponentInChildren<MeshRenderer>().GetMaterial().mainTexture = this.m_friendlyManaGemTexture;
	}

	// Token: 0x0400192C RID: 6444
	public Texture redCrystalTexture;

	// Token: 0x0400192D RID: 6445
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride manaLockPrefab;

	// Token: 0x0400192E RID: 6446
	public ManaCrystalEventSpells m_eventSpells;

	// Token: 0x0400192F RID: 6447
	public SlidingTray manaTrayPhone;

	// Token: 0x04001930 RID: 6448
	public Transform manaGemBone;

	// Token: 0x04001931 RID: 6449
	public GameObject friendlyManaCounter;

	// Token: 0x04001932 RID: 6450
	public GameObject opposingManaCounter;

	// Token: 0x04001933 RID: 6451
	public List<ManaCrystalAssetPaths> m_ManaCrystalAssetTable = new List<ManaCrystalAssetPaths>();

	// Token: 0x04001934 RID: 6452
	private const float SECS_BETW_MANA_SPAWNS = 0.2f;

	// Token: 0x04001935 RID: 6453
	private const float SECS_BETW_MANA_READIES = 0.2f;

	// Token: 0x04001936 RID: 6454
	private const float SECS_BETW_MANA_SPENDS = 0.2f;

	// Token: 0x04001937 RID: 6455
	private const float GEM_FLIP_TEXT_FADE_TIME = 0.1f;

	// Token: 0x04001938 RID: 6456
	private readonly string GEM_FLIP_ANIM_NAME = "Resource_Large_phone_Flip";

	// Token: 0x04001939 RID: 6457
	private static ManaCrystalMgr s_instance;

	// Token: 0x0400193A RID: 6458
	private ManaCrystalType m_manaCrystalType;

	// Token: 0x0400193B RID: 6459
	private List<ManaCrystal> m_permanentCrystals;

	// Token: 0x0400193C RID: 6460
	private List<ManaCrystal> m_temporaryCrystals;

	// Token: 0x0400193D RID: 6461
	private int m_proposedManaSourceEntID = -1;

	// Token: 0x0400193E RID: 6462
	private int m_numCrystalsLoading;

	// Token: 0x0400193F RID: 6463
	private int m_numQueuedToSpawn;

	// Token: 0x04001940 RID: 6464
	private int m_numQueuedToReady;

	// Token: 0x04001941 RID: 6465
	private int m_numQueuedToSpend;

	// Token: 0x04001942 RID: 6466
	private int m_additionalOverloadedCrystalsOwedNextTurn;

	// Token: 0x04001943 RID: 6467
	private int m_additionalOverloadedCrystalsOwedThisTurn;

	// Token: 0x04001944 RID: 6468
	private bool m_overloadLocksAreShowing;

	// Token: 0x04001945 RID: 6469
	private float m_manaCrystalWidth;

	// Token: 0x04001946 RID: 6470
	private GameObject m_friendlyManaGem;

	// Token: 0x04001947 RID: 6471
	private UberText m_friendlyManaText;

	// Token: 0x04001948 RID: 6472
	private AssetHandle<Texture> m_friendlyManaGemTexture;

	// Token: 0x020016AA RID: 5802
	private class LoadCrystalCallbackData
	{
		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x0600E4FA RID: 58618 RVA: 0x004075D4 File Offset: 0x004057D4
		public bool IsTempCrystal
		{
			get
			{
				return this.m_isTempCrystal;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x0600E4FB RID: 58619 RVA: 0x004075DC File Offset: 0x004057DC
		public bool IsTurnStart
		{
			get
			{
				return this.m_isTurnStart;
			}
		}

		// Token: 0x0600E4FC RID: 58620 RVA: 0x004075E4 File Offset: 0x004057E4
		public LoadCrystalCallbackData(bool isTempCrystal, bool isTurnStart)
		{
			this.m_isTempCrystal = isTempCrystal;
			this.m_isTurnStart = isTurnStart;
		}

		// Token: 0x0400B14C RID: 45388
		private bool m_isTempCrystal;

		// Token: 0x0400B14D RID: 45389
		private bool m_isTurnStart;
	}
}
