namespace NZWalks.API.Models.Domain
{
    public class WalkDifficulty
    {

        public Guid Id { get; set; }

        public string Code { get; set; }

        public static implicit operator WalkDifficulty(Walk v)
        {
            throw new NotImplementedException();
        }
    }
}
