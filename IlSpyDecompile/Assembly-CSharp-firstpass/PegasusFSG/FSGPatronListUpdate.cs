using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	public class FSGPatronListUpdate : IProtoBuf
	{
		public enum PacketID
		{
			ID = 0x200,
			System = 3
		}

		private List<FSGPatron> _AddedPatrons = new List<FSGPatron>();

		private List<FSGPatron> _RemovedPatrons = new List<FSGPatron>();

		public List<FSGPatron> AddedPatrons
		{
			get
			{
				return _AddedPatrons;
			}
			set
			{
				_AddedPatrons = value;
			}
		}

		public List<FSGPatron> RemovedPatrons
		{
			get
			{
				return _RemovedPatrons;
			}
			set
			{
				_RemovedPatrons = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (FSGPatron addedPatron in AddedPatrons)
			{
				num ^= addedPatron.GetHashCode();
			}
			foreach (FSGPatron removedPatron in RemovedPatrons)
			{
				num ^= removedPatron.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FSGPatronListUpdate fSGPatronListUpdate = obj as FSGPatronListUpdate;
			if (fSGPatronListUpdate == null)
			{
				return false;
			}
			if (AddedPatrons.Count != fSGPatronListUpdate.AddedPatrons.Count)
			{
				return false;
			}
			for (int i = 0; i < AddedPatrons.Count; i++)
			{
				if (!AddedPatrons[i].Equals(fSGPatronListUpdate.AddedPatrons[i]))
				{
					return false;
				}
			}
			if (RemovedPatrons.Count != fSGPatronListUpdate.RemovedPatrons.Count)
			{
				return false;
			}
			for (int j = 0; j < RemovedPatrons.Count; j++)
			{
				if (!RemovedPatrons[j].Equals(fSGPatronListUpdate.RemovedPatrons[j]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FSGPatronListUpdate Deserialize(Stream stream, FSGPatronListUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FSGPatronListUpdate DeserializeLengthDelimited(Stream stream)
		{
			FSGPatronListUpdate fSGPatronListUpdate = new FSGPatronListUpdate();
			DeserializeLengthDelimited(stream, fSGPatronListUpdate);
			return fSGPatronListUpdate;
		}

		public static FSGPatronListUpdate DeserializeLengthDelimited(Stream stream, FSGPatronListUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FSGPatronListUpdate Deserialize(Stream stream, FSGPatronListUpdate instance, long limit)
		{
			if (instance.AddedPatrons == null)
			{
				instance.AddedPatrons = new List<FSGPatron>();
			}
			if (instance.RemovedPatrons == null)
			{
				instance.RemovedPatrons = new List<FSGPatron>();
			}
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
					instance.AddedPatrons.Add(FSGPatron.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					instance.RemovedPatrons.Add(FSGPatron.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, FSGPatronListUpdate instance)
		{
			if (instance.AddedPatrons.Count > 0)
			{
				foreach (FSGPatron addedPatron in instance.AddedPatrons)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, addedPatron.GetSerializedSize());
					FSGPatron.Serialize(stream, addedPatron);
				}
			}
			if (instance.RemovedPatrons.Count <= 0)
			{
				return;
			}
			foreach (FSGPatron removedPatron in instance.RemovedPatrons)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, removedPatron.GetSerializedSize());
				FSGPatron.Serialize(stream, removedPatron);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (AddedPatrons.Count > 0)
			{
				foreach (FSGPatron addedPatron in AddedPatrons)
				{
					num++;
					uint serializedSize = addedPatron.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (RemovedPatrons.Count > 0)
			{
				foreach (FSGPatron removedPatron in RemovedPatrons)
				{
					num++;
					uint serializedSize2 = removedPatron.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
