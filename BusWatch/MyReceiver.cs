using Android.App;
using Android.Content;
using Android.Provider;
using Android.Support.V4.View;
using Android.Telephony;

using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;

namespace BusWatch
{
    //class MyReceiver
    [BroadcastReceiver(Exported = true, Label = "SMS Receiver")]
    [IntentFilter(new string[] { "android.provider.Telephony.SMS_RECEIVED", "com.alr.text" })]
    public class MyReceiver : Android.Content.BroadcastReceiver
    {
        //private string[,] bustimeinfo = new string[10,2];
        private const string Tag = "SMSBroadcastReceiver";
        private const string IntentAction = "android.provider.Telephony.SMS_RECEIVED";
        
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == IntentAction)
            {
                SmsMessage[] messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);

                //var sb = new StringBuilder();
                string[] splitstr;
                string[] timestr;
                for (var i = 0; i < messages.Length; i++)
                {
                    if (messages[i].OriginatingAddress == "74000")
                    {
                        splitstr = messages[i].MessageBody.Split('\n');
                        timestr = splitstr[1].Split(')');

                        int mcnt = 0;
                        for (mcnt = 0; mcnt < 10; mcnt++)
                        {
                            //find the matching stopid
                            if (BusWatch.CfgRW.busstopinfo[mcnt, 1] == splitstr[0])
                            {
                                BusWatch.Activity1.lay[mcnt].busstop.Text += timestr[1];
                                break;
                            }
                        }
                        
                    }
                }
                //Toast.MakeText(context, sb.ToString(), ToastLength.Short).Show();

            }
        }
    }


}