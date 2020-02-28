namespace Bijuteri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SatisDurum")]
    public partial class SatisDurum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SatisDurum()
        {
            Satis = new HashSet<Satis>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Adi { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Satis> Satis { get; set; }
    }
}
