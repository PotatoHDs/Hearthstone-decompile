using System;

namespace bgs
{
	// Token: 0x02000255 RID: 597
	public interface IFileUtil
	{
		// Token: 0x060024D9 RID: 9433
		bool LoadFromDrive(string filePath, out byte[] data);

		// Token: 0x060024DA RID: 9434
		bool RemoveFromDrive(string filePath);

		// Token: 0x060024DB RID: 9435
		bool StoreToDrive(byte[] data, string filePath, bool overwrite, bool compress);
	}
}
