using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace bgs.Shared.Util.platform_win32
{
	// Token: 0x02000280 RID: 640
	public class DPAPI
	{
		// Token: 0x060025DD RID: 9693
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptProtectData(ref DPAPI.DATA_BLOB pPlainText, string szDescription, ref DPAPI.DATA_BLOB pEntropy, IntPtr pReserved, ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DPAPI.DATA_BLOB pCipherText);

		// Token: 0x060025DE RID: 9694
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref DPAPI.DATA_BLOB pCipherText, ref string pszDescription, ref DPAPI.DATA_BLOB pEntropy, IntPtr pReserved, ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DPAPI.DATA_BLOB pPlainText);

		// Token: 0x060025DF RID: 9695 RVA: 0x000861F7 File Offset: 0x000843F7
		private static void InitPrompt(ref DPAPI.CRYPTPROTECT_PROMPTSTRUCT ps)
		{
			ps.cbSize = Marshal.SizeOf(typeof(DPAPI.CRYPTPROTECT_PROMPTSTRUCT));
			ps.dwPromptFlags = 0;
			ps.hwndApp = DPAPI.NullPtr;
			ps.szPrompt = null;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00086228 File Offset: 0x00084428
		private static void InitBLOB(byte[] data, ref DPAPI.DATA_BLOB blob)
		{
			if (data == null)
			{
				data = new byte[0];
			}
			blob.pbData = Marshal.AllocHGlobal(data.Length);
			if (blob.pbData == IntPtr.Zero)
			{
				throw new Exception("Unable to allocate data buffer for BLOB structure.");
			}
			blob.cbData = data.Length;
			Marshal.Copy(data, 0, blob.pbData, data.Length);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00086284 File Offset: 0x00084484
		public static string Encrypt(string plainText)
		{
			return DPAPI.Encrypt(DPAPI.defaultKeyType, plainText, string.Empty, string.Empty);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0008629B File Offset: 0x0008449B
		public static string Encrypt(DPAPI.KeyType keyType, string plainText)
		{
			return DPAPI.Encrypt(keyType, plainText, string.Empty, string.Empty);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000862AE File Offset: 0x000844AE
		public static string Encrypt(DPAPI.KeyType keyType, string plainText, string entropy)
		{
			return DPAPI.Encrypt(keyType, plainText, entropy, string.Empty);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000862BD File Offset: 0x000844BD
		public static string Encrypt(DPAPI.KeyType keyType, string plainText, string entropy, string description)
		{
			if (plainText == null)
			{
				plainText = string.Empty;
			}
			if (entropy == null)
			{
				entropy = string.Empty;
			}
			return Convert.ToBase64String(DPAPI.Encrypt(keyType, Encoding.UTF8.GetBytes(plainText), Encoding.UTF8.GetBytes(entropy), description));
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000862F8 File Offset: 0x000844F8
		public static byte[] Encrypt(DPAPI.KeyType keyType, byte[] plainTextBytes, byte[] entropyBytes, string description)
		{
			if (plainTextBytes == null)
			{
				plainTextBytes = new byte[0];
			}
			if (entropyBytes == null)
			{
				entropyBytes = new byte[0];
			}
			if (description == null)
			{
				description = string.Empty;
			}
			DPAPI.DATA_BLOB data_BLOB = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB data_BLOB2 = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB data_BLOB3 = default(DPAPI.DATA_BLOB);
			DPAPI.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = default(DPAPI.CRYPTPROTECT_PROMPTSTRUCT);
			DPAPI.InitPrompt(ref cryptprotect_PROMPTSTRUCT);
			byte[] result;
			try
			{
				try
				{
					DPAPI.InitBLOB(plainTextBytes, ref data_BLOB);
				}
				catch (Exception innerException)
				{
					throw new Exception("Cannot initialize plaintext BLOB.", innerException);
				}
				try
				{
					DPAPI.InitBLOB(entropyBytes, ref data_BLOB3);
				}
				catch (Exception innerException2)
				{
					throw new Exception("Cannot initialize entropy BLOB.", innerException2);
				}
				int num = 1;
				if (keyType == DPAPI.KeyType.MachineKey)
				{
					num |= 4;
				}
				if (!DPAPI.CryptProtectData(ref data_BLOB, description, ref data_BLOB3, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, num, ref data_BLOB2))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new Exception("CryptProtectData failed.", new Win32Exception(lastWin32Error));
				}
				byte[] array = new byte[data_BLOB2.cbData];
				Marshal.Copy(data_BLOB2.pbData, array, 0, data_BLOB2.cbData);
				result = array;
			}
			catch (Exception innerException3)
			{
				throw new Exception("DPAPI was unable to encrypt data.", innerException3);
			}
			finally
			{
				if (data_BLOB.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB.pbData);
				}
				if (data_BLOB2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB2.pbData);
				}
				if (data_BLOB3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB3.pbData);
				}
			}
			return result;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00086480 File Offset: 0x00084680
		public static string Decrypt(string cipherText)
		{
			string text;
			return DPAPI.Decrypt(cipherText, string.Empty, out text);
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0008649A File Offset: 0x0008469A
		public static string Decrypt(string cipherText, out string description)
		{
			return DPAPI.Decrypt(cipherText, string.Empty, out description);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000864A8 File Offset: 0x000846A8
		public static string Decrypt(string cipherText, string entropy, out string description)
		{
			if (entropy == null)
			{
				entropy = string.Empty;
			}
			return Encoding.UTF8.GetString(DPAPI.Decrypt(Convert.FromBase64String(cipherText), Encoding.UTF8.GetBytes(entropy), out description));
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000864D8 File Offset: 0x000846D8
		public static byte[] Decrypt(byte[] cipherTextBytes, byte[] entropyBytes, out string description)
		{
			DPAPI.DATA_BLOB data_BLOB = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB data_BLOB2 = default(DPAPI.DATA_BLOB);
			DPAPI.DATA_BLOB data_BLOB3 = default(DPAPI.DATA_BLOB);
			DPAPI.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = default(DPAPI.CRYPTPROTECT_PROMPTSTRUCT);
			DPAPI.InitPrompt(ref cryptprotect_PROMPTSTRUCT);
			description = string.Empty;
			byte[] result;
			try
			{
				try
				{
					DPAPI.InitBLOB(cipherTextBytes, ref data_BLOB2);
				}
				catch (Exception innerException)
				{
					throw new Exception("Cannot initialize ciphertext BLOB.", innerException);
				}
				try
				{
					DPAPI.InitBLOB(entropyBytes, ref data_BLOB3);
				}
				catch (Exception innerException2)
				{
					throw new Exception("Cannot initialize entropy BLOB.", innerException2);
				}
				int dwFlags = 1;
				if (!DPAPI.CryptUnprotectData(ref data_BLOB2, ref description, ref data_BLOB3, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, dwFlags, ref data_BLOB))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new Exception("CryptUnprotectData failed.", new Win32Exception(lastWin32Error));
				}
				byte[] array = new byte[data_BLOB.cbData];
				Marshal.Copy(data_BLOB.pbData, array, 0, data_BLOB.cbData);
				result = array;
			}
			catch (Exception innerException3)
			{
				throw new Exception("DPAPI was unable to decrypt data.", innerException3);
			}
			finally
			{
				if (data_BLOB.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB.pbData);
				}
				if (data_BLOB2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB2.pbData);
				}
				if (data_BLOB3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(data_BLOB3.pbData);
				}
			}
			return result;
		}

		// Token: 0x04001032 RID: 4146
		private static IntPtr NullPtr = (IntPtr)0;

		// Token: 0x04001033 RID: 4147
		private const int CRYPTPROTECT_UI_FORBIDDEN = 1;

		// Token: 0x04001034 RID: 4148
		private const int CRYPTPROTECT_LOCAL_MACHINE = 4;

		// Token: 0x04001035 RID: 4149
		private static DPAPI.KeyType defaultKeyType = DPAPI.KeyType.UserKey;

		// Token: 0x020006F5 RID: 1781
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DATA_BLOB
		{
			// Token: 0x040022C9 RID: 8905
			public int cbData;

			// Token: 0x040022CA RID: 8906
			public IntPtr pbData;
		}

		// Token: 0x020006F6 RID: 1782
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPTPROTECT_PROMPTSTRUCT
		{
			// Token: 0x040022CB RID: 8907
			public int cbSize;

			// Token: 0x040022CC RID: 8908
			public int dwPromptFlags;

			// Token: 0x040022CD RID: 8909
			public IntPtr hwndApp;

			// Token: 0x040022CE RID: 8910
			public string szPrompt;
		}

		// Token: 0x020006F7 RID: 1783
		public enum KeyType
		{
			// Token: 0x040022D0 RID: 8912
			UserKey = 1,
			// Token: 0x040022D1 RID: 8913
			MachineKey
		}
	}
}
