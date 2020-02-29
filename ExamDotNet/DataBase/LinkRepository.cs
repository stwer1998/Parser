using ExamDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamDotNet.DataBase
{
    public class LinkRepository
    {
        private MyDbContext db;

        public LinkRepository(MyDbContext db)
        {
            this.db = db;
        }
        public void Add(LinkModel[] linkModels) 
        {
            db.LinkModels.AddRange(linkModels);
            db.SaveChanges();
        }

        public LinkModel[] Get(string url) 
        {
            return db.LinkModels.Where(x => x.IndexedUrl == url).ToArray();
        }
    }
}
