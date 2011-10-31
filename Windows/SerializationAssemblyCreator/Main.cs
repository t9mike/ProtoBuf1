using System;
using ProtoBuf.Meta;
using System.Diagnostics;
using T9Mike.Samples.ClassLibrary;

namespace SerializationAssemblyCreator
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var model = TypeModel.Create();

            // Subtype business is required to get both base class and 
            // sub-class properties to be serialized. Ug.
            //
            // See http://stackoverflow.com/questions/6109868/protobuf-net-base-class-properties-is-not-included-when-serializing-derived-clas
			model.Add(typeof(BaseClass), true).AddSubType(101, typeof(SubClass));

            string class_name = "T9Mike.Samples.Serializer.Serializer";
            string dll_name = "T9Mike.Samples.Serializer.dll";

            model.Compile(class_name, dll_name);

            Run_Via_Bash("../../../../fix-protobuf-assembly",
                dll_name, class_name);
            Run_Via_Bash("/bin/cp", dll_name, "../../../../Assemblies/Any");
        }

        private static void Run_Via_Bash(params string[] cmd_and_args)
        {
            string bash_cmd = String.Join(" ", cmd_and_args);

            var pinfo = new ProcessStartInfo("c:/cygwin/bin/bash", 
                "-i -c '" + bash_cmd + "'");
            pinfo.CreateNoWindow = true;
            pinfo.UseShellExecute = false;
            pinfo.RedirectStandardError = true;
            pinfo.RedirectStandardOutput = true;
            pinfo.ErrorDialog = false;
            // pinfo.EnvironmentVariables["PERLLIB"] = "/usr/local/mmuegel/lib/perl5:/usr/local/mmuegel/lib/perl4";
            //int exit_code = MiscUtils.Run_Process(pinfo);

            Process proc = new Process();
            proc.EnableRaisingEvents = true;
            proc.ErrorDataReceived += new DataReceivedEventHandler(proc_ErrorDataReceived);
            proc.OutputDataReceived += new DataReceivedEventHandler(proc_OutputDataReceived);
            proc.StartInfo = pinfo;

            Console.WriteLine("Running " + bash_cmd + " ...");
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            proc.WaitForExit();
            int exit_code = proc.ExitCode;

            // int exit_code = MiscUtils.Run_Command(cmd, args);
            if (exit_code != 0)
            {
                throw new Exception("command " + bash_cmd + " exited with status " + 
                    exit_code);
            }
        }

        static void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
                Console.WriteLine(e.Data);
        }

        static void proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
                Console.WriteLine(e.Data);
        }
	}
}
