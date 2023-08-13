using System;
using System.Runtime.Serialization;

namespace RealEstate.Core
{
    /// <summary>
    /// Servisten dönen metodun statüsünü tutar
    /// </summary>
    [Serializable]
    [DataContract]
    public enum ServiceResponseStatuses
    {
        [EnumMember]
        Error,

        [EnumMember]
        Success,

        [EnumMember]
        Warning
    }
}
