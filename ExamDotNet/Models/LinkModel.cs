using System;
using System.ComponentModel.DataAnnotations;

namespace ExamDotNet.Models
{
    public class LinkModel
    {
        [Key]
        public Guid IdGuid { get; set; }

        public string Url { get; set; }

        public string IndexedUrl { get; set; }

        public string Text { get; set; }

        public LinkModel(Uri uri,string url)
        {
            IdGuid = Guid.NewGuid();
            IndexedUrl = url;
            Url = uri.AbsoluteUri;
        }

        public LinkModel()
        {

        }
    }
}
