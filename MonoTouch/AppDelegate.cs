using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using System.IO;
using ServiceStack.Text;
using System.Text;

namespace TestServiceStackJSON1
{

    [Register ("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate 
    {
        UIWindow App_Window;
		UIWebView Browser;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {   
            App_Window = new UIWindow(UIScreen.MainScreen.Bounds);
			var vc = new UIViewController();
			var view = new UIView();
			
			var button = UIButton.FromType(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(0, 0, 100, 40);
			button.SetTitle("Do It", UIControlState.Normal);
			button.TouchDown += delegate {
				Test_JSON();
			};
			view.AddSubview(button);			
			
			Browser = new UIWebView();
			Browser.Frame = new RectangleF(0, 50, UIScreen.MainScreen.Bounds.Width, 
				UIScreen.MainScreen.Bounds.Height-50);
			view.AddSubview(Browser);
				
			vc.View = view;
			App_Window.RootViewController = vc;
			App_Window.MakeKeyAndVisible();
						
			return true;
        }	
		
		private void Test_JSON()
		{
			string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
				"test.json");
			
			var obj = new TestClass();
			obj.Prop1 = "Hello World";
			
			try
			{
	            using (var writer = File.CreateText(file))
	            {
					JsonSerializer.SerializeToWriter<TestClass>(obj, writer);
	            }
			}
			catch (Exception ex)
			{
				Error(ex);
				return;
			}

			try
			{
	            using (var reader = File.OpenText(file))
	            {
	                obj = JsonSerializer.DeserializeFromReader<TestClass>(reader);
	            }
				HTML(obj.Prop1);
			}
			catch (Exception ex)
			{
				Error(ex);
			}
			
		}
		
		private void Error(Exception ex)
		{
			HTML("Error:<pre>" + Concat_Stack_Traces(ex) + "</pre>");
			System.Diagnostics.Debug.WriteLine(ex.StackTrace);
		}
				
		private void HTML(string html)
		{
			Browser.LoadHtmlString(html, new NSUrl("/"));
		}
		
		public static string Concat_Stack_Traces(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.AppendLine(ex.Message);
                string trace = null;
                if (string.IsNullOrEmpty(ex.StackTrace))
                {
                    trace = "<No Stack Trace>";
                }
                else
                {
                    trace = ex.StackTrace;
                }
                sb.AppendLine(trace);
                ex = ex.InnerException;
            } while (ex != null);

            return sb.ToString();
        }

    }
} 