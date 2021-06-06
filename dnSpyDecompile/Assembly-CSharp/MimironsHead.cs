using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000805 RID: 2053
public class MimironsHead : SuperSpell
{
	// Token: 0x06006F37 RID: 28471 RVA: 0x0023D034 File Offset: 0x0023B234
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		Card card = this.m_taskList.GetSourceEntity(true).GetCard();
		if (this.m_taskList.IsOrigin())
		{
			List<PowerTaskList> list = new List<PowerTaskList>();
			for (PowerTaskList powerTaskList = this.m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
			{
				list.Add(powerTaskList);
			}
			foreach (PowerTaskList powerTaskList2 in list)
			{
				foreach (PowerTask powerTask in powerTaskList2.GetTaskList())
				{
					Network.PowerHistory power = powerTask.GetPower();
					if (power.Type == Network.PowerType.TAG_CHANGE)
					{
						Network.HistTagChange histTagChange = power as Network.HistTagChange;
						if (histTagChange.Tag == 360 && histTagChange.Value == 1)
						{
							Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
							if (entity == null)
							{
								Debug.LogWarning(string.Format("{0}.AddPowerTargets() - WARNING trying to target entity with id {1} but there is no entity with that id", this, histTagChange.Entity));
								continue;
							}
							Card card2 = entity.GetCard();
							if (card2 != card)
							{
								this.m_mechMinions.Add(card2);
							}
							else
							{
								this.m_mimiron = card2;
							}
							this.m_waitForTaskList = powerTaskList2;
						}
					}
					if (power.Type == Network.PowerType.FULL_ENTITY)
					{
						Network.Entity entity2 = (power as Network.HistFullEntity).Entity;
						Entity entity3 = GameState.Get().GetEntity(entity2.ID);
						if (entity3 == null)
						{
							Debug.LogWarning(string.Format("{0}.AddPowerTargets() - WARNING trying to target entity with id {1} but there is no entity with that id", this, entity2.ID));
						}
						else if (!(entity3.GetCardId() != "GVG_111t"))
						{
							Card card3 = entity3.GetCard();
							this.m_volt = card3;
							this.m_waitForTaskList = powerTaskList2;
						}
					}
				}
			}
			if (this.m_volt != null && this.m_mimiron != null && this.m_mechMinions.Count > 0)
			{
				this.m_mimiron.IgnoreDeath(true);
				foreach (Card card4 in this.m_mechMinions)
				{
					card4.IgnoreDeath(true);
				}
				using (List<Card>.Enumerator enumerator3 = card.GetController().GetBattlefieldZone().GetCards().GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Card card5 = enumerator3.Current;
						card5.SetDoNotSort(true);
					}
					goto IL_297;
				}
			}
			this.m_volt = null;
			this.m_mimiron = null;
			this.m_mechMinions.Clear();
		}
		IL_297:
		if (this.m_volt == null || this.m_mimiron == null || this.m_mechMinions.Count == 0 || this.m_taskList != this.m_waitForTaskList)
		{
			return false;
		}
		foreach (Card card6 in card.GetController().GetBattlefieldZone().GetCards())
		{
			card6.SetDoNotSort(true);
		}
		return true;
	}

	// Token: 0x06006F38 RID: 28472 RVA: 0x0023D3D0 File Offset: 0x0023B5D0
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if (this.m_voltSpawnOverrideSpell)
		{
			this.m_volt.OverrideCustomSpawnSpell(UnityEngine.Object.Instantiate<Spell>(this.m_voltSpawnOverrideSpell));
		}
		base.StartCoroutine(this.TransformEffect());
	}

	// Token: 0x06006F39 RID: 28473 RVA: 0x0023D422 File Offset: 0x0023B622
	private IEnumerator TransformEffect()
	{
		foreach (string input in this.m_startSounds)
		{
			SoundManager.Get().LoadAndPlay(input);
		}
		this.m_volt.SetDoNotSort(true);
		this.m_taskList.DoAllTasks();
		while (!this.m_taskList.IsComplete())
		{
			yield return null;
		}
		this.m_volt.GetActor().Hide();
		GameObject gameObject = this.m_volt.GetActor().gameObject;
		this.m_voltParent = gameObject.transform.parent;
		gameObject.transform.parent = this.m_highPosBone.transform;
		gameObject.transform.localPosition = new Vector3(0f, -0.1f, 0f);
		this.m_root.transform.parent = null;
		this.m_root.transform.localPosition = Vector3.zero;
		iTween.MoveTo(this.m_mimiron.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.m_highPosBone.transform.localPosition,
			"easetype",
			iTween.EaseType.easeOutQuart,
			"time",
			this.m_mimironHighTime,
			"delay",
			0.5f
		}));
		yield return new WaitForSeconds(0.5f + this.m_mimironHighTime / 5f);
		this.TransformMinions();
		yield break;
	}

	// Token: 0x06006F3A RID: 28474 RVA: 0x0023D434 File Offset: 0x0023B634
	private void TransformMinions()
	{
		float num = 1f;
		Vector3 point = new Vector3(0f, 0f, 2.3f);
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_mechMinions.Count; i++)
		{
			list.Add(i);
		}
		List<int> list2 = new List<int>();
		for (int j = 0; j < this.m_mechMinions.Count; j++)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			list2.Add(list[index]);
			list.RemoveAt(index);
		}
		for (int k = 0; k < this.m_mechMinions.Count; k++)
		{
			Vector3 b = Quaternion.Euler(0f, (float)(360 / this.m_mechMinions.Count * list2[k] + 60), 0f) * point;
			this.m_minionPosBone.transform.localPosition = this.m_highPosBone.transform.localPosition + b;
			GameObject gameObject = this.m_mechMinions[k].GetActor().gameObject;
			float num2 = num / (float)this.m_mechMinions.Count * (float)k;
			base.StartCoroutine(this.MinionPlayFX(gameObject, this.m_minionElectricity, num2 / 2f));
			List<Vector3> list3 = new List<Vector3>();
			Vector3 b2 = new Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f));
			list3.Add(gameObject.transform.position + (this.m_minionPosBone.transform.localPosition - gameObject.transform.position) / 4f + b2);
			list3.Add(this.m_minionPosBone.transform.localPosition);
			if (k < this.m_mechMinions.Count - 1)
			{
				iTween.MoveTo(gameObject, iTween.Hash(new object[]
				{
					"path",
					list3.ToArray(),
					"easetype",
					iTween.EaseType.easeInOutSine,
					"delay",
					num2,
					"time",
					this.m_minionHighTime / (float)this.m_mechMinions.Count
				}));
			}
			else
			{
				iTween.MoveTo(gameObject, iTween.Hash(new object[]
				{
					"path",
					list3.ToArray(),
					"easetype",
					iTween.EaseType.easeInOutSine,
					"delay",
					num2,
					"time",
					this.m_minionHighTime / (float)this.m_mechMinions.Count,
					"oncomplete",
					new Action<object>(delegate(object newVal)
					{
						this.FadeInBackground();
					})
				}));
			}
		}
	}

	// Token: 0x06006F3B RID: 28475 RVA: 0x0023D726 File Offset: 0x0023B926
	private IEnumerator MinionPlayFX(GameObject minion, GameObject FX, float delay)
	{
		GameObject minionFX = UnityEngine.Object.Instantiate<GameObject>(FX);
		minionFX.transform.parent = minion.transform;
		minionFX.transform.localPosition = new Vector3(0f, 0.5f, 0f);
		if (!this.m_cleanup.ContainsKey(minion))
		{
			this.m_cleanup.Add(minion, new List<GameObject>());
		}
		this.m_cleanup[minion].Add(minionFX);
		yield return new WaitForSeconds(delay);
		minionFX.GetComponent<ParticleSystem>().Play();
		yield break;
	}

	// Token: 0x06006F3C RID: 28476 RVA: 0x0023D74A File Offset: 0x0023B94A
	private IEnumerator MimironNegativeFX()
	{
		while (this.m_isNegFlash)
		{
			yield return new WaitForSeconds(this.m_flashDelay);
			this.m_mimironNegative.SetActive(!this.m_mimironNegative.activeSelf);
			if (this.m_flashDelay > 0.05f)
			{
				this.m_flashDelay -= 0.01f;
			}
		}
		this.m_mimironNegative.SetActive(false);
		yield break;
	}

	// Token: 0x06006F3D RID: 28477 RVA: 0x0023D75C File Offset: 0x0023B95C
	private void MinionCleanup(GameObject minion)
	{
		if (this.m_cleanup.ContainsKey(minion))
		{
			foreach (GameObject gameObject in this.m_cleanup[minion])
			{
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
	}

	// Token: 0x06006F3E RID: 28478 RVA: 0x0023D7CC File Offset: 0x0023B9CC
	private void FadeInBackground()
	{
		this.m_background.SetActive(true);
		this.m_background.GetComponent<Renderer>().GetMaterial().SetColor("_Color", this.m_clear);
		HighlightState componentInChildren = this.m_volt.GetActor().gameObject.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			componentInChildren.Hide();
		}
		iTween.ColorTo(this.m_background, iTween.Hash(new object[]
		{
			"r",
			1f,
			"g",
			1f,
			"b",
			1f,
			"a",
			1f,
			"time",
			0.5f,
			"oncomplete",
			new Action<object>(delegate(object newVal)
			{
				this.MimironPowerUp();
			})
		}));
	}

	// Token: 0x06006F3F RID: 28479 RVA: 0x0023D8C4 File Offset: 0x0023BAC4
	private void SetGlow(Material glowMat, float newVal, string colorVal = "_TintColor")
	{
		glowMat.SetColor(colorVal, Color.Lerp(this.m_clear, Color.white, newVal));
	}

	// Token: 0x06006F40 RID: 28480 RVA: 0x0023D8E0 File Offset: 0x0023BAE0
	private void MimironPowerUp()
	{
		this.m_mimironElectricity.GetComponent<ParticleSystem>().Play();
		for (int i = 0; i < this.m_mechMinions.Count; i++)
		{
			GameObject gameObject = this.m_mechMinions[i].GetActor().gameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_minionGlow);
			if (!this.m_cleanup.ContainsKey(gameObject))
			{
				this.m_cleanup.Add(gameObject, new List<GameObject>());
			}
			this.m_cleanup[gameObject].Add(gameObject2);
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.localPosition = new Vector3(0f, 0.5f, 0f);
			float num = this.m_absorbTime / (float)this.m_mechMinions.Count * (float)i;
			Material rendererMaterial = gameObject2.GetComponent<Renderer>().GetMaterial();
			rendererMaterial.SetColor("_TintColor", this.m_clear);
			SceneUtils.EnableRenderers(gameObject2, true);
			if (i < this.m_mechMinions.Count - 1)
			{
				iTween.ValueTo(gameObject2, iTween.Hash(new object[]
				{
					"from",
					0f,
					"to",
					1f,
					"time",
					this.m_glowTime,
					"delay",
					0.1f + num + this.m_sparkDelay,
					"onstart",
					new Action<object>(delegate(object newVal)
					{
						SoundManager.Get().LoadAndPlay(this.m_perMinionSound);
					}),
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.SetGlow(rendererMaterial, (float)newVal, "_TintColor");
					})
				}));
				iTween.ValueTo(gameObject2, iTween.Hash(new object[]
				{
					"from",
					1f,
					"to",
					0f,
					"time",
					this.m_glowTime,
					"delay",
					0.1f + num + this.m_sparkDelay + this.m_glowTime,
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.SetGlow(rendererMaterial, (float)newVal, "_TintColor");
					})
				}));
			}
			else
			{
				iTween.ValueTo(gameObject2, iTween.Hash(new object[]
				{
					"from",
					0f,
					"to",
					1f,
					"time",
					this.m_glowTime,
					"delay",
					0.1f + num + this.m_sparkDelay,
					"onstart",
					new Action<object>(delegate(object newVal)
					{
						SoundManager.Get().LoadAndPlay(this.m_perMinionSound);
					}),
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.SetGlow(rendererMaterial, (float)newVal, "_TintColor");
					}),
					"oncomplete",
					new Action<object>(delegate(object newVal)
					{
						this.AbsorbMinions();
					})
				}));
				iTween.ValueTo(gameObject2, iTween.Hash(new object[]
				{
					"from",
					1f,
					"to",
					0f,
					"time",
					this.m_glowTime,
					"delay",
					0.1f + num + this.m_sparkDelay + this.m_glowTime,
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.SetGlow(rendererMaterial, (float)newVal, "_TintColor");
					})
				}));
			}
		}
	}

	// Token: 0x06006F41 RID: 28481 RVA: 0x0023DC88 File Offset: 0x0023BE88
	private void AbsorbMinions()
	{
		Vector3 b = new Vector3(0f, -1f, 0f);
		for (int i = 0; i < this.m_mechMinions.Count; i++)
		{
			float num = this.m_absorbTime / (float)this.m_mechMinions.Count * (float)i;
			GameObject minion = this.m_mechMinions[i].GetActor().gameObject;
			if (i < this.m_mechMinions.Count - 1)
			{
				iTween.MoveTo(minion, iTween.Hash(new object[]
				{
					"position",
					this.m_highPosBone.transform.localPosition + b,
					"easetype",
					iTween.EaseType.easeInOutSine,
					"delay",
					this.m_glowTime + num + this.m_sparkDelay,
					"time",
					0.5f,
					"oncomplete",
					new Action<object>(delegate(object newVal)
					{
						this.MinionCleanup(minion);
					})
				}));
			}
			else
			{
				iTween.MoveTo(minion, iTween.Hash(new object[]
				{
					"position",
					this.m_highPosBone.transform.localPosition + b,
					"easetype",
					iTween.EaseType.easeInOutSine,
					"delay",
					this.m_glowTime + num + this.m_sparkDelay,
					"time",
					0.5f,
					"oncomplete",
					new Action<object>(delegate(object newVal)
					{
						this.MinionCleanup(minion);
						this.FlareMimiron();
					})
				}));
			}
		}
		this.m_isNegFlash = true;
		base.StartCoroutine(this.MimironNegativeFX());
	}

	// Token: 0x06006F42 RID: 28482 RVA: 0x0023DE6C File Offset: 0x0023C06C
	private void FlareMimiron()
	{
		Material mimironGlowMaterial = this.m_mimironGlow.GetComponent<Renderer>().GetMaterial();
		Material mimironFlareMaterial = this.m_mimironFlare.GetComponent<Renderer>().GetMaterial();
		mimironGlowMaterial.SetColor("_TintColor", this.m_clear);
		mimironFlareMaterial.SetColor("_TintColor", this.m_clear);
		this.m_mimironGlow.SetActive(true);
		this.m_mimironFlare.SetActive(true);
		iTween.ValueTo(this.m_mimironGlow, iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			0.7f,
			"time",
			0.3,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGlow(mimironGlowMaterial, (float)newVal, "_TintColor");
			})
		}));
		iTween.ValueTo(this.m_mimironFlare, iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			2.5f,
			"time",
			0.3f,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGlow(mimironFlareMaterial, (float)newVal, "_Intensity");
			}),
			"oncomplete",
			new Action<object>(delegate(object newVal)
			{
				this.UnflareMimiron();
			})
		}));
	}

	// Token: 0x06006F43 RID: 28483 RVA: 0x0023DFEC File Offset: 0x0023C1EC
	private void UnflareMimiron()
	{
		this.m_volt.SetDoNotSort(false);
		ZonePlay battlefieldZone = this.m_volt.GetController().GetBattlefieldZone();
		foreach (Card card in battlefieldZone.GetCards())
		{
			card.SetDoNotSort(false);
		}
		battlefieldZone.UpdateLayout();
		this.DestroyMinions();
		this.m_volt.GetActor().Show();
		Material mimironGlowMaterial = this.m_mimironGlow.GetComponent<Renderer>().GetMaterial();
		Material mimironFlareMaterial = this.m_mimironFlare.GetComponent<Renderer>().GetMaterial();
		mimironGlowMaterial.SetColor("_TintColor", this.m_clear);
		mimironFlareMaterial.SetColor("_TintColor", this.m_clear);
		this.m_mimironGlow.SetActive(true);
		this.m_mimironFlare.SetActive(true);
		iTween.ValueTo(this.m_mimironGlow, iTween.Hash(new object[]
		{
			"from",
			0.7f,
			"to",
			0f,
			"time",
			0.3,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGlow(mimironGlowMaterial, (float)newVal, "_TintColor");
			})
		}));
		iTween.ValueTo(this.m_mimironFlare, iTween.Hash(new object[]
		{
			"from",
			2.5f,
			"to",
			0f,
			"time",
			0.3f,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGlow(mimironFlareMaterial, (float)newVal, "_Intensity");
			}),
			"oncomplete",
			new Action<object>(delegate(object newVal)
			{
				this.FadeOutBackground();
			})
		}));
		this.m_isNegFlash = false;
		this.OnSpellFinished();
	}

	// Token: 0x06006F44 RID: 28484 RVA: 0x0023E1F8 File Offset: 0x0023C3F8
	private void FadeOutBackground()
	{
		this.m_mimironGlow.SetActive(false);
		this.m_mimironFlare.SetActive(false);
		iTween.ColorTo(this.m_background, iTween.Hash(new object[]
		{
			"r",
			1f,
			"g",
			1f,
			"b",
			1f,
			"a",
			0f,
			"time",
			0.5f,
			"oncomplete",
			new Action<object>(delegate(object newVal)
			{
				this.RaiseVolt();
			})
		}));
	}

	// Token: 0x06006F45 RID: 28485 RVA: 0x0023E2B8 File Offset: 0x0023C4B8
	private void DestroyMinions()
	{
		foreach (Card card in this.m_mechMinions)
		{
			card.IgnoreDeath(false);
			card.SetDoNotSort(false);
			card.GetActor().Destroy();
		}
		this.m_mimiron.IgnoreDeath(false);
		this.m_mimiron.SetDoNotSort(false);
		this.m_mimiron.GetActor().Destroy();
	}

	// Token: 0x06006F46 RID: 28486 RVA: 0x0023E344 File Offset: 0x0023C544
	private void RaiseVolt()
	{
		this.m_mimironElectricity.GetComponent<ParticleSystem>().Stop();
		this.m_background.GetComponent<Renderer>().GetMaterial().SetColor("_Color", this.m_clear);
		this.m_background.SetActive(false);
		GameObject gameObject = this.m_volt.GetActor().gameObject;
		gameObject.transform.parent = this.m_voltParent;
		iTween.MoveTo(gameObject, iTween.Hash(new object[]
		{
			"position",
			gameObject.transform.localPosition + new Vector3(0f, 3f, 0f),
			"time",
			0.2f,
			"islocal",
			true,
			"oncomplete",
			new Action<object>(delegate(object newVal)
			{
				this.DropV07tron();
			})
		}));
	}

	// Token: 0x06006F47 RID: 28487 RVA: 0x0023E434 File Offset: 0x0023C634
	private void DropV07tron()
	{
		iTween.MoveTo(this.m_volt.GetActor().gameObject, iTween.Hash(new object[]
		{
			"position",
			Vector3.zero,
			"time",
			0.3f,
			"islocal",
			true
		}));
		this.Finish();
	}

	// Token: 0x06006F48 RID: 28488 RVA: 0x0023E4A2 File Offset: 0x0023C6A2
	private void Finish()
	{
		this.m_volt = null;
		this.m_mimiron = null;
		this.m_mechMinions.Clear();
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x04005929 RID: 22825
	public GameObject m_root;

	// Token: 0x0400592A RID: 22826
	public GameObject m_highPosBone;

	// Token: 0x0400592B RID: 22827
	public GameObject m_minionPosBone;

	// Token: 0x0400592C RID: 22828
	public GameObject m_background;

	// Token: 0x0400592D RID: 22829
	public GameObject m_minionElectricity;

	// Token: 0x0400592E RID: 22830
	public GameObject m_minionGlow;

	// Token: 0x0400592F RID: 22831
	public GameObject m_mimironNegative;

	// Token: 0x04005930 RID: 22832
	public GameObject m_mimironFlare;

	// Token: 0x04005931 RID: 22833
	public GameObject m_mimironGlow;

	// Token: 0x04005932 RID: 22834
	public GameObject m_mimironElectricity;

	// Token: 0x04005933 RID: 22835
	public Spell m_voltSpawnOverrideSpell;

	// Token: 0x04005934 RID: 22836
	public string m_perMinionSound;

	// Token: 0x04005935 RID: 22837
	public string[] m_startSounds;

	// Token: 0x04005936 RID: 22838
	private Card m_volt;

	// Token: 0x04005937 RID: 22839
	private Card m_mimiron;

	// Token: 0x04005938 RID: 22840
	private List<Card> m_mechMinions = new List<Card>();

	// Token: 0x04005939 RID: 22841
	private Transform m_voltParent;

	// Token: 0x0400593A RID: 22842
	private Color m_clear = new Color(1f, 1f, 1f, 0f);

	// Token: 0x0400593B RID: 22843
	private Map<GameObject, List<GameObject>> m_cleanup = new Map<GameObject, List<GameObject>>();

	// Token: 0x0400593C RID: 22844
	private bool m_isNegFlash;

	// Token: 0x0400593D RID: 22845
	private float m_flashDelay = 0.15f;

	// Token: 0x0400593E RID: 22846
	private float m_mimironHighTime = 1.5f;

	// Token: 0x0400593F RID: 22847
	private float m_minionHighTime = 2f;

	// Token: 0x04005940 RID: 22848
	private float m_sparkDelay = 0.3f;

	// Token: 0x04005941 RID: 22849
	private float m_absorbTime = 1f;

	// Token: 0x04005942 RID: 22850
	private float m_glowTime = 0.5f;

	// Token: 0x04005943 RID: 22851
	private PowerTaskList m_waitForTaskList;
}
