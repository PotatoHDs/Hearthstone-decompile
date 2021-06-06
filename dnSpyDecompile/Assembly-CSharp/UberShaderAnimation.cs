using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AC2 RID: 2754
[Serializable]
public class UberShaderAnimation : ScriptableObject
{
	// Token: 0x04007B41 RID: 31553
	[SerializeField]
	public List<UberShaderAnimation.UberAnimation> animations = new List<UberShaderAnimation.UberAnimation>();

	// Token: 0x04007B42 RID: 31554
	public List<int> materialPropertyIDs = new List<int>();

	// Token: 0x04007B43 RID: 31555
	public int version = 1;

	// Token: 0x020026F1 RID: 9969
	public enum PropertyType
	{
		// Token: 0x0400F2B0 RID: 62128
		Color,
		// Token: 0x0400F2B1 RID: 62129
		Vector,
		// Token: 0x0400F2B2 RID: 62130
		Float
	}

	// Token: 0x020026F2 RID: 9970
	[Serializable]
	public class UberAnimation
	{
		// Token: 0x0400F2B3 RID: 62131
		public string materialPropertyName;

		// Token: 0x0400F2B4 RID: 62132
		public UberShaderAnimation.PropertyType propertyType;

		// Token: 0x0400F2B5 RID: 62133
		public int materialIndex;

		// Token: 0x0400F2B6 RID: 62134
		public List<UberShaderAnimation.UberAnimationElement> animationElement = new List<UberShaderAnimation.UberAnimationElement>();
	}

	// Token: 0x020026F3 RID: 9971
	[Serializable]
	public class UberAnimationElement
	{
		// Token: 0x0400F2B7 RID: 62135
		public int element;

		// Token: 0x0400F2B8 RID: 62136
		public bool incrementingValue;

		// Token: 0x0400F2B9 RID: 62137
		public int incrementingElement;

		// Token: 0x0400F2BA RID: 62138
		public float incrementingLastValue;

		// Token: 0x0400F2BB RID: 62139
		public float incrementingSpeed;

		// Token: 0x0400F2BC RID: 62140
		public UberShaderAnimation.UberAnimationCurve animationCurve;

		// Token: 0x0400F2BD RID: 62141
		public UberShaderAnimation.UberAnimationRandom randomAnimation;

		// Token: 0x0400F2BE RID: 62142
		public UberShaderAnimation.UberAnimationColor colorAnimation;
	}

	// Token: 0x020026F4 RID: 9972
	[Serializable]
	public class UberAnimationCurve
	{
		// Token: 0x0400F2BF RID: 62143
		public bool enabled;

		// Token: 0x0400F2C0 RID: 62144
		public AnimationCurve animationCurve;

		// Token: 0x0400F2C1 RID: 62145
		public float speed = 1f;

		// Token: 0x0400F2C2 RID: 62146
		public float scale = 1f;

		// Token: 0x0400F2C3 RID: 62147
		public float offset;
	}

	// Token: 0x020026F5 RID: 9973
	[Serializable]
	public class UberAnimationRandom
	{
		// Token: 0x0400F2C4 RID: 62148
		public bool enabled;

		// Token: 0x0400F2C5 RID: 62149
		public AnimationCurve intensityCurve;

		// Token: 0x0400F2C6 RID: 62150
		public float intensitySpeed = 1f;

		// Token: 0x0400F2C7 RID: 62151
		public float seed;

		// Token: 0x0400F2C8 RID: 62152
		public float minValue = -1f;

		// Token: 0x0400F2C9 RID: 62153
		public float maxValue = 1f;

		// Token: 0x0400F2CA RID: 62154
		public float speed = 1f;

		// Token: 0x0400F2CB RID: 62155
		public float scale = 1f;
	}

	// Token: 0x020026F6 RID: 9974
	[Serializable]
	public class UberAnimationColor
	{
		// Token: 0x0400F2CC RID: 62156
		public bool enabled;

		// Token: 0x0400F2CD RID: 62157
		public Color color = Color.white;

		// Token: 0x0400F2CE RID: 62158
		public Gradient gradient = new Gradient();
	}
}
