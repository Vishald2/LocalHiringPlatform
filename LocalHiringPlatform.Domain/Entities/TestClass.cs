using LocalHiringPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Entities
{
    public class TestClass : BaseEntity
    {
        public Guid TestClassId
        {
            get => EntityId;
            set => EntityId = value;
        }
    }
}
