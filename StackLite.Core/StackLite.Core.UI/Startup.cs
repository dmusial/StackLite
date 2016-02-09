using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Domain.Answers;
using StackLite.Core.Persistance;
using StackLite.Core.EventHandlers;
using StackLite.Core.FakeReportingStores;

namespace StackLite.Core.UI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            ConfigureLogging(loggerFactory);

            var questionsStore = new QuestionsStore();
            var publisher = new MessageBus(loggerFactory);
            var questionHandler = new QuestionHandler(questionsStore, loggerFactory);

            services.AddInstance<IEventPublisher>(publisher);
            services.AddSingleton<IEventStore, EventStore>();
            
            services.AddInstance<IQuestionsStore>(questionsStore);
            services.AddSingleton<IQuestionRepository, QuestionRepository>();
            services.AddSingleton<IQuestionsQuery, QuestionsQuery>();        
            
            services.AddSingleton<IAnswerRepository, AnswerRepository>();
            
            publisher.RegisterHandler<QuestionAsked>(questionHandler.Handle);
            publisher.RegisterHandler<QuestionAmended>(questionHandler.Handle);        
        }

        private void ConfigureLogging(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseFileServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
