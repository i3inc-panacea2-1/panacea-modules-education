using Panacea.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Education.Models
{
    [DataContract]
    public class ServerTreeItem<T> : ServerItem
    {
        [DataMember(Name = "children")]
        public virtual ObservableCollection<T> Children { get; set; }
    }
}
