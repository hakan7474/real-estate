namespace RealEstate.Application.Features.Types.Queries.GetByCodeTypeDetail
{
    public class GetTypeDetailByTypeCodeQueryResponse
    {
        public Guid TypeId { get; set; }
        public Guid TypeItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int OrderIndex { get; set; }
    }
}
