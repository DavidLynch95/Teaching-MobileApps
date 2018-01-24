using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace PA1
{
    [Activity(Label = "PA1", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        Stack<double> calcStack = new Stack<double>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            Button button3 = FindViewById<Button>(Resource.Id.button3);
            Button button4 = FindViewById<Button>(Resource.Id.button4);
            Button button5 = FindViewById<Button>(Resource.Id.button5);
            Button button6 = FindViewById<Button>(Resource.Id.button6);
            Button button7 = FindViewById<Button>(Resource.Id.button7);
            Button button8 = FindViewById<Button>(Resource.Id.button8);
            Button button9 = FindViewById<Button>(Resource.Id.button9);
            Button button0 = FindViewById<Button>(Resource.Id.button0);
            Button buttonEnter = FindViewById<Button>(Resource.Id.buttonEnter);
            Button buttonClear = FindViewById<Button>(Resource.Id.buttonClear);
            Button buttonAdd = FindViewById<Button>(Resource.Id.buttonAdd);
            Button buttonSubtract = FindViewById<Button>(Resource.Id.buttonSubtract);
            Button buttonMult = FindViewById<Button>(Resource.Id.buttonMult);
            Button buttonDivide = FindViewById<Button>(Resource.Id.buttonDivide);
           
            button1.Click += Button_Click;
            button2.Click += Button_Click;
            button3.Click += Button_Click;
            button4.Click += Button_Click;
            button5.Click += Button_Click;
            button6.Click += Button_Click;
            button7.Click += Button_Click;
            button8.Click += Button_Click;
            button9.Click += Button_Click;
            button0.Click += Button_Click;
            buttonEnter.Click += Enter_Click;
            buttonClear.Click += Reset_Text;
            buttonAdd.Click += Button_Click; ;
            buttonSubtract.Click += Button_Click;
            buttonMult.Click += Button_Click;
            buttonDivide.Click += Button_Click;
            


        }

        private void Enter_Click(object sender, System.EventArgs e)
        {
            double a = 0;
            double b = 0;
            double result = 0.0;
            TextView output = FindViewById<TextView>(Resource.Id.textView1);

            if (double.TryParse(output.Text, out result) == false)
            {
                //must be an operation
                a = calcStack.Pop();
                b = calcStack.Pop();
                if (output.Text == "+")
                {
                    output.Text = (a + b).ToString();
                }
                else if (output.Text == "-")
                {
                    output.Text = (b - a).ToString();
                }
                else if (output.Text == "*")
                {
                    output.Text = (a * b).ToString();
                }
                else if (output.Text == "/")
                {
                    output.Text = (b / a).ToString();
                }
            }
            else
            {
                //must be a number
                calcStack.Push(System.Convert.ToDouble(output.Text));
                TextView output2 = FindViewById<TextView>(Resource.Id.textView1);
                output.Text = "";
            }
        }

        private void Reset_Text(object sender, System.EventArgs e)
        {
            TextView output = FindViewById<TextView>(Resource.Id.textView1);
            output.Text = "";
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Button someButton = sender as Button;
            if(someButton != null)
            {
                TextView output = FindViewById<TextView>(Resource.Id.textView1);
                output.Text += someButton.Text;
            }
        }
    }
}

