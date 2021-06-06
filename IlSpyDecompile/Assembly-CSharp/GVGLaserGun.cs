using System;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class GVGLaserGun : MonoBehaviour
{
	[Serializable]
	public class AngleDef
	{
		public bool m_Default;

		[CustomEditField(ListSortable = true)]
		public float m_Angle;

		public Spell m_ImpactSpell;

		public int CustomBehaviorListCompare(AngleDef def1, AngleDef def2)
		{
			return AngleDefSortComparison(def1, def2);
		}
	}

	[CustomEditField(Sections = "Lever")]
	public GameObject m_GunLever;

	[CustomEditField(Sections = "Lever")]
	public Spell m_PullLeverSpell;

	[CustomEditField(Sections = "Rotation")]
	[Tooltip("The thing that will be rotated.")]
	public GameObject m_GunRotator;

	[CustomEditField(Sections = "Rotation")]
	public GameObject m_RotateLeftButton;

	[CustomEditField(Sections = "Rotation")]
	public GameObject m_RotateRightButton;

	[CustomEditField(Sections = "Rotation")]
	public Spell m_StartRotationSpell;

	[CustomEditField(Sections = "Rotation")]
	public Spell m_StopRotationSpell;

	[CustomEditField(Sections = "Rotation")]
	[Tooltip("How fast the gun rotates in degrees per second.")]
	public float m_RotationSpeed;

	[CustomEditField(Sections = "Rotation", ListTable = true)]
	public List<AngleDef> m_AngleDefs = new List<AngleDef>();

	[CustomEditField(Sections = "Debug")]
	public bool m_DebugShowGunAngle;

	private List<int> m_sortedAngleDefIndexes = new List<int>();

	private int m_rotationDir;

	private int m_requestedRotationDir;

	private float m_angle;

	private int m_angleIndex = -1;

	private int m_minAngleIndex = -1;

	private int m_maxAngleIndex = -1;

	private bool m_leverEffectsActive;

	private void Awake()
	{
		if (m_AngleDefs.Count == 0)
		{
			return;
		}
		for (int i = 0; i < m_AngleDefs.Count; i++)
		{
			m_sortedAngleDefIndexes.Add(i);
		}
		m_sortedAngleDefIndexes.Sort(delegate(int index1, int index2)
		{
			AngleDef def = m_AngleDefs[index1];
			AngleDef def2 = m_AngleDefs[index2];
			return AngleDefSortComparison(def, def2);
		});
		m_angleIndex = 0;
		m_minAngleIndex = 0;
		m_maxAngleIndex = 0;
		float angle = m_AngleDefs[0].m_Angle;
		float num = angle;
		for (int j = 0; j < m_sortedAngleDefIndexes.Count; j++)
		{
			AngleDef angleDef = m_AngleDefs[m_sortedAngleDefIndexes[j]];
			if (angleDef.m_Angle < angle)
			{
				angle = angleDef.m_Angle;
				m_minAngleIndex = j;
			}
			if (angleDef.m_Angle > num)
			{
				num = angleDef.m_Angle;
				m_maxAngleIndex = j;
			}
			if (angleDef.m_Default)
			{
				m_angleIndex = j;
				SetAngle(angleDef.m_Angle);
			}
		}
	}

	private void Update()
	{
		HandleRotation();
		HandleLever();
	}

	private AngleDef GetAngleDef()
	{
		if (m_angleIndex < 0)
		{
			return null;
		}
		if (m_angleIndex >= m_sortedAngleDefIndexes.Count)
		{
			return null;
		}
		int index = m_sortedAngleDefIndexes[m_angleIndex];
		return m_AngleDefs[index];
	}

	private static int AngleDefSortComparison(AngleDef def1, AngleDef def2)
	{
		if (def1.m_Angle < def2.m_Angle)
		{
			return -1;
		}
		if (def1.m_Angle > def2.m_Angle)
		{
			return 1;
		}
		return 0;
	}

	private void HandleRotation()
	{
		if (m_leverEffectsActive)
		{
			return;
		}
		m_requestedRotationDir = 0;
		if (UniversalInputManager.Get().GetMouseButton(0))
		{
			if (IsOver(m_RotateLeftButton))
			{
				m_requestedRotationDir = -1;
			}
			else if (IsOver(m_RotateRightButton))
			{
				m_requestedRotationDir = 1;
			}
		}
		if (ShouldStartRotating())
		{
			StartRotating(m_requestedRotationDir);
		}
		if (m_rotationDir < 0)
		{
			RotateLeft();
		}
		else if (m_rotationDir > 0)
		{
			RotateRight();
		}
	}

	private bool ShouldStartRotating()
	{
		if (m_requestedRotationDir == 0)
		{
			return false;
		}
		if (m_requestedRotationDir == m_rotationDir)
		{
			return false;
		}
		if (m_requestedRotationDir < 0 && m_angleIndex == m_minAngleIndex)
		{
			return false;
		}
		if (m_requestedRotationDir > 0 && m_angleIndex == m_maxAngleIndex)
		{
			return false;
		}
		return true;
	}

	private void RotateLeft()
	{
		AngleDef angleDef = GetAngleDef();
		float num = Mathf.MoveTowards(m_angle, angleDef.m_Angle, m_RotationSpeed * Time.deltaTime);
		if (num <= angleDef.m_Angle)
		{
			if (m_requestedRotationDir == 0 || m_angleIndex == m_minAngleIndex)
			{
				SetAngle(num);
				StopRotating();
			}
			else
			{
				m_angleIndex--;
			}
		}
		else
		{
			SetAngle(num);
		}
	}

	private void RotateRight()
	{
		AngleDef angleDef = GetAngleDef();
		float num = Mathf.MoveTowards(m_angle, angleDef.m_Angle, m_RotationSpeed * Time.deltaTime);
		if (num >= angleDef.m_Angle)
		{
			if (m_requestedRotationDir == 0 || m_angleIndex == m_maxAngleIndex)
			{
				SetAngle(num);
				StopRotating();
			}
			else
			{
				m_angleIndex++;
			}
		}
		else
		{
			SetAngle(num);
		}
	}

	private void StartRotating(int dir)
	{
		m_rotationDir = dir;
		if (dir < 0)
		{
			m_angleIndex--;
		}
		else
		{
			m_angleIndex++;
		}
		if ((bool)m_StartRotationSpell)
		{
			m_StartRotationSpell.Activate();
		}
	}

	private void StopRotating()
	{
		m_rotationDir = 0;
		if ((bool)m_StopRotationSpell)
		{
			m_StopRotationSpell.Activate();
		}
	}

	private void SetAngle(float angle)
	{
		m_angle = angle;
		TransformUtil.SetLocalEulerAngleY(m_GunRotator, m_angle);
	}

	private void HandleLever()
	{
		if (m_rotationDir == 0 && !m_leverEffectsActive && UniversalInputManager.Get().GetMouseButtonUp(0) && IsOver(m_GunLever))
		{
			PullLever();
		}
	}

	private void PullLever()
	{
		if ((bool)m_PullLeverSpell)
		{
			m_leverEffectsActive = true;
			m_PullLeverSpell.AddFinishedCallback(OnPullLeverSpellFinished);
			m_PullLeverSpell.Activate();
		}
	}

	private void OnPullLeverSpellFinished(Spell spell, object userData)
	{
		Spell impactSpell = GetAngleDef().m_ImpactSpell;
		if (!impactSpell)
		{
			m_leverEffectsActive = false;
			return;
		}
		impactSpell.AddFinishedCallback(OnImpactSpellFinished);
		impactSpell.Activate();
	}

	private void OnImpactSpellFinished(Spell spell, object userData)
	{
		m_leverEffectsActive = false;
	}

	private bool IsOver(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		if (!InputUtil.IsPlayMakerMouseInputAllowed(go))
		{
			return false;
		}
		if (!UniversalInputManager.Get().InputIsOver(go))
		{
			return false;
		}
		return true;
	}
}
