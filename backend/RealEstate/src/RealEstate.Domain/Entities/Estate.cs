using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Domain.Entities;

public class Estate : AAuditableEntity<Guid>
{
    public string SysCode { get; set; }
    public string EstateCode { get; set; } //Emlak Code
    public string EstateName { get; set; } //Emlak Adı
    public int FloorNumber { get; set; } //Kat Numarası
    public DateTime? BuildingDate { get; set; } //İnşa tarihi
    public decimal? Price { get; set; } // Fiyat
    public string RoomCount { get; set; } //Oda Sayısı
    public decimal? GrossArea { get; set; } //Brüt Alan
    public decimal? NetArea { get; set; } //Net Alan
    public Guid EstateTypeId { get; set; } //Ev Tipi
    [CanBeNull] 
    public Contact Contact { get; set; } //Adres

    [ForeignKey(nameof(EstateTypeId))]
    public virtual TypeDetail EstateType { get; set; }

}