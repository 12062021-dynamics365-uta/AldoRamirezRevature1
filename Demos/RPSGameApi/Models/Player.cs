namespace Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public Player() { }

        public Player(string fname, string lname)
        {
            this.Fname = fname;
            this.Lname = lname;
        }
    }
}