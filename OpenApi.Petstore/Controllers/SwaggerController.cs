//using Microsoft.AspNetCore.Mvc;
//using Microsoft.OpenApi.Models;
//using Microsoft.OpenApi.Readers;
//using Microsoft.OpenApi.Writers;
//using System.Net;
//using System.Reflection;

//namespace OpenApi.Petstore.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class SwaggerController : Controller
//    {
//        public class Settings
//        {
//            /// <summary>
//            /// Gets or sets if the spesefication that is provide should insert the
//            /// current host as a server enter. 
//            /// </summary>
//            public bool AddHostServer { get; init; }

//            /// <summary>
//            /// Gets an delegate that can be used to apply any modifications to the document 
//            /// </summary>
//            public event Action<OpenApiDocument>? ModifyActions;

//            public Settings()
//            {
//                AddHostServer = true;
//            }
//        }

//        private readonly Settings m_settings;
//        private static readonly OpenApiDocument? m_openApiDocument;
//        private static readonly OpenApiDiagnostic? m_openApiDiagnostic;

//        static SwaggerController()
//        {
//            m_openApiDocument = null;
//            m_openApiDiagnostic = null;
//            Assembly assembly = typeof(SwaggerController).Assembly;
//            using Stream? stream = assembly.GetManifestResourceStream("OpenApiSpecification");
//            if (stream != null)
//            {
//                OpenApiStreamReader reader = new();
//                m_openApiDocument = reader.Read(stream, out m_openApiDiagnostic);
//            }

//        }

//        public SwaggerController([FromServices] Settings settings)
//        {
//            m_settings = settings;
//        }

//        [HttpGet]
//        [Route("v1/swagger.json")]
//        [Produces("application/json")]
//        public IActionResult GetAsync()
//        {
//            if (m_openApiDiagnostic != null && (m_openApiDiagnostic.Errors.Any() || m_openApiDiagnostic.Warnings.Any()))
//            {
//                JsonResult jsonResult = Json(new
//                {
//                    message = "error parsing open api documention, see diagnostic below",
//                    diagnostic = m_openApiDiagnostic
//                });
//                jsonResult.StatusCode = (int)HttpStatusCode.InternalServerError;
//                return jsonResult;
//            }

//            if (m_openApiDocument == null)
//            {
//                return StatusCode(StatusCodes.Status404NotFound);
//            }

//            if (m_settings.AddHostServer)
//            {
//                string uri = $"{(HttpContext.Request.IsHttps ? "https://" : "http://")}{HttpContext.Request.Host.Value}";
//                m_openApiDocument.Servers.Insert(0, new OpenApiServer()
//                {
//                    Url = uri,
//                    Description = "The current service"
//                });
//            }

//            using StringWriter stringWriter = new();
//            OpenApiJsonWriter jsonWriter = new(stringWriter);
//            m_openApiDocument.SerializeAsV3(jsonWriter);
//            stringWriter.Flush();
//            return Content(stringWriter.ToString(), "application/json");
//        }
//    }
//}