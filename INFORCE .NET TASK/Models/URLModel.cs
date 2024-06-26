using System;

namespace INFORCE_.NET_TASK.Models
{
    public class URLModel
    {
        public int Id { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Author { get; set; }
        public DateTime CreatedTime { get; set; }

        public URLModel()
        {
            CreatedTime = DateTime.Now;
        }

        public void GenerateShortName()
        {
            ShortName = Guid.NewGuid().ToString().Substring(0, 8); 
        }
    }
}
