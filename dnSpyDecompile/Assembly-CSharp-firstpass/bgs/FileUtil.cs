using System;
using System.IO;

namespace bgs
{
	// Token: 0x02000254 RID: 596
	public class FileUtil : IFileUtil
	{
		// Token: 0x060024D0 RID: 9424 RVA: 0x00082166 File Offset: 0x00080366
		public FileUtil(ICompressionProvider compressionProvider)
		{
			this.m_compressionProvider = compressionProvider;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00082178 File Offset: 0x00080378
		public bool StoreToDrive(byte[] data, string filePath, bool overwrite, bool compress)
		{
			if (data == null)
			{
				return false;
			}
			try
			{
				bool flag = File.Exists(filePath);
				if (flag && !overwrite)
				{
					return false;
				}
				byte[] array = data;
				if (compress)
				{
					array = this.Compress(array);
					if (array == null)
					{
						return false;
					}
				}
				if (flag && overwrite)
				{
					File.Delete(filePath);
				}
				if (!compress && data.Length == 0)
				{
					File.Create(filePath).Close();
					return true;
				}
				FileStream fileStream = File.Create(filePath, array.Length);
				fileStream.Write(array, 0, array.Length);
				fileStream.Flush();
				fileStream.Close();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0008220C File Offset: 0x0008040C
		public bool LoadFromDrive(string filePath, out byte[] data)
		{
			data = null;
			try
			{
				if (!File.Exists(filePath))
				{
					return false;
				}
				FileStream fileStream = File.OpenRead(filePath);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				if (this.IsCompressedStream(array))
				{
					byte[] array2 = this.Decompress(array);
					if (array2 == null)
					{
						return false;
					}
					data = array2;
				}
				else
				{
					data = array;
				}
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00082288 File Offset: 0x00080488
		public bool RemoveFromDrive(string filePath)
		{
			try
			{
				if (!File.Exists(filePath))
				{
					return false;
				}
				File.Delete(filePath);
			}
			catch (SystemException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000822C4 File Offset: 0x000804C4
		private byte[] Compress(byte[] data)
		{
			byte[] array;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(data.Length))
				{
					using (Stream deflateStream = this.m_compressionProvider.GetDeflateStream(memoryStream))
					{
						deflateStream.Write(data, 0, data.Length);
					}
					array = memoryStream.ToArray();
				}
			}
			catch (Exception)
			{
				return null;
			}
			byte[] bytes = BitConverter.GetBytes((ulong)((long)data.Length));
			byte[] array2 = new byte[array.Length + FileUtil.BNET_COMPRESSED_HEADER_SIZE];
			int num = 0;
			Array.Copy(FileUtil.BNET_COMPRESSED_MAGIC_BYTES, 0, array2, num, FileUtil.BNET_COMPRESSED_MAGIC_BYTES.Length);
			num += FileUtil.BNET_COMPRESSED_MAGIC_BYTES.Length;
			Array.Copy(FileUtil.BNET_COMPRESSED_VERSION_BYTES, 0, array2, num, FileUtil.BNET_COMPRESSED_VERSION_BYTES.Length);
			num += FileUtil.BNET_COMPRESSED_VERSION_BYTES.Length;
			Array.Copy(bytes, 0, array2, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(array, 0, array2, num, array.Length);
			return array2;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000823C4 File Offset: 0x000805C4
		private byte[] Decompress(byte[] data)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(data, FileUtil.BNET_COMPRESSED_HEADER_SIZE, data.Length - FileUtil.BNET_COMPRESSED_HEADER_SIZE))
			{
				int num = (int)this.GetDecompressedLength(data);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (Stream inflateStream = this.m_compressionProvider.GetInflateStream(memoryStream))
				{
					byte[] array = new byte[num];
					int num2 = 0;
					int num3 = array.Length;
					try
					{
						for (;;)
						{
							int num4 = inflateStream.Read(array, num2, num3);
							if (num4 <= 0)
							{
								break;
							}
							num2 += num4;
							num3 -= num4;
						}
					}
					catch (Exception)
					{
						return null;
					}
					if (num2 != num)
					{
						result = null;
					}
					else
					{
						result = array;
					}
				}
			}
			return result;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0008248C File Offset: 0x0008068C
		private bool IsCompressedStream(byte[] data)
		{
			try
			{
				if (data.Length < FileUtil.BNET_COMPRESSED_HEADER_SIZE)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, 0) != FileUtil.BNET_COMPRESSED_MAGIC)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, FileUtil.BNET_COMPRESSED_MAGIC_BYTES.Length) != FileUtil.BNET_COMPRESSED_VERSION)
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000824EC File Offset: 0x000806EC
		private ulong GetDecompressedLength(byte[] data)
		{
			return BitConverter.ToUInt64(data, FileUtil.BNET_COMPRESSED_MAGIC_BYTES.Length + FileUtil.BNET_COMPRESSED_VERSION_BYTES.Length);
		}

		// Token: 0x04000F4C RID: 3916
		private static readonly uint BNET_COMPRESSED_MAGIC = 1131245658U;

		// Token: 0x04000F4D RID: 3917
		private static readonly uint BNET_COMPRESSED_VERSION = 0U;

		// Token: 0x04000F4E RID: 3918
		private static readonly byte[] BNET_COMPRESSED_MAGIC_BYTES = BitConverter.GetBytes(FileUtil.BNET_COMPRESSED_MAGIC);

		// Token: 0x04000F4F RID: 3919
		private static readonly byte[] BNET_COMPRESSED_VERSION_BYTES = BitConverter.GetBytes(FileUtil.BNET_COMPRESSED_VERSION);

		// Token: 0x04000F50 RID: 3920
		private static readonly int BNET_COMPRESSED_HEADER_SIZE = FileUtil.BNET_COMPRESSED_MAGIC_BYTES.Length + FileUtil.BNET_COMPRESSED_VERSION_BYTES.Length + 8;

		// Token: 0x04000F51 RID: 3921
		private readonly ICompressionProvider m_compressionProvider;
	}
}
