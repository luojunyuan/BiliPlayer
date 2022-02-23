namespace BiliPlayer.Common.Data;

internal static class ScrollModeExtension
{
	public static bool IsFixedTitle(this ScrollMode mode)
	{
		if (mode != ScrollMode.Bottom)
		{
			return mode == ScrollMode.Top;
		}
		return true;
	}
}
