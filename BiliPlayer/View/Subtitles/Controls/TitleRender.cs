using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BiliPlayer.View.Subtitles.Controls
{
	// Token: 0x02000018 RID: 24
	internal class TitleRender
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000338B File Offset: 0x0000158B
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003392 File Offset: 0x00001592
		public static Dispatcher Dispatcher { get; private set; }

		// Token: 0x0600008D RID: 141 RVA: 0x0000339A File Offset: 0x0000159A
		static TitleRender()
		{
			TaskCompletionSource<Dispatcher> taskCompletionSource = new TaskCompletionSource<Dispatcher>();
			TitleRender.getDispatcher(taskCompletionSource);
			taskCompletionSource.Task.Wait();
			TitleRender.Dispatcher = taskCompletionSource.Task.Result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000033C1 File Offset: 0x000015C1
		private static void getDispatcher(TaskCompletionSource<Dispatcher> task)
		{
			Thread thread = new Thread(delegate()
			{
				task.SetResult(Dispatcher.CurrentDispatcher);
				Dispatcher.Run();
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.IsBackground = true;
			thread.Start();
		}
	}
}
