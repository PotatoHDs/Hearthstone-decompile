using UnityEngine;

public class ModularBundleText : MonoBehaviour
{
	public enum GlowSize
	{
		NONE,
		LARGE,
		MEDIUM,
		SMALL
	}

	public GameObject LargeGlow;

	public GameObject MediumGlow;

	public GameObject SmallGlow;

	public UberText Text;

	public void SetGlowSize(GlowSize activeGlowSize)
	{
		SetGlowActive(LargeGlow, active: false);
		SetGlowActive(MediumGlow, active: false);
		SetGlowActive(SmallGlow, active: false);
		switch (activeGlowSize)
		{
		case GlowSize.NONE:
			break;
		case GlowSize.LARGE:
			SetGlowActive(LargeGlow, active: true);
			break;
		case GlowSize.MEDIUM:
			SetGlowActive(MediumGlow, active: true);
			break;
		case GlowSize.SMALL:
			SetGlowActive(SmallGlow, active: true);
			break;
		}
	}

	public void SetGlowSize(string glowSizeString)
	{
		GlowSize glowSize = EnumUtils.SafeParse(glowSizeString, GlowSize.NONE, ignoreCase: true);
		SetGlowSize(glowSize);
	}

	private void SetGlowActive(GameObject glow, bool active)
	{
		if (glow != null)
		{
			glow.SetActive(active);
		}
		else if (active)
		{
			Debug.LogWarning($"Unable to activate glow for Text={base.name} in Node={base.transform.parent.name}");
		}
	}
}
