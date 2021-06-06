public class LogInfo
{
	public string m_name;

	public bool m_consolePrinting;

	public bool m_screenPrinting;

	public bool m_filePrinting;

	public Log.LogLevel m_minLevel = Log.LogLevel.Debug;

	public Log.LogLevel m_defaultLevel = Log.LogLevel.Debug;

	public bool m_alwaysPrintErrors = true;

	public bool m_verbose;
}
