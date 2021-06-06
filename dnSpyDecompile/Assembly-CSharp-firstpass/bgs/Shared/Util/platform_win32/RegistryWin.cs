using System;
using System.IO;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace bgs.Shared.Util.platform_win32
{
	// Token: 0x02000282 RID: 642
	public class RegistryWin : IRegistry
	{
		// Token: 0x060025EE RID: 9710 RVA: 0x000866D8 File Offset: 0x000848D8
		public BattleNetErrors RetrieveVector(string path, string name, out byte[] vec, bool encrypt = true)
		{
			vec = new byte[0];
			object obj = null;
			BattleNetErrors registryValue = this.GetRegistryValue(path, name, out obj);
			if (registryValue != BattleNetErrors.ERROR_OK)
			{
				return registryValue;
			}
			Type type = vec.GetType();
			if (obj.GetType() != type)
			{
				return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
			}
			if (encrypt)
			{
				try
				{
					byte[] cipherTextBytes = (byte[])obj;
					string text;
					vec = DPAPI.Decrypt(cipherTextBytes, Registry.s_entropy, out text);
					return BattleNetErrors.ERROR_OK;
				}
				catch (Exception)
				{
					return BattleNetErrors.ERROR_REGISTRY_DECRYPT_ERROR;
				}
			}
			vec = (byte[])obj;
			return BattleNetErrors.ERROR_OK;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00086760 File Offset: 0x00084960
		public BattleNetErrors RetrieveString(string path, string name, out string s, bool encrypt = false)
		{
			s = "";
			object obj = null;
			BattleNetErrors registryValue = this.GetRegistryValue(path, name, out obj);
			if (registryValue != BattleNetErrors.ERROR_OK)
			{
				return registryValue;
			}
			if (encrypt)
			{
				if (obj.GetType() != typeof(byte[]))
				{
					return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
				}
				byte[] cipherTextBytes = (byte[])obj;
				try
				{
					string text;
					byte[] bytes = DPAPI.Decrypt(cipherTextBytes, Registry.s_entropy, out text);
					s = Encoding.ASCII.GetString(bytes);
					return BattleNetErrors.ERROR_OK;
				}
				catch (Exception)
				{
					return BattleNetErrors.ERROR_REGISTRY_DECRYPT_ERROR;
				}
			}
			if (obj.GetType() != typeof(string))
			{
				return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
			}
			s = (string)obj;
			return BattleNetErrors.ERROR_OK;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00086810 File Offset: 0x00084A10
		public BattleNetErrors RetrieveInt(string path, string name, out int i)
		{
			i = 0;
			object obj = null;
			BattleNetErrors registryValue = this.GetRegistryValue(path, name, out obj);
			if (registryValue != BattleNetErrors.ERROR_OK)
			{
				return registryValue;
			}
			Type type = i.GetType();
			if (obj.GetType() != type)
			{
				return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
			}
			i = (int)obj;
			return BattleNetErrors.ERROR_OK;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0008685C File Offset: 0x00084A5C
		public BattleNetErrors DeleteData(string path, string name)
		{
			return BattleNetErrors.ERROR_REGISTRY_DELETE_ERROR;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00086864 File Offset: 0x00084A64
		private BattleNetErrors GetRegistryValue(string path, string name, out object outValue)
		{
			outValue = null;
			object obj = new object();
			try
			{
				outValue = Registry.GetValue(this.HKEY_CURRENT_USER + path, name, obj);
			}
			catch (SecurityException)
			{
				return BattleNetErrors.ERROR_REGISTRY_READ_ERROR;
			}
			catch (IOException)
			{
				return BattleNetErrors.ERROR_REGISTRY_DELETE_ERROR;
			}
			catch (ArgumentException)
			{
				return BattleNetErrors.ERROR_REGISTRY_NOT_FOUND;
			}
			catch (Exception)
			{
				return BattleNetErrors.ERROR_REGISTRY_OPEN_KEY_ERROR;
			}
			if (outValue == obj)
			{
				return BattleNetErrors.ERROR_REGISTRY_NOT_FOUND;
			}
			return BattleNetErrors.ERROR_OK;
		}

		// Token: 0x04001036 RID: 4150
		private readonly string HKEY_CURRENT_USER = "HKEY_CURRENT_USER\\";
	}
}
