using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime CreateTime { get; set; }

        DateTime UpdateTime { get; set; }
    }
}
