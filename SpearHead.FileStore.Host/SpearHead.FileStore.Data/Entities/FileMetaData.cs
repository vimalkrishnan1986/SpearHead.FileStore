using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearHead.FileStore.Data.Entities
{
    public sealed class FileMetaData
    {
        public int Id { get; set; }
        public DateTime UploadTime { get; set; }
        public string SavedFileName { get; set; }
        public string ActualFileName { get; set; }
    }
}
