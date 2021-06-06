using System.Text;
using bnet.protocol;

namespace bgs
{
	public class ContentHandle
	{
		public string Region { get; set; }

		public string Usage { get; set; }

		public string Sha256Digest { get; set; }

		public static ContentHandle FromProtocol(bnet.protocol.ContentHandle contentHandle)
		{
			if (contentHandle == null || !contentHandle.IsInitialized)
			{
				return null;
			}
			return new ContentHandle
			{
				Region = new FourCC(contentHandle.Region).ToString(),
				Usage = new FourCC(contentHandle.Usage).ToString(),
				Sha256Digest = ByteArrayToString(contentHandle.Hash)
			};
		}

		public override string ToString()
		{
			return $"Region={Region} Usage={Usage} Sha256={Sha256Digest}";
		}

		public static string ByteArrayToString(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}
	}
}
