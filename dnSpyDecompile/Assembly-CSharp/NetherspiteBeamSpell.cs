using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200080C RID: 2060
[RequireComponent(typeof(UberCurve))]
[RequireComponent(typeof(LineRenderer))]
public class NetherspiteBeamSpell : Spell
{
	// Token: 0x06006F83 RID: 28547 RVA: 0x0023F31C File Offset: 0x0023D51C
	protected override void Awake()
	{
		base.Awake();
		this.m_uberCurve = base.GetComponent<UberCurve>();
		this.m_lineRenderer = base.GetComponent<LineRenderer>();
		this.m_beamMaterial = this.m_lineRenderer.GetMaterial();
		if (!string.IsNullOrEmpty(this.m_beamFadeInMaterialVar))
		{
			this.m_beamFadeInPropertyID = Shader.PropertyToID(this.m_beamFadeInMaterialVar);
		}
	}

	// Token: 0x06006F84 RID: 28548 RVA: 0x0023F378 File Offset: 0x0023D578
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		if (this.m_beamSourceSpell != null)
		{
			this.m_beamSourceSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_beamSourceSpell);
			this.m_beamSourceSpellInstance.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished));
			this.m_beamSourceSpellInstance.transform.parent = base.GetSourceCard().GetActor().transform;
			TransformUtil.Identity(this.m_beamSourceSpellInstance);
			this.m_beamSourceSpellInstance.Activate();
		}
		if (this.m_fullPathParticles != null)
		{
			this.m_fullPathParticles.transform.parent = base.GetSourceCard().GetActor().transform;
			TransformUtil.Identity(this.m_fullPathParticles);
		}
		if (this.m_blockedPathParticles != null)
		{
			this.m_blockedPathParticles.transform.parent = base.GetSourceCard().GetActor().transform;
			TransformUtil.Identity(this.m_blockedPathParticles);
		}
		base.StartCoroutine("DoUpdate");
	}

	// Token: 0x06006F85 RID: 28549 RVA: 0x0023F478 File Offset: 0x0023D678
	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		if (this.m_beamSourceSpellInstance != null)
		{
			this.m_beamSourceSpellInstance.ActivateState(SpellStateType.DEATH);
		}
		if (this.m_fullPathParticles != null)
		{
			this.m_fullPathParticles.Stop();
			this.m_fullPathParticles.Clear();
		}
		if (this.m_blockedPathParticles != null)
		{
			this.m_blockedPathParticles.Stop();
			this.m_blockedPathParticles.Clear();
		}
		base.StopCoroutine("DoUpdate");
	}

	// Token: 0x06006F86 RID: 28550 RVA: 0x0023F4F9 File Offset: 0x0023D6F9
	private IEnumerator DoUpdate()
	{
		for (;;)
		{
			Actor actor = this.GetTargetMinion();
			int num;
			if (actor == null)
			{
				this.m_usingFullPath = true;
				actor = SpellUtils.FindOpponentPlayer(this).GetHeroCard().GetActor();
				num = this.m_fullPathPolys;
				this.UpdateFullPathControlPoints();
			}
			else
			{
				this.m_usingFullPath = false;
				num = this.m_blockedPathPolys;
				this.UpdateBlockedPathControlPoints(actor);
			}
			if (actor != this.m_targetActor)
			{
				if (this.m_beamTargetSpellInstance != null)
				{
					this.m_beamTargetSpellInstance.ActivateState(SpellStateType.DEATH);
				}
				if (this.m_usingFullPath)
				{
					if (!string.IsNullOrEmpty(this.m_beamFadeInMaterialVar))
					{
						iTween.StopByName(base.gameObject, "fadeBeam");
						this.UpdateBeamFade(0f);
						Hashtable args = iTween.Hash(new object[]
						{
							"from",
							0f,
							"to",
							1f,
							"time",
							this.m_beamFadeInTime,
							"easetype",
							iTween.EaseType.linear,
							"onupdate",
							"UpdateBeamFade",
							"onupdatetarget",
							base.gameObject,
							"name",
							"fadeBeam"
						});
						iTween.ValueTo(base.gameObject, args);
					}
					if (this.m_fullPathParticles != null)
					{
						this.m_fullPathParticles.Play();
					}
					if (this.m_blockedPathParticles != null)
					{
						this.m_blockedPathParticles.Stop();
						this.m_blockedPathParticles.Clear();
					}
				}
				else
				{
					if (this.m_fullPathParticles != null)
					{
						this.m_fullPathParticles.Stop();
						this.m_fullPathParticles.Clear();
					}
					if (this.m_blockedPathParticles != null)
					{
						this.m_blockedPathParticles.Play();
					}
				}
				this.m_targetActor = actor;
				if (this.m_targetActor != null)
				{
					Spell spell = (this.m_targetActor.GetEntity().GetCardType() == TAG_CARDTYPE.HERO) ? this.m_beamTargetHeroSpell : this.m_beamTargetMinionSpell;
					if (spell != null)
					{
						this.m_beamTargetSpellInstance = UnityEngine.Object.Instantiate<Spell>(spell);
						this.m_beamTargetSpellInstance.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished));
						this.m_beamTargetSpellInstance.transform.parent = this.m_targetActor.transform;
						TransformUtil.Identity(this.m_beamTargetSpellInstance);
						this.m_beamTargetSpellInstance.Activate();
					}
					else
					{
						this.m_beamTargetSpellInstance = null;
					}
				}
			}
			this.m_lineRenderer.positionCount = num;
			for (int i = 0; i < num; i++)
			{
				float position = (float)i / (float)num;
				this.m_lineRenderer.SetPosition(i, this.m_uberCurve.CatmullRomEvaluateWorldPosition(position));
			}
			this.VisualizeControlPoints();
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006F87 RID: 28551 RVA: 0x0023F508 File Offset: 0x0023D708
	private void UpdateBeamFade(float fadeValue)
	{
		Color color = this.m_beamMaterial.GetColor(this.m_beamFadeInPropertyID);
		color.a = fadeValue;
		this.m_beamMaterial.SetColor(this.m_beamFadeInPropertyID, color);
	}

	// Token: 0x06006F88 RID: 28552 RVA: 0x0023F544 File Offset: 0x0023D744
	private void UpdateBlockedPathControlPoints(Actor minionToRight)
	{
		int num = this.m_sourceCardOffsets.Count + this.m_destCardOffsets.Count;
		if (this.m_uberCurve.m_controlPoints.Count != num)
		{
			this.m_uberCurve.m_controlPoints.Clear();
			for (int i = 0; i < num; i++)
			{
				this.m_uberCurve.m_controlPoints.Add(new UberCurve.UberCurveControlPoint());
			}
		}
		int num2 = 0;
		Card sourceCard = base.GetSourceCard();
		int j = 0;
		while (j < this.m_sourceCardOffsets.Count)
		{
			this.m_uberCurve.m_controlPoints[num2].position = sourceCard.transform.position + this.m_sourceCardOffsets[j];
			j++;
			num2++;
		}
		int k = 0;
		while (k < this.m_destCardOffsets.Count)
		{
			this.m_uberCurve.m_controlPoints[num2].position = minionToRight.transform.position + this.m_destCardOffsets[k];
			k++;
			num2++;
		}
	}

	// Token: 0x06006F89 RID: 28553 RVA: 0x0023F658 File Offset: 0x0023D858
	private void UpdateFullPathControlPoints()
	{
		int num = this.m_sourceCardOffsets.Count + this.m_fullPathPoints.Count;
		if (this.m_uberCurve.m_controlPoints.Count != num)
		{
			this.m_uberCurve.m_controlPoints.Clear();
			for (int i = 0; i < num; i++)
			{
				this.m_uberCurve.m_controlPoints.Add(new UberCurve.UberCurveControlPoint());
			}
		}
		int num2 = 0;
		Card sourceCard = base.GetSourceCard();
		int j = 0;
		while (j < this.m_sourceCardOffsets.Count)
		{
			this.m_uberCurve.m_controlPoints[num2].position = sourceCard.transform.position + this.m_sourceCardOffsets[j];
			j++;
			num2++;
		}
		int k = 0;
		while (k < this.m_fullPathPoints.Count)
		{
			this.m_uberCurve.m_controlPoints[num2].position = this.m_fullPathPoints[k];
			k++;
			num2++;
		}
	}

	// Token: 0x06006F8A RID: 28554 RVA: 0x0023F75C File Offset: 0x0023D95C
	private Actor GetTargetMinion()
	{
		int zonePosition = base.GetSourceCard().GetZonePosition();
		ZonePlay battlefieldZone = base.GetSourceCard().GetController().GetBattlefieldZone();
		int num = this.m_targetMinionToRight ? (zonePosition + 1) : (zonePosition - 1);
		while (num > 0 && num <= battlefieldZone.GetCardCount())
		{
			Card cardAtPos = battlefieldZone.GetCardAtPos(num);
			if (cardAtPos.IsActorReady())
			{
				return cardAtPos.GetActor();
			}
			num += (this.m_targetMinionToRight ? 1 : -1);
		}
		return null;
	}

	// Token: 0x06006F8B RID: 28555 RVA: 0x0023F7D0 File Offset: 0x0023D9D0
	private void VisualizeControlPoints()
	{
		if (!this.m_visualizeControlPoints)
		{
			foreach (GameObject obj in this.m_visualizers)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.m_visualizers.Clear();
			return;
		}
		if (this.m_visualizers.Count != this.m_uberCurve.m_controlPoints.Count)
		{
			foreach (GameObject obj2 in this.m_visualizers)
			{
				UnityEngine.Object.Destroy(obj2);
			}
			this.m_visualizers.Clear();
			for (int i = 0; i < this.m_uberCurve.m_controlPoints.Count; i++)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				gameObject.transform.position = this.m_uberCurve.m_controlPoints[i].position;
				gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				this.m_visualizers.Add(gameObject);
			}
			return;
		}
		for (int j = 0; j < this.m_uberCurve.m_controlPoints.Count; j++)
		{
			this.m_visualizers[j].transform.position = this.m_uberCurve.transform.TransformPoint(this.m_uberCurve.m_controlPoints[j].position);
		}
	}

	// Token: 0x06006F8C RID: 28556 RVA: 0x0023F964 File Offset: 0x0023DB64
	private void OnSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(spell.gameObject);
		}
	}

	// Token: 0x0400595E RID: 22878
	[Range(1f, 1000f)]
	public int m_fullPathPolys = 50;

	// Token: 0x0400595F RID: 22879
	[Range(1f, 1000f)]
	public int m_blockedPathPolys = 5;

	// Token: 0x04005960 RID: 22880
	public bool m_targetMinionToRight = true;

	// Token: 0x04005961 RID: 22881
	public List<Vector3> m_sourceCardOffsets;

	// Token: 0x04005962 RID: 22882
	public List<Vector3> m_destCardOffsets;

	// Token: 0x04005963 RID: 22883
	public List<Vector3> m_fullPathPoints;

	// Token: 0x04005964 RID: 22884
	public bool m_visualizeControlPoints;

	// Token: 0x04005965 RID: 22885
	public string m_beamFadeInMaterialVar = "";

	// Token: 0x04005966 RID: 22886
	private int m_beamFadeInPropertyID;

	// Token: 0x04005967 RID: 22887
	public float m_beamFadeInTime = 1f;

	// Token: 0x04005968 RID: 22888
	public Spell m_beamSourceSpell;

	// Token: 0x04005969 RID: 22889
	public Spell m_beamTargetMinionSpell;

	// Token: 0x0400596A RID: 22890
	public Spell m_beamTargetHeroSpell;

	// Token: 0x0400596B RID: 22891
	public ParticleSystem m_fullPathParticles;

	// Token: 0x0400596C RID: 22892
	public ParticleSystem m_blockedPathParticles;

	// Token: 0x0400596D RID: 22893
	private bool m_usingFullPath;

	// Token: 0x0400596E RID: 22894
	private Actor m_targetActor;

	// Token: 0x0400596F RID: 22895
	private Spell m_beamTargetSpellInstance;

	// Token: 0x04005970 RID: 22896
	private Spell m_beamSourceSpellInstance;

	// Token: 0x04005971 RID: 22897
	private UberCurve m_uberCurve;

	// Token: 0x04005972 RID: 22898
	private LineRenderer m_lineRenderer;

	// Token: 0x04005973 RID: 22899
	private Material m_beamMaterial;

	// Token: 0x04005974 RID: 22900
	private List<GameObject> m_visualizers = new List<GameObject>();
}
