namespace Api
{
    public static class Routes
    {
        private const string ApiBase = "api";

        public static class Locations
        {
            public const string CreateCity = $"{ApiBase}/counties/{{id:int}}/cities";
            public const string GetAll = $"{ApiBase}/countries";
            public const string FindCountyById = $"{ApiBase}/counties/{{id:int}}";
            public const string FindCountyByName = $"{ApiBase}/counties/{{name}}";
            public const string FindCityById = $"{ApiBase}/counties/{{countyId:int}}/cities/{{cityId:int}}";
            public const string UpdateCityName = $"{ApiBase}/counties/{{countyId:int}}/cities/{{cityId:int}}/name";
            public const string DeleteCity = $"{ApiBase}/counties/{{countyId:int}}/cities/{{cityId:int}}";
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
            public const string GetAllCategories = $"{ControllerBase}";
            public const string UpdateCategoryName = $"{ControllerBase}/{{categoryId:int}}/name";
            public const string DeleteCategory = $"{ControllerBase}/{{categoryId:int}}";
        }

        public static class SubCategories
        {
            public const string CreateSubCategory = $"{ApiBase}/categories/{{categoryId:int}}/sub-categories";
            public const string GetSubCategories = $"{ApiBase}/categories/{{categoryId:int}}/sub-categories";
            public const string UpdateSubCategoryName = $"{ApiBase}/categories/{{categoryId:int}}/sub-categories/{{subCategoryId:int}}/name";
            public const string DeleteSubCategory = $"{ApiBase}/categories/{{categoryId:int}}/sub-categories/{{subCategoryId:int}}/";
        }

        public static class Products
        {
            private const string ControllerBase = $"{ApiBase}/products";

            public const string Create = $"{ControllerBase}";
            public const string GetAllFromCategory = $"{ApiBase}/categories/{{categoryId:int}}/products";
            public const string GetAllFromSubCategory = $"{ApiBase}/categories/{{categoryId:int}}/sub-categories/{{subCategoryId:int}}/products";
            public const string FindById = $"{ControllerBase}/{{id:int}}";
            public const string UpdateAvailability = $"{ControllerBase}/{{id:int}}/availability";
            public const string Renew = $"{ControllerBase}/{{id:int}}/renew";
            public const string Delete = $"{ControllerBase}/{{id:int}}";
        }
    }
}
