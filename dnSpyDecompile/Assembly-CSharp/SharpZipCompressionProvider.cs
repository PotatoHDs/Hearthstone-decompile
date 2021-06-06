using System;
using System.IO;
using bgs;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

// Token: 0x02000608 RID: 1544
public class SharpZipCompressionProvider : ICompressionProvider
{
	// Token: 0x06005687 RID: 22151 RVA: 0x001C5E36 File Offset: 0x001C4036
	public Stream GetDeflateStream(Stream baseOutputStream)
	{
		return new DeflaterOutputStream(baseOutputStream, new Deflater());
	}

	// Token: 0x06005688 RID: 22152 RVA: 0x001C5E43 File Offset: 0x001C4043
	public Stream GetInflateStream(Stream baseInputStream)
	{
		return new InflaterInputStream(baseInputStream, new Inflater(false));
	}
}
