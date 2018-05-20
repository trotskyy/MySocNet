using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MySocNet.Mvc.Providers
{
    /// <summary>
    /// This class is used to keep track of session via cookie file. This class - is the data to put into that cookie
    /// </summary>
    [DataContract]
    public class MembershipUserSerializeModel
    {
        [DataMember]
        public int UserId { get; set; }
        /// <summary>
        /// UserName, Email
        /// </summary>
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public List<string> Roles { get; set; }
    }
}