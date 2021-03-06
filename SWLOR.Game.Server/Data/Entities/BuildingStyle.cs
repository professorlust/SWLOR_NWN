namespace SWLOR.Game.Server.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BuildingStyle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuildingStyle()
        {
            PCBaseExteriorStructures = new HashSet<PCBaseStructure>();
            PCBaseInteriorStructures = new HashSet<PCBaseStructure>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BuildingStyleID { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(16)]
        public string Resref { get; set; }

        public bool IsInterior { get; set; }

        public int BaseStructureID { get; set; }

        public bool IsDefault { get; set; }

        public int DoorSpawnProcedure { get; set; }

        public virtual BaseStructure BaseStructure { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PCBaseStructure> PCBaseExteriorStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PCBaseStructure> PCBaseInteriorStructures { get; set; }
    }
}
