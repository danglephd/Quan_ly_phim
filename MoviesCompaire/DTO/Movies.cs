using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCompaire.DTO
{
    public class Movies
    {
        private string name;
        private int id;
        private string path;
        private int level;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
