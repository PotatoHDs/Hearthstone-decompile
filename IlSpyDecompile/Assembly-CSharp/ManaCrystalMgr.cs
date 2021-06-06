using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class ManaCrystalMgr : MonoBehaviour
{
	private class LoadCrystalCallbackData
	{
		private bool m_isTempCrystal;

		private bool m_isTurnStart;

		public bool IsTempCrystal => m_isTempCrystal;

		public bool IsTurnStart => m_isTurnStart;

		public LoadCrystalCallbackData(bool isTempCrystal, bool isTurnStart)
		{
			m_isTempCrystal = isTempCrystal;
			m_isTurnStart = isTurnStart;
		}
	}

	public Texture redCrystalTexture;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride manaLockPrefab;

	public ManaCrystalEventSpells m_eventSpells;

	public SlidingTray manaTrayPhone;

	public Transform manaGemBone;

	public GameObject friendlyManaCounter;

	public GameObject opposingManaCounter;

	public List<ManaCrystalAssetPaths> m_ManaCrystalAssetTable = new List<ManaCrystalAssetPaths>();

	private const float SECS_BETW_MANA_SPAWNS = 0.2f;

	private const float SECS_BETW_MANA_READIES = 0.2f;

	private const float SECS_BETW_MANA_SPENDS = 0.2f;

	private const float GEM_FLIP_TEXT_FADE_TIME = 0.1f;

	private readonly string GEM_FLIP_ANIM_NAME = "Resource_Large_phone_Flip";

	private static ManaCrystalMgr s_instance;

	private ManaCrystalType m_manaCrystalType;

	private List<ManaCrystal> m_permanentCrystals;

	private List<ManaCrystal> m_temporaryCrystals;

	private int m_proposedManaSourceEntID = -1;

	private int m_numCrystalsLoading;

	private int m_numQueuedToSpawn;

	private int m_numQueuedToReady;

	private int m_numQueuedToSpend;

	private int m_additionalOverloadedCrystalsOwedNextTurn;

	private int m_additionalOverloadedCrystalsOwedThisTurn;

	private bool m_overloadLocksAreShowing;

	private float m_manaCrystalWidth;

	private GameObject m_friendlyManaGem;

	private UberText m_friendlyManaText;

	private AssetHandle<Texture> m_friendlyManaGemTexture;

	private void Awake()
	{
		s_instance = this;
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
		AssetHandle.SafeDispose(ref m_friendlyManaGemTexture);
	}

	private void Start()
	{
		m_permanentCrystals = new List<ManaCrystal>();
		m_temporaryCrystals = new List<ManaCrystal>();
		InitializePhoneManaGems();
	}

	public static ManaCrystalMgr Get()
	{
		return s_instance;
	}

	public void Reset()
	{
		StopAllCoroutines();
		DestroyManaCrystals(m_permanentCrystals.Count);
		DestroyTempManaCrystals(m_temporaryCrystals.Count);
		UnlockCrystals(m_additionalOverloadedCrystalsOwedThisTurn);
		ReclaimCrystalsOwedForOverload(m_additionalOverloadedCrystalsOwedNextTurn);
		m_manaCrystalType = ManaCrystalType.DEFAULT;
	}

	public void ResetUnresolvedManaToBeReadied()
	{
		if (m_numQueuedToReady < 0)
		{
			m_numQueuedToReady = 0;
		}
	}

	public void SetManaCrystalType(ManaCrystalType type)
	{
		m_manaCrystalType = type;
		InitializePhoneManaGems();
	}

	public Vector3 GetManaCrystalSpawnPosition()
	{
		return base.transform.position;
	}

	public void AddManaCrystals(int numCrystals, bool isTurnStart)
	{
		for (int i = 0; i < numCrystals; i++)
		{
			GameState.Get().GetGameEntity().NotifyOfManaCrystalSpawned();
			StartCoroutine(WaitThenAddManaCrystal(isTemp: false, isTurnStart));
		}
	}

	public void AddTempManaCrystals(int numCrystals)
	{
		for (int i = 0; i < numCrystals; i++)
		{
			StartCoroutine(WaitThenAddManaCrystal(isTemp: true, isTurnStart: false));
		}
	}

	public void DestroyManaCrystals(int numCrystals)
	{
		StartCoroutine(WaitThenDestroyManaCrystals(isTemp: false, numCrystals));
	}

	public void DestroyTempManaCrystals(int numCrystals)
	{
		StartCoroutine(WaitThenDestroyManaCrystals(isTemp: true, numCrystals));
	}

	public void UpdateSpentMana(int shownChangeAmount)
	{
		if (shownChangeAmount > 0)
		{
			SpendManaCrystals(shownChangeAmount);
		}
		else if (GameState.Get().IsTurnStartManagerActive())
		{
			TurnStartManager.Get().NotifyOfManaCrystalFilled(-shownChangeAmount);
		}
		else
		{
			ReadyManaCrystals(-shownChangeAmount);
		}
	}

	public void SpendManaCrystals(int numCrystals)
	{
		ManaCrystalAssetPaths manaCrystalAssetPaths = GetManaCrystalAssetPaths(m_manaCrystalType);
		SoundManager.Get().LoadAndPlay(manaCrystalAssetPaths.m_SoundOnSpendPath, base.gameObject);
		for (int i = 0; i < numCrystals; i++)
		{
			SpendManaCrystal();
		}
	}

	public void ReadyManaCrystals(int numCrystals)
	{
		for (int i = 0; i < numCrystals; i++)
		{
			ReadyManaCrystal();
		}
	}

	public int GetSpendableManaCrystals()
	{
		int num = 0;
		for (int i = 0; i < m_temporaryCrystals.Count; i++)
		{
			if (m_temporaryCrystals[i].state == ManaCrystal.State.READY)
			{
				num++;
			}
		}
		for (int j = 0; j < m_permanentCrystals.Count; j++)
		{
			ManaCrystal manaCrystal = m_permanentCrystals[j];
			if (manaCrystal.state == ManaCrystal.State.READY && !manaCrystal.IsOverloaded())
			{
				num++;
			}
		}
		return num;
	}

	public void CancelAllProposedMana(Entity entity)
	{
		if (entity == null || m_proposedManaSourceEntID != entity.GetEntityId())
		{
			return;
		}
		m_proposedManaSourceEntID = -1;
		m_eventSpells.m_proposeUsageSpell.ActivateState(SpellStateType.DEATH);
		for (int i = 0; i < m_temporaryCrystals.Count; i++)
		{
			if (m_temporaryCrystals[i].state == ManaCrystal.State.PROPOSED)
			{
				m_temporaryCrystals[i].state = ManaCrystal.State.READY;
			}
		}
		for (int num = m_permanentCrystals.Count - 1; num >= 0; num--)
		{
			if (m_permanentCrystals[num].state == ManaCrystal.State.PROPOSED)
			{
				m_permanentCrystals[num].state = ManaCrystal.State.READY;
			}
		}
	}

	public void ProposeManaCrystalUsage(Entity entity)
	{
		if (entity == null)
		{
			return;
		}
		m_proposedManaSourceEntID = entity.GetEntityId();
		int cost = entity.GetCost();
		m_eventSpells.m_proposeUsageSpell.ActivateState(SpellStateType.BIRTH);
		int num = 0;
		for (int num2 = m_temporaryCrystals.Count - 1; num2 >= 0; num2--)
		{
			if (m_temporaryCrystals[num2].state == ManaCrystal.State.USED)
			{
				Log.Gameplay.Print("Found a SPENT temporary mana crystal... this shouldn't happen!");
			}
			else if (num < cost)
			{
				m_temporaryCrystals[num2].state = ManaCrystal.State.PROPOSED;
				num++;
			}
			else
			{
				m_temporaryCrystals[num2].state = ManaCrystal.State.READY;
			}
		}
		for (int i = 0; i < m_permanentCrystals.Count; i++)
		{
			if (m_permanentCrystals[i].state != ManaCrystal.State.USED && !m_permanentCrystals[i].IsOverloaded())
			{
				if (num < cost)
				{
					m_permanentCrystals[i].state = ManaCrystal.State.PROPOSED;
					num++;
				}
				else
				{
					m_permanentCrystals[i].state = ManaCrystal.State.READY;
				}
			}
		}
	}

	public void HandleSameTurnOverloadChanged(int crystalsChanged)
	{
		if (crystalsChanged > 0)
		{
			MarkCrystalsOwedForOverload(crystalsChanged);
		}
		else if (crystalsChanged < 0)
		{
			ReclaimCrystalsOwedForOverload(-crystalsChanged);
		}
	}

	public void SetCrystalsLockedForOverload(int numCrystals)
	{
		StartCoroutine(WaitForCrystalsToLoadThenLockThem(numCrystals));
	}

	private IEnumerator WaitForCrystalsToLoadThenLockThem(int numCrystals)
	{
		while (m_numCrystalsLoading > 0)
		{
			yield return null;
		}
		for (int i = 0; i < numCrystals; i++)
		{
			if (i < m_permanentCrystals.Count)
			{
				m_permanentCrystals[i].PayOverload();
			}
		}
	}

	public void MarkCrystalsOwedForOverload(int numCrystals)
	{
		if (numCrystals > 0)
		{
			m_overloadLocksAreShowing = true;
		}
		int num = 0;
		int num2 = 0;
		while (numCrystals != num)
		{
			if (num2 == m_permanentCrystals.Count)
			{
				m_additionalOverloadedCrystalsOwedNextTurn += numCrystals - num;
				break;
			}
			ManaCrystal manaCrystal = m_permanentCrystals[num2];
			if (!manaCrystal.IsOwedForOverload())
			{
				manaCrystal.MarkAsOwedForOverload();
				num++;
			}
			num2++;
		}
	}

	public void ReclaimCrystalsOwedForOverload(int numCrystals)
	{
		int i = 0;
		int num = m_permanentCrystals.FindLastIndex((ManaCrystal crystal) => crystal.IsOwedForOverload());
		for (; i < numCrystals; i++)
		{
			if (num < 0)
			{
				break;
			}
			m_permanentCrystals[num].ReclaimOverload();
			num--;
		}
		m_additionalOverloadedCrystalsOwedNextTurn -= numCrystals - i;
		m_overloadLocksAreShowing = num >= 0 || m_additionalOverloadedCrystalsOwedNextTurn > 0;
	}

	public void UnlockCrystals(int numCrystals)
	{
		int i = 0;
		int num = m_permanentCrystals.FindLastIndex((ManaCrystal crystal) => crystal.IsOverloaded());
		for (; i < numCrystals; i++)
		{
			if (num < 0)
			{
				break;
			}
			m_permanentCrystals[num].UnlockOverload();
			num--;
		}
		m_additionalOverloadedCrystalsOwedThisTurn -= numCrystals - i;
		m_overloadLocksAreShowing = num >= 0 || m_additionalOverloadedCrystalsOwedThisTurn > 0;
	}

	public void TurnCrystalsRed(int previous, int current)
	{
		for (int i = previous; i < current && i < m_permanentCrystals.Count; i++)
		{
			m_permanentCrystals[i].gem.gameObject.GetComponent<Renderer>().GetMaterial().mainTexture = redCrystalTexture;
		}
	}

	public void OnCurrentPlayerChanged()
	{
		m_additionalOverloadedCrystalsOwedThisTurn = m_additionalOverloadedCrystalsOwedNextTurn;
		m_additionalOverloadedCrystalsOwedNextTurn = 0;
		if (m_additionalOverloadedCrystalsOwedThisTurn > 0)
		{
			m_overloadLocksAreShowing = true;
		}
		else
		{
			m_overloadLocksAreShowing = false;
		}
		for (int i = 0; i < m_permanentCrystals.Count; i++)
		{
			ManaCrystal manaCrystal = m_permanentCrystals[i];
			if (manaCrystal.IsOverloaded())
			{
				manaCrystal.UnlockOverload();
			}
			if (manaCrystal.IsOwedForOverload())
			{
				m_overloadLocksAreShowing = true;
				manaCrystal.PayOverload();
			}
			else if (m_additionalOverloadedCrystalsOwedThisTurn > 0)
			{
				manaCrystal.PayOverload();
				m_additionalOverloadedCrystalsOwedThisTurn--;
			}
		}
	}

	public bool ShouldShowTooltip(ManaCrystalType type)
	{
		return m_manaCrystalType == type;
	}

	public bool ShouldShowOverloadTooltip()
	{
		return m_overloadLocksAreShowing;
	}

	public void SetFriendlyManaGemTexture(AssetHandle<Texture> texture)
	{
		AssetHandle.Set(ref m_friendlyManaGemTexture, texture);
		ApplyFriendlyManaGemTexture();
	}

	public void SetFriendlyManaGemTint(Color tint)
	{
		if (!(m_friendlyManaGem == null))
		{
			m_friendlyManaGem.GetComponentInChildren<MeshRenderer>().GetMaterial().SetColor("_TintColor", tint);
		}
	}

	public void ShowPhoneManaTray()
	{
		m_friendlyManaGem.GetComponent<Animation>()[GEM_FLIP_ANIM_NAME].speed = 1f;
		m_friendlyManaGem.GetComponent<Animation>().Play(GEM_FLIP_ANIM_NAME);
		iTween.ValueTo(base.gameObject, iTween.Hash("from", m_friendlyManaText.TextAlpha, "to", 0f, "time", 0.1f, "onupdate", (Action<object>)delegate(object newVal)
		{
			m_friendlyManaText.TextAlpha = (float)newVal;
		}));
		manaTrayPhone.ToggleTraySlider(show: true);
	}

	public void HidePhoneManaTray()
	{
		m_friendlyManaGem.GetComponent<Animation>()[GEM_FLIP_ANIM_NAME].speed = -1f;
		if (m_friendlyManaGem.GetComponent<Animation>()[GEM_FLIP_ANIM_NAME].time == 0f)
		{
			m_friendlyManaGem.GetComponent<Animation>()[GEM_FLIP_ANIM_NAME].time = m_friendlyManaGem.GetComponent<Animation>()[GEM_FLIP_ANIM_NAME].length;
		}
		m_friendlyManaGem.GetComponent<Animation>().Play(GEM_FLIP_ANIM_NAME);
		iTween.ValueTo(base.gameObject, iTween.Hash("from", m_friendlyManaText.TextAlpha, "to", 1f, "time", 0.1f, "onupdate", (Action<object>)delegate(object newVal)
		{
			m_friendlyManaText.TextAlpha = (float)newVal;
		}));
		manaTrayPhone.ToggleTraySlider(show: false);
	}

	public Material GetTemporaryManaCrystalMaterial()
	{
		return m_ManaCrystalAssetTable[(int)m_manaCrystalType].m_tempManaCrystalMaterial;
	}

	public Material GetTemporaryManaCrystalProposedQuadMaterial()
	{
		return m_ManaCrystalAssetTable[(int)m_manaCrystalType].m_tempManaCrystalProposedQuadMaterial;
	}

	public void SetEnemyManaCounterActive(bool active)
	{
		opposingManaCounter.GetComponent<ManaCounter>().enabled = active;
		opposingManaCounter.SetActive(active);
	}

	private void UpdateLayout()
	{
		Vector3 position = base.transform.position;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			position = manaGemBone.transform.position;
		}
		for (int num = m_permanentCrystals.Count - 1; num >= 0; num--)
		{
			m_permanentCrystals[num].transform.position = position;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position.z += m_manaCrystalWidth;
			}
			else
			{
				position.x += m_manaCrystalWidth;
			}
		}
		for (int i = 0; i < m_temporaryCrystals.Count; i++)
		{
			m_temporaryCrystals[i].transform.position = position;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position.z += m_manaCrystalWidth;
			}
			else
			{
				position.x += m_manaCrystalWidth;
			}
		}
	}

	private IEnumerator UpdatePermanentCrystalStates()
	{
		while (m_numQueuedToReady > 0 || m_numCrystalsLoading > 0 || m_numQueuedToSpend > 0)
		{
			yield return null;
		}
		int num = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.RESOURCES_USED);
		int val = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
		int i;
		for (i = 0; i < num && i != m_permanentCrystals.Count; i++)
		{
			if (m_permanentCrystals[i].state != ManaCrystal.State.USED)
			{
				m_permanentCrystals[i].state = ManaCrystal.State.USED;
			}
		}
		for (int j = i; j < m_permanentCrystals.Count; j++)
		{
			if (m_permanentCrystals[j].state != 0)
			{
				m_permanentCrystals[j].state = ManaCrystal.State.READY;
			}
		}
		for (int k = 0; k < Math.Min(m_permanentCrystals.Count, val); k++)
		{
			if (!m_permanentCrystals[k].IsOwedForOverload())
			{
				m_permanentCrystals[k].MarkAsOwedForOverload();
			}
		}
	}

	private void LoadCrystalCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_numCrystalsLoading--;
		if (m_manaCrystalWidth <= 0f)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_manaCrystalWidth = 0.33f;
			}
			else
			{
				m_manaCrystalWidth = go.transform.Find("Gem_Mana").GetComponent<Renderer>().bounds.size.x;
			}
		}
		LoadCrystalCallbackData loadCrystalCallbackData = callbackData as LoadCrystalCallbackData;
		ManaCrystal component = go.GetComponent<ManaCrystal>();
		if (loadCrystalCallbackData.IsTempCrystal)
		{
			component.MarkAsTemp();
			m_temporaryCrystals.Add(component);
		}
		else
		{
			m_permanentCrystals.Add(component);
			if (loadCrystalCallbackData.IsTurnStart)
			{
				if (m_additionalOverloadedCrystalsOwedThisTurn > 0)
				{
					component.PayOverload();
					m_additionalOverloadedCrystalsOwedThisTurn--;
				}
			}
			else if (m_additionalOverloadedCrystalsOwedNextTurn > 0)
			{
				component.state = ManaCrystal.State.USED;
				component.MarkAsOwedForOverload();
				m_additionalOverloadedCrystalsOwedNextTurn--;
			}
			StartCoroutine(UpdatePermanentCrystalStates());
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			component.transform.parent = manaGemBone.transform.parent;
			component.transform.localRotation = manaGemBone.transform.localRotation;
			component.transform.localScale = manaGemBone.transform.localScale;
		}
		else
		{
			component.transform.parent = base.transform;
		}
		component.transform.localPosition = Vector3.zero;
		component.PlayCreateAnimation();
		ManaCrystalAssetPaths manaCrystalAssetPaths = GetManaCrystalAssetPaths(m_manaCrystalType);
		SoundManager.Get().LoadAndPlay(manaCrystalAssetPaths.m_SoundOnAddPath, base.gameObject);
		UpdateLayout();
	}

	public float GetWidth()
	{
		if (m_permanentCrystals.Count == 0)
		{
			return 0f;
		}
		return m_permanentCrystals[0].transform.Find("Gem_Mana").GetComponent<Renderer>().bounds.size.x * (float)m_permanentCrystals.Count * (float)m_temporaryCrystals.Count;
	}

	private ManaCrystalAssetPaths GetManaCrystalAssetPaths(ManaCrystalType type)
	{
		foreach (ManaCrystalAssetPaths item in m_ManaCrystalAssetTable)
		{
			if (item.m_Type == type)
			{
				return item;
			}
		}
		return m_ManaCrystalAssetTable[0];
	}

	private IEnumerator WaitThenAddManaCrystal(bool isTemp, bool isTurnStart)
	{
		m_numCrystalsLoading++;
		m_numQueuedToSpawn++;
		yield return new WaitForSeconds((float)m_numQueuedToSpawn * 0.2f);
		ManaCrystalAssetPaths manaCrystalAssetPaths = GetManaCrystalAssetPaths(m_manaCrystalType);
		LoadCrystalCallbackData callbackData = new LoadCrystalCallbackData(isTemp, isTurnStart);
		AssetLoader.Get().InstantiatePrefab(manaCrystalAssetPaths.m_ResourcePath, LoadCrystalCallback, callbackData, AssetLoadingOptions.IgnorePrefabPosition);
		m_numQueuedToSpawn--;
	}

	private IEnumerator WaitThenDestroyManaCrystals(bool isTemp, int numCrystals)
	{
		while (m_numCrystalsLoading > 0)
		{
			yield return null;
		}
		for (int i = 0; i < numCrystals; i++)
		{
			if (isTemp)
			{
				DestroyTempManaCrystal();
			}
			else
			{
				DestroyManaCrystal();
			}
		}
	}

	private IEnumerator WaitThenReadyManaCrystal()
	{
		m_numQueuedToReady++;
		yield return new WaitForSeconds((float)m_numQueuedToReady * 0.2f);
		if (m_numQueuedToReady <= 0)
		{
			yield break;
		}
		if (m_permanentCrystals.Count > 0)
		{
			for (int num = m_permanentCrystals.Count - 1; num >= 0; num--)
			{
				if (m_permanentCrystals[num].state == ManaCrystal.State.USED)
				{
					ManaCrystalAssetPaths manaCrystalAssetPaths = GetManaCrystalAssetPaths(m_manaCrystalType);
					SoundManager.Get().LoadAndPlay(manaCrystalAssetPaths.m_SoundOnRefreshPath, base.gameObject);
					m_permanentCrystals[num].state = ManaCrystal.State.READY;
					break;
				}
			}
		}
		m_numQueuedToReady--;
	}

	private IEnumerator WaitThenSpendManaCrystal()
	{
		m_numQueuedToSpend++;
		yield return new WaitForSeconds((float)(m_numQueuedToSpend - 1) * 0.2f);
		if (m_numQueuedToSpend <= 0)
		{
			yield break;
		}
		bool flag = false;
		for (int i = 0; i < m_permanentCrystals.Count; i++)
		{
			if (m_permanentCrystals[i].state != ManaCrystal.State.USED)
			{
				m_permanentCrystals[i].state = ManaCrystal.State.USED;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			m_numQueuedToReady--;
		}
		m_numQueuedToSpend--;
		if (m_numQueuedToSpend <= 0)
		{
			InputManager.Get().OnManaCrystalMgrManaSpent();
		}
	}

	private void DestroyManaCrystal()
	{
		if (m_permanentCrystals.Count > 0)
		{
			int index = 0;
			ManaCrystal manaCrystal = m_permanentCrystals[index];
			m_permanentCrystals.RemoveAt(index);
			manaCrystal.GetComponent<ManaCrystal>().Destroy();
			UpdateLayout();
			StartCoroutine(UpdatePermanentCrystalStates());
		}
	}

	private void DestroyTempManaCrystal()
	{
		if (m_temporaryCrystals.Count > 0)
		{
			int index = m_temporaryCrystals.Count - 1;
			ManaCrystal manaCrystal = m_temporaryCrystals[index];
			m_temporaryCrystals.RemoveAt(index);
			manaCrystal.GetComponent<ManaCrystal>().Destroy();
			UpdateLayout();
		}
	}

	private void SpendManaCrystal()
	{
		StartCoroutine(WaitThenSpendManaCrystal());
	}

	private void ReadyManaCrystal()
	{
		StartCoroutine(WaitThenReadyManaCrystal());
	}

	private void InitializePhoneManaGems()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_friendlyManaText = friendlyManaCounter.GetComponent<UberText>();
			ManaCounter component = friendlyManaCounter.GetComponent<ManaCounter>();
			string phoneLargeResource = m_ManaCrystalAssetTable[(int)m_manaCrystalType].m_phoneLargeResource;
			component.InitializeLargeResourceGameObject(phoneLargeResource);
			if (opposingManaCounter.activeInHierarchy)
			{
				opposingManaCounter.GetComponent<ManaCounter>().InitializeLargeResourceGameObject(phoneLargeResource);
			}
			m_friendlyManaGem = component.GetPhoneGem();
			ApplyFriendlyManaGemTexture();
		}
	}

	private void ApplyFriendlyManaGemTexture()
	{
		if (!(m_friendlyManaGem == null) && m_friendlyManaGemTexture != null)
		{
			m_friendlyManaGem.GetComponentInChildren<MeshRenderer>().GetMaterial().mainTexture = m_friendlyManaGemTexture;
		}
	}
}
