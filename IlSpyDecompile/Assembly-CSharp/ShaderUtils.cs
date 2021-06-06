using UnityEngine;

public class ShaderUtils
{
	public static Shader FindShader(string name)
	{
		return ShaderPreCompiler.GetShader(name);
	}
}
