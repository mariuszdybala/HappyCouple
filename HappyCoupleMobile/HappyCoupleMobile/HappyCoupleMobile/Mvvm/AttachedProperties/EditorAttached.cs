using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.AttachedProperties
{
    public class EditorAttached
    {
        public static BindableProperty CompletedCommandProperty = BindableProperty.CreateAttached
            (nameof(TextCell), typeof(ICommand), typeof(Nullable), null, propertyChanged: OnCompletedCommand);

        public static ICommand GetCompletedCommand(BindableObject view)
        {
            return (ICommand) view.GetValue(CompletedCommandProperty);
        }

        public static void SetCompletedCommand(BindableObject view, ICommand cmd)
        {
            view.SetValue(CompletedCommandProperty, cmd);
        }


        private static void OnCompletedCommand(BindableObject bindable, object oldvalue, object newvalue)
        {
            var editor = bindable as Editor;

            if (editor == null)
            {
                return;
            }

            editor.Completed += EditorOnCompleted;
        }

        private static void EditorOnCompleted(object sender, EventArgs eventArgs)
        {
            var editor = sender as Editor;

            var command = GetCompletedCommand(editor);

            command.Execute(null);
        }
    }
}