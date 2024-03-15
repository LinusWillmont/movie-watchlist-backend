namespace movie_watchlist.server.Models.Payloads
{
    public class WatchlistPayload : IPayload
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public string CheckPayload()
        {
            if (string.IsNullOrWhiteSpace(Name)) { return "Watchlist name is cant be empty"; }
            // Validate UserId
            return string.Empty;
        }
    }
}