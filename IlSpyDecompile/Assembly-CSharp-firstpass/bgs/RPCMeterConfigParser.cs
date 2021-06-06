using System;
using bnet.protocol.config;

namespace bgs
{
	public class RPCMeterConfigParser
	{
		public static RPCMethodConfig ParseMethod(Tokenizer tokenizer)
		{
			RPCMethodConfig rPCMethodConfig = new RPCMethodConfig();
			tokenizer.NextOpenBracket();
			while (true)
			{
				string text = tokenizer.NextString();
				if (text == null)
				{
					throw new Exception("Parsing ended with unfinished RPCMethodConfig");
				}
				if (text == "}")
				{
					break;
				}
				switch (text)
				{
				case "service_name:":
					rPCMethodConfig.ServiceName = tokenizer.NextQuotedString();
					break;
				case "method_name:":
					rPCMethodConfig.MethodName = tokenizer.NextQuotedString();
					break;
				case "service_hash:":
					rPCMethodConfig.ServiceHash = tokenizer.NextUInt32();
					break;
				case "method_id:":
					rPCMethodConfig.MethodId = tokenizer.NextUInt32();
					break;
				case "fixed_call_cost:":
					rPCMethodConfig.FixedCallCost = tokenizer.NextUInt32();
					break;
				case "fixed_packet_size:":
					rPCMethodConfig.FixedPacketSize = tokenizer.NextUInt32();
					break;
				case "variable_multiplier:":
					rPCMethodConfig.VariableMultiplier = tokenizer.NextUInt32();
					break;
				case "multiplier:":
					rPCMethodConfig.Multiplier = tokenizer.NextFloat();
					break;
				case "rate_limit_count:":
					rPCMethodConfig.RateLimitCount = tokenizer.NextUInt32();
					break;
				case "rate_limit_seconds:":
					rPCMethodConfig.RateLimitSeconds = tokenizer.NextUInt32();
					break;
				case "max_packet_size:":
					rPCMethodConfig.MaxPacketSize = tokenizer.NextUInt32();
					break;
				case "max_encoded_size:":
					rPCMethodConfig.MaxEncodedSize = tokenizer.NextUInt32();
					break;
				case "timeout:":
					rPCMethodConfig.Timeout = tokenizer.NextFloat();
					break;
				default:
					tokenizer.SkipUnknownToken();
					break;
				}
			}
			return rPCMethodConfig;
		}

		public static RPCMeterConfig ParseConfig(string str)
		{
			RPCMeterConfig rPCMeterConfig = new RPCMeterConfig();
			Tokenizer tokenizer = new Tokenizer(str);
			while (true)
			{
				switch (tokenizer.NextString())
				{
				case "method":
					rPCMeterConfig.AddMethod(ParseMethod(tokenizer));
					break;
				case "income_per_second:":
					rPCMeterConfig.IncomePerSecond = tokenizer.NextUInt32();
					break;
				case "initial_balance:":
					rPCMeterConfig.InitialBalance = tokenizer.NextUInt32();
					break;
				case "cap_balance:":
					rPCMeterConfig.CapBalance = tokenizer.NextUInt32();
					break;
				case "startup_period:":
					rPCMeterConfig.StartupPeriod = tokenizer.NextFloat();
					break;
				default:
					tokenizer.SkipUnknownToken();
					break;
				case null:
					return rPCMeterConfig;
				}
			}
		}
	}
}
