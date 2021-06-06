using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.report.v1
{
	public class Report : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasReportQos;

		private int _ReportQos;

		public bool HasReportingAccount;

		private EntityId _ReportingAccount;

		public bool HasReportingGameAccount;

		private EntityId _ReportingGameAccount;

		public bool HasReportTimestamp;

		private ulong _ReportTimestamp;

		public string ReportType { get; set; }

		public List<Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public int ReportQos
		{
			get
			{
				return _ReportQos;
			}
			set
			{
				_ReportQos = value;
				HasReportQos = true;
			}
		}

		public EntityId ReportingAccount
		{
			get
			{
				return _ReportingAccount;
			}
			set
			{
				_ReportingAccount = value;
				HasReportingAccount = value != null;
			}
		}

		public EntityId ReportingGameAccount
		{
			get
			{
				return _ReportingGameAccount;
			}
			set
			{
				_ReportingGameAccount = value;
				HasReportingGameAccount = value != null;
			}
		}

		public ulong ReportTimestamp
		{
			get
			{
				return _ReportTimestamp;
			}
			set
			{
				_ReportTimestamp = value;
				HasReportTimestamp = true;
			}
		}

		public bool IsInitialized => true;

		public void SetReportType(string val)
		{
			ReportType = val;
		}

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
		}

		public void SetReportQos(int val)
		{
			ReportQos = val;
		}

		public void SetReportingAccount(EntityId val)
		{
			ReportingAccount = val;
		}

		public void SetReportingGameAccount(EntityId val)
		{
			ReportingGameAccount = val;
		}

		public void SetReportTimestamp(ulong val)
		{
			ReportTimestamp = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ReportType.GetHashCode();
			foreach (Attribute item in Attribute)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasReportQos)
			{
				hashCode ^= ReportQos.GetHashCode();
			}
			if (HasReportingAccount)
			{
				hashCode ^= ReportingAccount.GetHashCode();
			}
			if (HasReportingGameAccount)
			{
				hashCode ^= ReportingGameAccount.GetHashCode();
			}
			if (HasReportTimestamp)
			{
				hashCode ^= ReportTimestamp.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Report report = obj as Report;
			if (report == null)
			{
				return false;
			}
			if (!ReportType.Equals(report.ReportType))
			{
				return false;
			}
			if (Attribute.Count != report.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(report.Attribute[i]))
				{
					return false;
				}
			}
			if (HasReportQos != report.HasReportQos || (HasReportQos && !ReportQos.Equals(report.ReportQos)))
			{
				return false;
			}
			if (HasReportingAccount != report.HasReportingAccount || (HasReportingAccount && !ReportingAccount.Equals(report.ReportingAccount)))
			{
				return false;
			}
			if (HasReportingGameAccount != report.HasReportingGameAccount || (HasReportingGameAccount && !ReportingGameAccount.Equals(report.ReportingGameAccount)))
			{
				return false;
			}
			if (HasReportTimestamp != report.HasReportTimestamp || (HasReportTimestamp && !ReportTimestamp.Equals(report.ReportTimestamp)))
			{
				return false;
			}
			return true;
		}

		public static Report ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Report>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Report Deserialize(Stream stream, Report instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Report DeserializeLengthDelimited(Stream stream)
		{
			Report report = new Report();
			DeserializeLengthDelimited(stream, report);
			return report;
		}

		public static Report DeserializeLengthDelimited(Stream stream, Report instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Report Deserialize(Stream stream, Report instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			instance.ReportQos = 0;
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
					instance.ReportType = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 24:
					instance.ReportQos = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.ReportingAccount == null)
					{
						instance.ReportingAccount = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ReportingAccount);
					}
					continue;
				case 42:
					if (instance.ReportingGameAccount == null)
					{
						instance.ReportingGameAccount = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ReportingGameAccount);
					}
					continue;
				case 49:
					instance.ReportTimestamp = binaryReader.ReadUInt64();
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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

		public static void Serialize(Stream stream, Report instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.ReportType == null)
			{
				throw new ArgumentNullException("ReportType", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ReportType));
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasReportQos)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ReportQos);
			}
			if (instance.HasReportingAccount)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ReportingAccount.GetSerializedSize());
				EntityId.Serialize(stream, instance.ReportingAccount);
			}
			if (instance.HasReportingGameAccount)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ReportingGameAccount.GetSerializedSize());
				EntityId.Serialize(stream, instance.ReportingGameAccount);
			}
			if (instance.HasReportTimestamp)
			{
				stream.WriteByte(49);
				binaryWriter.Write(instance.ReportTimestamp);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ReportType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasReportQos)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ReportQos);
			}
			if (HasReportingAccount)
			{
				num++;
				uint serializedSize2 = ReportingAccount.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasReportingGameAccount)
			{
				num++;
				uint serializedSize3 = ReportingGameAccount.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasReportTimestamp)
			{
				num++;
				num += 8;
			}
			return num + 1;
		}
	}
}
