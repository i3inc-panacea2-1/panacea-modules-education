using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Education.Models
{
    [DataContract]
    public class GetCategoryResponse
    {
        [DataMember(Name = "Education")]
        public CategoryWrapper Education { get; set; }
    }

    [DataContract]
    public class CategoryWrapper
    {
        [DataMember(Name = "educationCategory")]
        public EducationCategory EducationCategory { get; set; }
    }
}
