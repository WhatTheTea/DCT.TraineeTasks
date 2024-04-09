﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Models
{
    internal class Person
    {
        public string Name => $"{this.FirstName} {this.LastName}";
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}