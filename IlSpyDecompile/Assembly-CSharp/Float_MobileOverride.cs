using System;

[Serializable]
public class Float_MobileOverride : MobileOverrideValue<float>
{
	public Float_MobileOverride()
	{
	}

	public Float_MobileOverride(float defaultValue)
		: base(defaultValue)
	{
	}
}
