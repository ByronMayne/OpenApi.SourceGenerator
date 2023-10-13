namespace OpenApi.SourceGenerator
{
	public struct View
    {
        public static readonly View SwaggerController = new View();
        public static readonly View ServiceCollectionExtensions = new View();
        public static readonly View OpenApiSettings;
        public static readonly View Controller;
        public static readonly View Command;

        public readonly string ViewPath;

        public View(string viewPath)
        {
            ViewPath = viewPath;
        }

        static View()
        {
            SwaggerController = new View("swagger_controller.hbs");
            ServiceCollectionExtensions = new View("service_collection_extensions.hbs");
            OpenApiSettings = new View("OpenApiSettings.hbs");
            Controller = new View("controller.hbs");
            Command = new View("command.hbs");
        }
    }
}