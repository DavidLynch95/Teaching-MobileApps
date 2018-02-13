using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using Android.Graphics;

namespace CameraExample
{
    [Activity(Label = "CameraExample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Used to track the file that we're manipulating between functions
        /// </summary>
        public static Java.IO.File _file;

        /// <summary>
        /// Used to track the directory that we'll be writing to between functions
        /// </summary>
        public static Java.IO.File _dir;

        public static Bitmap bitmap;
        public static Bitmap copyBitmap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (IsThereAnAppToTakePictures() == true)
            {
                CreateDirectoryForPictures();
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

        /// <summary>
        /// Creates a directory on the phone that we can place our images
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            _dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraExample");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            //android.support.v4.content.FileProvider
            //getUriForFile(getContext(), "com.mydomain.fileprovider", newFile);
            //FileProvider.GetUriForFile

            //The line is a problem line for Android 7+ development
            //intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
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

            //change view to the second layout where editing takes place
            //SetContentView(Resource.Layout.view2);

            //Make image available in the gallery
            /*
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(_file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            */

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = imageView.Height;
            int width = imageView.Width;
            bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            copyBitmap = bitmap.Copy(Bitmap.Config.Argb8888, true);

            ImageView editor = FindViewById<ImageView>(Resource.Id.ImageToEdit);

            SetContentView(Resource.Layout.view2);

            if (copyBitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
            }

            // Dispose of the Java side bitmap.
            System.GC.Collect();

            FindViewById<Button>(Resource.Id.RemoveRed).Click += RemoveRedButton;
            FindViewById<Button>(Resource.Id.RemoveGreen).Click += RemoveGreenButton;
            FindViewById<Button>(Resource.Id.RemoveBlue).Click += RemoveBlueButton;
            FindViewById<Button>(Resource.Id.NegateRed).Click += NegateRedButton;
            FindViewById<Button>(Resource.Id.NegateGreen).Click += NegateGreenButton;
            FindViewById<Button>(Resource.Id.NegateBlue).Click += NegateBlueButton;
            FindViewById<Button>(Resource.Id.Grayscale).Click += GrayscaleButton;
            FindViewById<Button>(Resource.Id.AddNoise).Click += AddNoiseButton;
            FindViewById<Button>(Resource.Id.HighContrast).Click += HighContrastButton;
        }

        private void HighContrastButton(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void AddNoiseButton(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void GrayscaleButton(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void NegateBlueButton(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void NegateGreenButton(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void NegateRedButton(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void RemoveBlueButton(object sender, System.EventArgs e)
        {
            //this code removes all red from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.B = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void RemoveGreenButton(object sender, System.EventArgs e)
        {
            //this code removes all red from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.G = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void RemoveRedButton (object sender, System.EventArgs e)
        {
            //this code removes all red from a picture
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
        }
    }
}