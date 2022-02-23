using System;
using System.Windows.Input;

namespace BiliPlayer.Common.MVVM;

public class DelegateCommand : NotifyItem, ICommand
{
	protected static readonly Func<object, bool> _defaultCanExecute;

	private Action<object> _exeCute;

	private Func<object, bool> _canExecute;

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

	static DelegateCommand()
	{
		_defaultCanExecute = (object P_0) => true;
	}

	public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
	{
		if (execute == null)
		{
			throw new ArgumentException();
		}
		_exeCute = execute;
		_canExecute = canExecute ?? _defaultCanExecute;
	}

	public virtual bool CanExecute(object parameter)
	{
		return _canExecute(parameter);
	}

	public virtual void Execute(object parameter)
	{
		_exeCute(parameter);
	}

	public static void UpdateCommandState()
	{
		CommandManager.InvalidateRequerySuggested();
	}
}
