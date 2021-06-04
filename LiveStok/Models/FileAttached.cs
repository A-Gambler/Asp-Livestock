using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livestock.Models
{
    public class FileAttached
    {
        public string Name { get; set; }
        public string NameCompleteWithGuid { get; set; }
        public string ImageThumbnailRelativePath { get; set; }
        public string FileRelativePath { get; set; }
        public string FilePhysicalPath { get; set; }
    }
}
