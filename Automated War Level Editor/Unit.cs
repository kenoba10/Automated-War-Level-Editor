﻿using System;
using System.Runtime.Serialization;

namespace Automated_War_Level_Editor
{

    [DataContract]
    public class Unit
    {

        public Unit()
        {

        }

        [DataMember(Name="id")]
        public string ID
        {

            get;
            set;

        }

        [DataMember(Name="name")]
        public string Name
        {

            get;
            set;

        }

    }

}
