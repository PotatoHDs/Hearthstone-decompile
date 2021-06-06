using UnityEngine;

public class DOPAsset : ScriptableObject
{
	public int DataVersion;

	public static DOPAsset GenerateDOPAsset()
	{
		DOPAsset dOPAsset = ScriptableObject.CreateInstance<DOPAsset>();
		dOPAsset.DataVersion = 20400;
		return dOPAsset;
	}
}
