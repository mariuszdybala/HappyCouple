using System;
using System.Linq;
using System.Windows.Input;
using HappyCoupleMobile.Helpers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.AttachedProperties
{
    public class ClickEventAttached
    {
        public static BindableProperty ClickCommandProperty = BindableProperty.CreateAttached
            (nameof(ClickCommandProperty).GetBindableName(), typeof(ICommand), typeof(Nullable), null, propertyChanged: OnClickEvent);

        public static BindableProperty ClickParameterProperty = BindableProperty.CreateAttached
            (nameof(ClickParameterProperty).GetBindableName(), typeof(object), typeof(Nullable), null, propertyChanged: OnClickParmeter);

        public static ICommand GetClickCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(ClickCommandProperty);
        }

        public static void SetClickCommand(BindableObject view, ICommand command)
        {
            view.SetValue(ClickCommandProperty, command);
        }

        public static object GetClickParameter(BindableObject view)
        {
            return (object)view.GetValue(ClickParameterProperty);
        }

        public static void SetClickParameter(BindableObject view, object command)
        {
            view.SetValue(ClickParameterProperty, command);
        }

        private static void OnClickEvent(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as Xamarin.Forms.View;

            if (view == null)
            {
                return;
            }

            TapGestureRecognizer tapGesture = GetGestureToView(view);
            tapGesture.Command = GetClickCommand(view);
        }

        private static void OnClickParmeter(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as Xamarin.Forms.View;

            if (view == null)
            {
                return;
            }

            TapGestureRecognizer tapGesture = GetGestureToView(view);
            tapGesture.CommandParameter = GetClickParameter(view);
        }

        private static TapGestureRecognizer GetGestureToView(Xamarin.Forms.View view)
        {
            if (!view.GestureRecognizers.Any())
            {
                TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                view.GestureRecognizers.Add(tapGesture);
            }

            TapGestureRecognizer gesture = (TapGestureRecognizer)view.GestureRecognizers.FirstOrDefault();

            return gesture;;
        }
    }
}