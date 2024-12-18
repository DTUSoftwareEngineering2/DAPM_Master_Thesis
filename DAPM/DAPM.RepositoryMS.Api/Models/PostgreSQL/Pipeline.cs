using System.ComponentModel.DataAnnotations.Schema;
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

                // Authors: s242147 and s241747 : List Attribute to store the execution dates of the pipeline
                public List<DateTime> ExecutionDate { get; set; } = new List<DateTime>();

                // Author: Maxime Rochat - s241741
                [Required]
                public int visibility { get; set; }

                // Author: Maxime Rochat - s241741
                [Required]
                public Guid userId { get; set; }

                // Navigation Attributes (Foreign Keys)
                [ForeignKey("RepositoryId")]
                public virtual Repository Repository { get; set; }

        }
}
