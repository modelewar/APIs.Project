namespace Talabat.APIS.Extentions
{
    public static class AddSwaggerExtention
    {
        public static WebApplication AddSwaggerMiddlewars(this WebApplication app )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;

        }
    }
}
