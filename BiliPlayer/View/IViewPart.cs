using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace BiliPlayer.View;

[InheritedExport]
internal interface IViewPart
{
	void Init(Panel container);
}
