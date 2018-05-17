using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Bll.Dto
{
    public class UserFilterDto
    {
        public string FullName { get; set; }
        public string CurrentState { get; set; }
        public string CurrentCity { get; set; }
        public string StateOfBirth { get; set; }
        public string CityOfBirth { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public bool? IsMale { get; set; }
        public string AboutSelf { get; set; }
    }
}
