using RealEstate.Domain.Entities;

namespace RealEstate.Domain.SeedData;

public static class EstateType
{
    public const string EstateTypeCode = "EstateType";
    public static Types EstateTypeData()
    {
        var typeData = new Types()
        {
            TypeCode = EstateTypeCode,
            TypeName = "Estate Type",
            TypeDescription = "Estate Type",
            TypeDetails = new List<TypeDetail>()
            {
                new TypeDetail()
                {
                    ItemCode = "01",
                    ItemName = "Apartment", //Daire
                    ItemDescription = "Apartment",
                    OrderIndex = 1
                },
                new TypeDetail()
                {
                    ItemCode = "02",
                    ItemName = "Residence",
                    ItemDescription = "Residence",
                    OrderIndex = 2
                },
                new TypeDetail()
                {
                    ItemCode = "03",
                    ItemName = "Detached House", //Müstakil ev
                    ItemDescription = "Detached House",
                    OrderIndex = 3
                },
                new TypeDetail()
                {
                    ItemCode = "04",
                    ItemName = "Villa",
                    ItemDescription = "Villa",
                    OrderIndex = 4
                },
                new TypeDetail()
                {
                    ItemCode = "05",
                    ItemName = "Waterside", //Yalı
                    ItemDescription = "Waterside",
                    OrderIndex = 5
                },
                new TypeDetail()
                {
                    ItemCode = "06",
                    ItemName = "Summer House", //Yazlık Ev
                    ItemDescription = "Summer House",
                    OrderIndex = 6
                }
            }
        };


        return typeData;
    }
}