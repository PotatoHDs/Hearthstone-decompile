using System;

[Serializable]
public class BoxStateInfoList
{
	public BoxCameraStateInfo m_CameraInfo;

	public BoxDiskStateInfo m_DiskInfo;

	public BoxDoorStateInfo m_LeftDoorInfo;

	public BoxDoorStateInfo m_RightDoorInfo;

	public BoxDrawerStateInfo m_DrawerInfo;

	public BoxLogoStateInfo m_LogoInfo;

	public BoxSpinnerStateInfo m_SpinnerInfo;

	public BoxStartButtonStateInfo m_StartButtonInfo;
}
