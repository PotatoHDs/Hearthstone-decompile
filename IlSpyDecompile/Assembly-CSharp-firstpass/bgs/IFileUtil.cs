namespace bgs
{
	public interface IFileUtil
	{
		bool LoadFromDrive(string filePath, out byte[] data);

		bool RemoveFromDrive(string filePath);

		bool StoreToDrive(byte[] data, string filePath, bool overwrite, bool compress);
	}
}
