using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
