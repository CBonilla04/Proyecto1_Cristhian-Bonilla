namespace Proyecto1_CristhianBonilla.ViewModels
{
    public class Filters
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime returnDate { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public int infants { get; set; }
        public bool nonStop { get; set; }

        public string travelClass { get; set; }
        public List<OriginOptions> originOptions { get; set; }

        public Filters()
        {
            origin = "";
            destination = "";
            departureDate = DateTime.Now;
            returnDate = DateTime.Now;
            adults = 1;
            children = 0;
            infants = 0;
            originOptions = new List<OriginOptions>();
            nonStop = false;
            travelClass = "ECONOMY";
        }

    }

}
