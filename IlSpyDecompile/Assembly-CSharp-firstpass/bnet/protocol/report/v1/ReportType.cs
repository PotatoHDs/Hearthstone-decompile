using System.IO;
using System.Text;

namespace bnet.protocol.report.v1
{
	public class ReportType : IProtoBuf
	{
		public bool HasUserDescription;

		private string _UserDescription;

		public bool HasCustomReport;

		private CustomReport _CustomReport;

		public bool HasSpamReport;

		private SpamReport _SpamReport;

		public bool HasHarassmentReport;

		private HarassmentReport _HarassmentReport;

		public bool HasRealLifeThreatReport;

		private RealLifeThreatReport _RealLifeThreatReport;

		public bool HasInappropriateBattleTagReport;

		private InappropriateBattleTagReport _InappropriateBattleTagReport;

		public bool HasHackingReport;

		private HackingReport _HackingReport;

		public bool HasBottingReport;

		private BottingReport _BottingReport;

		public string UserDescription
		{
			get
			{
				return _UserDescription;
			}
			set
			{
				_UserDescription = value;
				HasUserDescription = value != null;
			}
		}

		public CustomReport CustomReport
		{
			get
			{
				return _CustomReport;
			}
			set
			{
				_CustomReport = value;
				HasCustomReport = value != null;
			}
		}

		public SpamReport SpamReport
		{
			get
			{
				return _SpamReport;
			}
			set
			{
				_SpamReport = value;
				HasSpamReport = value != null;
			}
		}

		public HarassmentReport HarassmentReport
		{
			get
			{
				return _HarassmentReport;
			}
			set
			{
				_HarassmentReport = value;
				HasHarassmentReport = value != null;
			}
		}

		public RealLifeThreatReport RealLifeThreatReport
		{
			get
			{
				return _RealLifeThreatReport;
			}
			set
			{
				_RealLifeThreatReport = value;
				HasRealLifeThreatReport = value != null;
			}
		}

		public InappropriateBattleTagReport InappropriateBattleTagReport
		{
			get
			{
				return _InappropriateBattleTagReport;
			}
			set
			{
				_InappropriateBattleTagReport = value;
				HasInappropriateBattleTagReport = value != null;
			}
		}

		public HackingReport HackingReport
		{
			get
			{
				return _HackingReport;
			}
			set
			{
				_HackingReport = value;
				HasHackingReport = value != null;
			}
		}

		public BottingReport BottingReport
		{
			get
			{
				return _BottingReport;
			}
			set
			{
				_BottingReport = value;
				HasBottingReport = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetUserDescription(string val)
		{
			UserDescription = val;
		}

		public void SetCustomReport(CustomReport val)
		{
			CustomReport = val;
		}

		public void SetSpamReport(SpamReport val)
		{
			SpamReport = val;
		}

		public void SetHarassmentReport(HarassmentReport val)
		{
			HarassmentReport = val;
		}

		public void SetRealLifeThreatReport(RealLifeThreatReport val)
		{
			RealLifeThreatReport = val;
		}

		public void SetInappropriateBattleTagReport(InappropriateBattleTagReport val)
		{
			InappropriateBattleTagReport = val;
		}

		public void SetHackingReport(HackingReport val)
		{
			HackingReport = val;
		}

		public void SetBottingReport(BottingReport val)
		{
			BottingReport = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasUserDescription)
			{
				num ^= UserDescription.GetHashCode();
			}
			if (HasCustomReport)
			{
				num ^= CustomReport.GetHashCode();
			}
			if (HasSpamReport)
			{
				num ^= SpamReport.GetHashCode();
			}
			if (HasHarassmentReport)
			{
				num ^= HarassmentReport.GetHashCode();
			}
			if (HasRealLifeThreatReport)
			{
				num ^= RealLifeThreatReport.GetHashCode();
			}
			if (HasInappropriateBattleTagReport)
			{
				num ^= InappropriateBattleTagReport.GetHashCode();
			}
			if (HasHackingReport)
			{
				num ^= HackingReport.GetHashCode();
			}
			if (HasBottingReport)
			{
				num ^= BottingReport.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReportType reportType = obj as ReportType;
			if (reportType == null)
			{
				return false;
			}
			if (HasUserDescription != reportType.HasUserDescription || (HasUserDescription && !UserDescription.Equals(reportType.UserDescription)))
			{
				return false;
			}
			if (HasCustomReport != reportType.HasCustomReport || (HasCustomReport && !CustomReport.Equals(reportType.CustomReport)))
			{
				return false;
			}
			if (HasSpamReport != reportType.HasSpamReport || (HasSpamReport && !SpamReport.Equals(reportType.SpamReport)))
			{
				return false;
			}
			if (HasHarassmentReport != reportType.HasHarassmentReport || (HasHarassmentReport && !HarassmentReport.Equals(reportType.HarassmentReport)))
			{
				return false;
			}
			if (HasRealLifeThreatReport != reportType.HasRealLifeThreatReport || (HasRealLifeThreatReport && !RealLifeThreatReport.Equals(reportType.RealLifeThreatReport)))
			{
				return false;
			}
			if (HasInappropriateBattleTagReport != reportType.HasInappropriateBattleTagReport || (HasInappropriateBattleTagReport && !InappropriateBattleTagReport.Equals(reportType.InappropriateBattleTagReport)))
			{
				return false;
			}
			if (HasHackingReport != reportType.HasHackingReport || (HasHackingReport && !HackingReport.Equals(reportType.HackingReport)))
			{
				return false;
			}
			if (HasBottingReport != reportType.HasBottingReport || (HasBottingReport && !BottingReport.Equals(reportType.BottingReport)))
			{
				return false;
			}
			return true;
		}

		public static ReportType ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReportType>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReportType Deserialize(Stream stream, ReportType instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReportType DeserializeLengthDelimited(Stream stream)
		{
			ReportType reportType = new ReportType();
			DeserializeLengthDelimited(stream, reportType);
			return reportType;
		}

		public static ReportType DeserializeLengthDelimited(Stream stream, ReportType instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReportType Deserialize(Stream stream, ReportType instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					instance.UserDescription = ProtocolParser.ReadString(stream);
					continue;
				case 82:
					if (instance.CustomReport == null)
					{
						instance.CustomReport = CustomReport.DeserializeLengthDelimited(stream);
					}
					else
					{
						CustomReport.DeserializeLengthDelimited(stream, instance.CustomReport);
					}
					continue;
				case 90:
					if (instance.SpamReport == null)
					{
						instance.SpamReport = SpamReport.DeserializeLengthDelimited(stream);
					}
					else
					{
						SpamReport.DeserializeLengthDelimited(stream, instance.SpamReport);
					}
					continue;
				case 98:
					if (instance.HarassmentReport == null)
					{
						instance.HarassmentReport = HarassmentReport.DeserializeLengthDelimited(stream);
					}
					else
					{
						HarassmentReport.DeserializeLengthDelimited(stream, instance.HarassmentReport);
					}
					continue;
				case 106:
					if (instance.RealLifeThreatReport == null)
					{
						instance.RealLifeThreatReport = RealLifeThreatReport.DeserializeLengthDelimited(stream);
					}
					else
					{
						RealLifeThreatReport.DeserializeLengthDelimited(stream, instance.RealLifeThreatReport);
					}
					continue;
				case 114:
					if (instance.InappropriateBattleTagReport == null)
					{
						instance.InappropriateBattleTagReport = InappropriateBattleTagReport.DeserializeLengthDelimited(stream);
					}
					else
					{
						InappropriateBattleTagReport.DeserializeLengthDelimited(stream, instance.InappropriateBattleTagReport);
					}
					continue;
				case 122:
					if (instance.HackingReport == null)
					{
						instance.HackingReport = HackingReport.DeserializeLengthDelimited(stream);
					}
					else
					{
						HackingReport.DeserializeLengthDelimited(stream, instance.HackingReport);
					}
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.BottingReport == null)
							{
								instance.BottingReport = BottingReport.DeserializeLengthDelimited(stream);
							}
							else
							{
								BottingReport.DeserializeLengthDelimited(stream, instance.BottingReport);
							}
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ReportType instance)
		{
			if (instance.HasUserDescription)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserDescription));
			}
			if (instance.HasCustomReport)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.CustomReport.GetSerializedSize());
				CustomReport.Serialize(stream, instance.CustomReport);
			}
			if (instance.HasSpamReport)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.SpamReport.GetSerializedSize());
				SpamReport.Serialize(stream, instance.SpamReport);
			}
			if (instance.HasHarassmentReport)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteUInt32(stream, instance.HarassmentReport.GetSerializedSize());
				HarassmentReport.Serialize(stream, instance.HarassmentReport);
			}
			if (instance.HasRealLifeThreatReport)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteUInt32(stream, instance.RealLifeThreatReport.GetSerializedSize());
				RealLifeThreatReport.Serialize(stream, instance.RealLifeThreatReport);
			}
			if (instance.HasInappropriateBattleTagReport)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.InappropriateBattleTagReport.GetSerializedSize());
				InappropriateBattleTagReport.Serialize(stream, instance.InappropriateBattleTagReport);
			}
			if (instance.HasHackingReport)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteUInt32(stream, instance.HackingReport.GetSerializedSize());
				HackingReport.Serialize(stream, instance.HackingReport);
			}
			if (instance.HasBottingReport)
			{
				stream.WriteByte(130);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.BottingReport.GetSerializedSize());
				BottingReport.Serialize(stream, instance.BottingReport);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasUserDescription)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(UserDescription);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasCustomReport)
			{
				num++;
				uint serializedSize = CustomReport.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasSpamReport)
			{
				num++;
				uint serializedSize2 = SpamReport.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasHarassmentReport)
			{
				num++;
				uint serializedSize3 = HarassmentReport.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasRealLifeThreatReport)
			{
				num++;
				uint serializedSize4 = RealLifeThreatReport.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasInappropriateBattleTagReport)
			{
				num++;
				uint serializedSize5 = InappropriateBattleTagReport.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasHackingReport)
			{
				num++;
				uint serializedSize6 = HackingReport.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (HasBottingReport)
			{
				num += 2;
				uint serializedSize7 = BottingReport.GetSerializedSize();
				num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
			}
			return num;
		}
	}
}
