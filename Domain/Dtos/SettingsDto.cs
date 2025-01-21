using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class SettingsDto
    {
        public General General { get; set; }
    }
    public class General
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThemeColor { get; set; }
        public bool EnableCommenting { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }

    }

}
