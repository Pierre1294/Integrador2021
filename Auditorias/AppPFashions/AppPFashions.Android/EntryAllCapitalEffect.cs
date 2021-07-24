using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("StackOverflow")] 
[assembly: ExportEffect(typeof(AppPFashions.Droid.EntryAllCapitalEffect), "EntryAllCapitalEffect")]
namespace AppPFashions.Droid
{
    public class EntryAllCapitalEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                //Let's don't do anything if the control is not a EditText
                if (!(Control is EditText editText))
                {
                    return;
                }

                //Force the keyboard setup all Caps letters
                // But the user can still change the Caps taping on Shift
                editText.InputType = InputTypes.TextFlagCapCharacters;

                // Update any lowercase into Uppercase
                var filters = new List<IInputFilter>(editText.GetFilters());
                filters.Add(new InputFilterAllCaps());
                editText.SetFilters(filters.ToArray());
            }
            catch (Exception) { }
        }

        protected override void OnDetached()
        {
        }
    }
}