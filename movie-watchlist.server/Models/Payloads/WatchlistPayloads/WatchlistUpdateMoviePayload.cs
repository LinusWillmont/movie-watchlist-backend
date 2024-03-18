namespace movie_watchlist.server.Models.Payloads.WatchlistPayloads
{
    public class WatchlistUpdateMoviePayload : IPayload
    {
        public int MovieId { get; set; } = 0;
        public string CheckPayload()
        {
            if (MovieId < 0) { return "Movie id can't be negative"; }
            else { return string.Empty; }
        }
    }
}
