using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bnet.protocol.config;

namespace bgs
{
	// Token: 0x02000234 RID: 564
	public class RPCConnectionMetering
	{
		// Token: 0x06002394 RID: 9108 RVA: 0x0007D2BC File Offset: 0x0007B4BC
		public void SetConnectionMeteringData(byte[] data, ServiceCollectionHelper serviceHelper)
		{
			this.m_data = new RPCConnectionMetering.MeteringData();
			if (data == null || data.Length == 0 || serviceHelper == null)
			{
				this.m_log.LogError("Unable to retrieve Connection Metering data");
				return;
			}
			try
			{
				RPCMeterConfig rpcmeterConfig = RPCMeterConfigParser.ParseConfig(Encoding.ASCII.GetString(data));
				if (rpcmeterConfig == null || !rpcmeterConfig.IsInitialized)
				{
					this.m_data = null;
					throw new Exception("Unable to parse metering config protocol buffer.");
				}
				this.UpdateConfigStats(rpcmeterConfig);
				if (rpcmeterConfig.HasStartupPeriod)
				{
					this.m_data.StartupPeriodDurationSeconds = rpcmeterConfig.StartupPeriod;
					this.m_data.StartupPeriodEndTime = this.m_connectPacketSentTime + rpcmeterConfig.StartupPeriod;
					this.m_log.LogDebug("StartupPeriodDurationSeconds={0}", new object[]
					{
						rpcmeterConfig.StartupPeriod
					});
					this.m_log.LogDebug("StartupPeriodEndTime={0}", new object[]
					{
						this.m_data.StartupPeriodEndTime
					});
				}
				this.InitializeInternalState(rpcmeterConfig, serviceHelper);
			}
			catch (Exception ex)
			{
				this.m_data = null;
				this.m_log.LogError("EXCEPTION = {0} {1}", new object[]
				{
					ex.Message,
					ex.StackTrace
				});
			}
			if (this.m_data == null)
			{
				this.m_log.LogError("Unable to parse Connection Metering data");
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0007D408 File Offset: 0x0007B608
		public bool GetInStartupPeriod()
		{
			if (this.m_data == null)
			{
				return true;
			}
			float timeNow = this.TimeNow;
			return (double)this.m_data.StartupPeriodEndTime > 0.0 && timeNow < this.m_data.StartupPeriodEndTime;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0007D44D File Offset: 0x0007B64D
		public void SetConnectPacketSentToNow()
		{
			this.m_connectPacketSentTime = this.TimeNow;
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0007D45B File Offset: 0x0007B65B
		private float TimeNow
		{
			get
			{
				return (float)BattleNet.GetRealTimeSinceStartup();
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0007D464 File Offset: 0x0007B664
		public bool AllowRPCCall(ServiceDescriptor service, uint methodID)
		{
			if (service == null)
			{
				return false;
			}
			if (this.m_data == null)
			{
				return true;
			}
			uint hash = service.Hash;
			RPCConnectionMetering.RuntimeData runtimedData = this.GetRuntimedData(service, methodID);
			if (runtimedData == null)
			{
				return true;
			}
			float timeNow = this.TimeNow;
			if (this.GetInStartupPeriod())
			{
				float num = this.m_data.StartupPeriodEndTime - timeNow;
				this.m_log.LogDebug("Allow (STARTUP PERIOD {0}) {1} ({2}:{3})", new object[]
				{
					num,
					runtimedData.GetServiceAndMethodNames(),
					hash,
					methodID
				});
				return true;
			}
			if (runtimedData.AlwaysAllow)
			{
				this.m_log.LogDebug("Allow (ALWAYS ALLOW) {0} ({1}:{2})", new object[]
				{
					runtimedData.GetServiceAndMethodNames(),
					hash,
					methodID
				});
				return true;
			}
			if (runtimedData.AlwaysDeny)
			{
				this.m_log.LogDebug("Deny (ALWAYS DENY) {0} ({1}:{2})", new object[]
				{
					runtimedData.GetServiceAndMethodNames(),
					hash,
					methodID
				});
				return false;
			}
			if (runtimedData.FiniteCallsLeft == 4294967295U)
			{
				bool flag = runtimedData.CanCall(timeNow);
				this.m_log.LogDebug("{0} (TRACKER) {1} ({2}:{3})", new object[]
				{
					flag ? "Allow" : "Deny",
					runtimedData.GetServiceAndMethodNames(),
					hash,
					methodID
				});
				return flag;
			}
			if (runtimedData.FiniteCallsLeft > 0U)
			{
				this.m_log.LogDebug("Allow (FINITE CALLS LEFT {0}) {1} ({2}:{3})", new object[]
				{
					runtimedData.FiniteCallsLeft,
					runtimedData.GetServiceAndMethodNames(),
					hash,
					methodID
				});
				RPCConnectionMetering.RuntimeData runtimeData = runtimedData;
				uint finiteCallsLeft = runtimeData.FiniteCallsLeft - 1U;
				runtimeData.FiniteCallsLeft = finiteCallsLeft;
				return true;
			}
			this.m_log.LogDebug("Deny (FINITE CALLS LEFT 0) {0} ({1}:{2})", new object[]
			{
				runtimedData.GetServiceAndMethodNames(),
				hash,
				methodID
			});
			return false;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0007D650 File Offset: 0x0007B850
		private RPCConnectionMetering.RuntimeData GetRuntimedData(ServiceDescriptor service, uint methodID)
		{
			uint num = (service == null) ? 0U : service.Id;
			uint num2 = (service == null) ? 0U : service.Hash;
			FullMethodId fullMethodId = new FullMethodId(num2, methodID);
			RPCConnectionMetering.RuntimeData runtimeData = this.m_data.GetRuntimeData(fullMethodId);
			if (runtimeData == null)
			{
				runtimeData = new RPCConnectionMetering.RuntimeData();
				this.m_data.RuntimeDataByFullMethodId[fullMethodId] = runtimeData;
				RPCConnectionMetering.StaticData staticData = null;
				this.m_data.MethodDefaultsByFullMethodId.TryGetValue(fullMethodId, out staticData);
				if (staticData == null && num == 1U && num2 == 233634817U && methodID == 4U)
				{
					return this.GetRuntimedData(service, 6U);
				}
				if (staticData == null)
				{
					this.m_data.ServiceDefaultsByHash.TryGetValue(num2, out staticData);
				}
				if (staticData == null && this.m_data.GlobalDefault != null)
				{
					staticData = this.m_data.GlobalDefault;
				}
				if (staticData == null)
				{
					string text = (service == null) ? null : service.GetMethodName(methodID);
					if (string.IsNullOrEmpty(text))
					{
						text = "<null>";
					}
					this.m_log.LogDebug("Always allowing ServiceHash={0} ServiceId={1} MethodId={2} {3} (no metering data)", new object[]
					{
						num2,
						num,
						methodID,
						text
					});
					runtimeData.AlwaysAllow = true;
					return runtimeData;
				}
				runtimeData.StaticData = staticData;
				if (staticData.RateLimitCount == 0U)
				{
					runtimeData.AlwaysDeny = true;
				}
				else if (staticData.RateLimitSeconds == 0U)
				{
					runtimeData.FiniteCallsLeft = staticData.RateLimitCount;
				}
				else
				{
					runtimeData.Tracker = new RPCConnectionMetering.CallTracker(staticData.RateLimitCount, staticData.RateLimitSeconds);
				}
			}
			return runtimeData;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0007D7C4 File Offset: 0x0007B9C4
		private void InitializeInternalState(RPCMeterConfig config, ServiceCollectionHelper serviceHelper)
		{
			HashSet<string> hashSet = new HashSet<string>();
			List<string> list = new List<string>();
			int methodCount = config.MethodCount;
			for (int i = 0; i < methodCount; i++)
			{
				RPCMethodConfig rpcmethodConfig = config.Method[i];
				RPCConnectionMetering.StaticData staticData = new RPCConnectionMetering.StaticData();
				staticData.FromProtocol(rpcmethodConfig);
				if (!rpcmethodConfig.HasServiceName)
				{
					if (this.m_data.GlobalDefault == null)
					{
						this.m_data.GlobalDefault = staticData;
						this.m_log.LogDebug("Adding global default {0}", new object[]
						{
							staticData
						});
					}
					else
					{
						this.m_log.LogWarning("Static data has two defaults, ignoring additional ones.");
					}
				}
				else
				{
					ServiceDescriptor serviceDescriptor = rpcmethodConfig.HasServiceHash ? serviceHelper.GetImportedServiceByHash(rpcmethodConfig.ServiceHash) : null;
					if (serviceDescriptor == null)
					{
						if (!list.Contains(rpcmethodConfig.ServiceName))
						{
							this.m_log.LogDebug("Ignoring not imported service {0} ServiceHash={1}", new object[]
							{
								rpcmethodConfig.ServiceName,
								rpcmethodConfig.ServiceHash
							});
							list.Add(rpcmethodConfig.ServiceName);
						}
					}
					else
					{
						staticData.ServiceHash = serviceDescriptor.Hash;
						if (rpcmethodConfig.HasMethodId)
						{
							MethodDescriptor methodDescriptor = serviceDescriptor.GetMethodDescriptor(rpcmethodConfig.MethodId);
							if (methodDescriptor == null)
							{
								this.m_log.LogDebug("Configuration specifies an unused method ServiceHash={0} ServiceId={1} {2}.{3}, ignoring.", new object[]
								{
									serviceDescriptor.Hash,
									serviceDescriptor.Id,
									rpcmethodConfig.ServiceName,
									rpcmethodConfig.HasMethodName ? rpcmethodConfig.MethodName : "<unnamed method>"
								});
								goto IL_368;
							}
							FullMethodId key = new FullMethodId(rpcmethodConfig.ServiceHash, rpcmethodConfig.MethodId);
							if (this.m_data.MethodDefaultsByFullMethodId.ContainsKey(key))
							{
								this.m_log.LogWarning("Default for method ServiceHash={0} MethodId={1} {2}.{3} already exists, ignoring extras.", new object[]
								{
									key.ServiceHash,
									key.MethodId,
									rpcmethodConfig.ServiceName,
									methodDescriptor.Name
								});
								goto IL_368;
							}
							staticData.MethodId = methodDescriptor.Id;
							this.m_data.MethodDefaultsByFullMethodId[key] = staticData;
							this.m_log.LogDebug("Adding Method default ServiceHash={0} ServiceId={1} MethodId={2} {3}", new object[]
							{
								serviceDescriptor.Hash,
								serviceDescriptor.Id,
								methodDescriptor.Id,
								staticData
							});
						}
						else
						{
							if (rpcmethodConfig.HasMethodName)
							{
								this.m_log.LogWarning("Configuration specifies a method by name, but is missing a MethodId ServiceHash={0} {2}.{3}, ignoring.", new object[]
								{
									rpcmethodConfig.ServiceHash,
									rpcmethodConfig.ServiceName,
									rpcmethodConfig.MethodName
								});
								goto IL_368;
							}
							if (this.m_data.ServiceDefaultsByHash.ContainsKey(serviceDescriptor.Hash))
							{
								this.m_log.LogWarning("Default for service {0} ServiceHash={1} already exists, ignoring extras. {2}", new object[]
								{
									serviceDescriptor.Name,
									serviceDescriptor.Hash,
									staticData
								});
								goto IL_368;
							}
							this.m_data.ServiceDefaultsByHash[serviceDescriptor.Hash] = staticData;
							this.m_log.LogDebug("Adding Service default ServiceHash={0} ServiceId={1} {2}", new object[]
							{
								serviceDescriptor.Hash,
								serviceDescriptor.Id,
								staticData
							});
						}
						hashSet.Add(serviceDescriptor.Name);
					}
				}
				IL_368:;
			}
			foreach (KeyValuePair<uint, ServiceDescriptor> keyValuePair in serviceHelper.ImportedServices)
			{
				if (!hashSet.Contains(keyValuePair.Value.Name) && this.m_data.GlobalDefault == null)
				{
					this.m_log.LogDebug("Configuration for service {0} was not found and will not be metered.", new object[]
					{
						keyValuePair.Value.Name
					});
				}
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0007DBCC File Offset: 0x0007BDCC
		private void UpdateMethodStats(RPCMethodConfig method)
		{
			RPCConnectionMetering.Stats stats = this.m_data.Stats;
			uint num = stats.MethodCount + 1U;
			stats.MethodCount = num;
			if (method.HasServiceName)
			{
				RPCConnectionMetering.Stats stats2 = this.m_data.Stats;
				num = stats2.ServiceNameCount + 1U;
				stats2.ServiceNameCount = num;
			}
			if (method.HasMethodName)
			{
				RPCConnectionMetering.Stats stats3 = this.m_data.Stats;
				num = stats3.MethodNameCount + 1U;
				stats3.MethodNameCount = num;
			}
			if (method.HasFixedCallCost)
			{
				RPCConnectionMetering.Stats stats4 = this.m_data.Stats;
				num = stats4.FixedCalledCostCount + 1U;
				stats4.FixedCalledCostCount = num;
			}
			if (method.HasFixedPacketSize)
			{
				RPCConnectionMetering.Stats stats5 = this.m_data.Stats;
				num = stats5.FixedPacketSizeCount + 1U;
				stats5.FixedPacketSizeCount = num;
			}
			if (method.HasVariableMultiplier)
			{
				RPCConnectionMetering.Stats stats6 = this.m_data.Stats;
				num = stats6.VariableMultiplierCount + 1U;
				stats6.VariableMultiplierCount = num;
			}
			if (method.HasMultiplier)
			{
				RPCConnectionMetering.Stats stats7 = this.m_data.Stats;
				num = stats7.MultiplierCount + 1U;
				stats7.MultiplierCount = num;
			}
			if (method.HasRateLimitCount)
			{
				RPCConnectionMetering.Stats stats8 = this.m_data.Stats;
				num = stats8.RateLimitCountCount + 1U;
				stats8.RateLimitCountCount = num;
				this.m_data.Stats.AggregatedRateLimitCountCount += method.RateLimitCount;
			}
			if (method.HasRateLimitSeconds)
			{
				RPCConnectionMetering.Stats stats9 = this.m_data.Stats;
				num = stats9.RateLimitSecondsCount + 1U;
				stats9.RateLimitSecondsCount = num;
			}
			if (method.HasMaxPacketSize)
			{
				RPCConnectionMetering.Stats stats10 = this.m_data.Stats;
				num = stats10.MaxPacketSizeCount + 1U;
				stats10.MaxPacketSizeCount = num;
			}
			if (method.HasMaxEncodedSize)
			{
				RPCConnectionMetering.Stats stats11 = this.m_data.Stats;
				num = stats11.MaxEncodedSizeCount + 1U;
				stats11.MaxEncodedSizeCount = num;
			}
			if (method.HasTimeout)
			{
				RPCConnectionMetering.Stats stats12 = this.m_data.Stats;
				num = stats12.TimeoutCount + 1U;
				stats12.TimeoutCount = num;
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0007DD88 File Offset: 0x0007BF88
		private bool UpdateConfigStats(RPCMeterConfig config)
		{
			int methodCount = config.MethodCount;
			for (int i = 0; i < methodCount; i++)
			{
				RPCMethodConfig method = config.Method[i];
				this.UpdateMethodStats(method);
			}
			RPCConnectionMetering.Stats stats = this.m_data.Stats;
			this.m_log.LogDebug("Config Stats:");
			this.m_log.LogDebug("  MethodCount={0}", new object[]
			{
				stats.MethodCount
			});
			this.m_log.LogDebug("  ServiceNameCount={0}", new object[]
			{
				stats.ServiceNameCount
			});
			this.m_log.LogDebug("  MethodNameCount={0}", new object[]
			{
				stats.MethodNameCount
			});
			this.m_log.LogDebug("  FixedCalledCostCount={0}", new object[]
			{
				stats.FixedCalledCostCount
			});
			this.m_log.LogDebug("  FixedPacketSizeCount={0}", new object[]
			{
				stats.FixedPacketSizeCount
			});
			this.m_log.LogDebug("  VariableMultiplierCount={0}", new object[]
			{
				stats.VariableMultiplierCount
			});
			this.m_log.LogDebug("  MultiplierCount={0}", new object[]
			{
				stats.MultiplierCount
			});
			this.m_log.LogDebug("  RateLimitCountCount={0}", new object[]
			{
				stats.RateLimitCountCount
			});
			this.m_log.LogDebug("  RateLimitSecondsCount={0}", new object[]
			{
				stats.RateLimitSecondsCount
			});
			this.m_log.LogDebug("  MaxPacketSizeCount={0}", new object[]
			{
				stats.MaxPacketSizeCount
			});
			this.m_log.LogDebug("  MaxEncodedSizeCount={0}", new object[]
			{
				stats.MaxEncodedSizeCount
			});
			this.m_log.LogDebug("  TimeoutCount={0}", new object[]
			{
				stats.TimeoutCount
			});
			this.m_log.LogDebug("  AggregatedRateLimitCountCount={0}", new object[]
			{
				stats.AggregatedRateLimitCountCount
			});
			return true;
		}

		// Token: 0x04000E94 RID: 3732
		private BattleNetLogSource m_log = new BattleNetLogSource("ConnectionMetering");

		// Token: 0x04000E95 RID: 3733
		private RPCConnectionMetering.MeteringData m_data;

		// Token: 0x04000E96 RID: 3734
		private float m_connectPacketSentTime;

		// Token: 0x020006C5 RID: 1733
		private class StaticData
		{
			// Token: 0x17001295 RID: 4757
			// (get) Token: 0x0600628A RID: 25226 RVA: 0x00128956 File Offset: 0x00126B56
			// (set) Token: 0x0600628B RID: 25227 RVA: 0x0012895E File Offset: 0x00126B5E
			public string ServiceName { get; set; }

			// Token: 0x17001296 RID: 4758
			// (get) Token: 0x0600628C RID: 25228 RVA: 0x00128967 File Offset: 0x00126B67
			// (set) Token: 0x0600628D RID: 25229 RVA: 0x0012896F File Offset: 0x00126B6F
			public string MethodName { get; set; }

			// Token: 0x17001297 RID: 4759
			// (get) Token: 0x0600628E RID: 25230 RVA: 0x00128978 File Offset: 0x00126B78
			// (set) Token: 0x0600628F RID: 25231 RVA: 0x00128980 File Offset: 0x00126B80
			public uint FixedCallCost { get; set; }

			// Token: 0x17001298 RID: 4760
			// (get) Token: 0x06006290 RID: 25232 RVA: 0x00128989 File Offset: 0x00126B89
			// (set) Token: 0x06006291 RID: 25233 RVA: 0x00128991 File Offset: 0x00126B91
			public uint RateLimitCount { get; set; }

			// Token: 0x17001299 RID: 4761
			// (get) Token: 0x06006292 RID: 25234 RVA: 0x0012899A File Offset: 0x00126B9A
			// (set) Token: 0x06006293 RID: 25235 RVA: 0x001289A2 File Offset: 0x00126BA2
			public uint RateLimitSeconds { get; set; }

			// Token: 0x1700129A RID: 4762
			// (get) Token: 0x06006294 RID: 25236 RVA: 0x001289AB File Offset: 0x00126BAB
			// (set) Token: 0x06006295 RID: 25237 RVA: 0x001289B3 File Offset: 0x00126BB3
			public uint ServiceHash
			{
				get
				{
					return this.m_serviceHash;
				}
				set
				{
					this.m_serviceHash = value;
				}
			}

			// Token: 0x1700129B RID: 4763
			// (get) Token: 0x06006296 RID: 25238 RVA: 0x001289BC File Offset: 0x00126BBC
			// (set) Token: 0x06006297 RID: 25239 RVA: 0x001289C4 File Offset: 0x00126BC4
			public uint MethodId
			{
				get
				{
					return this.m_methodId;
				}
				set
				{
					this.m_methodId = value;
				}
			}

			// Token: 0x06006298 RID: 25240 RVA: 0x001289D0 File Offset: 0x00126BD0
			public void FromProtocol(RPCMethodConfig method)
			{
				if (method.HasServiceName)
				{
					this.ServiceName = method.ServiceName;
				}
				if (method.HasMethodName)
				{
					this.MethodName = method.MethodName;
				}
				if (method.HasFixedCallCost)
				{
					this.FixedCallCost = method.FixedCallCost;
				}
				if (method.HasRateLimitCount)
				{
					this.RateLimitCount = method.RateLimitCount;
				}
				if (method.HasRateLimitSeconds)
				{
					this.RateLimitSeconds = method.RateLimitSeconds;
				}
			}

			// Token: 0x06006299 RID: 25241 RVA: 0x00128A44 File Offset: 0x00126C44
			public override string ToString()
			{
				string text = string.IsNullOrEmpty(this.ServiceName) ? "<null>" : this.ServiceName;
				string text2 = string.IsNullOrEmpty(this.MethodName) ? "<null>" : this.MethodName;
				return string.Format("{0}.{1} RateLimitCount={2} RateLimitSeconds={3} FixedCallCost={4}", new object[]
				{
					text,
					text2,
					this.RateLimitCount,
					this.RateLimitSeconds,
					this.FixedCallCost
				});
			}

			// Token: 0x0400222A RID: 8746
			private uint m_serviceHash = uint.MaxValue;

			// Token: 0x0400222B RID: 8747
			private uint m_methodId = uint.MaxValue;
		}

		// Token: 0x020006C6 RID: 1734
		private class Stats
		{
			// Token: 0x1700129C RID: 4764
			// (get) Token: 0x0600629B RID: 25243 RVA: 0x00128ADF File Offset: 0x00126CDF
			// (set) Token: 0x0600629C RID: 25244 RVA: 0x00128AE7 File Offset: 0x00126CE7
			public uint MethodCount { get; set; }

			// Token: 0x1700129D RID: 4765
			// (get) Token: 0x0600629D RID: 25245 RVA: 0x00128AF0 File Offset: 0x00126CF0
			// (set) Token: 0x0600629E RID: 25246 RVA: 0x00128AF8 File Offset: 0x00126CF8
			public uint ServiceNameCount { get; set; }

			// Token: 0x1700129E RID: 4766
			// (get) Token: 0x0600629F RID: 25247 RVA: 0x00128B01 File Offset: 0x00126D01
			// (set) Token: 0x060062A0 RID: 25248 RVA: 0x00128B09 File Offset: 0x00126D09
			public uint MethodNameCount { get; set; }

			// Token: 0x1700129F RID: 4767
			// (get) Token: 0x060062A1 RID: 25249 RVA: 0x00128B12 File Offset: 0x00126D12
			// (set) Token: 0x060062A2 RID: 25250 RVA: 0x00128B1A File Offset: 0x00126D1A
			public uint FixedCalledCostCount { get; set; }

			// Token: 0x170012A0 RID: 4768
			// (get) Token: 0x060062A3 RID: 25251 RVA: 0x00128B23 File Offset: 0x00126D23
			// (set) Token: 0x060062A4 RID: 25252 RVA: 0x00128B2B File Offset: 0x00126D2B
			public uint FixedPacketSizeCount { get; set; }

			// Token: 0x170012A1 RID: 4769
			// (get) Token: 0x060062A5 RID: 25253 RVA: 0x00128B34 File Offset: 0x00126D34
			// (set) Token: 0x060062A6 RID: 25254 RVA: 0x00128B3C File Offset: 0x00126D3C
			public uint VariableMultiplierCount { get; set; }

			// Token: 0x170012A2 RID: 4770
			// (get) Token: 0x060062A7 RID: 25255 RVA: 0x00128B45 File Offset: 0x00126D45
			// (set) Token: 0x060062A8 RID: 25256 RVA: 0x00128B4D File Offset: 0x00126D4D
			public uint MultiplierCount { get; set; }

			// Token: 0x170012A3 RID: 4771
			// (get) Token: 0x060062A9 RID: 25257 RVA: 0x00128B56 File Offset: 0x00126D56
			// (set) Token: 0x060062AA RID: 25258 RVA: 0x00128B5E File Offset: 0x00126D5E
			public uint RateLimitCountCount { get; set; }

			// Token: 0x170012A4 RID: 4772
			// (get) Token: 0x060062AB RID: 25259 RVA: 0x00128B67 File Offset: 0x00126D67
			// (set) Token: 0x060062AC RID: 25260 RVA: 0x00128B6F File Offset: 0x00126D6F
			public uint RateLimitSecondsCount { get; set; }

			// Token: 0x170012A5 RID: 4773
			// (get) Token: 0x060062AD RID: 25261 RVA: 0x00128B78 File Offset: 0x00126D78
			// (set) Token: 0x060062AE RID: 25262 RVA: 0x00128B80 File Offset: 0x00126D80
			public uint MaxPacketSizeCount { get; set; }

			// Token: 0x170012A6 RID: 4774
			// (get) Token: 0x060062AF RID: 25263 RVA: 0x00128B89 File Offset: 0x00126D89
			// (set) Token: 0x060062B0 RID: 25264 RVA: 0x00128B91 File Offset: 0x00126D91
			public uint MaxEncodedSizeCount { get; set; }

			// Token: 0x170012A7 RID: 4775
			// (get) Token: 0x060062B1 RID: 25265 RVA: 0x00128B9A File Offset: 0x00126D9A
			// (set) Token: 0x060062B2 RID: 25266 RVA: 0x00128BA2 File Offset: 0x00126DA2
			public uint TimeoutCount { get; set; }

			// Token: 0x170012A8 RID: 4776
			// (get) Token: 0x060062B3 RID: 25267 RVA: 0x00128BAB File Offset: 0x00126DAB
			// (set) Token: 0x060062B4 RID: 25268 RVA: 0x00128BB3 File Offset: 0x00126DB3
			public uint AggregatedRateLimitCountCount { get; set; }
		}

		// Token: 0x020006C7 RID: 1735
		private class CallTracker
		{
			// Token: 0x060062B6 RID: 25270 RVA: 0x00128BBC File Offset: 0x00126DBC
			public CallTracker(uint maxCalls, uint timePeriodInSeconds)
			{
				if (maxCalls == 0U || timePeriodInSeconds == 0U)
				{
					return;
				}
				this.m_calls = new float[maxCalls];
				this.m_numberOfSeconds = timePeriodInSeconds;
			}

			// Token: 0x060062B7 RID: 25271 RVA: 0x00128BE0 File Offset: 0x00126DE0
			public bool CanCall(float now)
			{
				if (this.m_calls == null || this.m_calls.Length == 0)
				{
					return false;
				}
				int callIndex;
				if (this.m_callIndex < this.m_calls.Length)
				{
					float[] calls = this.m_calls;
					callIndex = this.m_callIndex;
					this.m_callIndex = callIndex + 1;
					calls[callIndex] = now;
					return true;
				}
				if (now - this.m_calls[0] <= this.m_numberOfSeconds)
				{
					return false;
				}
				if (this.m_calls.Length == 1)
				{
					this.m_calls[0] = now;
					this.m_callIndex = 1;
					return true;
				}
				int num = 0;
				while (num + 1 < this.m_calls.Length && now - this.m_calls[num + 1] > this.m_numberOfSeconds)
				{
					num++;
				}
				int num2 = this.m_calls.Length - (num + 1);
				Array.Copy(this.m_calls, num + 1, this.m_calls, 0, num2);
				this.m_callIndex = num2;
				float[] calls2 = this.m_calls;
				callIndex = this.m_callIndex;
				this.m_callIndex = callIndex + 1;
				calls2[callIndex] = now;
				return true;
			}

			// Token: 0x04002239 RID: 8761
			private float[] m_calls;

			// Token: 0x0400223A RID: 8762
			private int m_callIndex;

			// Token: 0x0400223B RID: 8763
			private float m_numberOfSeconds;
		}

		// Token: 0x020006C8 RID: 1736
		private class RuntimeData
		{
			// Token: 0x170012A9 RID: 4777
			// (get) Token: 0x060062B8 RID: 25272 RVA: 0x00128CC9 File Offset: 0x00126EC9
			// (set) Token: 0x060062B9 RID: 25273 RVA: 0x00128CD1 File Offset: 0x00126ED1
			public bool AlwaysAllow { get; set; }

			// Token: 0x170012AA RID: 4778
			// (get) Token: 0x060062BA RID: 25274 RVA: 0x00128CDA File Offset: 0x00126EDA
			// (set) Token: 0x060062BB RID: 25275 RVA: 0x00128CE2 File Offset: 0x00126EE2
			public bool AlwaysDeny { get; set; }

			// Token: 0x170012AB RID: 4779
			// (get) Token: 0x060062BC RID: 25276 RVA: 0x00128CEB File Offset: 0x00126EEB
			// (set) Token: 0x060062BD RID: 25277 RVA: 0x00128CF3 File Offset: 0x00126EF3
			public RPCConnectionMetering.StaticData StaticData { get; set; }

			// Token: 0x170012AC RID: 4780
			// (get) Token: 0x060062BE RID: 25278 RVA: 0x00128CFC File Offset: 0x00126EFC
			// (set) Token: 0x060062BF RID: 25279 RVA: 0x00128D04 File Offset: 0x00126F04
			public uint FiniteCallsLeft
			{
				get
				{
					return this.m_finiteCallsLeft;
				}
				set
				{
					this.m_finiteCallsLeft = value;
				}
			}

			// Token: 0x170012AD RID: 4781
			// (get) Token: 0x060062C0 RID: 25280 RVA: 0x00128D0D File Offset: 0x00126F0D
			// (set) Token: 0x060062C1 RID: 25281 RVA: 0x00128D15 File Offset: 0x00126F15
			public RPCConnectionMetering.CallTracker Tracker
			{
				get
				{
					return this.m_callTracker;
				}
				set
				{
					this.m_callTracker = value;
				}
			}

			// Token: 0x060062C2 RID: 25282 RVA: 0x00128D1E File Offset: 0x00126F1E
			public bool CanCall(float now)
			{
				return this.m_callTracker == null || this.m_callTracker.CanCall(now);
			}

			// Token: 0x060062C3 RID: 25283 RVA: 0x00128D38 File Offset: 0x00126F38
			public string GetServiceAndMethodNames()
			{
				string arg = (this.StaticData != null && this.StaticData.ServiceName != null) ? this.StaticData.ServiceName : "<null>";
				string arg2 = (this.StaticData != null && this.StaticData.MethodName != null) ? this.StaticData.MethodName : "<null>";
				return string.Format("{0}.{1}", arg, arg2);
			}

			// Token: 0x0400223F RID: 8767
			private uint m_finiteCallsLeft = uint.MaxValue;

			// Token: 0x04002240 RID: 8768
			private RPCConnectionMetering.CallTracker m_callTracker;
		}

		// Token: 0x020006C9 RID: 1737
		private class MeteringData
		{
			// Token: 0x170012AE RID: 4782
			// (get) Token: 0x060062C5 RID: 25285 RVA: 0x00128DB0 File Offset: 0x00126FB0
			public RPCConnectionMetering.Stats Stats
			{
				get
				{
					return this.m_staticDataStats;
				}
			}

			// Token: 0x170012AF RID: 4783
			// (get) Token: 0x060062C6 RID: 25286 RVA: 0x00128DB8 File Offset: 0x00126FB8
			// (set) Token: 0x060062C7 RID: 25287 RVA: 0x00128DC0 File Offset: 0x00126FC0
			public RPCConnectionMetering.StaticData GlobalDefault
			{
				get
				{
					return this.m_globalDefault;
				}
				set
				{
					this.m_globalDefault = value;
				}
			}

			// Token: 0x170012B0 RID: 4784
			// (get) Token: 0x060062C8 RID: 25288 RVA: 0x00128DC9 File Offset: 0x00126FC9
			public Dictionary<uint, RPCConnectionMetering.StaticData> ServiceDefaultsByHash
			{
				get
				{
					return this.m_serviceDefaultsByHash;
				}
			}

			// Token: 0x170012B1 RID: 4785
			// (get) Token: 0x060062C9 RID: 25289 RVA: 0x00128DD1 File Offset: 0x00126FD1
			public Dictionary<FullMethodId, RPCConnectionMetering.StaticData> MethodDefaultsByFullMethodId
			{
				get
				{
					return this.m_methodDefaultsByFullMethodId;
				}
			}

			// Token: 0x170012B2 RID: 4786
			// (get) Token: 0x060062CA RID: 25290 RVA: 0x00128DD9 File Offset: 0x00126FD9
			public Dictionary<FullMethodId, RPCConnectionMetering.RuntimeData> RuntimeDataByFullMethodId
			{
				get
				{
					return this.m_runtimeDataByFullMethodId;
				}
			}

			// Token: 0x060062CB RID: 25291 RVA: 0x00128DE4 File Offset: 0x00126FE4
			public RPCConnectionMetering.RuntimeData GetRuntimeData(FullMethodId id)
			{
				RPCConnectionMetering.RuntimeData result;
				if (this.m_runtimeDataByFullMethodId.TryGetValue(id, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x170012B3 RID: 4787
			// (get) Token: 0x060062CC RID: 25292 RVA: 0x00128E04 File Offset: 0x00127004
			// (set) Token: 0x060062CD RID: 25293 RVA: 0x00128E0C File Offset: 0x0012700C
			public float StartupPeriodEndTime { get; set; }

			// Token: 0x170012B4 RID: 4788
			// (get) Token: 0x060062CE RID: 25294 RVA: 0x00128E15 File Offset: 0x00127015
			// (set) Token: 0x060062CF RID: 25295 RVA: 0x00128E1D File Offset: 0x0012701D
			public float StartupPeriodDurationSeconds { get; set; }

			// Token: 0x04002243 RID: 8771
			private RPCConnectionMetering.Stats m_staticDataStats = new RPCConnectionMetering.Stats();

			// Token: 0x04002244 RID: 8772
			private RPCConnectionMetering.StaticData m_globalDefault;

			// Token: 0x04002245 RID: 8773
			private Dictionary<uint, RPCConnectionMetering.StaticData> m_serviceDefaultsByHash = new Dictionary<uint, RPCConnectionMetering.StaticData>();

			// Token: 0x04002246 RID: 8774
			private Dictionary<FullMethodId, RPCConnectionMetering.StaticData> m_methodDefaultsByFullMethodId = new Dictionary<FullMethodId, RPCConnectionMetering.StaticData>();

			// Token: 0x04002247 RID: 8775
			private Dictionary<FullMethodId, RPCConnectionMetering.RuntimeData> m_runtimeDataByFullMethodId = new Dictionary<FullMethodId, RPCConnectionMetering.RuntimeData>();
		}
	}
}
