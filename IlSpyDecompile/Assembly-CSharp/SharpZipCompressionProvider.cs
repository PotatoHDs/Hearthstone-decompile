using System.IO;
using bgs;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

public class SharpZipCompressionProvider : ICompressionProvider
{
	public Stream GetDeflateStream(Stream baseOutputStream)
	{
		return new DeflaterOutputStream(baseOutputStream, new Deflater());
	}

	public Stream GetInflateStream(Stream baseInputStream)
	{
		return new InflaterInputStream(baseInputStream, new Inflater(noHeader: false));
	}
}
