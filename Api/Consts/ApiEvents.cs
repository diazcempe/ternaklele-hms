using Microsoft.Extensions.Logging;

namespace Api.Consts
{
    public static class ApiEvents
    {
        // INVENTORIES
        public static EventId InventoryAdded = new EventId(1, "Inventory Added");
        public static EventId InventoryUpdated = new EventId(2, "Inventory Updated");
        public static EventId InventoryDeleted = new EventId(3, "Inventory Deleted");
        public static EventId InventoryError = new EventId(4, "Inventory Error");
    }
}
