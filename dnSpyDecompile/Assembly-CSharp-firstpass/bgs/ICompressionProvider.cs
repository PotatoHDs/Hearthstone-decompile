using System;
using System.IO;

namespace bgs
{
	// Token: 0x02000222 RID: 546
	public interface ICompressionProvider
	{
		// Token: 0x06002301 RID: 8961
		Stream GetDeflateStream(Stream baseOutputStream);

		// Token: 0x06002302 RID: 8962
		Stream GetInflateStream(Stream baseInputStream);
	}
}
