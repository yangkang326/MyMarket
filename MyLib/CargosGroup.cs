using FreeSql.DataAnnotations;

namespace MyLib
{
    public class CargosGroup
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int PDId { get; set; }

        public string PDGroup { get; set; }
    }
}