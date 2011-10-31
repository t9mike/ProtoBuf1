using System;
using System.Diagnostics;
using T9Mike.Samples.ClassLibrary;
using System.IO;
using System.Text;

namespace T9Mike.Samples.SerializationTest
{
	using T9Mike.Samples.Serializer;
	public static class TestSerialization
	{
        public static string Run(string out_dir)
        {
			var sb = new StringBuilder();
			
            // Time creation of serializer
            var sw = new Stopwatch();
            var serializer = new Serializer();
            sb.AppendLine("Serializer init took " + sw.Elapsed);
            
            var data = SubClass.Sample();

            // Time serializing prefs
            string out_file = Path.Combine(out_dir, "data.bin"); 
            sw.Reset();
            using (var stream = File.Create(out_file))
            {
                serializer.Serialize(stream, data);
            }
            sb.AppendLine("Serialization took " + sw.Elapsed);

            // Time de-serializing prefs
            sw.Reset();
            using (var stream = File.OpenRead(out_file))
            {
                data = (SubClass)serializer.Deserialize(stream, null, typeof(SubClass));
            }
            sb.AppendLine("De-serialization took " + sw.Elapsed);

            sb.AppendLine("Date_Saved: " + data.Date_Saved);
            sb.AppendLine("Field1: " + data.Field1);
            sb.AppendLine("Field2: " + data.Field2);
			
			return sb.ToString();
        }	
	}
}