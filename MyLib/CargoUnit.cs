using FreeSql.DataAnnotations;

namespace MyLib
{
    public class CargoUnit
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int PDId { get; set; }

        public string Unit { get; set; }
    }
}