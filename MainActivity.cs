using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using Xamarin.Essentials;

namespace PreferenceEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText editTextN;
        private Switch switchM;
        private TextView textViewV;
        private SeekBar seekBarV;
        private Button buttonSave;
        private Button buttonDelete;
        private const string Name = "name";
        private const string StreamSelection = "streamselection";
        private const string Volume = "volume";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            UIReferences();
            UIClickEvents();
            ShowUserPreferencesIfAlreadySaved();
        }

        private void ShowUserPreferencesIfAlreadySaved()
        {
            if (Preferences.ContainsKey(Name))
            {
                editTextN.Text = Preferences.Get(Name, string.Empty);
            }
            if (Preferences.ContainsKey(StreamSelection))
            {
                switchM.Checked = Preferences.Get(StreamSelection, defaultValue: false);
            }
            int volume = Preferences.Get(Volume, defaultValue: 0);
            seekBarV.SetProgress(volume, animate: true);

        }

        private void UIClickEvents()
        {
            switchM.Click += SwitchM_Click;
        }

        private void SwitchM_Click(object sender, EventArgs e)
        {
            string username = editTextN.Text;
            bool shouldStreamOnMobile = switchM.Checked;
            int volume = seekBarV.Progress;

            Preferences.Set(Name, username);
            Preferences.Set(StreamSelection, shouldStreamOnMobile);
            Preferences.Set(Volume, volume);
            Toast.MakeText(this, text: "User preferences saved successfully", ToastLength.Short).Show();
        }

        private void UIReferences()
        {
            editTextN = FindViewById<EditText>(Resource.Id.editTextName);
            switchM = FindViewById<Switch>(Resource.Id.switchE);
            seekBarV = FindViewById<SeekBar>(Resource.Id.seekBarV);
            buttonSave = FindViewById<Button>(Resource.Id.btnSave);
            buttonDelete = FindViewById<Button>(Resource.Id.btnDelete);
        }
    }
}