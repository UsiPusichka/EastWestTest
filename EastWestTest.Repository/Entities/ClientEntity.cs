using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EastWestTest.Repository.Entities
{
    [Table("Clients")]
    public class ClientEntity : BaseEntity
    {
        public ClientEntity()
        {
            Sales = new HashSet<SaleEntity>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public ICollection<SaleEntity> Sales { get; set; }
    }
}
