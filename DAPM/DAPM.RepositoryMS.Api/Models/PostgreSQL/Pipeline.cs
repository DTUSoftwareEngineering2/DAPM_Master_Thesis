﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAPM.RepositoryMS.Api.Models.PostgreSQL
{
        public class Pipeline
        {
                [Key]
                [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
                public Guid Id { get; set; }

                [Required]
                public Guid RepositoryId { get; set; }

                [Required]
                public string Name { get; set; }

                [Required]
                public string PipelineJson { get; set; }

                public List<DateTime> ExecutionDate { get; set; } = new List<DateTime>();

                [Required]
                public int visibility { get; set; }

                [Required]
                public Guid userId { get; set; }

                // Navigation Attributes (Foreign Keys)
                [ForeignKey("RepositoryId")]
                public virtual Repository Repository { get; set; }

        }
}
