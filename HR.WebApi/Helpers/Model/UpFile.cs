﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Helpers.Model
{
    #region using

    using System;

    #endregion

    public class UpFile
    {
        public DateTime CreatedDate { get; set; }

        public string FileType { get; set; }

        public string FullPath { get; set; }

        public Guid DocumentId { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int Size { get; set; }
    }
}
