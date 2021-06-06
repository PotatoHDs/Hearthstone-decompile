using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x020008F5 RID: 2293
public class Options
{
	// Token: 0x06007F67 RID: 32615 RVA: 0x002953A7 File Offset: 0x002935A7
	public static Options Get()
	{
		if (Options.s_instance == null)
		{
			Options.s_instance = new Options();
			Options.s_instance.Initialize();
		}
		return Options.s_instance;
	}

	// Token: 0x06007F68 RID: 32616 RVA: 0x002953C9 File Offset: 0x002935C9
	public bool IsClientOption(Option option)
	{
		return this.m_clientOptionMap.ContainsKey(option);
	}

	// Token: 0x06007F69 RID: 32617 RVA: 0x002953D7 File Offset: 0x002935D7
	public bool IsServerOption(Option option)
	{
		return this.m_serverOptionMap.ContainsKey(option);
	}

	// Token: 0x06007F6A RID: 32618 RVA: 0x002953E5 File Offset: 0x002935E5
	public bool IsServerOptionFlag(Option option)
	{
		return this.m_serverOptionFlagMap.ContainsKey(option);
	}

	// Token: 0x06007F6B RID: 32619 RVA: 0x002953F3 File Offset: 0x002935F3
	public Map<Option, string> GetClientOptions()
	{
		return this.m_clientOptionMap;
	}

	// Token: 0x06007F6C RID: 32620 RVA: 0x002953FB File Offset: 0x002935FB
	public Map<Option, ServerOption> GetServerOptions()
	{
		return this.m_serverOptionMap;
	}

	// Token: 0x06007F6D RID: 32621 RVA: 0x00295404 File Offset: 0x00293604
	public Type GetOptionType(Option option)
	{
		Type result;
		if (OptionDataTables.s_typeMap.TryGetValue(option, out result))
		{
			return result;
		}
		if (this.m_serverOptionFlagMap.ContainsKey(option))
		{
			return typeof(bool);
		}
		return null;
	}

	// Token: 0x06007F6E RID: 32622 RVA: 0x0029543C File Offset: 0x0029363C
	public Type GetServerOptionType(ServerOption serverOption)
	{
		if (Array.Exists<ServerOption>(Options.s_serverFlagContainers, (ServerOption flagContainer) => flagContainer == serverOption))
		{
			return typeof(ulong);
		}
		foreach (KeyValuePair<Option, ServerOption> keyValuePair in this.m_serverOptionMap)
		{
			if (keyValuePair.Value == serverOption)
			{
				Option key = keyValuePair.Key;
				Type result;
				if (OptionDataTables.s_typeMap.TryGetValue(key, out result))
				{
					return result;
				}
				break;
			}
		}
		return null;
	}

	// Token: 0x06007F6F RID: 32623 RVA: 0x002954E8 File Offset: 0x002936E8
	public static FormatType GetFormatType()
	{
		return Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE);
	}

	// Token: 0x06007F70 RID: 32624 RVA: 0x002954F9 File Offset: 0x002936F9
	public static void SetFormatType(FormatType formatType)
	{
		Options.Get().SetEnum<FormatType>(Option.FORMAT_TYPE, formatType);
	}

	// Token: 0x06007F71 RID: 32625 RVA: 0x0029550B File Offset: 0x0029370B
	public static bool GetInRankedPlayMode()
	{
		return Options.Get().GetBool(Option.IN_RANKED_PLAY_MODE);
	}

	// Token: 0x06007F72 RID: 32626 RVA: 0x0029551C File Offset: 0x0029371C
	public static void SetInRankedPlayMode(bool inRankedPlayMode)
	{
		Options.Get().SetBool(Option.IN_RANKED_PLAY_MODE, inRankedPlayMode);
	}

	// Token: 0x06007F73 RID: 32627 RVA: 0x0029552E File Offset: 0x0029372E
	public bool RegisterChangedListener(Option option, Options.ChangedCallback callback)
	{
		return this.RegisterChangedListener(option, callback, null);
	}

	// Token: 0x06007F74 RID: 32628 RVA: 0x0029553C File Offset: 0x0029373C
	public bool RegisterChangedListener(Option option, Options.ChangedCallback callback, object userData)
	{
		Options.ChangedListener changedListener = new Options.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		List<Options.ChangedListener> list;
		if (!this.m_changedListeners.TryGetValue(option, out list))
		{
			list = new List<Options.ChangedListener>();
			this.m_changedListeners.Add(option, list);
		}
		else if (list.Contains(changedListener))
		{
			return false;
		}
		list.Add(changedListener);
		return true;
	}

	// Token: 0x06007F75 RID: 32629 RVA: 0x00295595 File Offset: 0x00293795
	public bool UnregisterChangedListener(Option option, Options.ChangedCallback callback)
	{
		return this.UnregisterChangedListener(option, callback, null);
	}

	// Token: 0x06007F76 RID: 32630 RVA: 0x002955A0 File Offset: 0x002937A0
	public bool UnregisterChangedListener(Option option, Options.ChangedCallback callback, object userData)
	{
		Options.ChangedListener changedListener = new Options.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		List<Options.ChangedListener> list;
		if (!this.m_changedListeners.TryGetValue(option, out list))
		{
			return false;
		}
		if (!list.Remove(changedListener))
		{
			return false;
		}
		if (list.Count == 0)
		{
			this.m_changedListeners.Remove(option);
		}
		return true;
	}

	// Token: 0x06007F77 RID: 32631 RVA: 0x002955F4 File Offset: 0x002937F4
	public bool RegisterGlobalChangedListener(Options.ChangedCallback callback)
	{
		return this.RegisterGlobalChangedListener(callback, null);
	}

	// Token: 0x06007F78 RID: 32632 RVA: 0x00295600 File Offset: 0x00293800
	public bool RegisterGlobalChangedListener(Options.ChangedCallback callback, object userData)
	{
		Options.ChangedListener changedListener = new Options.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		if (this.m_globalChangedListeners.Contains(changedListener))
		{
			return false;
		}
		this.m_globalChangedListeners.Add(changedListener);
		return true;
	}

	// Token: 0x06007F79 RID: 32633 RVA: 0x0029563E File Offset: 0x0029383E
	public bool UnregisterGlobalChangedListener(Options.ChangedCallback callback)
	{
		return this.UnregisterGlobalChangedListener(callback, null);
	}

	// Token: 0x06007F7A RID: 32634 RVA: 0x00295648 File Offset: 0x00293848
	public bool UnregisterGlobalChangedListener(Options.ChangedCallback callback, object userData)
	{
		Options.ChangedListener changedListener = new Options.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		return this.m_globalChangedListeners.Remove(changedListener);
	}

	// Token: 0x06007F7B RID: 32635 RVA: 0x00295678 File Offset: 0x00293878
	public bool HasOption(Option option)
	{
		string key;
		if (this.m_clientOptionMap.TryGetValue(option, out key))
		{
			return LocalOptions.Get().Has(key);
		}
		ServerOption type;
		if (this.m_serverOptionMap.TryGetValue(option, out type))
		{
			return NetCache.Get().ClientOptionExists(type);
		}
		ServerOptionFlag serverOptionFlag;
		return this.m_serverOptionFlagMap.TryGetValue(option, out serverOptionFlag) && this.HasServerOptionFlag(serverOptionFlag);
	}

	// Token: 0x06007F7C RID: 32636 RVA: 0x002956D8 File Offset: 0x002938D8
	public void DeleteOption(Option option)
	{
		string optionName;
		if (this.m_clientOptionMap.TryGetValue(option, out optionName))
		{
			this.DeleteClientOption(option, optionName);
			return;
		}
		ServerOption serverOption;
		if (this.m_serverOptionMap.TryGetValue(option, out serverOption))
		{
			this.DeleteServerOption(option, serverOption);
			return;
		}
		ServerOptionFlag serverOptionFlag;
		if (this.m_serverOptionFlagMap.TryGetValue(option, out serverOptionFlag))
		{
			this.DeleteServerOptionFlag(option, serverOptionFlag);
			return;
		}
	}

	// Token: 0x06007F7D RID: 32637 RVA: 0x00295730 File Offset: 0x00293930
	public void DeleteOption(string optionStr)
	{
		Option option = Option.INVALID;
		try
		{
			option = EnumUtils.GetEnum<Option>(optionStr, StringComparison.OrdinalIgnoreCase);
		}
		catch (ArgumentException)
		{
			Debug.LogErrorFormat("No matched option with '{0}'", new object[]
			{
				optionStr
			});
			return;
		}
		this.DeleteOption(option);
	}

	// Token: 0x06007F7E RID: 32638 RVA: 0x00295778 File Offset: 0x00293978
	public object GetOption(Option option)
	{
		object result;
		if (this.GetOptionImpl(option, out result))
		{
			return result;
		}
		object result2;
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out result2))
		{
			return result2;
		}
		return null;
	}

	// Token: 0x06007F7F RID: 32639 RVA: 0x002957A4 File Offset: 0x002939A4
	public object GetOption(Option option, object defaultVal)
	{
		object result;
		if (this.GetOptionImpl(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F80 RID: 32640 RVA: 0x002957C0 File Offset: 0x002939C0
	public bool GetBool(Option option)
	{
		bool result;
		if (this.GetBoolImpl(option, out result))
		{
			return result;
		}
		object obj;
		return OptionDataTables.s_defaultsMap.TryGetValue(option, out obj) && (bool)obj;
	}

	// Token: 0x06007F81 RID: 32641 RVA: 0x002957F4 File Offset: 0x002939F4
	public bool GetBool(Option option, bool defaultVal)
	{
		bool result;
		if (this.GetBoolImpl(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F82 RID: 32642 RVA: 0x00295810 File Offset: 0x00293A10
	public int GetInt(Option option)
	{
		int result;
		if (this.GetIntImpl(option, out result))
		{
			return result;
		}
		object obj;
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out obj))
		{
			return (int)obj;
		}
		return 0;
	}

	// Token: 0x06007F83 RID: 32643 RVA: 0x00295844 File Offset: 0x00293A44
	public int GetInt(Option option, int defaultVal)
	{
		int result;
		if (this.GetIntImpl(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F84 RID: 32644 RVA: 0x00295860 File Offset: 0x00293A60
	public long GetLong(Option option)
	{
		long result;
		if (this.GetLongImpl(option, out result))
		{
			return result;
		}
		object obj;
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out obj))
		{
			return (long)obj;
		}
		return 0L;
	}

	// Token: 0x06007F85 RID: 32645 RVA: 0x00295894 File Offset: 0x00293A94
	public long GetLong(Option option, long defaultVal)
	{
		long result;
		if (this.GetLongImpl(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F86 RID: 32646 RVA: 0x002958B0 File Offset: 0x00293AB0
	public float GetFloat(Option option)
	{
		float result;
		if (this.GetFloatImpl(option, out result))
		{
			return result;
		}
		object obj;
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out obj))
		{
			return (float)obj;
		}
		return 0f;
	}

	// Token: 0x06007F87 RID: 32647 RVA: 0x002958E8 File Offset: 0x00293AE8
	public float GetFloat(Option option, float defaultVal)
	{
		float result;
		if (this.GetFloatImpl(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F88 RID: 32648 RVA: 0x00295904 File Offset: 0x00293B04
	public ulong GetULong(Option option)
	{
		ulong result;
		if (this.GetULongImpl(option, out result))
		{
			return result;
		}
		object obj;
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out obj))
		{
			return (ulong)obj;
		}
		return 0UL;
	}

	// Token: 0x06007F89 RID: 32649 RVA: 0x00295938 File Offset: 0x00293B38
	public ulong GetULong(Option option, ulong defaultVal)
	{
		ulong result;
		if (this.GetULongImpl(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F8A RID: 32650 RVA: 0x00295954 File Offset: 0x00293B54
	public string GetString(Option option)
	{
		string result;
		if (this.GetStringImpl(option, out result))
		{
			return result;
		}
		object obj;
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out obj))
		{
			return (string)obj;
		}
		return "";
	}

	// Token: 0x06007F8B RID: 32651 RVA: 0x0029598C File Offset: 0x00293B8C
	public string GetString(Option option, string defaultVal)
	{
		string result;
		if (this.GetStringImpl(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F8C RID: 32652 RVA: 0x002959A8 File Offset: 0x00293BA8
	public T GetEnum<T>(Option option)
	{
		T result;
		if (this.GetEnumImpl<T>(option, out result))
		{
			return result;
		}
		object genericVal;
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out genericVal) && this.TranslateEnumVal<T>(option, genericVal, out result))
		{
			return result;
		}
		return default(T);
	}

	// Token: 0x06007F8D RID: 32653 RVA: 0x002959E8 File Offset: 0x00293BE8
	public T GetEnum<T>(Option option, T defaultVal)
	{
		T result;
		if (this.GetEnumImpl<T>(option, out result))
		{
			return result;
		}
		return defaultVal;
	}

	// Token: 0x06007F8E RID: 32654 RVA: 0x00295A04 File Offset: 0x00293C04
	public void SetOption(Option option, object val)
	{
		Type optionType = this.GetOptionType(option);
		if (optionType == typeof(bool))
		{
			this.SetBool(option, (bool)val);
			return;
		}
		if (optionType == typeof(int))
		{
			this.SetInt(option, (int)val);
			return;
		}
		if (optionType == typeof(long))
		{
			this.SetLong(option, (long)val);
			return;
		}
		if (optionType == typeof(float))
		{
			this.SetFloat(option, (float)val);
			return;
		}
		if (optionType == typeof(string))
		{
			this.SetString(option, (string)val);
			return;
		}
		if (optionType == typeof(ulong))
		{
			this.SetULong(option, (ulong)val);
			return;
		}
		Error.AddDevFatal("Options.SetOption() - option {0} has unsupported underlying type {1}", new object[]
		{
			option,
			optionType
		});
	}

	// Token: 0x06007F8F RID: 32655 RVA: 0x00295AF8 File Offset: 0x00293CF8
	public void SetBool(Option option, bool val)
	{
		string key;
		if (this.m_clientOptionMap.TryGetValue(option, out key))
		{
			bool flag = LocalOptions.Get().Has(key);
			bool @bool = LocalOptions.Get().GetBool(key);
			if (!flag || @bool != val)
			{
				LocalOptions.Get().Set(key, val);
				this.FireChangedEvent(option, @bool, flag);
			}
			return;
		}
		ServerOptionFlag flag2;
		if (this.m_serverOptionFlagMap.TryGetValue(option, out flag2))
		{
			ServerOption type;
			ulong num;
			ulong num2;
			this.GetServerOptionFlagInfo(flag2, out type, out num, out num2);
			ulong ulongOption = NetCache.Get().GetULongOption(type);
			bool flag3 = (ulongOption & num) > 0UL;
			bool flag4 = (ulongOption & num2) > 0UL;
			if (!flag4 || flag3 != val)
			{
				ulong num3 = val ? (ulongOption | num) : (ulongOption & ~num);
				num3 |= num2;
				NetCache.Get().SetULongOption(type, num3);
				this.FireChangedEvent(option, flag3, flag4);
			}
			return;
		}
	}

	// Token: 0x06007F90 RID: 32656 RVA: 0x00295BD8 File Offset: 0x00293DD8
	public void SetInt(Option option, int val)
	{
		string key;
		if (this.m_clientOptionMap.TryGetValue(option, out key))
		{
			bool flag = LocalOptions.Get().Has(key);
			int @int = LocalOptions.Get().GetInt(key);
			if (!flag || @int != val)
			{
				LocalOptions.Get().Set(key, val);
				this.FireChangedEvent(option, @int, flag);
			}
			return;
		}
		ServerOption type;
		if (this.m_serverOptionMap.TryGetValue(option, out type))
		{
			int num;
			bool intOption = NetCache.Get().GetIntOption(type, out num);
			if (!intOption || num != val)
			{
				NetCache.Get().SetIntOption(type, val);
				this.FireChangedEvent(option, num, intOption);
			}
			return;
		}
	}

	// Token: 0x06007F91 RID: 32657 RVA: 0x00295C7C File Offset: 0x00293E7C
	public void SetLong(Option option, long val)
	{
		string key;
		if (this.m_clientOptionMap.TryGetValue(option, out key))
		{
			bool flag = LocalOptions.Get().Has(key);
			long @long = LocalOptions.Get().GetLong(key);
			if (!flag || @long != val)
			{
				LocalOptions.Get().Set(key, val);
				this.FireChangedEvent(option, @long, flag);
			}
			return;
		}
		ServerOption type;
		if (this.m_serverOptionMap.TryGetValue(option, out type))
		{
			long num;
			bool longOption = NetCache.Get().GetLongOption(type, out num);
			if (!longOption || num != val)
			{
				NetCache.Get().SetLongOption(type, val);
				this.FireChangedEvent(option, num, longOption);
			}
			return;
		}
	}

	// Token: 0x06007F92 RID: 32658 RVA: 0x00295D20 File Offset: 0x00293F20
	public void SetFloat(Option option, float val)
	{
		string key;
		if (this.m_clientOptionMap.TryGetValue(option, out key))
		{
			bool flag = LocalOptions.Get().Has(key);
			float @float = LocalOptions.Get().GetFloat(key);
			if (!flag || @float != val)
			{
				LocalOptions.Get().Set(key, val);
				this.FireChangedEvent(option, @float, flag);
			}
			return;
		}
		ServerOption type;
		if (this.m_serverOptionMap.TryGetValue(option, out type))
		{
			float num;
			bool floatOption = NetCache.Get().GetFloatOption(type, out num);
			if (!floatOption || num != val)
			{
				NetCache.Get().SetFloatOption(type, val);
				this.FireChangedEvent(option, num, floatOption);
			}
			return;
		}
	}

	// Token: 0x06007F93 RID: 32659 RVA: 0x00295DC4 File Offset: 0x00293FC4
	public void SetULong(Option option, ulong val)
	{
		string key;
		if (this.m_clientOptionMap.TryGetValue(option, out key))
		{
			bool flag = LocalOptions.Get().Has(key);
			ulong @ulong = LocalOptions.Get().GetULong(key);
			if (!flag || @ulong != val)
			{
				LocalOptions.Get().Set(key, val);
				this.FireChangedEvent(option, @ulong, flag);
			}
			return;
		}
		ServerOption type;
		if (this.m_serverOptionMap.TryGetValue(option, out type))
		{
			ulong num;
			bool ulongOption = NetCache.Get().GetULongOption(type, out num);
			if (!ulongOption || num != val)
			{
				NetCache.Get().SetULongOption(type, val);
				this.FireChangedEvent(option, num, ulongOption);
			}
			return;
		}
	}

	// Token: 0x06007F94 RID: 32660 RVA: 0x00295E68 File Offset: 0x00294068
	public void SetString(Option option, string val)
	{
		string key;
		if (this.m_clientOptionMap.TryGetValue(option, out key))
		{
			bool flag = LocalOptions.Get().Has(key);
			string @string = LocalOptions.Get().GetString(key);
			if (!flag || @string != val)
			{
				LocalOptions.Get().Set(key, val);
				this.FireChangedEvent(option, @string, flag);
			}
			return;
		}
	}

	// Token: 0x06007F95 RID: 32661 RVA: 0x00295EC0 File Offset: 0x002940C0
	public void SetEnum<T>(Option option, T val)
	{
		if (!Enum.IsDefined(typeof(T), val))
		{
			Error.AddDevFatal("Options.SetEnum() - {0} is not convertible to enum type {1} for option {2}", new object[]
			{
				val,
				typeof(T),
				option
			});
			return;
		}
		Type optionType = this.GetOptionType(option);
		if (optionType == typeof(int))
		{
			this.SetInt(option, Convert.ToInt32(val));
			return;
		}
		if (optionType == typeof(long))
		{
			this.SetLong(option, Convert.ToInt64(val));
			return;
		}
		Error.AddDevFatal("Options.SetEnum() - option {0} has unsupported underlying type {1}", new object[]
		{
			option,
			optionType
		});
	}

	// Token: 0x06007F96 RID: 32662 RVA: 0x00295F84 File Offset: 0x00294184
	private void Initialize()
	{
		Array values = Enum.GetValues(typeof(Option));
		Map<string, Option> map = new Map<string, Option>();
		foreach (object obj in values)
		{
			Option option = (Option)obj;
			if (option != Option.INVALID)
			{
				string key = option.ToString();
				map.Add(key, option);
			}
		}
		this.BuildClientOptionMap(map);
		this.BuildServerOptionMap(map);
		this.BuildServerOptionFlagMap(map);
	}

	// Token: 0x06007F97 RID: 32663 RVA: 0x00296018 File Offset: 0x00294218
	private void BuildClientOptionMap(Map<string, Option> options)
	{
		this.m_clientOptionMap = new Map<Option, string>();
		foreach (object obj in Enum.GetValues(typeof(ClientOption)))
		{
			ClientOption clientOption = (ClientOption)obj;
			if (clientOption != ClientOption.INVALID)
			{
				string key = clientOption.ToString();
				Option option;
				Type type;
				if (!options.TryGetValue(key, out option))
				{
					Debug.LogError(string.Format("Options.BuildClientOptionMap() - ClientOption {0} is not mirrored in the Option enum", clientOption));
				}
				else if (!OptionDataTables.s_typeMap.TryGetValue(option, out type))
				{
					Debug.LogError(string.Format("Options.BuildClientOptionMap() - ClientOption {0} has no type. Please add its type to the type map.", clientOption));
				}
				else
				{
					string @string = EnumUtils.GetString<Option>(option);
					this.m_clientOptionMap.Add(option, @string);
				}
			}
		}
	}

	// Token: 0x06007F98 RID: 32664 RVA: 0x002960F4 File Offset: 0x002942F4
	private void BuildServerOptionMap(Map<string, Option> options)
	{
		this.m_serverOptionMap = new Map<Option, ServerOption>();
		foreach (object obj in Enum.GetValues(typeof(ServerOption)))
		{
			ServerOption serverOption = (ServerOption)obj;
			if (serverOption != ServerOption.INVALID && serverOption != ServerOption.LIMIT)
			{
				string text = serverOption.ToString();
				if (!text.StartsWith("FLAGS") && !text.StartsWith("DEPRECATED"))
				{
					Option key;
					Type left;
					if (!options.TryGetValue(text, out key))
					{
						Debug.LogError(string.Format("Options.BuildServerOptionMap() - ServerOption {0} is not mirrored in the Option enum", serverOption));
					}
					else if (!OptionDataTables.s_typeMap.TryGetValue(key, out left))
					{
						Debug.LogError(string.Format("Options.BuildServerOptionMap() - ServerOption {0} has no type. Please add its type to the type map.", serverOption));
					}
					else if (left == typeof(bool))
					{
						Debug.LogError(string.Format("Options.BuildServerOptionMap() - ServerOption {0} is a bool. You should convert it to a ServerOptionFlag.", serverOption));
					}
					else
					{
						this.m_serverOptionMap.Add(key, serverOption);
					}
				}
			}
		}
	}

	// Token: 0x06007F99 RID: 32665 RVA: 0x0029621C File Offset: 0x0029441C
	private void BuildServerOptionFlagMap(Map<string, Option> options)
	{
		this.m_serverOptionFlagMap = new Map<Option, ServerOptionFlag>();
		foreach (object obj in Enum.GetValues(typeof(ServerOptionFlag)))
		{
			ServerOptionFlag serverOptionFlag = (ServerOptionFlag)obj;
			if (serverOptionFlag != ServerOptionFlag.LIMIT)
			{
				string text = serverOptionFlag.ToString();
				if (!text.StartsWith("DEPRECATED"))
				{
					Option key;
					if (!options.TryGetValue(text, out key))
					{
						Debug.LogError(string.Format("Options.BuildServerOptionFlagMap() - ServerOptionFlag {0} is not mirrored in the Option enum", serverOptionFlag));
					}
					else
					{
						this.m_serverOptionFlagMap.Add(key, serverOptionFlag);
					}
				}
			}
		}
	}

	// Token: 0x06007F9A RID: 32666 RVA: 0x002962D8 File Offset: 0x002944D8
	private void GetServerOptionFlagInfo(ServerOptionFlag flag, out ServerOption container, out ulong flagBit, out ulong existenceBit)
	{
		ServerOptionFlag serverOptionFlag = ServerOptionFlag.HAS_SEEN_TOURNAMENT * flag;
		int num = Mathf.FloorToInt((float)serverOptionFlag * 0.015625f);
		int num2 = (int)(serverOptionFlag % ServerOptionFlag.HAS_SEEN_UNLOCK_ALL_HEROES_TRANSITION);
		int num3 = 1 + num2;
		container = Options.s_serverFlagContainers[num];
		flagBit = 1UL << num2;
		existenceBit = 1UL << num3;
	}

	// Token: 0x06007F9B RID: 32667 RVA: 0x0029631C File Offset: 0x0029451C
	private bool HasServerOptionFlag(ServerOptionFlag serverOptionFlag)
	{
		ServerOption type;
		ulong num;
		ulong num2;
		this.GetServerOptionFlagInfo(serverOptionFlag, out type, out num, out num2);
		return (NetCache.Get().GetULongOption(type) & num2) > 0UL;
	}

	// Token: 0x06007F9C RID: 32668 RVA: 0x00296348 File Offset: 0x00294548
	private void DeleteClientOption(Option option, string optionName)
	{
		if (!LocalOptions.Get().Has(optionName))
		{
			return;
		}
		object clientOption = this.GetClientOption(option, optionName);
		LocalOptions.Get().Delete(optionName);
		this.RemoveListeners(option, clientOption);
	}

	// Token: 0x06007F9D RID: 32669 RVA: 0x00296380 File Offset: 0x00294580
	private void DeleteServerOption(Option option, ServerOption serverOption)
	{
		if (!NetCache.Get().ClientOptionExists(serverOption))
		{
			return;
		}
		object serverOption2 = this.GetServerOption(option, serverOption);
		NetCache.Get().DeleteClientOption(serverOption);
		this.RemoveListeners(option, serverOption2);
	}

	// Token: 0x06007F9E RID: 32670 RVA: 0x002963B8 File Offset: 0x002945B8
	private void DeleteServerOptionFlag(Option option, ServerOptionFlag serverOptionFlag)
	{
		ServerOption type;
		ulong num;
		ulong num2;
		this.GetServerOptionFlagInfo(serverOptionFlag, out type, out num, out num2);
		ulong num3 = NetCache.Get().GetULongOption(type);
		if ((num3 & num2) <= 0UL)
		{
			return;
		}
		bool flag = (num3 & num) > 0UL;
		num3 &= ~num2;
		NetCache.Get().SetULongOption(type, num3);
		this.RemoveListeners(option, flag);
	}

	// Token: 0x06007F9F RID: 32671 RVA: 0x00296410 File Offset: 0x00294610
	private object GetClientOption(Option option, string optionName)
	{
		Type optionType = this.GetOptionType(option);
		if (optionType == typeof(bool))
		{
			return LocalOptions.Get().GetBool(optionName);
		}
		if (optionType == typeof(int))
		{
			return LocalOptions.Get().GetInt(optionName);
		}
		if (optionType == typeof(long))
		{
			return LocalOptions.Get().GetLong(optionName);
		}
		if (optionType == typeof(ulong))
		{
			return LocalOptions.Get().GetULong(optionName);
		}
		if (optionType == typeof(float))
		{
			return LocalOptions.Get().GetFloat(optionName);
		}
		if (optionType == typeof(string))
		{
			return LocalOptions.Get().GetString(optionName);
		}
		Error.AddDevFatal("Options.GetClientOption() - option {0} has unsupported underlying type {1}", new object[]
		{
			option,
			optionType
		});
		return null;
	}

	// Token: 0x06007FA0 RID: 32672 RVA: 0x00296510 File Offset: 0x00294710
	private object GetServerOption(Option option, ServerOption serverOption)
	{
		Type optionType = this.GetOptionType(option);
		if (optionType == typeof(int))
		{
			return NetCache.Get().GetIntOption(serverOption);
		}
		if (optionType == typeof(long))
		{
			return NetCache.Get().GetLongOption(serverOption);
		}
		if (optionType == typeof(float))
		{
			return NetCache.Get().GetFloatOption(serverOption);
		}
		if (optionType == typeof(ulong))
		{
			return NetCache.Get().GetULongOption(serverOption);
		}
		Error.AddDevFatal("Options.GetServerOption() - option {0} has unsupported underlying type {1}", new object[]
		{
			option,
			optionType
		});
		return null;
	}

	// Token: 0x06007FA1 RID: 32673 RVA: 0x002965D0 File Offset: 0x002947D0
	private bool GetOptionImpl(Option option, out object val)
	{
		val = null;
		string text;
		ServerOption serverOption;
		ServerOptionFlag flag;
		if (this.m_clientOptionMap.TryGetValue(option, out text))
		{
			if (LocalOptions.Get().Has(text))
			{
				val = this.GetClientOption(option, text);
			}
		}
		else if (this.m_serverOptionMap.TryGetValue(option, out serverOption))
		{
			if (NetCache.Get().ClientOptionExists(serverOption))
			{
				val = this.GetServerOption(option, serverOption);
			}
		}
		else if (this.m_serverOptionFlagMap.TryGetValue(option, out flag))
		{
			ulong num;
			ulong num2;
			this.GetServerOptionFlagInfo(flag, out serverOption, out num, out num2);
			ulong ulongOption = NetCache.Get().GetULongOption(serverOption);
			if ((ulongOption & num2) > 0UL)
			{
				val = ((ulongOption & num) > 0UL);
			}
		}
		return val != null;
	}

	// Token: 0x06007FA2 RID: 32674 RVA: 0x0029667C File Offset: 0x0029487C
	private bool GetBoolImpl(Option option, out bool val)
	{
		val = false;
		object obj;
		if (this.GetOptionImpl(option, out obj))
		{
			val = (bool)obj;
			return true;
		}
		return false;
	}

	// Token: 0x06007FA3 RID: 32675 RVA: 0x002966A4 File Offset: 0x002948A4
	private bool GetIntImpl(Option option, out int val)
	{
		val = 0;
		object obj;
		if (this.GetOptionImpl(option, out obj))
		{
			val = (int)obj;
			return true;
		}
		return false;
	}

	// Token: 0x06007FA4 RID: 32676 RVA: 0x002966CC File Offset: 0x002948CC
	private bool GetLongImpl(Option option, out long val)
	{
		val = 0L;
		object obj;
		if (this.GetOptionImpl(option, out obj))
		{
			val = (long)obj;
			return true;
		}
		return false;
	}

	// Token: 0x06007FA5 RID: 32677 RVA: 0x002966F4 File Offset: 0x002948F4
	private bool GetFloatImpl(Option option, out float val)
	{
		val = 0f;
		object obj;
		if (this.GetOptionImpl(option, out obj))
		{
			val = (float)obj;
			return true;
		}
		return false;
	}

	// Token: 0x06007FA6 RID: 32678 RVA: 0x00296720 File Offset: 0x00294920
	private bool GetULongImpl(Option option, out ulong val)
	{
		val = 0UL;
		object obj;
		if (this.GetOptionImpl(option, out obj))
		{
			val = (ulong)obj;
			return true;
		}
		return false;
	}

	// Token: 0x06007FA7 RID: 32679 RVA: 0x00296748 File Offset: 0x00294948
	private bool GetStringImpl(Option option, out string val)
	{
		val = "";
		object obj;
		if (this.GetOptionImpl(option, out obj))
		{
			val = (string)obj;
			return true;
		}
		return false;
	}

	// Token: 0x06007FA8 RID: 32680 RVA: 0x00296774 File Offset: 0x00294974
	private bool GetEnumImpl<T>(Option option, out T val)
	{
		val = default(T);
		object genericVal;
		return this.GetOptionImpl(option, out genericVal) && this.TranslateEnumVal<T>(option, genericVal, out val);
	}

	// Token: 0x06007FA9 RID: 32681 RVA: 0x002967A0 File Offset: 0x002949A0
	private bool TranslateEnumVal<T>(Option option, object genericVal, out T val)
	{
		val = default(T);
		if (genericVal == null)
		{
			return true;
		}
		Type type = genericVal.GetType();
		Type typeFromHandle = typeof(T);
		bool result;
		try
		{
			if (type == typeFromHandle)
			{
				val = (T)((object)genericVal);
				result = true;
			}
			else
			{
				object obj = Convert.ChangeType(genericVal, Enum.GetUnderlyingType(typeFromHandle));
				val = (T)((object)obj);
				result = true;
			}
		}
		catch (Exception ex)
		{
			Debug.LogErrorFormat("Options.TranslateEnumVal() - option {0} has value {1} ({2}), which cannot be converted to type {3}: {4}", new object[]
			{
				option,
				genericVal,
				type,
				typeFromHandle,
				ex.ToString()
			});
			result = false;
		}
		return result;
	}

	// Token: 0x06007FAA RID: 32682 RVA: 0x00296848 File Offset: 0x00294A48
	private void RemoveListeners(Option option, object prevVal)
	{
		this.FireChangedEvent(option, prevVal, true);
		this.m_changedListeners.Remove(option);
	}

	// Token: 0x06007FAB RID: 32683 RVA: 0x00296860 File Offset: 0x00294A60
	private void FireChangedEvent(Option option, object prevVal, bool existed)
	{
		List<Options.ChangedListener> list;
		if (this.m_changedListeners.TryGetValue(option, out list))
		{
			Options.ChangedListener[] array = list.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(option, prevVal, existed);
			}
		}
		Options.ChangedListener[] array2 = this.m_globalChangedListeners.ToArray();
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].Fire(option, prevVal, existed);
		}
	}

	// Token: 0x040066C0 RID: 26304
	private const string DEPRECATED_NAME_PREFIX = "DEPRECATED";

	// Token: 0x040066C1 RID: 26305
	private const string FLAG_NAME_PREFIX = "FLAGS";

	// Token: 0x040066C2 RID: 26306
	private const int FLAG_BIT_COUNT = 64;

	// Token: 0x040066C3 RID: 26307
	private const float RCP_FLAG_BIT_COUNT = 0.015625f;

	// Token: 0x040066C4 RID: 26308
	private static readonly ServerOption[] s_serverFlagContainers = new ServerOption[]
	{
		ServerOption.FLAGS1,
		ServerOption.FLAGS2,
		ServerOption.FLAGS3,
		ServerOption.FLAGS4,
		ServerOption.FLAGS5,
		ServerOption.FLAGS6,
		ServerOption.FLAGS7,
		ServerOption.FLAGS8,
		ServerOption.FLAGS9,
		ServerOption.FLAGS10
	};

	// Token: 0x040066C5 RID: 26309
	private static Options s_instance;

	// Token: 0x040066C6 RID: 26310
	private Map<Option, string> m_clientOptionMap;

	// Token: 0x040066C7 RID: 26311
	private Map<Option, ServerOption> m_serverOptionMap;

	// Token: 0x040066C8 RID: 26312
	private Map<Option, ServerOptionFlag> m_serverOptionFlagMap;

	// Token: 0x040066C9 RID: 26313
	private Map<Option, List<Options.ChangedListener>> m_changedListeners = new Map<Option, List<Options.ChangedListener>>();

	// Token: 0x040066CA RID: 26314
	private List<Options.ChangedListener> m_globalChangedListeners = new List<Options.ChangedListener>();

	// Token: 0x020025B3 RID: 9651
	// (Invoke) Token: 0x06013432 RID: 78898
	public delegate void ChangedCallback(Option option, object prevValue, bool existed, object userData);

	// Token: 0x020025B4 RID: 9652
	private class ChangedListener : EventListener<Options.ChangedCallback>
	{
		// Token: 0x06013435 RID: 78901 RVA: 0x0052F773 File Offset: 0x0052D973
		public void Fire(Option option, object prevValue, bool didExist)
		{
			this.m_callback(option, prevValue, didExist, this.m_userData);
		}
	}
}
