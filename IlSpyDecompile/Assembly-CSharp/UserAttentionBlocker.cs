using System;

[Flags]
public enum UserAttentionBlocker
{
	NONE = 0x0,
	FATAL_ERROR_SCENE = 0x1,
	SET_ROTATION_INTRO = 0x2,
	SET_ROTATION_CM_TUTORIALS = 0x4,
	ALL = -1,
	ALL_EXCEPT_FATAL_ERROR_SCENE = -2
}
