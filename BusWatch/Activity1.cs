using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.Telephony;

namespace BusWatch
{
    public class Buslayout
    {
        public LinearLayout separator;
        public LinearLayout buslayout;
        public TextView busnum;
        public TextView busstop;

        //convert points to pixels
        private int dptopixels(float dp)
        {
            int pixels = (int)((dp) * Resources.System.DisplayMetrics.Density + 0.5f);
            return pixels;
        }

        //This view is used to display bus info
        public void createview()
        {
            Context thiscon = Application.Context;

            //bus information
            buslayout = new LinearLayout(thiscon);
            buslayout.Orientation = Android.Widget.Orientation.Horizontal;
            buslayout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                                                                     ViewGroup.LayoutParams.WrapContent);
            buslayout.LayoutParameters.Height = dptopixels(89f);

            //bus number at left
            busnum = new TextView(thiscon);
            busnum.Text = "15";
            busnum.SetTextColor(Android.Graphics.Color.White);
            busnum.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent);

            busnum.SetWidth(dptopixels(65f));
            busnum.SetTextSize(Android.Util.ComplexUnitType.Px, 80);

            //bus stop information
            busstop = new TextView(thiscon);
            busstop.Text = "Shannon and Shawigan";
            busstop.SetTextColor(Android.Graphics.Color.White);

            busstop.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent);

            busstop.SetWidth(dptopixels(319.5f));
            busstop.SetTextSize(Android.Util.ComplexUnitType.Px, 35);

            buslayout.AddView(busnum);
            buslayout.AddView(busstop);

            //line between each bus information
            separator = new LinearLayout(thiscon);
            separator = new LinearLayout(thiscon);
            separator.Orientation = Android.Widget.Orientation.Horizontal;
            separator.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, dptopixels(1.5f));
            separator.SetBackgroundColor(Android.Graphics.Color.Black);
        }
    }


    [Activity(Label = "BusWatch", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : FragmentActivity
    {
        public static Buslayout[] lay = new Buslayout[10];
        
        public static View view1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            var pager = FindViewById<ViewPager>(Resource.Id.pager);
            var adaptor = new GenericFragmentPagerAdaptor(SupportFragmentManager);

            BusWatch.CfgRW.Writecfg();
            BusWatch.CfgRW.Readcfg();

            adaptor.AddFragmentView((i, v, b) =>
            {
                view1 = i.Inflate(Resource.Layout.buspage, v, false);

                LinearLayout baselayout = view1.FindViewById<LinearLayout>(Resource.Id.linearLayout1);

                int cnt = 0;

                lay[cnt] = new Buslayout();
                lay[cnt].createview();
                lay[cnt].busnum.Text = BusWatch.CfgRW.busstopinfo[cnt, 0];
                lay[cnt].busstop.Text = BusWatch.CfgRW.busstopinfo[cnt, 2] + ", stop id: " + BusWatch.CfgRW.busstopinfo[cnt, 1] + "\r\n" + "Next bus: ";
                baselayout.AddView(lay[cnt].buslayout);
                baselayout.AddView(lay[cnt].separator);
                cnt++;

                lay[cnt] = new Buslayout();
                lay[cnt].createview();
                lay[cnt].busnum.Text = BusWatch.CfgRW.busstopinfo[cnt, 0];
                lay[cnt].busstop.Text = BusWatch.CfgRW.busstopinfo[cnt, 2] + ", stop id: " + BusWatch.CfgRW.busstopinfo[cnt, 1] + "\r\n" + "Next bus: ";
                baselayout.AddView(lay[cnt].buslayout);
                baselayout.AddView(lay[cnt].separator);
                cnt++;
                /*
                lay[cnt] = new Buslayout();
                lay[cnt].createview();
                lay[cnt].busnum.Text = BusWatch.CfgRW.busstopinfo[cnt, 0];
                lay[cnt].busstop.Text = BusWatch.CfgRW.busstopinfo[cnt, 2] + ", stop id: " + BusWatch.CfgRW.busstopinfo[cnt, 1] + "\r\n" + "Next bus: ";
                baselayout.AddView(lay[cnt].buslayout);
                baselayout.AddView(lay[cnt].separator);
                cnt++;
                */
                var button1 = view1.FindViewById<Button>(Resource.Id.button1);
                button1.Click += delegate {
                    //lay[0].busstop.Text = "At return page";
                    cnt = 0;
                    string msg = BusWatch.CfgRW.busstopinfo[cnt, 1] + "#" + BusWatch.CfgRW.busstopinfo[cnt, 0];
                    SmsManager.Default.SendTextMessage(BusWatch.CfgRW.busstopinfo[cnt, 3], null, msg, null, null);
                    cnt++;

                    msg = BusWatch.CfgRW.busstopinfo[cnt, 1] + "#" + BusWatch.CfgRW.busstopinfo[cnt, 0];
                    SmsManager.Default.SendTextMessage(BusWatch.CfgRW.busstopinfo[cnt, 3], null, msg, null, null);
                    cnt++;

                    msg = BusWatch.CfgRW.busstopinfo[cnt, 1] + "#" + BusWatch.CfgRW.busstopinfo[cnt, 0];
                    SmsManager.Default.SendTextMessage(BusWatch.CfgRW.busstopinfo[cnt, 3], null, msg, null, null);
                    cnt++;

                };

                return view1;
            }
            );

            adaptor.AddFragmentView((i, v, b) =>
            {
                var view = i.Inflate(Resource.Layout.buspage, v, false);

                LinearLayout baselayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout1);

                int cnt2 = 5;

                lay[cnt2] = new Buslayout();
                lay[cnt2].createview();
                lay[cnt2].busnum.Text = BusWatch.CfgRW.busstopinfo[cnt2, 0];
                lay[cnt2].busstop.Text = BusWatch.CfgRW.busstopinfo[cnt2, 2] + ", stop id: " + BusWatch.CfgRW.busstopinfo[cnt2, 1] + "\r\n" + "Next bus: ";
                baselayout.AddView(lay[cnt2].buslayout);
                baselayout.AddView(lay[cnt2].separator);
                cnt2++;
                /*
                lay[cnt2] = new Buslayout();
                lay[cnt2].createview();
                lay[cnt2].busnum.Text = BusWatch.CfgRW.busstopinfo[cnt2, 0];
                lay[cnt2].busstop.Text = BusWatch.CfgRW.busstopinfo[cnt2, 2] + ", stop id: " + BusWatch.CfgRW.busstopinfo[cnt2, 1] + "\r\n" + "Next bus: ";
                baselayout.AddView(lay[cnt2].buslayout);
                baselayout.AddView(lay[cnt2].separator);
                cnt2++;
                */
                var button1 = view.FindViewById<Button>(Resource.Id.button1);
                button1.Click += delegate {
                    //lay[5].busstop.Text = "At leave page";
                    cnt2 = 5;
                    string msg = BusWatch.CfgRW.busstopinfo[cnt2, 1] + "#" + BusWatch.CfgRW.busstopinfo[cnt2, 0];
                    SmsManager.Default.SendTextMessage(BusWatch.CfgRW.busstopinfo[cnt2, 3], null, msg, null, null);
                    cnt2++;

                    msg = BusWatch.CfgRW.busstopinfo[cnt2, 1] + "#" + BusWatch.CfgRW.busstopinfo[cnt2, 0];
                    SmsManager.Default.SendTextMessage(BusWatch.CfgRW.busstopinfo[cnt2, 3], null, msg, null, null);
                    cnt2++;
                };

                //BusWatch.CfgRW.Readcfg();

                return view;
            }
            );

            pager.Adapter = adaptor;
            //pager.SetOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));
            pager.AddOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));

            ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Return"));
            ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Leave"));

        }
    }
}

