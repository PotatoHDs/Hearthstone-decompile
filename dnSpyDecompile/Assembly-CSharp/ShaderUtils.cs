using System;
using UnityEngine;

// Token: 0x02000A90 RID: 2704
public class ShaderUtils
{
	// Token: 0x060090A1 RID: 37025 RVA: 0x002EEA4A File Offset: 0x002ECC4A
	public static Shader FindShader(string name)
	{
		return ShaderPreCompiler.GetShader(name);
	}
}
