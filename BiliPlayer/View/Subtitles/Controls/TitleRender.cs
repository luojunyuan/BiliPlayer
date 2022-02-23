using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BiliPlayer.View.Subtitles.Controls;

internal class TitleRender
{
	public static Dispatcher Dispatcher { get; private set; }

	static TitleRender()
	{
		TaskCompletionSource<Dispatcher> taskCompletionSource = new TaskCompletionSource<Dispatcher>();
		getDispatcher(taskCompletionSource);
		taskCompletionSource.Task.Wait();
		Dispatcher = taskCompletionSource.Task.Result;
	}

	private static void getDispatcher(TaskCompletionSource<Dispatcher> task)
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			task.SetResult(Dispatcher.CurrentDispatcher);
			Dispatcher.Run();
		});
		thread.SetApartmentState(ApartmentState.STA);
		thread.IsBackground = true;
		thread.Start();
	}
}
