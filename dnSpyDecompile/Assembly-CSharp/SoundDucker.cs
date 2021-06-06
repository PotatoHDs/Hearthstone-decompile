using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x02000954 RID: 2388
public class SoundDucker : MonoBehaviour
{
	// Token: 0x06008332 RID: 33586 RVA: 0x002A85B4 File Offset: 0x002A67B4
	private void Awake()
	{
		this.InitDuckedCategoryDefs();
	}

	// Token: 0x06008333 RID: 33587 RVA: 0x002A85BC File Offset: 0x002A67BC
	private void OnDestroy()
	{
		this.StopDucking();
	}

	// Token: 0x06008334 RID: 33588 RVA: 0x002A85C4 File Offset: 0x002A67C4
	public override string ToString()
	{
		return string.Format("[SoundDucker: {0}]", base.name);
	}

	// Token: 0x06008335 RID: 33589 RVA: 0x002A85D6 File Offset: 0x002A67D6
	public List<SoundDuckedCategoryDef> GetDuckedCategoryDefs()
	{
		return this.m_DuckedCategoryDefs;
	}

	// Token: 0x06008336 RID: 33590 RVA: 0x002A85DE File Offset: 0x002A67DE
	public void SetDuckedCategoryDefs(List<SoundDuckedCategoryDef> duckedCategoryDef)
	{
		this.m_DuckedCategoryDefs = duckedCategoryDef;
		this.InitDuckedCategoryDefs();
	}

	// Token: 0x06008337 RID: 33591 RVA: 0x002A85ED File Offset: 0x002A67ED
	public bool IsDucking()
	{
		return this.m_ducking;
	}

	// Token: 0x06008338 RID: 33592 RVA: 0x002A85F5 File Offset: 0x002A67F5
	public void StartDucking()
	{
		if (SoundManager.Get() == null)
		{
			return;
		}
		if (this.m_ducking)
		{
			return;
		}
		this.InitDuckedCategoryDefs();
		this.m_ducking = SoundManager.Get().StartDucking(this);
	}

	// Token: 0x06008339 RID: 33593 RVA: 0x002A861F File Offset: 0x002A681F
	public void StopDucking()
	{
		if (SoundManager.Get() == null)
		{
			return;
		}
		if (!this.m_ducking)
		{
			return;
		}
		this.m_ducking = false;
		SoundManager.Get().StopDucking(this);
	}

	// Token: 0x0600833A RID: 33594 RVA: 0x002A8644 File Offset: 0x002A6844
	private void InitDuckedCategoryDefs()
	{
		if (!this.m_DuckAllCategories)
		{
			return;
		}
		if (this.m_GlobalDuckDef == null)
		{
			return;
		}
		this.m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
		foreach (object obj in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			Global.SoundCategory soundCategory = (Global.SoundCategory)obj;
			if (soundCategory != Global.SoundCategory.NONE)
			{
				SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
				SoundUtils.CopyDuckedCategoryDef(this.m_GlobalDuckDef, soundDuckedCategoryDef);
				soundDuckedCategoryDef.m_Category = soundCategory;
				this.m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
			}
		}
	}

	// Token: 0x04006E75 RID: 28277
	public bool m_DuckAllCategories = true;

	// Token: 0x04006E76 RID: 28278
	public SoundDuckedCategoryDef m_GlobalDuckDef;

	// Token: 0x04006E77 RID: 28279
	public List<SoundDuckedCategoryDef> m_DuckedCategoryDefs;

	// Token: 0x04006E78 RID: 28280
	private bool m_ducking;
}
