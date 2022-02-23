namespace BiliPlayer.Util;

public sealed class BooleanToVisibility : VisibilityConverter<bool>
{
	public bool VisibleValue { get; set; }

	protected override bool IsValaueVisible(bool value)
	{
		return VisibleValue == value;
	}
}
