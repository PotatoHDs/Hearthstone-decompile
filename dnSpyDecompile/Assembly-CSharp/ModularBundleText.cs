using System;
using UnityEngine;

// Token: 0x02000713 RID: 1811
public class ModularBundleText : MonoBehaviour
{
	// Token: 0x0600651D RID: 25885 RVA: 0x0020FBA8 File Offset: 0x0020DDA8
	public void SetGlowSize(ModularBundleText.GlowSize activeGlowSize)
	{
		this.SetGlowActive(this.LargeGlow, false);
		this.SetGlowActive(this.MediumGlow, false);
		this.SetGlowActive(this.SmallGlow, false);
		switch (activeGlowSize)
		{
		case ModularBundleText.GlowSize.NONE:
			return;
		case ModularBundleText.GlowSize.LARGE:
			this.SetGlowActive(this.LargeGlow, true);
			return;
		case ModularBundleText.GlowSize.MEDIUM:
			this.SetGlowActive(this.MediumGlow, true);
			return;
		case ModularBundleText.GlowSize.SMALL:
			this.SetGlowActive(this.SmallGlow, true);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600651E RID: 25886 RVA: 0x0020FC20 File Offset: 0x0020DE20
	public void SetGlowSize(string glowSizeString)
	{
		ModularBundleText.GlowSize glowSize = EnumUtils.SafeParse<ModularBundleText.GlowSize>(glowSizeString, ModularBundleText.GlowSize.NONE, true);
		this.SetGlowSize(glowSize);
	}

	// Token: 0x0600651F RID: 25887 RVA: 0x0020FC3D File Offset: 0x0020DE3D
	private void SetGlowActive(GameObject glow, bool active)
	{
		if (glow != null)
		{
			glow.SetActive(active);
			return;
		}
		if (active)
		{
			Debug.LogWarning(string.Format("Unable to activate glow for Text={0} in Node={1}", base.name, base.transform.parent.name));
		}
	}

	// Token: 0x040053E3 RID: 21475
	public GameObject LargeGlow;

	// Token: 0x040053E4 RID: 21476
	public GameObject MediumGlow;

	// Token: 0x040053E5 RID: 21477
	public GameObject SmallGlow;

	// Token: 0x040053E6 RID: 21478
	public UberText Text;

	// Token: 0x020022A3 RID: 8867
	public enum GlowSize
	{
		// Token: 0x0400E438 RID: 58424
		NONE,
		// Token: 0x0400E439 RID: 58425
		LARGE,
		// Token: 0x0400E43A RID: 58426
		MEDIUM,
		// Token: 0x0400E43B RID: 58427
		SMALL
	}
}
