﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OxuAz.Utilities
{
    public static class Utilities
    {
        public static void RemoveImage(string root, string image)
        {
            string path = Path.Combine(root, "img", image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
