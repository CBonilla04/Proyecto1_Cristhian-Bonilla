namespace Proyecto1_CristhianBonilla.ViewModels
{
    public class OriginOptions
    {
        public string IataCode { get; set; }
        public string Name { get; set; }

        public Address Address { get; set; }

        public OriginOptions()
        {
            IataCode = "";
            Name = "";
            Address = new Address();
        }
    }

    public class OriginData
    {
        public List<OriginOptions> Data { get; set; }
    }
    public class Address
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string RegionCode { get; set; }

        public Address(string cityName, string cityCode, string countryName, string countryCode, string regionCode)
        {
            CityName = cityName;
            CityCode = cityCode;
            CountryName = countryName;
            CountryCode = countryCode;
            RegionCode = regionCode;
        }

        public Address() { }
    }
}
