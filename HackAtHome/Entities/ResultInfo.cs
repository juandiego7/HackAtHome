﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Entities
{
    public class ResultInfo
    {
        public Status Status { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
    }

    public enum Status
    {
        Error = 0,
        Success = 1,
        InvalidUserOrNotInEvent = 2,
        OutOfDate = 3,
        AllSuccess = 999
    }
}