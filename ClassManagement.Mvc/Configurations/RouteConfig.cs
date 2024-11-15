namespace ClassManagement.Mvc.Configurations
{
    internal static class RouteConfig
    {
        public static void ConfigureRoute(WebApplication app)
        {
            //app.UseEndpoints(endpoints =>
            //{
            app.MapControllerRoute(

                name: "areaRoute",

                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(

                name: "default",

                pattern: "{controller=Login}/{action=Index}/{id?}"
            );
            //});
        }
    }
}
