﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.Domain
{
    public class RandomWord : BaseEntity
    {
        public string? Word { get; set; }
        public int? CountWord { get; set; }
    }
}
