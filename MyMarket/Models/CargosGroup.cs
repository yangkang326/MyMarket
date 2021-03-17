using FreeSql.DataAnnotations;

namespace MyMarket.Models
{
    public class CargosGroup
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        [Column(IsNullable = false)] public string PDGroup { get; set; }
    }
}