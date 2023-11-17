
using System.ComponentModel;

namespace ErpSystem.TruckData.Domain.Enums
{
    public enum TruckStatus
    {

        [Description("Out of Service")]
        OutOfService,

        [Description("Loading")]
        Loading,

        [Description("To Job")]
        ToJob,

        [Description("At Job")]
        AtJob,

        [Description("Returning")]
        Returning
    }
}
