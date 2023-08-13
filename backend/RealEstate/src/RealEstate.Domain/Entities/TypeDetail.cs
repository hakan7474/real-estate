using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Domain.Entities;

public class TypeDetail : AAuditableEntity<Guid>
{
    public Guid TypeId { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    [DefaultValue(99)]
    public int OrderIndex { get; set; }
    [ForeignKey(nameof(TypeId))]
    public virtual Types Types { get; set; }
}