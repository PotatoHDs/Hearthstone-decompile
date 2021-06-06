using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	public class UpdateGameOptions : IProtoBuf
	{
		public bool HasGameHandle;

		private GameHandle _GameHandle;

		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasReplaceAttributes;

		private bool _ReplaceAttributes;

		public GameHandle GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = value != null;
			}
		}

		public List<bnet.protocol.v2.Attribute> Attribute
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

		public List<bnet.protocol.v2.Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public bool ReplaceAttributes
		{
			get
			{
				return _ReplaceAttributes;
			}
			set
			{
				_ReplaceAttributes = value;
				HasReplaceAttributes = true;
			}
		}

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
		}

		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			Attribute = val;
		}

		public void SetReplaceAttributes(bool val)
		{
			ReplaceAttributes = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasReplaceAttributes)
			{
				num ^= ReplaceAttributes.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateGameOptions updateGameOptions = obj as UpdateGameOptions;
			if (updateGameOptions == null)
			{
				return false;
			}
			if (HasGameHandle != updateGameOptions.HasGameHandle || (HasGameHandle && !GameHandle.Equals(updateGameOptions.GameHandle)))
			{
				return false;
			}
			if (Attribute.Count != updateGameOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(updateGameOptions.Attribute[i]))
				{
					return false;
				}
			}
			if (HasReplaceAttributes != updateGameOptions.HasReplaceAttributes || (HasReplaceAttributes && !ReplaceAttributes.Equals(updateGameOptions.ReplaceAttributes)))
			{
				return false;
			}
			return true;
		}

		public static UpdateGameOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateGameOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateGameOptions Deserialize(Stream stream, UpdateGameOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateGameOptions DeserializeLengthDelimited(Stream stream)
		{
			UpdateGameOptions updateGameOptions = new UpdateGameOptions();
			DeserializeLengthDelimited(stream, updateGameOptions);
			return updateGameOptions;
		}

		public static UpdateGameOptions DeserializeLengthDelimited(Stream stream, UpdateGameOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateGameOptions Deserialize(Stream stream, UpdateGameOptions instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			instance.ReplaceAttributes = true;
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
				case 18:
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 24:
					instance.ReplaceAttributes = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, UpdateGameOptions instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasReplaceAttributes)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ReplaceAttributes);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameHandle)
			{
				num++;
				uint serializedSize = GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Attribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasReplaceAttributes)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
