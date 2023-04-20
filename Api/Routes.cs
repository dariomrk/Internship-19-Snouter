namespace Api
{
    public static class Routes
    {
        private const string ApiBase = "api";

        public static class Locations
        {
            private const string ControllerBase = $"{ApiBase}/locations";

            public const string CreateCity = $"{ControllerBase}/counties/{{id:int}}/cities";
            public const string GetAll = $"{ControllerBase}";
            public const string FindCountyById = $"{ControllerBase}/counties/{{id:int}}";
            public const string FindCountyByName = $"{ControllerBase}/counties/{{name}}";
            public const string FindCityById = $"{ControllerBase}/counties/{{countyId:int}}/cities/{{cityId:int}}";
            public const string DeleteCity = $"{ControllerBase}/counties/{{countyId:int}}/cities/{{cityId:int}}";
        }
        public static class Users
        {
            private const string ControllerBase = $"{ApiBase}/users";

            public const string Create = $"{ControllerBase}";
            public const string GetAll = $"{ControllerBase}";
            public const string Find = $"{ControllerBase}/{{id:int}}";
            public const string Update = $"{ControllerBase}/{{id:int}}";
            public const string Delete = $"{ControllerBase}/{{id:int}}";
        }

        public static class Categories
        {
            private const string ControllerBase = $"{ApiBase}/categories";

            public const string CreateCategory = $"{ControllerBase}";
            public const string CreateSubCategory = $"{ControllerBase}/categories/{{categoryId:int}}/sub-categories";
            public const string GetAllCategories = $"{ControllerBase}";
            public const string GetSubCategories = $"{ControllerBase}/categories/{{categoryId:int}}/sub-categories";
            public const string UpdateCategoryName = $"{ControllerBase}/categories/{{categoryId:int}}"; // TODO
            public const string UpdateSubCategoryName = $"{ControllerBase}/categories/{{categoryId:int}}/sub-categories/{{subCategoryId:int}}/"; // TODO
            public const string DeleteCategory = $"{ControllerBase}/categories/{{categoryId:int}}/sub-categories"; // TODO
            public const string DeleteSubCategory = $"{ControllerBase}/categories/{{categoryId:int}}/sub-categories/{{subCategoryId:int}}/"; // TODO
        }

        public static class Products
        {
            private const string ControllerBase = $"{ApiBase}/products";

            public const string Create = $"{ControllerBase}/categories/{{categoryId:int}}/sub-categories/{{subCategoryId:int}}/new"; // TODO
            public const string GetAllFromCategory = $"{ControllerBase}/categories/{{categoryId:int}}/products"; // TODO
            public const string GetAllFromSubCategory = $"{ControllerBase}/categories/{{categoryId:int}}/sub-categories/{{subCategoryId:int}}/products"; // TODO
            public const string FindById = $"{ControllerBase}/products/{{id:int}}"; // TODO
            public const string UpdateAvailability = $"{ControllerBase}/products/{{id:int}}/availability"; // TODO
            public const string Renew = $"{ControllerBase}/products/{{id:int}}/renew"; // TODO
            public const string Delete = $"{ControllerBase}/products/{{id:int}}"; // TODO
        }
    }
}
