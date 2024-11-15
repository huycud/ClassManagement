namespace ClassManagement.Api.Common.EmailReader
{
    class EmailTemplateReader : IEmailTemplateReader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string _contentForlder;

        public EmailTemplateReader(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;

            _contentForlder = Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, configuration.GetSection("Url:TemplateDir").Value))

                            ? Path.Combine(_webHostEnvironment.WebRootPath, configuration.GetSection("Url:TemplateDir").Value)

                            : Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, configuration.GetSection("Url:TemplateDir").Value)).FullName;
        }

        public async Task<string> GetTemplateAsync(string templateName)
        {
            var templatePath = Path.Combine(_contentForlder, templateName);

            var content = await File.ReadAllTextAsync(templatePath);

            return content;
        }
    }
}
