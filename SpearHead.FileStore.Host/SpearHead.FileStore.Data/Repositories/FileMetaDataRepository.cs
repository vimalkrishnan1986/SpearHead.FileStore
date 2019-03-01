using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpearHead.FileStore.Data.Entities;

namespace SpearHead.FileStore.Data.Repositories
{
    public sealed class FileMetaDataRepository : BaseRepository<FileMetaData>
    {
        public FileMetaDataRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
