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
	}
}