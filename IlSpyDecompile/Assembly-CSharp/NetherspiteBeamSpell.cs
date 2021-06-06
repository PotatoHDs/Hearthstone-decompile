using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UberCurve))]
[RequireComponent(typeof(LineRenderer))]
public class NetherspiteBeamSpell : Spell
{
	[Range(1f, 1000f)]
	public int m_fullPathPolys = 50;

	[Range(1f, 1000f)]
	public int m_blockedPathPolys = 5;

	public bool m_targetMinionToRight = true;

	public List<Vector3> m_sourceCardOffsets;

	public List<Vector3> m_destCardOffsets;

	public List<Vector3> m_fullPathPoints;

	public bool m_visualizeControlPoints;

	public string m_beamFadeInMaterialVar = "";

	private int m_beamFadeInPropertyID;

	public float m_beamFadeInTime = 1f;

	public Spell m_beamSourceSpell;

	public Spell m_beamTargetMinionSpell;

	public Spell m_beamTargetHeroSpell;

	public ParticleSystem m_fullPathParticles;

	public ParticleSystem m_blockedPathParticles;

	private bool m_usingFullPath;

	private Actor m_targetActor;

	private Spell m_beamTargetSpellInstance;

	private Spell m_beamSourceSpellInstance;

	private UberCurve m_uberCurve;

	private LineRenderer m_lineRenderer;

	private Material m_beamMaterial;

	private List<GameObject> m_visualizers = new List<GameObject>();

	protected override void Awake()
	{
		base.Awake();
		m_uberCurve = GetComponent<UberCurve>();
		m_lineRenderer = GetComponent<LineRenderer>();
		m_beamMaterial = m_lineRenderer.GetMaterial();
		if (!string.IsNullOrEmpty(m_beamFadeInMaterialVar))
		{
			m_beamFadeInPropertyID = Shader.PropertyToID(m_beamFadeInMaterialVar);
		}
	}

	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		if (m_beamSourceSpell != null)
		{
			m_beamSourceSpellInstance = Object.Instantiate(m_beamSourceSpell);
			m_beamSourceSpellInstance.AddStateFinishedCallback(OnSpellStateFinished);
			m_beamSourceSpellInstance.transform.parent = GetSourceCard().GetActor().transform;
			TransformUtil.Identity(m_beamSourceSpellInstance);
			m_beamSourceSpellInstance.Activate();
		}
		if (m_fullPathParticles != null)
		{
			m_fullPathParticles.transform.parent = GetSourceCard().GetActor().transform;
			TransformUtil.Identity(m_fullPathParticles);
		}
		if (m_blockedPathParticles != null)
		{
			m_blockedPathParticles.transform.parent = GetSourceCard().GetActor().transform;
			TransformUtil.Identity(m_blockedPathParticles);
		}
		StartCoroutine("DoUpdate");
	}

	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		if (m_beamSourceSpellInstance != null)
		{
			m_beamSourceSpellInstance.ActivateState(SpellStateType.DEATH);
		}
		if (m_fullPathParticles != null)
		{
			m_fullPathParticles.Stop();
			m_fullPathParticles.Clear();
		}
		if (m_blockedPathParticles != null)
		{
			m_blockedPathParticles.Stop();
			m_blockedPathParticles.Clear();
		}
		StopCoroutine("DoUpdate");
	}

	private IEnumerator DoUpdate()
	{
		while (true)
		{
			Actor actor = GetTargetMinion();
			int num;
			if (actor == null)
			{
				m_usingFullPath = true;
				actor = SpellUtils.FindOpponentPlayer(this).GetHeroCard().GetActor();
				num = m_fullPathPolys;
				UpdateFullPathControlPoints();
			}
			else
			{
				m_usingFullPath = false;
				num = m_blockedPathPolys;
				UpdateBlockedPathControlPoints(actor);
			}
			if (actor != m_targetActor)
			{
				if (m_beamTargetSpellInstance != null)
				{
					m_beamTargetSpellInstance.ActivateState(SpellStateType.DEATH);
				}
				if (m_usingFullPath)
				{
					if (!string.IsNullOrEmpty(m_beamFadeInMaterialVar))
					{
						iTween.StopByName(base.gameObject, "fadeBeam");
						UpdateBeamFade(0f);
						Hashtable args = iTween.Hash("from", 0f, "to", 1f, "time", m_beamFadeInTime, "easetype", iTween.EaseType.linear, "onupdate", "UpdateBeamFade", "onupdatetarget", base.gameObject, "name", "fadeBeam");
						iTween.ValueTo(base.gameObject, args);
					}
					if (m_fullPathParticles != null)
					{
						m_fullPathParticles.Play();
					}
					if (m_blockedPathParticles != null)
					{
						m_blockedPathParticles.Stop();
						m_blockedPathParticles.Clear();
					}
				}
				else
				{
					if (m_fullPathParticles != null)
					{
						m_fullPathParticles.Stop();
						m_fullPathParticles.Clear();
					}
					if (m_blockedPathParticles != null)
					{
						m_blockedPathParticles.Play();
					}
				}
				m_targetActor = actor;
				if (m_targetActor != null)
				{
					Spell spell = ((m_targetActor.GetEntity().GetCardType() == TAG_CARDTYPE.HERO) ? m_beamTargetHeroSpell : m_beamTargetMinionSpell);
					if (spell != null)
					{
						m_beamTargetSpellInstance = Object.Instantiate(spell);
						m_beamTargetSpellInstance.AddStateFinishedCallback(OnSpellStateFinished);
						m_beamTargetSpellInstance.transform.parent = m_targetActor.transform;
						TransformUtil.Identity(m_beamTargetSpellInstance);
						m_beamTargetSpellInstance.Activate();
					}
					else
					{
						m_beamTargetSpellInstance = null;
					}
				}
			}
			m_lineRenderer.positionCount = num;
			for (int i = 0; i < num; i++)
			{
				float position = (float)i / (float)num;
				m_lineRenderer.SetPosition(i, m_uberCurve.CatmullRomEvaluateWorldPosition(position));
			}
			VisualizeControlPoints();
			yield return null;
		}
	}

	private void UpdateBeamFade(float fadeValue)
	{
		Color color = m_beamMaterial.GetColor(m_beamFadeInPropertyID);
		color.a = fadeValue;
		m_beamMaterial.SetColor(m_beamFadeInPropertyID, color);
	}

	private void UpdateBlockedPathControlPoints(Actor minionToRight)
	{
		int num = m_sourceCardOffsets.Count + m_destCardOffsets.Count;
		if (m_uberCurve.m_controlPoints.Count != num)
		{
			m_uberCurve.m_controlPoints.Clear();
			for (int i = 0; i < num; i++)
			{
				m_uberCurve.m_controlPoints.Add(new UberCurve.UberCurveControlPoint());
			}
		}
		int num2 = 0;
		Card sourceCard = GetSourceCard();
		int num3 = 0;
		while (num3 < m_sourceCardOffsets.Count)
		{
			m_uberCurve.m_controlPoints[num2].position = sourceCard.transform.position + m_sourceCardOffsets[num3];
			num3++;
			num2++;
		}
		int num4 = 0;
		while (num4 < m_destCardOffsets.Count)
		{
			m_uberCurve.m_controlPoints[num2].position = minionToRight.transform.position + m_destCardOffsets[num4];
			num4++;
			num2++;
		}
	}

	private void UpdateFullPathControlPoints()
	{
		int num = m_sourceCardOffsets.Count + m_fullPathPoints.Count;
		if (m_uberCurve.m_controlPoints.Count != num)
		{
			m_uberCurve.m_controlPoints.Clear();
			for (int i = 0; i < num; i++)
			{
				m_uberCurve.m_controlPoints.Add(new UberCurve.UberCurveControlPoint());
			}
		}
		int num2 = 0;
		Card sourceCard = GetSourceCard();
		int num3 = 0;
		while (num3 < m_sourceCardOffsets.Count)
		{
			m_uberCurve.m_controlPoints[num2].position = sourceCard.transform.position + m_sourceCardOffsets[num3];
			num3++;
			num2++;
		}
		int num4 = 0;
		while (num4 < m_fullPathPoints.Count)
		{
			m_uberCurve.m_controlPoints[num2].position = m_fullPathPoints[num4];
			num4++;
			num2++;
		}
	}

	private Actor GetTargetMinion()
	{
		int zonePosition = GetSourceCard().GetZonePosition();
		ZonePlay battlefieldZone = GetSourceCard().GetController().GetBattlefieldZone();
		for (int i = (m_targetMinionToRight ? (zonePosition + 1) : (zonePosition - 1)); i > 0 && i <= battlefieldZone.GetCardCount(); i += (m_targetMinionToRight ? 1 : (-1)))
		{
			Card cardAtPos = battlefieldZone.GetCardAtPos(i);
			if (cardAtPos.IsActorReady())
			{
				return cardAtPos.GetActor();
			}
		}
		return null;
	}

	private void VisualizeControlPoints()
	{
		if (!m_visualizeControlPoints)
		{
			foreach (GameObject visualizer in m_visualizers)
			{
				Object.Destroy(visualizer);
			}
			m_visualizers.Clear();
		}
		else if (m_visualizers.Count != m_uberCurve.m_controlPoints.Count)
		{
			foreach (GameObject visualizer2 in m_visualizers)
			{
				Object.Destroy(visualizer2);
			}
			m_visualizers.Clear();
			for (int i = 0; i < m_uberCurve.m_controlPoints.Count; i++)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				gameObject.transform.position = m_uberCurve.m_controlPoints[i].position;
				gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				m_visualizers.Add(gameObject);
			}
		}
		else
		{
			for (int j = 0; j < m_uberCurve.m_controlPoints.Count; j++)
			{
				m_visualizers[j].transform.position = m_uberCurve.transform.TransformPoint(m_uberCurve.m_controlPoints[j].position);
			}
		}
	}

	private void OnSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell.gameObject);
		}
	}
}
