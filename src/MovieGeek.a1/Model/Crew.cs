using System;
using System.Collections.Generic;

namespace MovieGeek.a1.Model
{
    public partial class Crew
    {
        public String Name { get; set; }
        public String Affiliation { get; set; }
        public String RefUrl { get; set; }
        public String RolePlay { get { return _rolePlay; } set { _rolePlay = value.Trim().Replace(" /  \n            "," aka "); } }
        public String PotraitUri { get; set; }
        public String KnownFor { get; set; }
        public String Bio { get; set; }
        public String Ranking
        {
            get { return _ranking; }
            set
            {
                if (value.Contains("."))
                    value = value.Remove(value.Length);
                _ranking = value;
            }
        }
    }
    public partial class Crew
    {
        private String _ranking;
        private String _rolePlay;
    }
}