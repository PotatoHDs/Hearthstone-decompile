using System.IO;

namespace bgs
{
	public interface ICompressionProvider
	{
		Stream GetDeflateStream(Stream baseOutputStream);

		Stream GetInflateStream(Stream baseInputStream);
	}
}
