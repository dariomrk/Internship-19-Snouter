namespace Api
{
    public static class Routes
    {
        private const string ApiBase = "api";
        private const string IntIdParam = "{id:int}";

        public static class Category
        {
            private const string ControllerBase = $"{ApiBase}/categories";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string GetProductsFromCategory = $"{Find}/products";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class City
        {
            private const string ControllerBase = $"{ApiBase}/cities";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class Country
        {
            private const string ControllerBase = $"{ApiBase}/countries";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class County
        {
            private const string ControllerBase = $"{ApiBase}/counties";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class Currency
        {
            private const string ControllerBase = $"{ApiBase}/currencies";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class PreciseLocation
        {
            private const string ControllerBase = $"{ApiBase}/locations";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class Product
        {
            private const string ControllerBase = $"{ApiBase}/products";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class SubCategory
        {
            private const string ControllerBase = $"{ApiBase}/sub-categories";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string GetProductsFromSubCategory = $"{Find}/products";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
        public static class User
        {
            private const string ControllerBase = $"{ApiBase}/users";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{IntIdParam}";
            public const string Update = $"{ControllerBase}/{IntIdParam}";
            public const string Delete = $"{ControllerBase}/{IntIdParam}";
        }
    }
}
