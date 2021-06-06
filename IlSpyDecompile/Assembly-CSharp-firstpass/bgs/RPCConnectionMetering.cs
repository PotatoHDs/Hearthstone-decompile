using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bnet.protocol.config;

namespace bgs
{
	public class RPCConnectionMetering
	{
		private class StaticData
		{
			private uint m_serviceHash = uint.MaxValue;

			private uint m_methodId = uint.MaxValue;

			public string ServiceName { get; set; }

			public string MethodName { get; set; }

			public uint FixedCallCost { get; set; }

			public uint RateLimitCount { get; set; }

			public uint RateLimitSeconds { get; set; }

			public uint ServiceHash
			{
				get
				{
					return m_serviceHash;
				}
				set
				{
					m_serviceHash = value;
				}
			}

			public uint MethodId
			{
				get
				{
					return m_methodId;
				}
				set
				{
					m_methodId = value;
				}
			}

			public void FromProtocol(RPCMethodConfig method)
			{
				if (method.HasServiceName)
				{
					ServiceName = method.ServiceName;
				}
				if (method.HasMethodName)
				{
					MethodName = method.MethodName;
				}
				if (method.HasFixedCallCost)
				{
					FixedCallCost = method.FixedCallCost;
				}
				if (method.HasRateLimitCount)
				{
					RateLimitCount = method.RateLimitCount;
				}
				if (method.HasRateLimitSeconds)
				{
					RateLimitSeconds = method.RateLimitSeconds;
				}
			}

			public override string ToString()
			{
				string text = (string.IsNullOrEmpty(ServiceName) ? "<null>" : ServiceName);
				string text2 = (string.IsNullOrEmpty(MethodName) ? "<null>" : MethodName);
				return $"{text}.{text2} RateLimitCount={RateLimitCount} RateLimitSeconds={RateLimitSeconds} FixedCallCost={FixedCallCost}";
			}
		}

		private class Stats
		{
			public uint MethodCount { get; set; }

			public uint ServiceNameCount { get; set; }

			public uint MethodNameCount { get; set; }

			public uint FixedCalledCostCount { get; set; }

			public uint FixedPacketSizeCount { get; set; }

			public uint VariableMultiplierCount { get; set; }

			public uint MultiplierCount { get; set; }

			public uint RateLimitCountCount { get; set; }

			public uint RateLimitSecondsCount { get; set; }

			public uint MaxPacketSizeCount { get; set; }

			public uint MaxEncodedSizeCount { get; set; }

			public uint TimeoutCount { get; set; }

			public uint AggregatedRateLimitCountCount { get; set; }
		}

		private class CallTracker
		{
			private float[] m_calls;

			private int m_callIndex;

			private float m_numberOfSeconds;

			public CallTracker(uint maxCalls, uint timePeriodInSeconds)
			{
				if (maxCalls != 0 && timePeriodInSeconds != 0)
				{
					m_calls = new float[maxCalls];
					m_numberOfSeconds = timePeriodInSeconds;
				}
			}

			public bool CanCall(float now)
			{
				if (m_calls == null || m_calls.Length == 0)
				{
					return false;
				}
				if (m_callIndex < m_calls.Length)
				{
					m_calls[m_callIndex++] = now;
					return true;
				}
				if (now - m_calls[0] <= m_numberOfSeconds)
				{
					return false;
				}
				if (m_calls.Length == 1)
				{
					m_calls[0] = now;
					m_callIndex = 1;
					return true;
				}
				int i;
				for (i = 0; i + 1 < m_calls.Length && now - m_calls[i + 1] > m_numberOfSeconds; i++)
				{
				}
				int num = m_calls.Length - (i + 1);
				Array.Copy(m_calls, i + 1, m_calls, 0, num);
				m_callIndex = num;
				m_calls[m_callIndex++] = now;
				return true;
			}
		}

		private class RuntimeData
		{
			private uint m_finiteCallsLeft = uint.MaxValue;

			private CallTracker m_callTracker;

			public bool AlwaysAllow { get; set; }

			public bool AlwaysDeny { get; set; }

			public StaticData StaticData { get; set; }

			public uint FiniteCallsLeft
			{
				get
				{
					return m_finiteCallsLeft;
				}
				set
				{
					m_finiteCallsLeft = value;
				}
			}

			public CallTracker Tracker
			{
				get
				{
					return m_callTracker;
				}
				set
				{
					m_callTracker = value;
				}
			}

			public bool CanCall(float now)
			{
				if (m_callTracker == null)
				{
					return true;
				}
				return m_callTracker.CanCall(now);
			}

			public string GetServiceAndMethodNames()
			{
				string arg = ((StaticData != null && StaticData.ServiceName != null) ? StaticData.ServiceName : "<null>");
				string arg2 = ((StaticData != null && StaticData.MethodName != null) ? StaticData.MethodName : "<null>");
				return $"{arg}.{arg2}";
			}
		}

		private class MeteringData
		{
			private Stats m_staticDataStats = new Stats();

			private StaticData m_globalDefault;

			private Dictionary<uint, StaticData> m_serviceDefaultsByHash = new Dictionary<uint, StaticData>();

			private Dictionary<FullMethodId, StaticData> m_methodDefaultsByFullMethodId = new Dictionary<FullMethodId, StaticData>();

			private Dictionary<FullMethodId, RuntimeData> m_runtimeDataByFullMethodId = new Dictionary<FullMethodId, RuntimeData>();

			public Stats Stats => m_staticDataStats;

			public StaticData GlobalDefault
			{
				get
				{
					return m_globalDefault;
				}
				set
				{
					m_globalDefault = value;
				}
			}

			public Dictionary<uint, StaticData> ServiceDefaultsByHash => m_serviceDefaultsByHash;

			public Dictionary<FullMethodId, StaticData> MethodDefaultsByFullMethodId => m_methodDefaultsByFullMethodId;

			public Dictionary<FullMethodId, RuntimeData> RuntimeDataByFullMethodId => m_runtimeDataByFullMethodId;

			public float StartupPeriodEndTime { get; set; }

			public float StartupPeriodDurationSeconds { get; set; }

			public RuntimeData GetRuntimeData(FullMethodId id)
			{
				if (m_runtimeDataByFullMethodId.TryGetValue(id, out var value))
				{
					return value;
				}
				return null;
			}
		}

		private BattleNetLogSource m_log = new BattleNetLogSource("ConnectionMetering");

		private MeteringData m_data;

		private float m_connectPacketSentTime;

		private float TimeNow => (float)BattleNet.GetRealTimeSinceStartup();

		public void SetConnectionMeteringData(byte[] data, ServiceCollectionHelper serviceHelper)
		{
			m_data = new MeteringData();
			if (data == null || data.Length == 0 || serviceHelper == null)
			{
				m_log.LogError("Unable to retrieve Connection Metering data");
				return;
			}
			try
			{
				RPCMeterConfig rPCMeterConfig = RPCMeterConfigParser.ParseConfig(Encoding.ASCII.GetString(data));
				if (rPCMeterConfig == null || !rPCMeterConfig.IsInitialized)
				{
					m_data = null;
					throw new Exception("Unable to parse metering config protocol buffer.");
				}
				UpdateConfigStats(rPCMeterConfig);
				if (rPCMeterConfig.HasStartupPeriod)
				{
					m_data.StartupPeriodDurationSeconds = rPCMeterConfig.StartupPeriod;
					m_data.StartupPeriodEndTime = m_connectPacketSentTime + rPCMeterConfig.StartupPeriod;
					m_log.LogDebug("StartupPeriodDurationSeconds={0}", rPCMeterConfig.StartupPeriod);
					m_log.LogDebug("StartupPeriodEndTime={0}", m_data.StartupPeriodEndTime);
				}
				InitializeInternalState(rPCMeterConfig, serviceHelper);
			}
			catch (Exception ex)
			{
				m_data = null;
				m_log.LogError("EXCEPTION = {0} {1}", ex.Message, ex.StackTrace);
			}
			if (m_data == null)
			{
				m_log.LogError("Unable to parse Connection Metering data");
			}
		}

		public bool GetInStartupPeriod()
		{
			if (m_data == null)
			{
				return true;
			}
			float timeNow = TimeNow;
			if ((double)m_data.StartupPeriodEndTime > 0.0)
			{
				return timeNow < m_data.StartupPeriodEndTime;
			}
			return false;
		}

		public void SetConnectPacketSentToNow()
		{
			m_connectPacketSentTime = TimeNow;
		}

		public bool AllowRPCCall(ServiceDescriptor service, uint methodID)
		{
			if (service == null)
			{
				return false;
			}
			if (m_data == null)
			{
				return true;
			}
			uint hash = service.Hash;
			RuntimeData runtimedData = GetRuntimedData(service, methodID);
			if (runtimedData == null)
			{
				return true;
			}
			float timeNow = TimeNow;
			if (GetInStartupPeriod())
			{
				float num = m_data.StartupPeriodEndTime - timeNow;
				m_log.LogDebug("Allow (STARTUP PERIOD {0}) {1} ({2}:{3})", num, runtimedData.GetServiceAndMethodNames(), hash, methodID);
				return true;
			}
			if (runtimedData.AlwaysAllow)
			{
				m_log.LogDebug("Allow (ALWAYS ALLOW) {0} ({1}:{2})", runtimedData.GetServiceAndMethodNames(), hash, methodID);
				return true;
			}
			if (runtimedData.AlwaysDeny)
			{
				m_log.LogDebug("Deny (ALWAYS DENY) {0} ({1}:{2})", runtimedData.GetServiceAndMethodNames(), hash, methodID);
				return false;
			}
			if (runtimedData.FiniteCallsLeft != uint.MaxValue)
			{
				if (runtimedData.FiniteCallsLeft != 0)
				{
					m_log.LogDebug("Allow (FINITE CALLS LEFT {0}) {1} ({2}:{3})", runtimedData.FiniteCallsLeft, runtimedData.GetServiceAndMethodNames(), hash, methodID);
					runtimedData.FiniteCallsLeft--;
					return true;
				}
				m_log.LogDebug("Deny (FINITE CALLS LEFT 0) {0} ({1}:{2})", runtimedData.GetServiceAndMethodNames(), hash, methodID);
				return false;
			}
			bool flag = runtimedData.CanCall(timeNow);
			m_log.LogDebug("{0} (TRACKER) {1} ({2}:{3})", flag ? "Allow" : "Deny", runtimedData.GetServiceAndMethodNames(), hash, methodID);
			return flag;
		}

		private RuntimeData GetRuntimedData(ServiceDescriptor service, uint methodID)
		{
			uint num = service?.Id ?? 0;
			uint num2 = service?.Hash ?? 0;
			FullMethodId fullMethodId = new FullMethodId(num2, methodID);
			RuntimeData runtimeData = m_data.GetRuntimeData(fullMethodId);
			if (runtimeData == null)
			{
				runtimeData = new RuntimeData();
				m_data.RuntimeDataByFullMethodId[fullMethodId] = runtimeData;
				StaticData value = null;
				m_data.MethodDefaultsByFullMethodId.TryGetValue(fullMethodId, out value);
				if (value == null && num == 1 && num2 == 233634817 && methodID == 4)
				{
					return GetRuntimedData(service, 6u);
				}
				if (value == null)
				{
					m_data.ServiceDefaultsByHash.TryGetValue(num2, out value);
				}
				if (value == null && m_data.GlobalDefault != null)
				{
					value = m_data.GlobalDefault;
				}
				if (value == null)
				{
					string text = service?.GetMethodName(methodID);
					if (string.IsNullOrEmpty(text))
					{
						text = "<null>";
					}
					m_log.LogDebug("Always allowing ServiceHash={0} ServiceId={1} MethodId={2} {3} (no metering data)", num2, num, methodID, text);
					runtimeData.AlwaysAllow = true;
					return runtimeData;
				}
				runtimeData.StaticData = value;
				if (value.RateLimitCount == 0)
				{
					runtimeData.AlwaysDeny = true;
				}
				else if (value.RateLimitSeconds == 0)
				{
					runtimeData.FiniteCallsLeft = value.RateLimitCount;
				}
				else
				{
					runtimeData.Tracker = new CallTracker(value.RateLimitCount, value.RateLimitSeconds);
				}
			}
			return runtimeData;
		}

		private void InitializeInternalState(RPCMeterConfig config, ServiceCollectionHelper serviceHelper)
		{
			HashSet<string> hashSet = new HashSet<string>();
			List<string> list = new List<string>();
			int methodCount = config.MethodCount;
			for (int i = 0; i < methodCount; i++)
			{
				RPCMethodConfig rPCMethodConfig = config.Method[i];
				StaticData staticData = new StaticData();
				staticData.FromProtocol(rPCMethodConfig);
				if (!rPCMethodConfig.HasServiceName)
				{
					if (m_data.GlobalDefault == null)
					{
						m_data.GlobalDefault = staticData;
						m_log.LogDebug("Adding global default {0}", staticData);
					}
					else
					{
						m_log.LogWarning("Static data has two defaults, ignoring additional ones.");
					}
					continue;
				}
				ServiceDescriptor serviceDescriptor = (rPCMethodConfig.HasServiceHash ? serviceHelper.GetImportedServiceByHash(rPCMethodConfig.ServiceHash) : null);
				if (serviceDescriptor == null)
				{
					if (!list.Contains(rPCMethodConfig.ServiceName))
					{
						m_log.LogDebug("Ignoring not imported service {0} ServiceHash={1}", rPCMethodConfig.ServiceName, rPCMethodConfig.ServiceHash);
						list.Add(rPCMethodConfig.ServiceName);
					}
					continue;
				}
				staticData.ServiceHash = serviceDescriptor.Hash;
				if (rPCMethodConfig.HasMethodId)
				{
					MethodDescriptor methodDescriptor = serviceDescriptor.GetMethodDescriptor(rPCMethodConfig.MethodId);
					if (methodDescriptor == null)
					{
						m_log.LogDebug("Configuration specifies an unused method ServiceHash={0} ServiceId={1} {2}.{3}, ignoring.", serviceDescriptor.Hash, serviceDescriptor.Id, rPCMethodConfig.ServiceName, rPCMethodConfig.HasMethodName ? rPCMethodConfig.MethodName : "<unnamed method>");
						continue;
					}
					FullMethodId key = new FullMethodId(rPCMethodConfig.ServiceHash, rPCMethodConfig.MethodId);
					if (m_data.MethodDefaultsByFullMethodId.ContainsKey(key))
					{
						m_log.LogWarning("Default for method ServiceHash={0} MethodId={1} {2}.{3} already exists, ignoring extras.", key.ServiceHash, key.MethodId, rPCMethodConfig.ServiceName, methodDescriptor.Name);
						continue;
					}
					staticData.MethodId = methodDescriptor.Id;
					m_data.MethodDefaultsByFullMethodId[key] = staticData;
					m_log.LogDebug("Adding Method default ServiceHash={0} ServiceId={1} MethodId={2} {3}", serviceDescriptor.Hash, serviceDescriptor.Id, methodDescriptor.Id, staticData);
				}
				else
				{
					if (rPCMethodConfig.HasMethodName)
					{
						m_log.LogWarning("Configuration specifies a method by name, but is missing a MethodId ServiceHash={0} {2}.{3}, ignoring.", rPCMethodConfig.ServiceHash, rPCMethodConfig.ServiceName, rPCMethodConfig.MethodName);
						continue;
					}
					if (m_data.ServiceDefaultsByHash.ContainsKey(serviceDescriptor.Hash))
					{
						m_log.LogWarning("Default for service {0} ServiceHash={1} already exists, ignoring extras. {2}", serviceDescriptor.Name, serviceDescriptor.Hash, staticData);
						continue;
					}
					m_data.ServiceDefaultsByHash[serviceDescriptor.Hash] = staticData;
					m_log.LogDebug("Adding Service default ServiceHash={0} ServiceId={1} {2}", serviceDescriptor.Hash, serviceDescriptor.Id, staticData);
				}
				hashSet.Add(serviceDescriptor.Name);
			}
			foreach (KeyValuePair<uint, ServiceDescriptor> importedService in serviceHelper.ImportedServices)
			{
				if (!hashSet.Contains(importedService.Value.Name) && m_data.GlobalDefault == null)
				{
					m_log.LogDebug("Configuration for service {0} was not found and will not be metered.", importedService.Value.Name);
				}
			}
		}

		private void UpdateMethodStats(RPCMethodConfig method)
		{
			m_data.Stats.MethodCount++;
			if (method.HasServiceName)
			{
				m_data.Stats.ServiceNameCount++;
			}
			if (method.HasMethodName)
			{
				m_data.Stats.MethodNameCount++;
			}
			if (method.HasFixedCallCost)
			{
				m_data.Stats.FixedCalledCostCount++;
			}
			if (method.HasFixedPacketSize)
			{
				m_data.Stats.FixedPacketSizeCount++;
			}
			if (method.HasVariableMultiplier)
			{
				m_data.Stats.VariableMultiplierCount++;
			}
			if (method.HasMultiplier)
			{
				m_data.Stats.MultiplierCount++;
			}
			if (method.HasRateLimitCount)
			{
				m_data.Stats.RateLimitCountCount++;
				m_data.Stats.AggregatedRateLimitCountCount += method.RateLimitCount;
			}
			if (method.HasRateLimitSeconds)
			{
				m_data.Stats.RateLimitSecondsCount++;
			}
			if (method.HasMaxPacketSize)
			{
				m_data.Stats.MaxPacketSizeCount++;
			}
			if (method.HasMaxEncodedSize)
			{
				m_data.Stats.MaxEncodedSizeCount++;
			}
			if (method.HasTimeout)
			{
				m_data.Stats.TimeoutCount++;
			}
		}

		private bool UpdateConfigStats(RPCMeterConfig config)
		{
			int methodCount = config.MethodCount;
			for (int i = 0; i < methodCount; i++)
			{
				RPCMethodConfig method = config.Method[i];
				UpdateMethodStats(method);
			}
			Stats stats = m_data.Stats;
			m_log.LogDebug("Config Stats:");
			m_log.LogDebug("  MethodCount={0}", stats.MethodCount);
			m_log.LogDebug("  ServiceNameCount={0}", stats.ServiceNameCount);
			m_log.LogDebug("  MethodNameCount={0}", stats.MethodNameCount);
			m_log.LogDebug("  FixedCalledCostCount={0}", stats.FixedCalledCostCount);
			m_log.LogDebug("  FixedPacketSizeCount={0}", stats.FixedPacketSizeCount);
			m_log.LogDebug("  VariableMultiplierCount={0}", stats.VariableMultiplierCount);
			m_log.LogDebug("  MultiplierCount={0}", stats.MultiplierCount);
			m_log.LogDebug("  RateLimitCountCount={0}", stats.RateLimitCountCount);
			m_log.LogDebug("  RateLimitSecondsCount={0}", stats.RateLimitSecondsCount);
			m_log.LogDebug("  MaxPacketSizeCount={0}", stats.MaxPacketSizeCount);
			m_log.LogDebug("  MaxEncodedSizeCount={0}", stats.MaxEncodedSizeCount);
			m_log.LogDebug("  TimeoutCount={0}", stats.TimeoutCount);
			m_log.LogDebug("  AggregatedRateLimitCountCount={0}", stats.AggregatedRateLimitCountCount);
			return true;
		}
	}
}
