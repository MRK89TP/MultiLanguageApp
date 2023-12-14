using System;
using System.Windows.Input;

namespace WpfApp1
{
	public class BaseCommand<T> : ICommand
	{
		private readonly Action<T> execute;

		private readonly Func<T, bool> canExecute;

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

		public BaseCommand(Action<T> execute)
		{
			this.execute = execute;
		}

		public BaseCommand(Action<T> execute, Func<T, bool> canExecute = null)
		{
			this.execute = execute;

			this.canExecute = canExecute == null ? ((T obj) => true) : canExecute;
		}

		public bool CanExecute(object parameter)
		{
			if (canExecute != null)
			{
				if (parameter is T t)
				{
					return canExecute.Invoke(t);
				}

				try
				{
					T param = (T)Convert.ChangeType(parameter, typeof(T));
					return canExecute.Invoke(param);

				}
				catch (InvalidCastException)
				{
					return false;
				}
			}

			return true;
		}

		public void Execute(object parameter)
		{
			if (CanExecute(parameter))
			{
				if (parameter is T t)
				{
					execute.Invoke(t);
				}
				else
				{
					try
					{
						T param = (T)Convert.ChangeType(parameter, typeof(T));
						canExecute.Invoke(param);

					}
					catch (InvalidCastException)
					{
						return;
					}
				}
			}
		}
	}

	public class BaseCommand : ICommand
	{
		private readonly Action execute;

		private readonly Func<bool> canExecute;

		public BaseCommand(Action execute)
		{
			this.execute = execute;
		}

		public BaseCommand(Action execute, Func<bool> canExecute = null) : this(execute)
		{
			this.canExecute = canExecute == null ? (() => true) : canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute != null ? canExecute.Invoke() : true;
		}

		public void Execute(object parameter)
		{
			if (CanExecute(parameter))
			{
				execute.Invoke();
			}
		}

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
	}
}
