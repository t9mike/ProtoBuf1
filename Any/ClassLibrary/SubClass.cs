using System;
using ProtoBuf;

namespace T9Mike.Samples.ClassLibrary
{
    [ProtoContract]
	public class SubClass : BaseClass
	{	
        // [ProtoMember(1)]
		// public TaskID Last_Task = TaskID.Articles;

        [ProtoMember(2)]
        public string Field1 = "";

        [ProtoMember(3)]
        public string Field2 = "";

		public static SubClass Sample()
		{
			return new SubClass { Date_Saved = DateTime.Now, Field1 = "Hello", Field2 = "World" };
		}
}
	
}