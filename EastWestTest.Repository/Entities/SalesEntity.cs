using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EastWestTest.Repository.Entities
{
    [Table("Sales")]
    public class SaleEntity : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public int ClientId { get; set; }
        public ClientEntity Client { get; set; }
    }
}
