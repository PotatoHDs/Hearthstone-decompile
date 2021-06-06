using System.Text;

namespace bgs
{
	public class Compute32
	{
		public const uint FNV_32_PRIME = 16777619u;

		public const uint COMPUTE_32_OFFSET = 2166136261u;

		public static uint Hash(string str)
		{
			uint num = 2166136261u;
			byte[] bytes = Encoding.ASCII.GetBytes(str);
			foreach (byte b in bytes)
			{
				num ^= b;
				num *= 16777619;
			}
			return num;
		}
	}
}
