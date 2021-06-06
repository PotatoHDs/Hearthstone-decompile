using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
[CustomEditClass]
public class GVGLaserGun : MonoBehaviour
{
	// Token: 0x06000AC7 RID: 2759 RVA: 0x0003FE58 File Offset: 0x0003E058
	private void Awake()
	{
		if (this.m_AngleDefs.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.m_AngleDefs.Count; i++)
		{
			this.m_sortedAngleDefIndexes.Add(i);
		}
		this.m_sortedAngleDefIndexes.Sort(delegate(int index1, int index2)
		{
			GVGLaserGun.AngleDef def = this.m_AngleDefs[index1];
			GVGLaserGun.AngleDef def2 = this.m_AngleDefs[index2];
			return GVGLaserGun.AngleDefSortComparison(def, def2);
		});
		this.m_angleIndex = 0;
		this.m_minAngleIndex = 0;
		this.m_maxAngleIndex = 0;
		float angle = this.m_AngleDefs[0].m_Angle;
		float num = angle;
		for (int j = 0; j < this.m_sortedAngleDefIndexes.Count; j++)
		{
			GVGLaserGun.AngleDef angleDef = this.m_AngleDefs[this.m_sortedAngleDefIndexes[j]];
			if (angleDef.m_Angle < angle)
			{
				angle = angleDef.m_Angle;
				this.m_minAngleIndex = j;
			}
			if (angleDef.m_Angle > num)
			{
				num = angleDef.m_Angle;
				this.m_maxAngleIndex = j;
			}
			if (angleDef.m_Default)
			{
				this.m_angleIndex = j;
				this.SetAngle(angleDef.m_Angle);
			}
		}
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0003FF53 File Offset: 0x0003E153
	private void Update()
	{
		this.HandleRotation();
		this.HandleLever();
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0003FF64 File Offset: 0x0003E164
	private GVGLaserGun.AngleDef GetAngleDef()
	{
		if (this.m_angleIndex < 0)
		{
			return null;
		}
		if (this.m_angleIndex >= this.m_sortedAngleDefIndexes.Count)
		{
			return null;
		}
		int index = this.m_sortedAngleDefIndexes[this.m_angleIndex];
		return this.m_AngleDefs[index];
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0003FFAF File Offset: 0x0003E1AF
	private static int AngleDefSortComparison(GVGLaserGun.AngleDef def1, GVGLaserGun.AngleDef def2)
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

	// Token: 0x06000ACB RID: 2763 RVA: 0x0003FFD4 File Offset: 0x0003E1D4
	private void HandleRotation()
	{
		if (this.m_leverEffectsActive)
		{
			return;
		}
		this.m_requestedRotationDir = 0;
		if (UniversalInputManager.Get().GetMouseButton(0))
		{
			if (this.IsOver(this.m_RotateLeftButton))
			{
				this.m_requestedRotationDir = -1;
			}
			else if (this.IsOver(this.m_RotateRightButton))
			{
				this.m_requestedRotationDir = 1;
			}
		}
		if (this.ShouldStartRotating())
		{
			this.StartRotating(this.m_requestedRotationDir);
		}
		if (this.m_rotationDir < 0)
		{
			this.RotateLeft();
			return;
		}
		if (this.m_rotationDir > 0)
		{
			this.RotateRight();
		}
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00040060 File Offset: 0x0003E260
	private bool ShouldStartRotating()
	{
		return this.m_requestedRotationDir != 0 && this.m_requestedRotationDir != this.m_rotationDir && (this.m_requestedRotationDir >= 0 || this.m_angleIndex != this.m_minAngleIndex) && (this.m_requestedRotationDir <= 0 || this.m_angleIndex != this.m_maxAngleIndex);
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x000400BC File Offset: 0x0003E2BC
	private void RotateLeft()
	{
		GVGLaserGun.AngleDef angleDef = this.GetAngleDef();
		float num = Mathf.MoveTowards(this.m_angle, angleDef.m_Angle, this.m_RotationSpeed * Time.deltaTime);
		if (num > angleDef.m_Angle)
		{
			this.SetAngle(num);
			return;
		}
		if (this.m_requestedRotationDir == 0 || this.m_angleIndex == this.m_minAngleIndex)
		{
			this.SetAngle(num);
			this.StopRotating();
			return;
		}
		this.m_angleIndex--;
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x00040134 File Offset: 0x0003E334
	private void RotateRight()
	{
		GVGLaserGun.AngleDef angleDef = this.GetAngleDef();
		float num = Mathf.MoveTowards(this.m_angle, angleDef.m_Angle, this.m_RotationSpeed * Time.deltaTime);
		if (num < angleDef.m_Angle)
		{
			this.SetAngle(num);
			return;
		}
		if (this.m_requestedRotationDir == 0 || this.m_angleIndex == this.m_maxAngleIndex)
		{
			this.SetAngle(num);
			this.StopRotating();
			return;
		}
		this.m_angleIndex++;
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x000401AC File Offset: 0x0003E3AC
	private void StartRotating(int dir)
	{
		this.m_rotationDir = dir;
		if (dir < 0)
		{
			this.m_angleIndex--;
		}
		else
		{
			this.m_angleIndex++;
		}
		if (this.m_StartRotationSpell)
		{
			this.m_StartRotationSpell.Activate();
		}
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x000401FA File Offset: 0x0003E3FA
	private void StopRotating()
	{
		this.m_rotationDir = 0;
		if (this.m_StopRotationSpell)
		{
			this.m_StopRotationSpell.Activate();
		}
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0004021B File Offset: 0x0003E41B
	private void SetAngle(float angle)
	{
		this.m_angle = angle;
		TransformUtil.SetLocalEulerAngleY(this.m_GunRotator, this.m_angle);
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x00040235 File Offset: 0x0003E435
	private void HandleLever()
	{
		if (this.m_rotationDir != 0)
		{
			return;
		}
		if (this.m_leverEffectsActive)
		{
			return;
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_GunLever))
		{
			this.PullLever();
		}
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0004026A File Offset: 0x0003E46A
	private void PullLever()
	{
		if (!this.m_PullLeverSpell)
		{
			return;
		}
		this.m_leverEffectsActive = true;
		this.m_PullLeverSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnPullLeverSpellFinished));
		this.m_PullLeverSpell.Activate();
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x000402A4 File Offset: 0x0003E4A4
	private void OnPullLeverSpellFinished(Spell spell, object userData)
	{
		Spell impactSpell = this.GetAngleDef().m_ImpactSpell;
		if (!impactSpell)
		{
			this.m_leverEffectsActive = false;
			return;
		}
		impactSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnImpactSpellFinished));
		impactSpell.Activate();
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x000402E5 File Offset: 0x0003E4E5
	private void OnImpactSpellFinished(Spell spell, object userData)
	{
		this.m_leverEffectsActive = false;
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x040006D8 RID: 1752
	[CustomEditField(Sections = "Lever")]
	public GameObject m_GunLever;

	// Token: 0x040006D9 RID: 1753
	[CustomEditField(Sections = "Lever")]
	public Spell m_PullLeverSpell;

	// Token: 0x040006DA RID: 1754
	[CustomEditField(Sections = "Rotation")]
	[Tooltip("The thing that will be rotated.")]
	public GameObject m_GunRotator;

	// Token: 0x040006DB RID: 1755
	[CustomEditField(Sections = "Rotation")]
	public GameObject m_RotateLeftButton;

	// Token: 0x040006DC RID: 1756
	[CustomEditField(Sections = "Rotation")]
	public GameObject m_RotateRightButton;

	// Token: 0x040006DD RID: 1757
	[CustomEditField(Sections = "Rotation")]
	public Spell m_StartRotationSpell;

	// Token: 0x040006DE RID: 1758
	[CustomEditField(Sections = "Rotation")]
	public Spell m_StopRotationSpell;

	// Token: 0x040006DF RID: 1759
	[CustomEditField(Sections = "Rotation")]
	[Tooltip("How fast the gun rotates in degrees per second.")]
	public float m_RotationSpeed;

	// Token: 0x040006E0 RID: 1760
	[CustomEditField(Sections = "Rotation", ListTable = true)]
	public List<GVGLaserGun.AngleDef> m_AngleDefs = new List<GVGLaserGun.AngleDef>();

	// Token: 0x040006E1 RID: 1761
	[CustomEditField(Sections = "Debug")]
	public bool m_DebugShowGunAngle;

	// Token: 0x040006E2 RID: 1762
	private List<int> m_sortedAngleDefIndexes = new List<int>();

	// Token: 0x040006E3 RID: 1763
	private int m_rotationDir;

	// Token: 0x040006E4 RID: 1764
	private int m_requestedRotationDir;

	// Token: 0x040006E5 RID: 1765
	private float m_angle;

	// Token: 0x040006E6 RID: 1766
	private int m_angleIndex = -1;

	// Token: 0x040006E7 RID: 1767
	private int m_minAngleIndex = -1;

	// Token: 0x040006E8 RID: 1768
	private int m_maxAngleIndex = -1;

	// Token: 0x040006E9 RID: 1769
	private bool m_leverEffectsActive;

	// Token: 0x020013B5 RID: 5045
	[Serializable]
	public class AngleDef
	{
		// Token: 0x0600D83D RID: 55357 RVA: 0x003EDC1F File Offset: 0x003EBE1F
		public int CustomBehaviorListCompare(GVGLaserGun.AngleDef def1, GVGLaserGun.AngleDef def2)
		{
			return GVGLaserGun.AngleDefSortComparison(def1, def2);
		}

		// Token: 0x0400A790 RID: 42896
		public bool m_Default;

		// Token: 0x0400A791 RID: 42897
		[CustomEditField(ListSortable = true)]
		public float m_Angle;

		// Token: 0x0400A792 RID: 42898
		public Spell m_ImpactSpell;
	}
}
