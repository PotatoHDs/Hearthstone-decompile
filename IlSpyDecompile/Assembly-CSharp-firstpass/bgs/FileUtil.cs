using System;
using System.IO;

namespace bgs
{
	public class FileUtil : IFileUtil
	{
		private static readonly uint BNET_COMPRESSED_MAGIC = 1131245658u;

		private static readonly uint BNET_COMPRESSED_VERSION = 0u;

		private static readonly byte[] BNET_COMPRESSED_MAGIC_BYTES = BitConverter.GetBytes(BNET_COMPRESSED_MAGIC);

		private static readonly byte[] BNET_COMPRESSED_VERSION_BYTES = BitConverter.GetBytes(BNET_COMPRESSED_VERSION);

		private static readonly int BNET_COMPRESSED_HEADER_SIZE = BNET_COMPRESSED_MAGIC_BYTES.Length + BNET_COMPRESSED_VERSION_BYTES.Length + 8;

		private readonly ICompressionProvider m_compressionProvider;

		public FileUtil(ICompressionProvider compressionProvider)
		{
			m_compressionProvider = compressionProvider;
		}

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
					array = Compress(array);
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
				if (IsCompressedStream(array))
				{
					byte[] array2 = Decompress(array);
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

		private byte[] Compress(byte[] data)
		{
			byte[] array;
			try
			{
				using MemoryStream memoryStream = new MemoryStream(data.Length);
				using (Stream stream = m_compressionProvider.GetDeflateStream(memoryStream))
				{
					stream.Write(data, 0, data.Length);
				}
				array = memoryStream.ToArray();
			}
			catch (Exception)
			{
				return null;
			}
			byte[] bytes = BitConverter.GetBytes((ulong)data.Length);
			byte[] array2 = new byte[array.Length + BNET_COMPRESSED_HEADER_SIZE];
			int num = 0;
			Array.Copy(BNET_COMPRESSED_MAGIC_BYTES, 0, array2, num, BNET_COMPRESSED_MAGIC_BYTES.Length);
			num += BNET_COMPRESSED_MAGIC_BYTES.Length;
			Array.Copy(BNET_COMPRESSED_VERSION_BYTES, 0, array2, num, BNET_COMPRESSED_VERSION_BYTES.Length);
			num += BNET_COMPRESSED_VERSION_BYTES.Length;
			Array.Copy(bytes, 0, array2, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(array, 0, array2, num, array.Length);
			return array2;
		}

		private byte[] Decompress(byte[] data)
		{
			using MemoryStream memoryStream = new MemoryStream(data, BNET_COMPRESSED_HEADER_SIZE, data.Length - BNET_COMPRESSED_HEADER_SIZE);
			int num = (int)GetDecompressedLength(data);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			using Stream stream = m_compressionProvider.GetInflateStream(memoryStream);
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = array.Length;
			try
			{
				while (true)
				{
					int num4 = stream.Read(array, num2, num3);
					if (num4 > 0)
					{
						num2 += num4;
						num3 -= num4;
						continue;
					}
					break;
				}
			}
			catch (Exception)
			{
				return null;
			}
			if (num2 != num)
			{
				return null;
			}
			return array;
		}

		private bool IsCompressedStream(byte[] data)
		{
			try
			{
				if (data.Length < BNET_COMPRESSED_HEADER_SIZE)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, 0) != BNET_COMPRESSED_MAGIC)
				{
					return false;
				}
				if (BitConverter.ToUInt32(data, BNET_COMPRESSED_MAGIC_BYTES.Length) != BNET_COMPRESSED_VERSION)
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

		private ulong GetDecompressedLength(byte[] data)
		{
			return BitConverter.ToUInt64(data, BNET_COMPRESSED_MAGIC_BYTES.Length + BNET_COMPRESSED_VERSION_BYTES.Length);
		}
	}
}
