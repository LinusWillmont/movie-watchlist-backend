namespace movie_watchlist.server.Models.Payloads.WatchlistPayloads
{
    public class WatchlistPayload : IPayload
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public string CheckPayload()
        {
            if (string.IsNullOrWhiteSpace(Name)) { return "Watchlist name can't be empty"; }
            // Validate UserId
            return string.Empty;
        }
    }
}