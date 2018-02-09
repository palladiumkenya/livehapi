using System;

namespace LiveHAPI.Shared
{
    public class SearchHit
    {
        public int Hits { get; set; }
        public Guid ItemId { get; set; }


        public SearchHit(Guid itemId)
        {
            Hits = 1;
            ItemId = itemId;
        }

        public SearchHit(int hits, Guid itemId):this(itemId)
        {
            Hits = hits;
        }
    }
}