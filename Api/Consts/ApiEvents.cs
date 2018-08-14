﻿using Microsoft.Extensions.Logging;

namespace Api.Consts
{
    public static class ApiEvents
    {
        // INVENTORIES
        public static EventId InventoryAdded = new EventId(1, "Inventory Added");
    }
}
