using System.IO;

public interface IProtoBuf
{
	void Deserialize(Stream stream);

	void Serialize(Stream stream);

	uint GetSerializedSize();
}
