using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Trill.Core.Repositories;
using Trill.Infrastructure.Mongo.Repositories;

namespace Trill.Infrastructure.Mongo
{
    internal static class Extensions
    {
        private static bool _conventionsRegistered;

        internal static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<MongoOptions>(configuration.GetSection("mongo"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<IOptions<MongoOptions>>().Value;
                return new MongoClient(options.ConnectionString);
            });

            services.AddTransient(sp =>
            {
                var options = sp.GetService<IOptions<MongoOptions>>().Value;
                var client = sp.GetService<IMongoClient>();
                return client.GetDatabase(options.Database);
            });

            if (!_conventionsRegistered)
            {
                RegisterConventions();
            }

            return services;
        }
        
        private static void RegisterConventions()
        {
            _conventionsRegistered = true;
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?),
                new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            ConventionRegistry.Register("trill", new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
            }, _ => true);
        }
    }
}