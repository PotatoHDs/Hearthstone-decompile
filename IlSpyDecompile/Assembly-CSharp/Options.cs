using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

public class Options
{
	public delegate void ChangedCallback(Option option, object prevValue, bool existed, object userData);

	private class ChangedListener : EventListener<ChangedCallback>
	{
		public void Fire(Option option, object prevValue, bool didExist)
		{
			m_callback(option, prevValue, didExist, m_userData);
		}
	}

	private const string DEPRECATED_NAME_PREFIX = "DEPRECATED";

	private const string FLAG_NAME_PREFIX = "FLAGS";

	private const int FLAG_BIT_COUNT = 64;

	private const float RCP_FLAG_BIT_COUNT = 0.015625f;

	private static readonly ServerOption[] s_serverFlagContainers = new ServerOption[10]
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

	private static Options s_instance;

	private Map<Option, string> m_clientOptionMap;

	private Map<Option, ServerOption> m_serverOptionMap;

	private Map<Option, ServerOptionFlag> m_serverOptionFlagMap;

	private Map<Option, List<ChangedListener>> m_changedListeners = new Map<Option, List<ChangedListener>>();

	private List<ChangedListener> m_globalChangedListeners = new List<ChangedListener>();

	public static Options Get()
	{
		if (s_instance == null)
		{
			s_instance = new Options();
			s_instance.Initialize();
		}
		return s_instance;
	}

	public bool IsClientOption(Option option)
	{
		return m_clientOptionMap.ContainsKey(option);
	}

	public bool IsServerOption(Option option)
	{
		return m_serverOptionMap.ContainsKey(option);
	}

	public bool IsServerOptionFlag(Option option)
	{
		return m_serverOptionFlagMap.ContainsKey(option);
	}

	public Map<Option, string> GetClientOptions()
	{
		return m_clientOptionMap;
	}

	public Map<Option, ServerOption> GetServerOptions()
	{
		return m_serverOptionMap;
	}

	public Type GetOptionType(Option option)
	{
		if (OptionDataTables.s_typeMap.TryGetValue(option, out var value))
		{
			return value;
		}
		if (m_serverOptionFlagMap.ContainsKey(option))
		{
			return typeof(bool);
		}
		return null;
	}

	public Type GetServerOptionType(ServerOption serverOption)
	{
		if (Array.Exists(s_serverFlagContainers, (ServerOption flagContainer) => flagContainer == serverOption))
		{
			return typeof(ulong);
		}
		foreach (KeyValuePair<Option, ServerOption> item in m_serverOptionMap)
		{
			if (item.Value == serverOption)
			{
				Option key = item.Key;
				if (OptionDataTables.s_typeMap.TryGetValue(key, out var value))
				{
					return value;
				}
				break;
			}
		}
		return null;
	}

	public static FormatType GetFormatType()
	{
		return Get().GetEnum<FormatType>(Option.FORMAT_TYPE);
	}

	public static void SetFormatType(FormatType formatType)
	{
		Get().SetEnum(Option.FORMAT_TYPE, formatType);
	}

	public static bool GetInRankedPlayMode()
	{
		return Get().GetBool(Option.IN_RANKED_PLAY_MODE);
	}

	public static void SetInRankedPlayMode(bool inRankedPlayMode)
	{
		Get().SetBool(Option.IN_RANKED_PLAY_MODE, inRankedPlayMode);
	}

	public bool RegisterChangedListener(Option option, ChangedCallback callback)
	{
		return RegisterChangedListener(option, callback, null);
	}

	public bool RegisterChangedListener(Option option, ChangedCallback callback, object userData)
	{
		ChangedListener changedListener = new ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		if (!m_changedListeners.TryGetValue(option, out var value))
		{
			value = new List<ChangedListener>();
			m_changedListeners.Add(option, value);
		}
		else if (value.Contains(changedListener))
		{
			return false;
		}
		value.Add(changedListener);
		return true;
	}

	public bool UnregisterChangedListener(Option option, ChangedCallback callback)
	{
		return UnregisterChangedListener(option, callback, null);
	}

	public bool UnregisterChangedListener(Option option, ChangedCallback callback, object userData)
	{
		ChangedListener changedListener = new ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		if (!m_changedListeners.TryGetValue(option, out var value))
		{
			return false;
		}
		if (!value.Remove(changedListener))
		{
			return false;
		}
		if (value.Count == 0)
		{
			m_changedListeners.Remove(option);
		}
		return true;
	}

	public bool RegisterGlobalChangedListener(ChangedCallback callback)
	{
		return RegisterGlobalChangedListener(callback, null);
	}

	public bool RegisterGlobalChangedListener(ChangedCallback callback, object userData)
	{
		ChangedListener changedListener = new ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		if (m_globalChangedListeners.Contains(changedListener))
		{
			return false;
		}
		m_globalChangedListeners.Add(changedListener);
		return true;
	}

	public bool UnregisterGlobalChangedListener(ChangedCallback callback)
	{
		return UnregisterGlobalChangedListener(callback, null);
	}

	public bool UnregisterGlobalChangedListener(ChangedCallback callback, object userData)
	{
		ChangedListener changedListener = new ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		return m_globalChangedListeners.Remove(changedListener);
	}

	public bool HasOption(Option option)
	{
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			return LocalOptions.Get().Has(value);
		}
		if (m_serverOptionMap.TryGetValue(option, out var value2))
		{
			return NetCache.Get().ClientOptionExists(value2);
		}
		if (m_serverOptionFlagMap.TryGetValue(option, out var value3))
		{
			return HasServerOptionFlag(value3);
		}
		return false;
	}

	public void DeleteOption(Option option)
	{
		ServerOption value2;
		ServerOptionFlag value3;
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			DeleteClientOption(option, value);
		}
		else if (m_serverOptionMap.TryGetValue(option, out value2))
		{
			DeleteServerOption(option, value2);
		}
		else if (m_serverOptionFlagMap.TryGetValue(option, out value3))
		{
			DeleteServerOptionFlag(option, value3);
		}
	}

	public void DeleteOption(string optionStr)
	{
		Option option = Option.INVALID;
		try
		{
			option = EnumUtils.GetEnum<Option>(optionStr, StringComparison.OrdinalIgnoreCase);
		}
		catch (ArgumentException)
		{
			Debug.LogErrorFormat("No matched option with '{0}'", optionStr);
			return;
		}
		DeleteOption(option);
	}

	public object GetOption(Option option)
	{
		if (GetOptionImpl(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value))
		{
			return value;
		}
		return null;
	}

	public object GetOption(Option option, object defaultVal)
	{
		if (GetOptionImpl(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public bool GetBool(Option option)
	{
		if (GetBoolImpl(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value))
		{
			return (bool)value;
		}
		return false;
	}

	public bool GetBool(Option option, bool defaultVal)
	{
		if (GetBoolImpl(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public int GetInt(Option option)
	{
		if (GetIntImpl(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value))
		{
			return (int)value;
		}
		return 0;
	}

	public int GetInt(Option option, int defaultVal)
	{
		if (GetIntImpl(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public long GetLong(Option option)
	{
		if (GetLongImpl(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value))
		{
			return (long)value;
		}
		return 0L;
	}

	public long GetLong(Option option, long defaultVal)
	{
		if (GetLongImpl(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public float GetFloat(Option option)
	{
		if (GetFloatImpl(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value))
		{
			return (float)value;
		}
		return 0f;
	}

	public float GetFloat(Option option, float defaultVal)
	{
		if (GetFloatImpl(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public ulong GetULong(Option option)
	{
		if (GetULongImpl(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value))
		{
			return (ulong)value;
		}
		return 0uL;
	}

	public ulong GetULong(Option option, ulong defaultVal)
	{
		if (GetULongImpl(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public string GetString(Option option)
	{
		if (GetStringImpl(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value))
		{
			return (string)value;
		}
		return "";
	}

	public string GetString(Option option, string defaultVal)
	{
		if (GetStringImpl(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public T GetEnum<T>(Option option)
	{
		if (GetEnumImpl<T>(option, out var val))
		{
			return val;
		}
		if (OptionDataTables.s_defaultsMap.TryGetValue(option, out var value) && TranslateEnumVal<T>(option, value, out val))
		{
			return val;
		}
		return default(T);
	}

	public T GetEnum<T>(Option option, T defaultVal)
	{
		if (GetEnumImpl<T>(option, out var val))
		{
			return val;
		}
		return defaultVal;
	}

	public void SetOption(Option option, object val)
	{
		Type optionType = GetOptionType(option);
		if (optionType == typeof(bool))
		{
			SetBool(option, (bool)val);
			return;
		}
		if (optionType == typeof(int))
		{
			SetInt(option, (int)val);
			return;
		}
		if (optionType == typeof(long))
		{
			SetLong(option, (long)val);
			return;
		}
		if (optionType == typeof(float))
		{
			SetFloat(option, (float)val);
			return;
		}
		if (optionType == typeof(string))
		{
			SetString(option, (string)val);
			return;
		}
		if (optionType == typeof(ulong))
		{
			SetULong(option, (ulong)val);
			return;
		}
		Error.AddDevFatal("Options.SetOption() - option {0} has unsupported underlying type {1}", option, optionType);
	}

	public void SetBool(Option option, bool val)
	{
		ServerOptionFlag value2;
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			bool flag = LocalOptions.Get().Has(value);
			bool @bool = LocalOptions.Get().GetBool(value);
			if (!flag || @bool != val)
			{
				LocalOptions.Get().Set(value, val);
				FireChangedEvent(option, @bool, flag);
			}
		}
		else if (m_serverOptionFlagMap.TryGetValue(option, out value2))
		{
			GetServerOptionFlagInfo(value2, out var container, out var flagBit, out var existenceBit);
			ulong uLongOption = NetCache.Get().GetULongOption(container);
			bool flag2 = (uLongOption & flagBit) != 0;
			bool flag3 = (uLongOption & existenceBit) != 0;
			if (!flag3 || flag2 != val)
			{
				ulong num = (val ? (uLongOption | flagBit) : (uLongOption & ~flagBit));
				num |= existenceBit;
				NetCache.Get().SetULongOption(container, num);
				FireChangedEvent(option, flag2, flag3);
			}
		}
	}

	public void SetInt(Option option, int val)
	{
		ServerOption value2;
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			bool flag = LocalOptions.Get().Has(value);
			int @int = LocalOptions.Get().GetInt(value);
			if (!flag || @int != val)
			{
				LocalOptions.Get().Set(value, val);
				FireChangedEvent(option, @int, flag);
			}
		}
		else if (m_serverOptionMap.TryGetValue(option, out value2))
		{
			int ret;
			bool intOption = NetCache.Get().GetIntOption(value2, out ret);
			if (!intOption || ret != val)
			{
				NetCache.Get().SetIntOption(value2, val);
				FireChangedEvent(option, ret, intOption);
			}
		}
	}

	public void SetLong(Option option, long val)
	{
		ServerOption value2;
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			bool flag = LocalOptions.Get().Has(value);
			long @long = LocalOptions.Get().GetLong(value);
			if (!flag || @long != val)
			{
				LocalOptions.Get().Set(value, val);
				FireChangedEvent(option, @long, flag);
			}
		}
		else if (m_serverOptionMap.TryGetValue(option, out value2))
		{
			long ret;
			bool longOption = NetCache.Get().GetLongOption(value2, out ret);
			if (!longOption || ret != val)
			{
				NetCache.Get().SetLongOption(value2, val);
				FireChangedEvent(option, ret, longOption);
			}
		}
	}

	public void SetFloat(Option option, float val)
	{
		ServerOption value2;
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			bool flag = LocalOptions.Get().Has(value);
			float @float = LocalOptions.Get().GetFloat(value);
			if (!flag || @float != val)
			{
				LocalOptions.Get().Set(value, val);
				FireChangedEvent(option, @float, flag);
			}
		}
		else if (m_serverOptionMap.TryGetValue(option, out value2))
		{
			float ret;
			bool floatOption = NetCache.Get().GetFloatOption(value2, out ret);
			if (!floatOption || ret != val)
			{
				NetCache.Get().SetFloatOption(value2, val);
				FireChangedEvent(option, ret, floatOption);
			}
		}
	}

	public void SetULong(Option option, ulong val)
	{
		ServerOption value2;
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			bool flag = LocalOptions.Get().Has(value);
			ulong uLong = LocalOptions.Get().GetULong(value);
			if (!flag || uLong != val)
			{
				LocalOptions.Get().Set(value, val);
				FireChangedEvent(option, uLong, flag);
			}
		}
		else if (m_serverOptionMap.TryGetValue(option, out value2))
		{
			ulong ret;
			bool uLongOption = NetCache.Get().GetULongOption(value2, out ret);
			if (!uLongOption || ret != val)
			{
				NetCache.Get().SetULongOption(value2, val);
				FireChangedEvent(option, ret, uLongOption);
			}
		}
	}

	public void SetString(Option option, string val)
	{
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			bool flag = LocalOptions.Get().Has(value);
			string @string = LocalOptions.Get().GetString(value);
			if (!flag || @string != val)
			{
				LocalOptions.Get().Set(value, val);
				FireChangedEvent(option, @string, flag);
			}
		}
	}

	public void SetEnum<T>(Option option, T val)
	{
		if (!Enum.IsDefined(typeof(T), val))
		{
			Error.AddDevFatal("Options.SetEnum() - {0} is not convertible to enum type {1} for option {2}", val, typeof(T), option);
			return;
		}
		Type optionType = GetOptionType(option);
		if (optionType == typeof(int))
		{
			SetInt(option, Convert.ToInt32(val));
			return;
		}
		if (optionType == typeof(long))
		{
			SetLong(option, Convert.ToInt64(val));
			return;
		}
		Error.AddDevFatal("Options.SetEnum() - option {0} has unsupported underlying type {1}", option, optionType);
	}

	private void Initialize()
	{
		Array values = Enum.GetValues(typeof(Option));
		Map<string, Option> map = new Map<string, Option>();
		foreach (Option item in values)
		{
			if (item != 0)
			{
				string key = item.ToString();
				map.Add(key, item);
			}
		}
		BuildClientOptionMap(map);
		BuildServerOptionMap(map);
		BuildServerOptionFlagMap(map);
	}

	private void BuildClientOptionMap(Map<string, Option> options)
	{
		m_clientOptionMap = new Map<Option, string>();
		foreach (ClientOption value3 in Enum.GetValues(typeof(ClientOption)))
		{
			if (value3 != 0)
			{
				string key = value3.ToString();
				if (!options.TryGetValue(key, out var value))
				{
					Debug.LogError($"Options.BuildClientOptionMap() - ClientOption {value3} is not mirrored in the Option enum");
					continue;
				}
				if (!OptionDataTables.s_typeMap.TryGetValue(value, out var _))
				{
					Debug.LogError($"Options.BuildClientOptionMap() - ClientOption {value3} has no type. Please add its type to the type map.");
					continue;
				}
				string @string = EnumUtils.GetString(value);
				m_clientOptionMap.Add(value, @string);
			}
		}
	}

	private void BuildServerOptionMap(Map<string, Option> options)
	{
		m_serverOptionMap = new Map<Option, ServerOption>();
		foreach (ServerOption value3 in Enum.GetValues(typeof(ServerOption)))
		{
			if (value3 == ServerOption.INVALID || value3 == ServerOption.LIMIT)
			{
				continue;
			}
			string text = value3.ToString();
			if (!text.StartsWith("FLAGS") && !text.StartsWith("DEPRECATED"))
			{
				Type value2;
				if (!options.TryGetValue(text, out var value))
				{
					Debug.LogError($"Options.BuildServerOptionMap() - ServerOption {value3} is not mirrored in the Option enum");
				}
				else if (!OptionDataTables.s_typeMap.TryGetValue(value, out value2))
				{
					Debug.LogError($"Options.BuildServerOptionMap() - ServerOption {value3} has no type. Please add its type to the type map.");
				}
				else if (value2 == typeof(bool))
				{
					Debug.LogError($"Options.BuildServerOptionMap() - ServerOption {value3} is a bool. You should convert it to a ServerOptionFlag.");
				}
				else
				{
					m_serverOptionMap.Add(value, value3);
				}
			}
		}
	}

	private void BuildServerOptionFlagMap(Map<string, Option> options)
	{
		m_serverOptionFlagMap = new Map<Option, ServerOptionFlag>();
		foreach (ServerOptionFlag value2 in Enum.GetValues(typeof(ServerOptionFlag)))
		{
			if (value2 == ServerOptionFlag.LIMIT)
			{
				continue;
			}
			string text = value2.ToString();
			if (!text.StartsWith("DEPRECATED"))
			{
				if (!options.TryGetValue(text, out var value))
				{
					Debug.LogError($"Options.BuildServerOptionFlagMap() - ServerOptionFlag {value2} is not mirrored in the Option enum");
				}
				else
				{
					m_serverOptionFlagMap.Add(value, value2);
				}
			}
		}
	}

	private void GetServerOptionFlagInfo(ServerOptionFlag flag, out ServerOption container, out ulong flagBit, out ulong existenceBit)
	{
		int num = 2 * (int)flag;
		int num2 = Mathf.FloorToInt((float)num * 0.015625f);
		int num3 = num % 64;
		int num4 = 1 + num3;
		container = s_serverFlagContainers[num2];
		flagBit = (ulong)(1L << num3);
		existenceBit = (ulong)(1L << num4);
	}

	private bool HasServerOptionFlag(ServerOptionFlag serverOptionFlag)
	{
		GetServerOptionFlagInfo(serverOptionFlag, out var container, out var _, out var existenceBit);
		return (NetCache.Get().GetULongOption(container) & existenceBit) != 0;
	}

	private void DeleteClientOption(Option option, string optionName)
	{
		if (LocalOptions.Get().Has(optionName))
		{
			object clientOption = GetClientOption(option, optionName);
			LocalOptions.Get().Delete(optionName);
			RemoveListeners(option, clientOption);
		}
	}

	private void DeleteServerOption(Option option, ServerOption serverOption)
	{
		if (NetCache.Get().ClientOptionExists(serverOption))
		{
			object serverOption2 = GetServerOption(option, serverOption);
			NetCache.Get().DeleteClientOption(serverOption);
			RemoveListeners(option, serverOption2);
		}
	}

	private void DeleteServerOptionFlag(Option option, ServerOptionFlag serverOptionFlag)
	{
		GetServerOptionFlagInfo(serverOptionFlag, out var container, out var flagBit, out var existenceBit);
		ulong uLongOption = NetCache.Get().GetULongOption(container);
		if ((uLongOption & existenceBit) != 0)
		{
			bool flag = (uLongOption & flagBit) != 0;
			uLongOption &= ~existenceBit;
			NetCache.Get().SetULongOption(container, uLongOption);
			RemoveListeners(option, flag);
		}
	}

	private object GetClientOption(Option option, string optionName)
	{
		Type optionType = GetOptionType(option);
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
		Error.AddDevFatal("Options.GetClientOption() - option {0} has unsupported underlying type {1}", option, optionType);
		return null;
	}

	private object GetServerOption(Option option, ServerOption serverOption)
	{
		Type optionType = GetOptionType(option);
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
		Error.AddDevFatal("Options.GetServerOption() - option {0} has unsupported underlying type {1}", option, optionType);
		return null;
	}

	private bool GetOptionImpl(Option option, out object val)
	{
		val = null;
		ServerOption value2;
		ServerOptionFlag value3;
		if (m_clientOptionMap.TryGetValue(option, out var value))
		{
			if (LocalOptions.Get().Has(value))
			{
				val = GetClientOption(option, value);
			}
		}
		else if (m_serverOptionMap.TryGetValue(option, out value2))
		{
			if (NetCache.Get().ClientOptionExists(value2))
			{
				val = GetServerOption(option, value2);
			}
		}
		else if (m_serverOptionFlagMap.TryGetValue(option, out value3))
		{
			GetServerOptionFlagInfo(value3, out value2, out var flagBit, out var existenceBit);
			ulong uLongOption = NetCache.Get().GetULongOption(value2);
			if ((uLongOption & existenceBit) != 0)
			{
				val = (uLongOption & flagBit) != 0;
			}
		}
		return val != null;
	}

	private bool GetBoolImpl(Option option, out bool val)
	{
		val = false;
		if (GetOptionImpl(option, out var val2))
		{
			val = (bool)val2;
			return true;
		}
		return false;
	}

	private bool GetIntImpl(Option option, out int val)
	{
		val = 0;
		if (GetOptionImpl(option, out var val2))
		{
			val = (int)val2;
			return true;
		}
		return false;
	}

	private bool GetLongImpl(Option option, out long val)
	{
		val = 0L;
		if (GetOptionImpl(option, out var val2))
		{
			val = (long)val2;
			return true;
		}
		return false;
	}

	private bool GetFloatImpl(Option option, out float val)
	{
		val = 0f;
		if (GetOptionImpl(option, out var val2))
		{
			val = (float)val2;
			return true;
		}
		return false;
	}

	private bool GetULongImpl(Option option, out ulong val)
	{
		val = 0uL;
		if (GetOptionImpl(option, out var val2))
		{
			val = (ulong)val2;
			return true;
		}
		return false;
	}

	private bool GetStringImpl(Option option, out string val)
	{
		val = "";
		if (GetOptionImpl(option, out var val2))
		{
			val = (string)val2;
			return true;
		}
		return false;
	}

	private bool GetEnumImpl<T>(Option option, out T val)
	{
		val = default(T);
		if (GetOptionImpl(option, out var val2))
		{
			return TranslateEnumVal<T>(option, val2, out val);
		}
		return false;
	}

	private bool TranslateEnumVal<T>(Option option, object genericVal, out T val)
	{
		val = default(T);
		if (genericVal == null)
		{
			return true;
		}
		Type type = genericVal.GetType();
		Type typeFromHandle = typeof(T);
		try
		{
			if (type == typeFromHandle)
			{
				val = (T)genericVal;
				return true;
			}
			object obj = Convert.ChangeType(genericVal, Enum.GetUnderlyingType(typeFromHandle));
			val = (T)obj;
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogErrorFormat("Options.TranslateEnumVal() - option {0} has value {1} ({2}), which cannot be converted to type {3}: {4}", option, genericVal, type, typeFromHandle, ex.ToString());
			return false;
		}
	}

	private void RemoveListeners(Option option, object prevVal)
	{
		FireChangedEvent(option, prevVal, existed: true);
		m_changedListeners.Remove(option);
	}

	private void FireChangedEvent(Option option, object prevVal, bool existed)
	{
		if (m_changedListeners.TryGetValue(option, out var value))
		{
			ChangedListener[] array = value.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(option, prevVal, existed);
			}
		}
		ChangedListener[] array2 = m_globalChangedListeners.ToArray();
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].Fire(option, prevVal, existed);
		}
	}
}
