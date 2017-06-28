using Android.App;
using Android.Widget;
using Android.OS;
using Entities;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/hh")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var ButtonValidate = FindViewById<Button>(Resource.Id.buttonValidate);
            var EmailText = FindViewById<EditText>(Resource.Id.editTextEmail);
            var PasswordText = FindViewById<EditText>(Resource.Id.editTextPassword);

            ButtonValidate.Click += (object sender, System.EventArgs e) =>
            {
                Validate(EmailText.Text, PasswordText.Text);
            };

            SendEvidence();
        }

        public async void Validate(string Email, string Password)
        {
            var ServiceClient = new SAL.ServiceClient();

            var Result =
                await ServiceClient.AutenticateAsync(
                    Email, Password);

            if (Result.Status == Entities.Status.Success)
            {
                var ActivityIntent =
                    new Android.Content.Intent(this, typeof(EvidencesActivity));
                ActivityIntent.PutExtra("Token",Result.Token);
                ActivityIntent.PutExtra("FullName", Result.FullName);
                StartActivity(ActivityIntent);
            }
            else
            {
                AlertDialog.Builder Builder =
                    new AlertDialog.Builder(this);
                AlertDialog Alert = Builder.Create();
                Alert.SetTitle("Resultado de la verificación");
                Alert.SetIcon(Resource.Drawable.Icon);
                Alert.SetMessage(
                    $"{Result.Status}\n{Result.FullName}\n{Result.Token}");
                Alert.SetButton("Ok", (s, ev) => { });
                Alert.Show();
            }
        }

        public async void SendEvidence()
        {
            var MicrosoftEvidence = new LabItem
            {
                Email = "jdiego7118@gmail.com",
                Lab = "Hack@Home",
                DeviceId = Android.Provider.Settings.Secure.GetString(
                    ContentResolver, Android.Provider.Settings.Secure.AndroidId)
            };

            var MicrosoftClient = new SAL.MicrosoftServiceClient();
            await MicrosoftClient.SendEvidence(MicrosoftEvidence);
        }
    }
}

