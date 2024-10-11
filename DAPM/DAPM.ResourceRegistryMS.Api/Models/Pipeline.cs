using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAPM.ResourceRegistryMS.Api.Models
{
    public class Pipeline
    {
        // Attributes (Columns)
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid PeerId { get; set; }
        public Guid RepositoryId { get; set; }
        public Guid CompanyAId {get; set; }
        public Guid CompanyBId {get; set;}
        public Guid SharedProcessDetails { get; set; }
        // Navigation Attributes (Foreign Keys)

        [ForeignKey("PeerId")]
        public virtual Peer Peer { get; set; }
        [ForeignKey("PeerId, RepositoryId")]
        public virtual Repository Repository { get; set; }
    }
}
