using System;
using ProtoBuf;

namespace T9Mike.Samples.ClassLibrary
{
    [ProtoContract()]
    public class BaseClass
	{
        [ProtoMember(1)]
        public DateTime Date_Saved;
	}
}

