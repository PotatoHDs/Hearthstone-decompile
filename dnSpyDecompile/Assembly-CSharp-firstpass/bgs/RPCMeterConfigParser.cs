using System;
using bnet.protocol.config;

namespace bgs
{
	// Token: 0x02000238 RID: 568
	public class RPCMeterConfigParser
	{
		// Token: 0x060023C2 RID: 9154 RVA: 0x0007E3B0 File Offset: 0x0007C5B0
		public static RPCMethodConfig ParseMethod(Tokenizer tokenizer)
		{
			RPCMethodConfig rpcmethodConfig = new RPCMethodConfig();
			tokenizer.NextOpenBracket();
			for (;;)
			{
				string text = tokenizer.NextString();
				if (text == null)
				{
					break;
				}
				if (text == "}")
				{
					return rpcmethodConfig;
				}
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1532179540U)
				{
					if (num <= 811309752U)
					{
						if (num != 502077013U)
						{
							if (num != 662620843U)
							{
								if (num == 811309752U)
								{
									if (text == "fixed_call_cost:")
									{
										rpcmethodConfig.FixedCallCost = tokenizer.NextUInt32();
										continue;
									}
								}
							}
							else if (text == "rate_limit_count:")
							{
								rpcmethodConfig.RateLimitCount = tokenizer.NextUInt32();
								continue;
							}
						}
						else if (text == "service_hash:")
						{
							rpcmethodConfig.ServiceHash = tokenizer.NextUInt32();
							continue;
						}
					}
					else if (num != 1056685108U)
					{
						if (num != 1273284896U)
						{
							if (num == 1532179540U)
							{
								if (text == "max_packet_size:")
								{
									rpcmethodConfig.MaxPacketSize = tokenizer.NextUInt32();
									continue;
								}
							}
						}
						else if (text == "service_name:")
						{
							rpcmethodConfig.ServiceName = tokenizer.NextQuotedString();
							continue;
						}
					}
					else if (text == "fixed_packet_size:")
					{
						rpcmethodConfig.FixedPacketSize = tokenizer.NextUInt32();
						continue;
					}
				}
				else if (num <= 2044422092U)
				{
					if (num != 1707589662U)
					{
						if (num != 1750048753U)
						{
							if (num == 2044422092U)
							{
								if (text == "method_name:")
								{
									rpcmethodConfig.MethodName = tokenizer.NextQuotedString();
									continue;
								}
							}
						}
						else if (text == "variable_multiplier:")
						{
							rpcmethodConfig.VariableMultiplier = tokenizer.NextUInt32();
							continue;
						}
					}
					else if (text == "max_encoded_size:")
					{
						rpcmethodConfig.MaxEncodedSize = tokenizer.NextUInt32();
						continue;
					}
				}
				else if (num <= 2756175313U)
				{
					if (num != 2511118164U)
					{
						if (num == 2756175313U)
						{
							if (text == "rate_limit_seconds:")
							{
								rpcmethodConfig.RateLimitSeconds = tokenizer.NextUInt32();
								continue;
							}
						}
					}
					else if (text == "multiplier:")
					{
						rpcmethodConfig.Multiplier = tokenizer.NextFloat();
						continue;
					}
				}
				else if (num != 3348988086U)
				{
					if (num == 3943780662U)
					{
						if (text == "timeout:")
						{
							rpcmethodConfig.Timeout = tokenizer.NextFloat();
							continue;
						}
					}
				}
				else if (text == "method_id:")
				{
					rpcmethodConfig.MethodId = tokenizer.NextUInt32();
					continue;
				}
				tokenizer.SkipUnknownToken();
			}
			throw new Exception("Parsing ended with unfinished RPCMethodConfig");
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x0007E6B0 File Offset: 0x0007C8B0
		public static RPCMeterConfig ParseConfig(string str)
		{
			RPCMeterConfig rpcmeterConfig = new RPCMeterConfig();
			Tokenizer tokenizer = new Tokenizer(str);
			for (;;)
			{
				string text = tokenizer.NextString();
				if (text == null)
				{
					break;
				}
				if (!(text == "method"))
				{
					if (!(text == "income_per_second:"))
					{
						if (!(text == "initial_balance:"))
						{
							if (!(text == "cap_balance:"))
							{
								if (!(text == "startup_period:"))
								{
									tokenizer.SkipUnknownToken();
								}
								else
								{
									rpcmeterConfig.StartupPeriod = tokenizer.NextFloat();
								}
							}
							else
							{
								rpcmeterConfig.CapBalance = tokenizer.NextUInt32();
							}
						}
						else
						{
							rpcmeterConfig.InitialBalance = tokenizer.NextUInt32();
						}
					}
					else
					{
						rpcmeterConfig.IncomePerSecond = tokenizer.NextUInt32();
					}
				}
				else
				{
					rpcmeterConfig.AddMethod(RPCMeterConfigParser.ParseMethod(tokenizer));
				}
			}
			return rpcmeterConfig;
		}
	}
}
