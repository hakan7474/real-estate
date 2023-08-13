namespace RealEstate.Application.Features.Estate.Queries.GetListEstate
{
    public class GetListEstateQueryResponse
    {
        public Guid EstateId { get; set; }
        public string SysCode { get; set; }
        public string EstateCode { get; set; }
        public string EstateName { get; set; }
        public int FloorNumber { get; set; }
        public DateTime? BuildingDate { get; set; }
        public decimal? Price { get; set; }
        public string RoomCount { get; set; }
        public decimal? GrossArea { get; set; }
        public decimal? NetArea { get; set; }
        public Guid EstateTypeId { get; set; }
        public string EstateTypeCode { get; set; }
        public string EstateTypeName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
    }
}
