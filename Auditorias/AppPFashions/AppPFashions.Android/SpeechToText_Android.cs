using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Views;
using Android.Widget;
using AppPFashions.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppPFashions.Droid.SpeechToText_Android))]

namespace AppPFashions.Droid
{
    public class SpeechToText_Android : ISpeechToText
    {
        public static AutoResetEvent autoEvent = new AutoResetEvent(false);
        public static string SpeechText;
        const int VOICE = 10;

        public async Task<string> SpeechToTextAsync()
        {
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Sprechen Sie jetzt");
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 200);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 200);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 200);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

            SpeechText = "";
            autoEvent.Reset();
            ((Activity)Forms.Context).StartActivityForResult(voiceIntent, VOICE);
            await Task.Run(() => { autoEvent.WaitOne(new TimeSpan(0, 1, 0)); });
            return SpeechText;
        }
    }
}