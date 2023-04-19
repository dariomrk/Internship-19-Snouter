namespace Api
{
    public static class Routes
    {
        private const string ApiBase = "api";

        public static class Location
        {
            private const string ControllerBase = $"{ApiBase}/locations";

            public const string GetAll = $"{ControllerBase}";
            public const string FindCountyById = $"{ControllerBase}/counties/{{id:int}}";
            public const string FindCountyByName = $"{ControllerBase}/counties/{{name}}";
            public const string CreateCity = $"{ControllerBase}/counties/{{id:int}}/cities";
            public const string FindCityById = $"{ControllerBase}/counties/{{countyId:int}}/cities/{{cityId:int}}";
        }
        public static class User
        {
            private const string ControllerBase = $"{ApiBase}/users";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{{id:int}}";
            public const string Update = $"{ControllerBase}/{{id:int}}";
            public const string Delete = $"{ControllerBase}/{{id:int}}";
        }
    }
}
