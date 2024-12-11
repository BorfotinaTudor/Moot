using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Moot.Models
{
    public class Agent
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Agent Name")]
        [StringLength(50)]
        public string AgentName { get; set; }

        [StringLength(70)]
        public string Title { get; set; }
        public ICollection<PublishedProperty>? PublishedProperties { get; set; }

    }
}

