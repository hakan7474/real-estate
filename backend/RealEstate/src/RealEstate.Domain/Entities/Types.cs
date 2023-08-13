using RealEstate.Core.Domain.Entities;

namespace RealEstate.Domain.Entities;

public class Types : AAuditableEntity<Guid>
{
    public string SysCode { get; set; }
    public string TypeCode { get; set; }
    public string TypeName { get; set; }
    public string TypeDescription { get; set; }

    public virtual ICollection<TypeDetail> TypeDetails { get; set; }
}