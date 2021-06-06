using System;
using System.Linq;

// Token: 0x020009A0 RID: 2464
public static class BoolExtension
{
	// Token: 0x06008684 RID: 34436 RVA: 0x002B7184 File Offset: 0x002B5384
	public static bool AllSame(this bool firstValue, params bool[] bools)
	{
		return !bools.Any((bool b) => b != firstValue);
	}
}
