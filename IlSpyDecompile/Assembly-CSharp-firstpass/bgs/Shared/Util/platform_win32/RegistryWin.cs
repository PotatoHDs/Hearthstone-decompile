using System;
using System.IO;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace bgs.Shared.Util.platform_win32
{
	public class RegistryWin : IRegistry
	{
		private readonly string HKEY_CURRENT_USER = "HKEY_CURRENT_USER\\";

		public BattleNetErrors RetrieveVector(string path, string name, out byte[] vec, bool encrypt = true)
		{
			vec = new byte[0];
			object outValue = null;
			BattleNetErrors registryValue = GetRegistryValue(path, name, out outValue);
			if (registryValue != 0)
			{
				return registryValue;
			}
			Type type = vec.GetType();
			if (outValue.GetType() != type)
			{
				return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
			}
			if (encrypt)
			{
				try
				{
					byte[] cipherTextBytes = (byte[])outValue;
					vec = DPAPI.Decrypt(cipherTextBytes, Registry.s_entropy, out var _);
				}
				catch (Exception)
				{
					return BattleNetErrors.ERROR_REGISTRY_DECRYPT_ERROR;
				}
			}
			else
			{
				vec = (byte[])outValue;
			}
			return BattleNetErrors.ERROR_OK;
		}

		public BattleNetErrors RetrieveString(string path, string name, out string s, bool encrypt = false)
		{
			s = "";
			object outValue = null;
			BattleNetErrors registryValue = GetRegistryValue(path, name, out outValue);
			if (registryValue != 0)
			{
				return registryValue;
			}
			if (encrypt)
			{
				if (outValue.GetType() != typeof(byte[]))
				{
					return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
				}
				byte[] cipherTextBytes = (byte[])outValue;
				try
				{
					string description;
					byte[] bytes = DPAPI.Decrypt(cipherTextBytes, Registry.s_entropy, out description);
					s = Encoding.ASCII.GetString(bytes);
				}
				catch (Exception)
				{
					return BattleNetErrors.ERROR_REGISTRY_DECRYPT_ERROR;
				}
			}
			else
			{
				if (outValue.GetType() != typeof(string))
				{
					return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
				}
				s = (string)outValue;
			}
			return BattleNetErrors.ERROR_OK;
		}

		public BattleNetErrors RetrieveInt(string path, string name, out int i)
		{
			i = 0;
			object outValue = null;
			BattleNetErrors registryValue = GetRegistryValue(path, name, out outValue);
			if (registryValue != 0)
			{
				return registryValue;
			}
			Type type = i.GetType();
			if (outValue.GetType() != type)
			{
				return BattleNetErrors.ERROR_REGISTRY_TYPE_ERROR;
			}
			i = (int)outValue;
			return BattleNetErrors.ERROR_OK;
		}

		public BattleNetErrors DeleteData(string path, string name)
		{
			return BattleNetErrors.ERROR_REGISTRY_DELETE_ERROR;
		}

		private BattleNetErrors GetRegistryValue(string path, string name, out object outValue)
		{
			outValue = null;
			object obj = new object();
			try
			{
				outValue = Microsoft.Win32.Registry.GetValue(HKEY_CURRENT_USER + path, name, obj);
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
	}
}
