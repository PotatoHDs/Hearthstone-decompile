using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class ChangeGameRequest : IProtoBuf
	{
		public bool HasOpen;

		private bool _Open;

		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasReplace;

		private bool _Replace;

		public GameHandle GameHandle { get; set; }

		public bool Open
		{
			get
			{
				return _Open;
			}
			set
			{
				_Open = value;
				HasOpen = true;
			}
		}

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

		public bool Replace
		{
			get
			{
				return _Replace;
			}
			set
			{
				_Replace = value;
				HasReplace = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void SetOpen(bool val)
		{
			Open = val;
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

		public void SetReplace(bool val)
		{
			Replace = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			if (HasOpen)
			{
				hashCode ^= Open.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasReplace)
			{
				hashCode ^= Replace.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ChangeGameRequest changeGameRequest = obj as ChangeGameRequest;
			if (changeGameRequest == null)
			{
				return false;
			}
			if (!GameHandle.Equals(changeGameRequest.GameHandle))
			{
				return false;
			}
			if (HasOpen != changeGameRequest.HasOpen || (HasOpen && !Open.Equals(changeGameRequest.Open)))
			{
				return false;
			}
			if (Attribute.Count != changeGameRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(changeGameRequest.Attribute[i]))
				{
					return false;
				}
			}
			if (HasReplace != changeGameRequest.HasReplace || (HasReplace && !Replace.Equals(changeGameRequest.Replace)))
			{
				return false;
			}
			return true;
		}

		public static ChangeGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChangeGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream)
		{
			ChangeGameRequest changeGameRequest = new ChangeGameRequest();
			DeserializeLengthDelimited(stream, changeGameRequest);
			return changeGameRequest;
		}

		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream, ChangeGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			instance.Replace = false;
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
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 16:
					instance.Open = ProtocolParser.ReadBool(stream);
					continue;
				case 26:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 32:
					instance.Replace = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ChangeGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.HasOpen)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Open);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasReplace)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Replace);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasOpen)
			{
				num++;
				num++;
			}
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasReplace)
			{
				num++;
				num++;
			}
			return num + 1;
		}
	}
}
