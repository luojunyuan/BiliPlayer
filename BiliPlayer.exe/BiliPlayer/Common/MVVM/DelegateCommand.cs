using System;
using System.Windows.Input;

namespace BiliPlayer.Common.MVVM
{
	// Token: 0x0200003A RID: 58
	public class DelegateCommand : NotifyItem, ICommand
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00004D80 File Offset: 0x00002F80
		public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			if (execute == null)
			{
				throw new ArgumentException();
			}
			this._exeCute = execute;
			this._canExecute = (canExecute ?? DelegateCommand._defaultCanExecute);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public virtual bool CanExecute(object parameter)
		{
			return this._canExecute(parameter);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004DB6 File Offset: 0x00002FB6
		public virtual void Execute(object parameter)
		{
			this._exeCute(parameter);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000140 RID: 320 RVA: 0x00004DC4 File Offset: 0x00002FC4
		// (remove) Token: 0x06000141 RID: 321 RVA: 0x00004DCC File Offset: 0x00002FCC
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public static void UpdateCommandState()
		{
			CommandManager.InvalidateRequerySuggested();
		}

		// Token: 0x04000074 RID: 116
		protected static readonly Func<object, bool> _defaultCanExecute = (object A_1) => true;

		// Token: 0x04000075 RID: 117
		private Action<object> _exeCute;

		// Token: 0x04000076 RID: 118
		private Func<object, bool> _canExecute;
	}
}
