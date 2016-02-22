using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using StackLite.Core.Persistance.ReadModels;

namespace StackLite.Core.Persistance.Migrations
{
    [DbContext(typeof(ReadContext))]
    partial class ReadContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StackLite.Core.Persistance.ReadModels.AnswerData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnsweredBy");

                    b.Property<string>("Content");

                    b.Property<Guid>("QuestionId");

                    b.Property<int>("Votes");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("StackLite.Core.Persistance.ReadModels.QuestionData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AskedByUserName");

                    b.Property<string>("Content");

                    b.HasKey("Id");
                });
        }
    }
}
