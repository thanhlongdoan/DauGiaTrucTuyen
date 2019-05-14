﻿using System;
using System.Runtime.Serialization;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class ChartViewModel
    {
        public DateTime dateTime { get; set; }
    }

    [DataContract]
    public class DataPoint
    {
        public DataPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public Nullable<double> X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}