using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Paint.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public KeyGesture Gesture { get; set; }

        public string GestureText
        {
            get
            {
                return Gesture == null ? "" : Gesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);
            }
        }

        public string Text { get; set; }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
