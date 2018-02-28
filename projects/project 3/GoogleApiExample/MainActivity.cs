﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;

namespace GoogleApiExample
{
    [Activity(Label = "GoogleApiExample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        List<string> tags = new List<string>();
        private string submittedAnswer;
        private Google.Apis.Vision.v1.Data.BatchAnnotateImagesResponse apiResult;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (IsThereAnAppToTakePictures() == true)
            {
                FindViewById<Button>(Resource.Id.launchCameraButton).Click += TakePicture;
            }
        }


        /// <summary>
        /// Apparently, some android devices do not have a camera.  To guard against this,
        /// we need to make sure that we can take pictures before we actually try to take a picture.
        /// </summary>
        /// <returns></returns>
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities
                (intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }


        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        // <summary>
        // Called automatically whenever an activity finishes
        // </summary>
        // <param name = "requestCode" ></ param >
        // < param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;

            //AC: workaround for not passing actual files
            Android.Graphics.Bitmap bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");

            //convert bitmap into stream to be sent to Google API
            string bitmapString = "";
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 0, stream);

                var bytes = stream.ToArray();
                bitmapString = System.Convert.ToBase64String(bytes);
            }

            //credential is stored in "assets" folder
            string credPath = "google_api.json";
            Google.Apis.Auth.OAuth2.GoogleCredential cred;

            //Load credentials into object form
            using (var stream = Assets.Open(credPath))
            {
                cred = Google.Apis.Auth.OAuth2.GoogleCredential.FromStream(stream);
            }
            cred = cred.CreateScoped(Google.Apis.Vision.v1.VisionService.Scope.CloudPlatform);

            // By default, the library client will authenticate 
            // using the service account file (created in the Google Developers 
            // Console) specified by the GOOGLE_APPLICATION_CREDENTIALS 
            // environment variable. We are specifying our own credentials via json file.
            var client = new Google.Apis.Vision.v1.VisionService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApplicationName = "mobile-apps-pa3",
                HttpClientInitializer = cred
            });

            //set up request
            var request = new Google.Apis.Vision.v1.Data.AnnotateImageRequest();
            request.Image = new Google.Apis.Vision.v1.Data.Image();
            request.Image.Content = bitmapString;

            //tell google that we want to perform label detection
            request.Features = new List<Google.Apis.Vision.v1.Data.Feature>();
            request.Features.Add(new Google.Apis.Vision.v1.Data.Feature() { Type = "LABEL_DETECTION" });
            var batch = new Google.Apis.Vision.v1.Data.BatchAnnotateImagesRequest();
            batch.Requests = new List<Google.Apis.Vision.v1.Data.AnnotateImageRequest>();
            batch.Requests.Add(request);

            //send request.  Note that I'm calling execute() here, but you might want to use
            //ExecuteAsync instead
            apiResult = client.Images.Annotate(batch).Execute();
            tags = new List<string>();
            foreach(var item in apiResult.Responses[0].LabelAnnotations)
                {
                tags.Add(item.Description);
            }
            SetContentView(Resource.Layout.IsItA);

            ImageView apiPicture = FindViewById<ImageView>(Resource.Id.ImageToGuess);
            apiPicture.SetImageBitmap(bitmap);

            string question = string.Format("Is it (a(n)) {0}?", tags[0]);
            TextView output = FindViewById<TextView>(Resource.Id.IsItATextView);
            output.Text = question;

            FindViewById<Button>(Resource.Id.YesButton).Click += YesButtonClick;
            FindViewById<Button>(Resource.Id.NoButton).Click += NoButtonClick;
            
            if (bitmap != null)
            {
                imageView.SetImageBitmap(bitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;
                bitmap = null;
            }

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }
        
        private void SubmitButtonClick(object sender, System.EventArgs e)
        {
            EditText TextboxSubmit = FindViewById<EditText>(Resource.Id.TextboxSubmit);
            submittedAnswer = TextboxSubmit.Text;
            foreach (var annotation in apiResult.Responses[0].LabelAnnotations)
            {
                if (submittedAnswer == annotation.Description)
                {
                    SetContentView(Resource.Layout.PercentChance);
                    float percentage = (float)annotation.Score * 100;
                    string question = "There was a " + percentage + "% chance that it was (a(n)) ";
                    question += submittedAnswer;
                    TextView output = FindViewById<TextView>(Resource.Id.PercentText);
                    output.Text = question;
                break;
                }
                else
                {
                    SetContentView(Resource.Layout.NoIdea);
                    TextView output = FindViewById<TextView>(Resource.Id.NoIdeaText);
                    output.Text = "Wow, I had no idea that the picture was (a(n)) " + submittedAnswer;
                }
            }
        }

        private void NoButtonClick(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Darn);
            FindViewById<Button>(Resource.Id.SubmitButton).Click += SubmitButtonClick;
        }

        private void YesButtonClick(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Success);
        }
    }
}

